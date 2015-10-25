using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using Event;

namespace Lib
{
    public static class EventProcess
    {
        public delegate void EntityChangeCallback(IModel model, IEntity entity, IEvent baseEvent);
        public delegate void ModelChangeCallback(IModel model);
        public delegate void PrepareEntityCallback(IEntity entity);
        public delegate void CreateEntityCallback(ref IEntity entity, string entityName);

        /// <summary>
        /// Implémente les interactions du presse-papier avec model de données
        /// </summary>
        /// <param name="thisProc">Pointeur sur l'instance du controler recevant les événements</param>
        /// <param name="dataModel">Pointeur sur le model de données concerné</param>
        /// <param name="from">Paramètre passé par la méthode ProcessEvent</param>
        /// <param name="_this">Paramètre passé par la méthode ProcessEvent</param>
        /// <param name="e">Paramètre passé par la méthode ProcessEvent</param>
        public static void ProcessCopyPasteEvents(IEventProcess app, IEventProcess thisProc, IModel dataModel, object from, object _this, IEvent e)
        {
            //
            // Copy/Paste
            // Copie/Colle l'entité dans le presse-papier
            //
            if (e is EntityCopyPasteEvent)
            {
                EntityCopyPasteEvent ev = e as EntityCopyPasteEvent;
                // Copier
                if (ev.Type == EntityCopyPasteEventType.Copy && ev.IsEmpty() == false)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.AppendChild(doc.CreateElement("root"));
                    foreach (IEntity entity in ev.Entities)
                    {
                        if (entity is IEntitySerializable)
                        {
                            IEntitySerializable serial = entity as IEntitySerializable;
                            serial.ToXml(doc.DocumentElement);
                        }
                    }
                    //Clipboard.SetData("text/xml", doc.InnerXml);
                    Clipboard.SetText(doc.InnerXml);
                }
                // Coller
                if (ev.Type == EntityCopyPasteEventType.Paste)
                {
                    //string text = Clipboard.GetData("text/xml");
                    string text = Clipboard.GetText(); // Texte ou XML
                    if (String.IsNullOrWhiteSpace(text))
                        return;

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(text);
                    if (doc.DocumentElement == null)
                        return;

                    XmlNode cur = doc.DocumentElement.FirstChild;
                    while (cur != null)
                    {
                        if (cur is XmlElement)
                        {
                            try
                            {
                                // Alloue une instance de l’entité 
                                XmlElement elm = cur as XmlElement;
                                EntityPreCreateEvent evCreate = new EntityPreCreateEvent(elm.LocalName);
                                thisProc.ProcessEvent(_this, _this, evCreate);
                                IEntity entity = evCreate.Entity;
                                if (entity == null)
                                    continue;

                                // Dé-sérialise les données dans l’instance
                                entity.EntityState = EntityState.Added;

                                if (entity is IEntitySerializable)
                                {
                                    IEntitySerializable serial = entity as IEntitySerializable;
                                    serial.FromXml(elm, (aggr) =>
                                    {
                                        aggr.EntityState = EntityState.Added;
                                        if (aggr is IEntityPersistent)
                                            (aggr as IEntityPersistent).RaiseIdentity();
                                    });
                                }

                                // Ajoute l’objet créé dans l’événement EntityCopyPasteEventType
                                ev.AddEntity(entity);

                                // Notifie la création de l'entité
                                thisProc.ProcessEvent(_this, _this, new EntityCreateEvent(entity));
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                        cur = cur.NextSibling;
                    }
                }
            }
        }

        /// <summary>
        /// Liste les enfants de premier niveau
        /// </summary>
        /// <param name="obj">Objet parent</param>
        /// <param name="list">Liste recevant les objets enfants implémentant IEventHandler</param>
        /// <returns></returns>
        public static List<DependencyObject> EnumChildren(DependencyObject obj, List<DependencyObject> list)
        {
            int nChild = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < nChild; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child as IEventProcess != null)
                {
                    list.Add(child);
                }
                else if (VisualTreeHelper.GetChildrenCount(child) > 0)
                {
                    EnumChildren(child, list);
                }
            }
            return list;
        }

        /// <summary>
        /// Transmet un événement aux propriétés (controleurs) enfant d'une classe
        /// </summary>
        /// <param name="model">Classe parent</param>
        /// <param name="from">Paramètre passé par la méthode ProcessEvent</param>
        /// <param name="_this">Paramètre passé par la méthode ProcessEvent</param>
        /// <param name="param">Paramètre passé par la méthode ProcessEvent</param>
        public static void SendToProperties(IEventProcess model, object from, object _this, IEvent param)
        {
            // passe l'evenement aux modeles enfants
            foreach (PropertyInfo p in model.GetType().GetProperties())
            {
                //
                IEventProcess child = p.GetValue(model, null) as IEventProcess;
                if (child != null)
                {
                    child.ProcessEvent(from, _this, param);
                    continue;
                }
            }
        }

        /// <summary>
        /// Transmet un événement aux controles (vues) enfant d'une classe
        /// </summary>
        /// <param name="ctrl">Controle parent</param>
        /// <param name="from">Paramètre passé par la méthode ProcessEvent</param>
        /// <param name="_this">Paramètre passé par la méthode ProcessEvent</param>
        /// <param name="param">Paramètre passé par la méthode ProcessEvent</param>
        public static void SendToControls(Control ctrl, object from, object _this, IEvent param)
        {
            List<DependencyObject> list = EventProcess.EnumChildren(ctrl, new List<DependencyObject>());
            foreach (DependencyObject obj in list)
            {
                (obj as IEventProcess).ProcessEvent(from, _this, param);
            }
        }

    }
}

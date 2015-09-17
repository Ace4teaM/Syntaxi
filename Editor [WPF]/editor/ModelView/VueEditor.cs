using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AppModel.Entity;
using AppModel.Domain;
using EditorModel.Entity;
using Lib;
using System.IO;
using System.Xml;
using Event;

namespace editor.ModelView
{
    class VueEditor : ViewModelBase, IEventProcess
    {
        editor.App app = Application.Current as editor.App;
        public VueEditor()
        {
            ListObjectContent();
        }

        //-----------------------------------------------------------------------------------------
        // Méthodes
        //-----------------------------------------------------------------------------------------
        #region Methods
        public void ListObjectContent()
        {
            if (app.Project != null)
                ObjectContentList = new ObservableCollection<ObjectContent>(app.Project.ObjectContent);
        }
        public void OnEntityChange(IEntity entity)
        {
            if (entity == null)
                return;

            if (entity.EntityState == EntityState.Added)
            {
                if (entity is ObjectContent)
                {
                    ObjectContent e = entity as ObjectContent;
                    // Ajoute au projet en cours
                    app.Project.AddObjectContent(e);
                    // Regénére des identifiants uniques pour la base de données
                    e.Id = Guid.NewGuid().ToString("N");
                    foreach (var p in e.ParamContent)
                        p.Id = Guid.NewGuid().ToString("N");
                    // Ajoute à l'interface
                    if (this.ObjectContentList.Contains(e) == false)
                        this.ObjectContentList.Add(e);
                }
                if (entity is ParamContent)
                {
                    ParamContent e = entity as ParamContent;
                    CurObjectContent.AddParamContent(e);
                    e.Id = Guid.NewGuid().ToString("N");
                }
                if (entity is DatabaseSource)
                {
                    DatabaseSource e = entity as DatabaseSource;
                    app.Project.AddDatabaseSource(e);

                    if (app.States.SelectedDatabaseSourceId == null)
                        app.States.SelectedDatabaseSourceId = app.Project.DatabaseSource.First().Id;
                }

                //OK
                entity.EntityState = EntityState.Unmodified;
            }

            if (entity.EntityState == EntityState.Modified)
            {
                //OK
                entity.EntityState = EntityState.Unmodified;
            }

            if (entity.EntityState == EntityState.Deleted)
            {
                if (entity is ObjectContent)
                {
                    ObjectContent e = entity as ObjectContent;
                    app.Project.RemoveObjectContent(e);
                }
                if (entity is ParamContent)
                {
                    ParamContent e = entity as ParamContent;
                    CurObjectContent.RemoveParamContent(e);
                }
                if (entity is DatabaseSource)
                {
                    DatabaseSource e = entity as DatabaseSource;
                    app.Project.RemoveDatabaseSource(e);
                }

                //OK
                entity.EntityState = EntityState.Deleted;
            }
        }
        #endregion

        //
        public string ProjectName
        {
            get { return app.Project.Name; }
        }
        //
        public string ProjectVersion
        {
            get { return app.Project.Version; }
        }
        //
        public string ProjectPath
        {
            get { return app.ProjectFileName; }
        }
        //
        private EditorSampleCode curEditorSampleCode;
        public EditorSampleCode CurEditorSampleCode {
            get { return curEditorSampleCode; }
            set {
                curEditorSampleCode = value;
                OnPropertyChanged("CurEditorSampleCode");
            }
        }
        //
        public Collection<ObjectSyntax> ObjectSyntaxList
        {
            get { return app.Project.ObjectSyntax; }
        }
        //
        private ObjectSyntax curObjectSyntax;
        public ObjectSyntax CurObjectSyntax {
            get { return curObjectSyntax; }
            set {
                curObjectSyntax = value;
                if (curObjectSyntax != null)
                    CurEditorSampleCode = app.States.EditorSampleCode.Where(p => p.ObjectSyntaxType == curObjectSyntax.ObjectType).FirstOrDefault();
                OnPropertyChanged("CurObjectSyntax");
                OnPropertyChanged("ParamSyntaxList");
            }
        }
        //
        public Collection<ParamSyntax> ParamSyntaxList
        {
            get {
                if (CurObjectSyntax != null)
                    return new Collection<ParamSyntax>(app.Project.ParamSyntax.Where(p => p.GroupName.ToLower() == CurObjectSyntax.GroupName.ToLower()).ToArray());
                return null;
            }
        }
        //
        private ObjectSyntax curParamSyntax;
        public ObjectSyntax CurParamSyntax
        {
            get { return curParamSyntax; }
            set
            {
                curParamSyntax = value;
                OnPropertyChanged("CurParamSyntax");
            }
        }
        //
        private ObservableCollection<ObjectContent> objectContentList;
        public ObservableCollection<ObjectContent> ObjectContentList
        {
            get { return objectContentList; }
            set
            {
                objectContentList = value;
                OnPropertyChanged("ObjectContentList");
            }
        }
        //
        private ObjectContent curObjectContent;
        public ObjectContent CurObjectContent
        {
            get { return curObjectContent; }
            set
            {
                curObjectContent = value;
                if (curObjectContent != null)
                    CurParamContentList = new ObservableCollection<ParamContent>(curObjectContent.ParamContent);
                OnPropertyChanged("CurObjectContent");
            }
        }
        //
        private ObservableCollection<ParamContent> curParamContentList;
        public ObservableCollection<ParamContent> CurParamContentList
        {
            get { return curParamContentList; }
            set
            {
                curParamContentList = value;
                OnPropertyChanged("CurParamContentList");
            }
        }
        //
        public Collection<SearchParams> SearchParamsList
        {
            get { return app.Project.SearchParams; }
        }
        //
        private SearchParams curSearchParams;
        public SearchParams CurSearchParams
        {
            get { return curSearchParams; }
            set
            {
                curSearchParams = value;
                OnPropertyChanged("CurSearchParams");
            }
        }

        //
        public Collection<DatabaseSource> DatabaseSourceList
        {
            get { return app.Project.DatabaseSource; }
        }
        //
        private DatabaseSource curDatabaseSource;
        public DatabaseSource CurDatabaseSource
        {
            get { return curDatabaseSource; }
            set
            {
                curDatabaseSource = value;
                OnPropertyChanged("CurDatabaseSource");
            }
        }

        //-----------------------------------------------------------------------------------------
        // Commandes
        //-----------------------------------------------------------------------------------------
        #region Commands
        #region SaveProject
        private ICommand saveProject;
        public ICommand SaveProject
        {
            get
            {
                if (this.saveProject == null)
                    this.saveProject = new DelegateCommand(() =>
                    {
                        app.SaveProject();
                    });

                return this.saveProject;
            }
        }
        #endregion
        #region ScanObjects
        private ICommand scanObjects;
        public ICommand ScanObjects
        {
            get
            {
                if (this.scanObjects == null)
                    this.scanObjects = new DelegateCommand(() =>
                    {
                        app.ScanObjects();
                        ObjectContentList = new ObservableCollection<ObjectContent>(app.Project.ObjectContent);
                        CurObjectContent = ObjectContentList.FirstOrDefault();
                    });

                return this.scanObjects;
            }
        }
        #endregion
        #region ImportSyntaxDirectory
        private ICommand importSyntaxDirectory;
        public ICommand ImportSyntaxDirectory
        {
            get
            {
                if (this.importSyntaxDirectory == null)
                    this.importSyntaxDirectory = new DelegateCommand(() =>
                    {
                        WPFFolderBrowser.WPFFolderBrowserDialog dialog = new WPFFolderBrowser.WPFFolderBrowserDialog();
                        dialog.InitialDirectory = app.ProjectFilePath;
                        if (dialog.ShowDialog() == true)
                        {
                            app.appModel.ImportSyntaxDirectory(dialog.FileName);
                            OnPropertyChanged("ObjectSyntaxList");
                        }
                    });

                return this.importSyntaxDirectory;
            }
        }
        #endregion
        #region ExportToDatabase
        private ICommand exportToDatabase;
        public ICommand ExportToDatabase
        {
            get
            {
                if (this.exportToDatabase == null)
                    this.exportToDatabase = new DelegateCommand(() =>
                    {
                        // Aucune source de données ?
                        if(app.Project.DatabaseSource.Count == 0)
                        {
                            if(MessageBox.Show("Aucune source de données n'est configurée. Atteindre la page de configuration ?","Configurer une source de données",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            return;
                        }

                        // Aucune source de données par défaut
                        DatabaseSource source = app.Project.DatabaseSource.Where(p => (p.Id == app.States.SelectedDatabaseSourceId)).SingleOrDefault();
                        if (app.States.SelectedDatabaseSourceId == null || source == null)
                        {
                            MessageBox.Show("Veuillez sélectionner une source de données.", "Source de données");
                            // a implementer avec EditWnd...
                            return;
                        }

                        try
                        {
                            // Initialise la factory
                            IEntityFactory factory;
                            switch (source.Provider)
                            {
                                case DatabaseProvider.Odbc:
                                    factory = new SqlOdbcFactory();
                                    ((SqlOdbcFactory)factory).SetConnection(source.ConnectionString);
                                    break;
                                case DatabaseProvider.PostgreSQL:
                                    factory = new SqlPostgresFactory();
                                    ((SqlPostgresFactory)factory).SetConnection(source.ConnectionString);
                                    break;
                                case DatabaseProvider.SqlServer:
                                    factory = new SqlServerFactory();
                                    ((SqlServerFactory)factory).SetConnection(source.ConnectionString);
                                    break;
                                default:
                                    MessageBox.Show("La source de données ne correspond pas à une source valide.", "Source de données");
                                    return;
                            }

                            app.appModel.Export(factory);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    });

                return this.exportToDatabase;
            }
        }
        #endregion
        #region ImportFromDatabase
        private ICommand importFromDatabase;
        public ICommand ImportFromDatabase
        {
            get
            {
                if (this.importFromDatabase == null)
                    this.importFromDatabase = new DelegateCommand(() =>
                    {
                        try
                        {
                            SqlOdbcFactory factory = new SqlOdbcFactory();
                            factory.SetConnection(@"DSN=Syntaxi;");
                            app.appModel.Import(factory);
                            ObjectContentList = new ObservableCollection<ObjectContent>(app.Project.ObjectContent);
                            CurObjectContent = ObjectContentList.FirstOrDefault();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    });

                return this.importFromDatabase;
            }
        }
        #endregion
        /*#region ApplyChanges
        private ICommand applyChanges;
        public ICommand ApplyChanges
        {
            get
            {
                if (this.applyChanges == null)
                    this.applyChanges = new DelegateCommand(() =>
                    {
                        List<ObjectContent> deleted = new List<ObjectContent>();
                        foreach (var e in this.ObjectContentList)
                        {
                            switch (e.EntityState)
                            {
                                case EntityState.Added:
                                    e.Project = app.Project;
                                    foreach (var p in e.ParamContent)
                                    {
                                        switch (e.EntityState)
                                        {
                                            case EntityState.Added:
                                                e.Project = app.Project;
                                                break;
                                            case EntityState.Deleted:
                                                deleted.Add(e);
                                                break;
                                        }
                                    }
                                    break;
                                case EntityState.Deleted:
                                    deleted.Add(e);
                                    break;
                            }
                        }
                    });

                return this.applyChanges;
            }
        }
        #endregion*/
        #endregion

        //-----------------------------------------------------------------------------------------
        // Evénements
        //-----------------------------------------------------------------------------------------
        #region IEventProcess
        public void ProcessEvent(object from, object _this, IEvent e)
        {
            //
            // Pre-Create
            // Préprare la création d'une nouvelle entité (avant édition des champs)
            //
            if (e is EntityPreCreateEvent)
            {
                EntityPreCreateEvent create = e as EntityPreCreateEvent;
                if (create.Entity is IEntityPersistent)
                {
                    IEntityPersistent p = create.Entity as IEntityPersistent;
                    //Affecte la source de données à la nouvelle entitée
                    p.Factory = app.appModel.project.Factory;
                }
            }
            //
            // Create
            // Crée la nouvelle entité (après édition des champs)
            //
            if (e is EntityCreateEvent)
            {
                EntityCreateEvent ev = e as EntityCreateEvent;
                // actualise le model
                OnEntityChange(ev.Entity);
            }
            //
            // Change
            // Les données de l'entité son modifiées
            //
            if (e is EntityChangeEvent)
            {
                EntityChangeEvent ev = e as EntityChangeEvent;
                // actualise le model
                OnEntityChange(ev.Entity);
            }
            //
            // Delete
            // Supprime l'entité 
            //
            if (e is EntityDeleteEvent)
            {
                EntityDeleteEvent ev = e as EntityDeleteEvent;
                ev.Entity.EntityState = EntityState.Deleted;
                // actualise le model
                OnEntityChange(ev.Entity);
            }
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
                                // Alloue une instance de l'objet
                                XmlElement elm = cur as XmlElement;
                                Type t = Type.GetType("AppModel.Entity." + elm.LocalName);
                                IEntity entity = Activator.CreateInstance(t) as IEntity;

                                // initialise les données de l'objet
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

                                if (entity is IEntityPersistent)
                                {
                                    IEntityPersistent persistent = entity as IEntityPersistent;
                                    // attache à la base de données active
                                    persistent.Factory = app.appModel.project.Factory;
                                    persistent.RaiseIdentity();
                                }

                                // liste les objets créés
                                ev.AddEntity(entity);

                                // actualise le model
                                OnEntityChange(entity);
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
        #endregion
    }
}

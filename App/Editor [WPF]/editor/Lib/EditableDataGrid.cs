using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Event;

namespace Lib
{
    public class EditableDataGrid : DataGrid
    {
        public EditableDataGrid()
        {
            //this.CanUserAddRows = false;// Gestion de l'allocation
            this.IsSynchronizedWithCurrentItem = true;
            this.ClipboardCopyMode = DataGridClipboardCopyMode.None;// Customize les operations de Copier/Coller
            this.CellEditEnding += EditableDataGrid_CellEditEnding;
            this.BeginningEdit += EditableDataGrid_BeginningEdit;
            this.RowEditEnding += EditableDataGrid_RowEditEnding;
            this.PreviewKeyDown += EditableDataGrid_PreviewKeyDown;
           // this.SelectionChanged += EditableDataGrid_SelectionChanged;
        }


        ~EditableDataGrid()
        {
            this.CellEditEnding -= EditableDataGrid_CellEditEnding;
            this.BeginningEdit -= EditableDataGrid_BeginningEdit;
            this.RowEditEnding -= EditableDataGrid_RowEditEnding;
            this.PreviewKeyDown -= EditableDataGrid_PreviewKeyDown;
            //this.SelectionChanged -= EditableDataGrid_SelectionChanged;
        }

        bool EditMode = false;
        //EntityChangeEvent CurEvent = null;
        IEvent CurEvent = null;
        IApp app = null;
        /*bool IsNew = false;

        void EditableDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.IsNew = (this.SelectedItem == CollectionView.NewItemPlaceholder);
        }*/

        void EditableDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            app = Application.Current as IApp;

            if (e.EditAction == DataGridEditAction.Cancel)
                return;

            if (e.Row.Item is IEntity)
            {
                IEntity entity = e.Row.Item as IEntity;
                if (entity.EntityState == EntityState.Added)
                    return;
                entity.EntityState = EntityState.Modified;
            }
        }

        void EditableDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            IEventProcess process = this.DataContext as IEventProcess;
            this.EditMode = true;

            if (CurEvent == null && process != null && e.Row.Item is IEntity)
            {
                IEntity entity = e.Row.Item as IEntity;

                // Nouvelle entité ?
                // Note: l'attribut Model est testé pour savoir si l'entité a été créé automatiquement par le DataGrid (e.Row.IsNewItem ne peut pas être utilisé car il retourne toujours true pour une entité crée précédemment)
                // Note: Si l'entité est à l'état EntityState.Deleted alors elle doit être créée par le model
                // Note: Si l'entité est à l'état EntityState.Added alors elle doit être recréée car sa création n'a pas été confirmé par le model
                if (entity.Model == null || entity.EntityState == EntityState.Deleted || entity.EntityState == EntityState.Added /*e.Row.IsNewItem*/)
                {
                    // pré création de l'entité (si ce n'est pas déjà fait)
                    // NOTE: l'allocation est réalisée automatiquement par le binding, EntityPreCreateEvent est appelé pour insérer l'entité au model
                    if (entity.Model == null || entity.EntityState == EntityState.Deleted)
                        process.ProcessEvent(this, this, new EntityPreCreateEvent(entity));

                    // prépare l'événement final
                    CurEvent = new EntityCreateEvent(entity);
                }
                else
                {
                    // pré modification de l'entité
                    // NOTE: permet entre autre de sauvegarder l'état de l'entité par le gestionnaire d'états
                    process.ProcessEvent(this, this, new EntityPreUpdateEvent(entity));

                    // prépare l'événement final
                    CurEvent = new EntityUpdateEvent(entity);
                }
            }
        }

       // IEntity save;

        private void EditableDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            this.EditMode = false;

            if (e.EditAction == DataGridEditAction.Cancel)
            {
                CurEvent = null;
                return;
            }

            if (CurEvent != null)
            {
                if (CurEvent is EntityCreateEvent)
                {
                    EntityCreateEvent ev = CurEvent as EntityCreateEvent;
                    try
                    {
                        ev.Entity.Model.Create(ev.Entity);
                    }
                    catch (Exception ex)
                    {
                        app.ProcessException(ex);
                    }
                }
                if (CurEvent is EntityUpdateEvent)
                {
                    EntityUpdateEvent ev = CurEvent as EntityUpdateEvent;
		            try{
                        ev.Entity.Model.Update(ev.Entity);
		            }
		            catch (Exception ex){
			            app.ProcessException(ex);
		            }
                }

                // Notifie la modification au model
                //IEventProcess process = Application.Current as IEventProcess;
                /*IEventProcess process = this.DataContext as IEventProcess;
                if (process != null)
                    process.ProcessEvent(this, this, CurEvent);
                */
                CurEvent = null;
                e.Cancel = false;
            }
        }

        private void EditableDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Supprimer
            if (e.Key == Key.Delete && this.SelectedItem != null && this.EditMode == false)
            {
                // liste la sélection
                // (utilise une nouvelle instance de la liste pour éviter toute exception lors de la modification de la collection durant son énumération)
                List<IEntity> selection = new List<IEntity>();
                if (this.SelectionMode == DataGridSelectionMode.Extended && this.SelectedItems != null)
                {
                    foreach (var item in this.SelectedItems)
                    {
                        if (item is IEntity)
                            selection.Add(item as IEntity);
                    }
                }
                else
                {
                    if (this.SelectedItem is IEntity)
                        selection.Add(this.SelectedItem as IEntity);
                }

                // Applique les modifications
                foreach (IEntity entity in selection)
                {
                    // Demande la supression
                    try
                    {
                        entity.Model.Delete(entity);
                    }
                    catch (Exception ex)
                    {
                        app.ProcessException(ex);
                        e.Handled = true;// Annule la suppression de la ligne
                        return;
                    }
                }
            }

            // Copier
            if (e.Key == Key.C && e.KeyboardDevice.Modifiers == ModifierKeys.Control && this.SelectedItem != null && this.EditMode == false)
            {
                EntityCopyPasteEvent ev = new EntityCopyPasteEvent(EntityCopyPasteEventType.Copy);

                if (this.SelectionMode == DataGridSelectionMode.Extended && this.SelectedItems != null)
                {
                    foreach (IEntity entity in this.SelectedItems)
                    {
                        ev.Entities.Add(entity);
                    }
                }
                else if (this.SelectedItem != null)
                {
                    ev.Entities.Add(this.SelectedItem as IEntity);
                }

                if (ev.IsEmpty() == false)
                {
                    //IEventProcess process = Application.Current as IEventProcess;
                    IEventProcess process = this.DataContext as IEventProcess;
                    if (process != null)
                        process.ProcessEvent(this, this, ev);
                }
            }

            // Coller
            if (e.Key == Key.V && e.KeyboardDevice.Modifiers == ModifierKeys.Control && this.SelectedItem != null && this.EditMode == false)
            {
                IEventProcess process = this.DataContext as IEventProcess;
                if (process != null)
                    process.ProcessEvent(this, this, new EntityCopyPasteEvent(EntityCopyPasteEventType.Paste));
            }

            // Inserer
            if (e.Key == Key.Insert && this.SelectedItem != null && this.EditMode == false)
            {
                Type itemType = this.Items.SourceCollection.GetType().GetGenericArguments().Single();

                // pré création de l'entité
                EntityPreCreateEvent ev = new EntityPreCreateEvent(itemType.Name);
                IEventProcess process = this.DataContext as IEventProcess;
                if (process == null)
                    return;

                // Pré création OK ?
                process.ProcessEvent(this, this, ev);
                if (ev.Entity == null)
                    return;
                
                // Création
                try
                {
                    ev.Entity.Model.Create(ev.Entity);
                }
                catch (Exception ex)
                {
                    app.ProcessException(ex);
                }
            }
        }
    }
}

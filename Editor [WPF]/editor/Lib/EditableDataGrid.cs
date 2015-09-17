using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Event;

namespace Lib
{
    public class EditableDataGrid : DataGrid
    {
        public EditableDataGrid()
        {
            this.ClipboardCopyMode = DataGridClipboardCopyMode.None;// Customize les operations de Copier/Coller
            this.CellEditEnding += EditableDataGrid_CellEditEnding;
            this.BeginningEdit += EditableDataGrid_BeginningEdit;
            this.RowEditEnding += EditableDataGrid_RowEditEnding;
            this.PreviewKeyDown += EditableDataGrid_PreviewKeyDown;
        }

        ~EditableDataGrid()
        {
            this.CellEditEnding -= EditableDataGrid_CellEditEnding;
            this.BeginningEdit -= EditableDataGrid_BeginningEdit;
            this.RowEditEnding -= EditableDataGrid_RowEditEnding;
            this.PreviewKeyDown -= EditableDataGrid_PreviewKeyDown;
        }

        bool EditMode = false;

        void EditableDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
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
            this.EditMode = true;
            Type t = e.EditingEventArgs.Source.GetType();
            // NOTE: 
            // e.Row.IsNewItem vaut toujours True même après la création de la ligne
            // Pour savoir si l'entité est une nouvelle insertion ou non, l'expression test le membre Factory
            if (e.Row.IsNewItem && e.Row.Item is IEntityPersistent && (e.Row.Item as IEntityPersistent).Factory == null)
            {
                IEntityPersistent entity = e.Row.Item as IEntityPersistent;
                entity.EntityState = EntityState.Added;
                IEventProcess process = this.DataContext as IEventProcess;
                if (process!=null)
                    process.ProcessEvent(this, this, new EntityPreCreateEvent(entity));
            }
        }

        private void EditableDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            this.EditMode = false;

            if (e.EditAction == DataGridEditAction.Cancel)
                return;

            if (e.Row.Item is IEntity)
            {
                IEntity entity = e.Row.Item as IEntity;

                IEventProcess process = this.DataContext as IEventProcess;
                if (process != null && entity.EntityState == EntityState.Modified)
                    process.ProcessEvent(this, this, new EntityChangeEvent(entity));
                if (process != null && entity.EntityState == EntityState.Added)
                    process.ProcessEvent(this, this, new EntityCreateEvent(entity));

                e.Cancel = false;
            }
        }

        private void EditableDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && this.SelectedItem != null && this.EditMode == false)
            {
                if (this.SelectionMode == DataGridSelectionMode.Extended && this.SelectedItems != null)
                {
                    foreach (var item in this.SelectedItems)
                    {
                        IEntity entity = item as IEntity;
                        if (entity != null)
                        {
                            EntityDeleteEvent ev = new EntityDeleteEvent(entity);

                            // Demande la supression
                            IEventProcess process = this.DataContext as IEventProcess;
                            if (process != null)
                                process.ProcessEvent(this, this, ev);

                            // Suppression ok ?
                            if (entity.EntityState != EntityState.Deleted)
                            {
                                e.Handled = true;// Annule la suppression
                                return;
                            }
                        }
                    }
                }
                else
                {
                    IEntity entity = this.SelectedItem as IEntity;
                    if (entity != null)
                    {
                        EntityDeleteEvent ev = new EntityDeleteEvent(entity);

                        // Demande la supression
                        IEventProcess process = this.DataContext as IEventProcess;
                        if (process != null)
                            process.ProcessEvent(this, this, ev);

                        // Suppression ok ?
                        if (entity.EntityState != EntityState.Deleted)
                        {
                            e.Handled = true;// Annule la suppression
                            return;
                        }
                    }
                }
            }

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
                    IEventProcess process = this.DataContext as IEventProcess;
                    if (process != null)
                        process.ProcessEvent(this, this, ev);
                }
            }

            if (e.Key == Key.V && e.KeyboardDevice.Modifiers == ModifierKeys.Control && this.SelectedItem != null && this.EditMode == false)
            {
                IEventProcess process = this.DataContext as IEventProcess;
                if (process != null)
                    process.ProcessEvent(this, this, new EntityCopyPasteEvent(EntityCopyPasteEventType.Paste));
            }
        }
    }
}

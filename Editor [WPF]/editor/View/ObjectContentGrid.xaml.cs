using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lib;

namespace editor.View
{
    /// <summary>
    /// Logique d'interaction pour ObjectContentGrid.xaml
    /// </summary>
    public partial class ObjectContentGrid : DataGrid
    {
        bool EditMode = false;

        public ObjectContentGrid()
        {
            InitializeComponent();
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Cancel)
                return;

            IEntity entity = e.Row.Item as IEntity;
            if (entity.EntityState == EntityState.Added)
                return;
            entity.EntityState = EntityState.Modified;

            /*
            if (e.Row.Item is IEntityState)
            {
                IEntityState entity = e.Row.Item as IEntityState;
                if (entity.EntityState == EntityState.Added)
                    return;
                entity.EntityState = EntityState.Modified;
            }*/
        }

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (e.Row.IsNewItem && e.Row.Item is IEntity)
            {
                IEntity entity = e.Row.Item as IEntity;
                entity.EntityState = EntityState.Added;
            }
            this.EditMode = true;
        }

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            this.EditMode = false;
        }

        private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
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
                            entity.EntityState = EntityState.Deleted;
                        }
                    }
                }
                else
                {
                    IEntity entity = this.SelectedItem as IEntity;
                    if (entity != null)
                    {
                        entity.EntityState = EntityState.Deleted;
                    }
                }
            }
        }
    }
}

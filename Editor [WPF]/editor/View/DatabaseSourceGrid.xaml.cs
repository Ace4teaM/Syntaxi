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
using AppModel.Entity;
using AppModel.View;
using Lib;

namespace editor.View
{
    /// <summary>
    /// Logique d'interaction pour DatabaseSourceGrid.xaml
    /// </summary>
    public partial class DatabaseSourceGrid : EditableDataGrid
    {
        public DatabaseSourceGrid()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            App app = Application.Current as App;
            if (this.SelectedItem != null && this.SelectedItem is DatabaseSource)
            {
                app.States.SelectedDatabaseSourceId = (this.SelectedItem as DatabaseSource).Id;
            }
        }

        private void PropertiesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Edit_DatabaseSource view = new Edit_DatabaseSource();
            view.DataContext = this.SelectedItem;
            view.Width = 500;

            EditWindow wnd = new EditWindow("Source de données", view);
            wnd.ShowDialog();
        }
    }
}

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
using AppModel.View;
using Lib;

namespace editor.View
{
    /// <summary>
    /// Logique d'interaction pour ParamContentGrid.xaml
    /// </summary>
    public partial class ParamContentGrid : EditableDataGrid
    {
        public ParamContentGrid()
        {
            InitializeComponent();
        }

        private void PropertiesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Edit_ParamContent view = new Edit_ParamContent();
            view.DataContext = this.SelectedItem;
            view.Width = 500;

            EditWindow wnd = new EditWindow("Données du paramètre", view);
            wnd.ShowDialog();
        }
    }
}

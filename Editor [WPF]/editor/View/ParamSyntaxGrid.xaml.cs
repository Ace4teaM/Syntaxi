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
    /// Logique d'interaction pour ParamSyntaxGrid.xaml
    /// </summary>
    public partial class ParamSyntaxGrid : EditableDataGrid
    {
        public ParamSyntaxGrid()
        {
            InitializeComponent();
        }

        private void PropertiesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Edit_ParamSyntax view = new Edit_ParamSyntax();
            view.DataContext = this.SelectedItem;
            view.Width = 500;

            EditWindow wnd = new EditWindow("Syntaxe de paramètre", view);
            wnd.ShowDialog();
        }
    }
}

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

namespace editor.View
{
    /// <summary>
    /// Logique d'interaction pour SearchParamsEdit.xaml
    /// </summary>
    public partial class SearchParamsEdit : UserControl
    {
        public SearchParamsEdit()
        {
            InitializeComponent();
        }

        private void OpenInputDir_Click(object sender, RoutedEventArgs e)
        {

            System.Windows.Forms.FolderBrowserDialog Dialog = new System.Windows.Forms.FolderBrowserDialog();
            while (Dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            this.InputDir.Text = Dialog.SelectedPath;
        }
    }
}

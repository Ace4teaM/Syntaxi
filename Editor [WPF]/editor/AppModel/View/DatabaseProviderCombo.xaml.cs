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

namespace AppModel.View
{
    /// <summary>
    /// Logique d'interaction pour DatabaseProviderCombo.xaml
    /// </summary>
    public partial class DatabaseProviderCombo : ComboBox
    {
        public DatabaseProviderCombo()
        {
            InitializeComponent();
            
            this.ItemsSource = AppModel.Domain.DatabaseProviderConverter.ItemsSource;
            this.SelectedValuePath = "Value";
            this.DisplayMemberPath= "Key";
        }
    }
}
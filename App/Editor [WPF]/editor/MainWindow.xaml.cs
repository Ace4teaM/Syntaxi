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

namespace editor
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IEventProcess
    {
        public MainWindow()
        {
            InitializeComponent();

            homeView.DataContext = new ModelView.VueHome();
        }

        public void ChangeView(UIElement view)
        {
            this.Client.Children.Clear();
            this.Client.Children.Add(view);
        }

        //-----------------------------------------------------------------------------------------
        // Evénements
        //-----------------------------------------------------------------------------------------
        #region IEventProcess
        public void ProcessEvent(object from, object _this, IEvent e)
        {
            if (from == this)
                return;

            // passe aux vues enfants
            EventProcess.SendToControls(this, from, this, e);
        }
        #endregion
    }
}

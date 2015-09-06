using System;
using System.Collections.Generic;
using System.IO;
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
using EditorModel.Entity;
using Microsoft.Win32;

namespace editor.View
{
    /// <summary>
    /// Logique d'interaction pour VueHome.xaml
    /// </summary>
    public partial class VueHome : UserControl
    {
        public VueHome()
        {
            InitializeComponent();
        }

        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            editor.App app = Application.Current as editor.App;
            MainWindow wnd = app.MainWindow as MainWindow;

            OpenFileDialog dlg = new OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".prj";
            dlg.Filter = "Projet Syntaxi (.prj)|*.prj";

            // Get the selected file name and display in a TextBox
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    app.OpenProject(dlg.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(app.MainWindow, "Impossible de charger le projet.\n" + ex.Message, "Oups", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else return;

            //
            VueEditor view = new VueEditor();
            view.DataContext = new ModelView.VueEditor();
            wnd.ChangeView(view);
        }

        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            editor.App app = Application.Current as editor.App;
            MainWindow wnd = app.MainWindow as MainWindow;

            // dossier d'enregistrement
            SaveFileDialog dlg = new SaveFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".prj";
            dlg.Filter = "Projet Syntaxi|*.prj";

            // Get the selected file name and display in a TextBox
            if (dlg.ShowDialog() != true)
            {
                return;
            }

            app.ProjectFileName = dlg.FileName;

            // Initialise le projet
            switch (this.ProjectTypeCb.SelectedValue as string)
            {
                /*case "c#":
                    app.Project = new Project(this.NameTb.Text, this.VersionTb.Text);
                    app.appModel.AddCSharpSyntax();
                    app.Editor = app.MakeCSharpStates();
                    break;*/
                case "c++":
                    app.Project = new Project(this.NameTb.Text, this.VersionTb.Text);
                    app.appModel.AddCppSyntax();
                    app.States = app.MakeCppStates();
                    break;
                case "empty":
                    app.Project = new Project(this.NameTb.Text, this.VersionTb.Text);
                    app.States = new EditorStates(app.Version,String.Empty);
                    break;
            }

            //
            VueEditor view = new VueEditor();
            view.DataContext = new ModelView.VueEditor();
            wnd.ChangeView(view);
        }
    }
}

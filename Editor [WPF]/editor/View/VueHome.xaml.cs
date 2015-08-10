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
            App app = Application.Current as App;
            MainWindow wnd = app.MainWindow as MainWindow;

            OpenFileDialog dlg = new OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".prj";
            dlg.Filter = "Projet Syntaxi (.prj)|*.prj";

            // Get the selected file name and display in a TextBox
            if (dlg.ShowDialog() == true)
            {
                // Charge le projet
                {
                    FileStream file = File.Open(dlg.FileName, FileMode.Open);
                    BinaryReader reader = new BinaryReader(file);
                    Project project = new Project();
                    project.ReadBinary(reader);
                    reader.Close();
                    file.Close();
                    app.Project = project;
                }

                // Charge les infos sur le projet
                String EditorDataFilename = dlg.FileName.Remove(-3, 3) + ".dat";
                if (File.Exists(EditorDataFilename))
                {
                    FileStream file = File.Open(dlg.FileName, FileMode.Open);
                    BinaryReader reader = new BinaryReader(file);
                    EditorStates states = new EditorStates();
                    states.ReadBinary(reader);
                    reader.Close();
                    file.Close();
                    app.States = states;
                }
            }

            //
            VueObjectSyntax view = new VueObjectSyntax();
            view.DataContext = new ModelView.VueObjectSyntax(app.Project);
            wnd.ChangeView(view);
        }

        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            App app = Application.Current as App;
            MainWindow wnd = app.MainWindow as MainWindow;

            switch (this.ProjectTypeCb.SelectedValue as string)
            {
                /*case "c#":
                    app.Project = app.MakeCSharpProject(this.NameTb.Text, this.VersionTb.Text);
                    app.Editor = app.MakeCSharpStates();
                    break;*/
                case "c++":
                    app.Project = app.MakeCppProject(this.NameTb.Text, this.VersionTb.Text);
                    app.States = app.MakeCppStates();
                    break;
                case "empty":
                    app.Project = new Project(this.NameTb.Text, this.VersionTb.Text);
                    app.States = new EditorStates(app.Version);
                    break;
            }

            //
            VueObjectSyntax view = new VueObjectSyntax();
            view.DataContext = new ModelView.VueObjectSyntax(app.Project);
            wnd.ChangeView(view);
        }
    }
}

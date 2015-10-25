using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AppModel.Entity;
using EditorModel.Entity;
using Lib;
using Microsoft.Win32;

namespace editor.ModelView
{
    class VueHome : ViewModelBase, IEventProcess
    {
        editor.App app = Application.Current as editor.App;
        public VueHome()
        {
            SelProjectType = "empty";
        }

        //-----------------------------------------------------------------------------------------
        // Méthodes
        //-----------------------------------------------------------------------------------------
        #region Methods
        private void OpenProject()
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
            View.VueEditor view = new View.VueEditor();
            view.DataContext = new ModelView.VueEditor();
            wnd.ChangeView(view);
        }

        private void NewProject()
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
            switch (selProjectType)
            {
                /*case "c#":
                    app.Project = new Project(projectName, projectVersion);
                    app.appModel.AddCSharpSyntax();
                    app.editorModel.CreateModel();
                    app.editorModel.AddCSharpStates();
                    break;*/
                case "c++":
                    app.Project = new Project(projectName, projectVersion);
                    app.appModel.AddCppSyntax();
                    app.editorModel.CreateModel();
                    app.editorModel.AddCppStates();
                    break;
                case "empty":
                    app.Project = new Project(projectName, projectVersion);
                    app.editorModel.CreateModel();
                    break;
            }

            //
            View.VueEditor view = new View.VueEditor();
            view.DataContext = new ModelView.VueEditor();
            wnd.ChangeView(view);
        }
        #endregion

        //-----------------------------------------------------------------------------------------
        // Data
        //-----------------------------------------------------------------------------------------
        #region Data
        private string selProjectType;
        private string projectName;
        private string projectVersion;
        #endregion

        //-----------------------------------------------------------------------------------------
        // Propriétés
        //-----------------------------------------------------------------------------------------
        #region Properties
        public string SelProjectType
        {
            get{
                return selProjectType;
            }
            set
            {
                selProjectType = value;
                OnPropertyChanged("SelProjectType");
            }
        }
        public string ProjectName
        {
            get
            {
                return projectName;
            }
            set
            {
                projectName = value;
                OnPropertyChanged("ProjectName");
            }
        }
        public string ProjectVersion
        {
            get
            {
                return projectVersion;
            }
            set
            {
                projectVersion = value;
                OnPropertyChanged("ProjectVersion");
            }
        }
        #endregion

        //-----------------------------------------------------------------------------------------
        // Commandes
        //-----------------------------------------------------------------------------------------
        #region Commands
        #region NewProjectCmd
        private ICommand newProjectCmd;
        public ICommand NewProjectCmd
        {
            get
            {
                if (this.newProjectCmd == null)
                    this.newProjectCmd = new DelegateCommand(() =>
                    {
                        NewProject();
                    });

                return this.newProjectCmd;
            }
        }
        #endregion
        #region OpenProjectCmd
        private ICommand openProjectCmd;
        public ICommand OpenProjectCmd
        {
            get
            {
                if (this.openProjectCmd == null)
                    this.openProjectCmd = new DelegateCommand(() =>
                    {
                        OpenProject();
                    });

                return this.openProjectCmd;
            }
        }
        #endregion
        #endregion

        //-----------------------------------------------------------------------------------------
        // Evénements
        //-----------------------------------------------------------------------------------------
        #region IEventProcess
        // Traite les événements
        public void ProcessEvent(object from, object _this, IEvent e)
        {
        }
        #endregion
    }
}

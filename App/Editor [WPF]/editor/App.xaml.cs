using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using AppModel.Entity;
using Event;
using EditorModel.Entity;
using Lib;
using Microsoft.Win32;

namespace editor
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application, IApp
    {
        public String Version = "1.0";
        public string ProjectFileName;

        // Application
        public AppModel.Model appModel = new AppModel.Model();
        public EditorModel.Model editorModel = new EditorModel.Model();
        
        public Project Project {
            get { return appModel.project; }
            set {
                appModel.project = value;
                if (appModel.project != null)
                    appModel.Add(appModel.project);
                if (appModel.project != null && MainWindow != null)
                    MainWindow.Title = "Syntaxi - " + Project.Name + " [" + Project.Version + "]";
            }
        }

        //private EditorStates states;
        public EditorStates States
        {
            get { return editorModel.states; }
            /*set
            {
                states = value;
            }*/
        }
        public string ProjectFilePath
        {
            get {
                if (ProjectFileName == null)
                    return null;
                int index = ProjectFileName.LastIndexOf(@"\");
                if (index<0)
                    return null;
                return ProjectFileName.Substring(0, ProjectFileName.Length-(ProjectFileName.Length-index));
            }
        }
        
        public bool SaveProject()
        {
            if (ProjectFileName == null)
            {
                SaveFileDialog dlg = new SaveFileDialog();

                // Set filter for file extension and default file extension
                dlg.DefaultExt = ".prj";
                dlg.Filter = "Projet Syntaxi|*.prj";

                // Get the selected file name and display in a TextBox
                if (dlg.ShowDialog() != true)
                {
                    return false;
                }

                ProjectFileName = dlg.FileName;
            }

            // Sauvegarde le projet
            appModel.SaveProject(ProjectFileName);

            // Sauvegarde les infos sur le projet
            String EditorDataFilename = ProjectFileName.Remove(ProjectFileName.Length - 3, 3) + "dat";
            editorModel.SaveToFile(EditorDataFilename);
            /*if (this.States != null)
            {
                String EditorDataFilename = ProjectFileName.Remove(ProjectFileName.Length-3, 3) + "dat";
                FileStream file = File.Open(EditorDataFilename, FileMode.OpenOrCreate);
                BinaryWriter reader = new BinaryWriter(file);
                this.States.WriteBinary(reader);
                reader.Close();
                file.Close();
            }*/

            return true;
        }

        public bool OpenProject(string FileName)
        {
            // Charge le projet
            appModel.LoadProject(FileName);
            this.ProjectFileName = FileName;

            // Charge les infos sur le projet
            String EditorDataFilename = FileName.Remove(ProjectFileName.Length - 3, 3) + "dat";
            if (editorModel.LoadFromFile(EditorDataFilename) != true)
                editorModel.CreateModel();
            /*
            if (File.Exists(EditorDataFilename))
            {
                FileStream file = File.Open(EditorDataFilename, FileMode.Open);
                BinaryReader reader = new BinaryReader(file);
                EditorStates states = new EditorStates();
                states.ReadBinary(reader,null);
                reader.Close();
                file.Close();
                this.States = states;
            }
            else
                this.States = new EditorStates(this.Version,String.Empty);
            */
            return true;
        }

        public void ScanObjects()
        {
            // Supprime le contenu existant
            foreach (ObjectContent e in appModel.project.ObjectContent.ToList())
            {
                appModel.Remove(e);
            }

            // Scan le nouveau contenu
            foreach (var s in appModel.project.SearchParams.ToList())
            {
                SearchParams search = new SearchParams(s);
                search.Model = s.Model;

                // Fix les chemins relatifs
                if (s.InputDir.StartsWith(@".\"))
                    search.InputDir = this.ProjectFilePath + @"\" + s.InputDir.Substring(2);
                else if (s.InputDir.StartsWith(@".."))
                    search.InputDir = this.ProjectFilePath + @"\" + s.InputDir;
                else
                    search.InputDir = s.InputDir;

                //
                appModel.AddObjects(search);
            }

            appModel.NotifyEvent(new ModelChangeEvent(appModel));
        }

        //-----------------------------------------------------------------------------------------
        // IApp
        //-----------------------------------------------------------------------------------------

        // Traite les exceptions
        public void ProcessException(Exception ex)
        {
            MessageBox.Show(ex.Message, "Erreur");
        }

        //-----------------------------------------------------------------------------------------
        // IEventProcess
        //-----------------------------------------------------------------------------------------
        public void ProcessEvent(object from, object _this, IEvent e)
        {
            //
            // Passe l'événement aux fenêtres actives
            //
            /*foreach (Window window in Application.Current.Windows)
            {
                //Console.WriteLine("Process to window "+window);
                if (window.DataContext is IEventProcess)
                    ((IEventProcess)window.DataContext).ProcessEvent(from, this, e);
                if (window is IEventProcess)
                    ((IEventProcess)window).ProcessEvent(from, this, e);
            }*/
        }
    }
}

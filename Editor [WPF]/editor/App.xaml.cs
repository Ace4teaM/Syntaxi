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
    public partial class App : Application, IEventProcess
    {
        public String Version = "1.0";
        public string ProjectFileName;

        // Application
        public AppModel.App appModel = new AppModel.App();

        public Project Project {
            get { return appModel.project; }
            set {
                appModel.project = value;
                if (appModel.project != null && MainWindow != null)
                    MainWindow.Title = "Syntaxi - " + Project.Name + " [" + Project.Version + "]";
            }
        }

        private EditorStates states;
        public EditorStates States
        {
            get { return states; }
            set
            {
                states = value;
            }
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
        
        public EditorStates MakeCppStates()
        {
            // Initialise
            EditorStates states = new EditorStates(this.Version, String.Empty);

            // function example
            states.AddEditorSampleCode(new EditorSampleCode(
@"
/**
	Alloue est initialise la mémoire

	Parametres:
		handle_count : nombre d'handle allouable
		handle_size  : taille d'un handle

	Retourne:
		1 en cas de succes, 0 en cas d'erreur.
*/
ushort npInitHandle(ushort handle_count,ushort handle_size)
{ ... }
",
                @"function")
            );

            // struct example
            states.AddEditorSampleCode(new EditorSampleCode(
@"
/**
	En-tete d'un handle
*/
typedef struct _NP_HANDLE_HEADER{
	ushort chunk_count;
	ushort chunk_size;
	size_t data_size;
	NP_HANDLE_INDICE* index;
	char* data;
	char* lock_data;
}NP_HANDLE_HEADER;
",
                @"struct")
            );

            return states;
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
            if (this.States != null)
            {
                String EditorDataFilename = ProjectFileName.Remove(ProjectFileName.Length-3, 3) + "dat";
                FileStream file = File.Open(EditorDataFilename, FileMode.OpenOrCreate);
                BinaryWriter reader = new BinaryWriter(file);
                this.States.WriteBinary(reader);
                reader.Close();
                file.Close();
            }

            return true;
        }

        public bool OpenProject(string FileName)
        {
            // Charge le projet
            appModel.LoadProject(FileName);
            this.ProjectFileName = FileName;

            // Charge les infos sur le projet
            String EditorDataFilename = FileName.Remove(ProjectFileName.Length - 3, 3) + "dat";
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

            return true;
        }

        public void ScanObjects()
        {
            appModel.project.ObjectContent.Clear();
            foreach (var s in appModel.project.SearchParams)
            {
                SearchParams search = new SearchParams(s);

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
        }
        

        //-----------------------------------------------------------------------------------------
        // IEventProcess
        //-----------------------------------------------------------------------------------------
        public void ProcessEvent(object from, object _this, IEvent e)
        {
            if (this.MainWindow != null && this.MainWindow is IEventProcess)
                (this.MainWindow as IEventProcess).ProcessEvent(from, this, e);
        }
    }
}

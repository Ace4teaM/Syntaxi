using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppModel.Entity;
using EditorModel.Entity;
using Lib;
using Microsoft.Win32;

namespace editor
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public String Version = "1.0";
        private string ProjectFileName;
        private Project project;
        public Project Project {
            get { return project; }
            set { 
                project = value;
                if (project != null && MainWindow != null)
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

        public EditorStates MakeCppStates()
        {
            // Initialise
            EditorStates states = new EditorStates(this.Version);

            // function example
            states.EditorSampleCode.Add(new EditorSampleCode(
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
            states.EditorSampleCode.Add(new EditorSampleCode(
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

        public Project MakeCppProject(string title, string version)
        {
            // Initialise un projet
            Project project = new Project(title, version);

            //
            // Params Syntaxes
            //

            project.ParamSyntax.Add(new ParamSyntax(
                @"^(?:\s*)(?:description[s]?)?\:\n(?<content>(?!\n{2,})(?:.|\n[^\n])*)",
                @"^(?:[\n\s]*)(?<content>[^\n]+)",
                @"description")
            );

            project.ParamSyntax.Add(new ParamSyntax(
                @"^(?:\s*)(?:exemple|example|sample?)?\:\n(?<content>(?!\n{2,})(?:.|\n[^\n])*)",
                @"(?<content>[^\0]*)",
                @"exemple")
            );

            project.ParamSyntax.Add(new ParamSyntax(
                @"^(?:\s*)(?:param[eè]tre[s]|parameter[s]?)?\:\n(?<content>(?!\n{2,})(?:.|\n[^\n])*)",
                @"^(?:[\n\s]*)(?<content>[^\n]+)",
                @"param")
            );

            project.ParamSyntax.Add(new ParamSyntax(
                @"^(?:\s*)(?:remarque[s]?|remark[s]?)?\:\n(?<content>(?!\n{2,})(?:.|\n[^\n])*)",
                @"^(?:[\n\s]*)(?<content>[^\n]+)",
                @"remark")
            );

            project.ParamSyntax.Add(new ParamSyntax(
                @"^(?:\s*)(?:retourne|return)?\:\n(?<content>(?!\n{2,})(?:.|\n[^\n])*)",
                @"^(?:[\n\s]*)(?<content>[^\n]+)",
                @"return")
            );

            //
            // Objets Syntaxes
            //
            ObjectSyntax objSyntax;

            // Fonction
            objSyntax = new ObjectSyntax(
                @"^(?:\s*\/\*\*)(?:[\n\s]+)(?<description>[^\n]*)[\n]+(?<content>(?:[^*]|\*[^\/])+)(?:\*\/)(?:[\n\s]*)(?<return_type>[A-Za-z_]+)(?:[\n\s]+)(?<name>[A-Za-z_]+)(?:[\n\s]*)\((?<params>[^\)]*)\)",
                @"^(?:\s*)\@(?<type>[A-Za-z]+)(?:\s+)(?<content>[^@])+",
                @"function",
                @"Fonction");
            /*objSyntax.ParamSyntax = new Collection<ParamSyntax>(
                project.ParamSyntax.Where(p=>p.ParamType == "description" || p.ParamType == "return").ToArray()
            );*/
            project.ObjectSyntax.Add(objSyntax);

            // Structure
            objSyntax = new ObjectSyntax(
                @"^(?:\s*\/\*\*)(?:[\n\s]+)(?<description>[^\n]*)[\n]+(?<content>(?:[^*]|\*[^\/])*)(?:\*\/)(?:[\n\s]*)typedef(?:[\n\s]+)struct(?:[\n\s]+)(?<name>[A-Za-z_]+)(?:[\n\s]*)\{",
                @"^(?:\s*)\@(?<type>[A-Za-z]+)(?:\s+)(?<content>[^@])+",
                @"struct",
                @"Structure de données");
            // objSyntax.ParamSyntax = new Collection<ParamSyntax>();
            project.ObjectSyntax.Add(objSyntax);

            return project;
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
            {
                FileStream file = File.Open(ProjectFileName, FileMode.OpenOrCreate);
                BinaryWriter reader = new BinaryWriter(file);
                this.Project.WriteBinary(reader);
                reader.Close();
                file.Close();
            }

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
            {
                FileStream file = File.Open(FileName, FileMode.Open);
                BinaryReader reader = new BinaryReader(file);
                Project project = new Project();
                project.ReadBinary(reader);
                reader.Close();
                file.Close();
                this.Project = project;
                this.ProjectFileName = FileName;
            }

            // Charge les infos sur le projet
            String EditorDataFilename = FileName.Remove(ProjectFileName.Length - 3, 3) + "dat";
            if (File.Exists(EditorDataFilename))
            {
                FileStream file = File.Open(EditorDataFilename, FileMode.Open);
                BinaryReader reader = new BinaryReader(file);
                EditorStates states = new EditorStates();
                states.ReadBinary(reader);
                reader.Close();
                file.Close();
                this.States = states;
            }

            return true;
        }

        public void ScanFolder(SearchParams searchParams)
        {
        }

        public void Export(IEntityFactory factory)
        {
            foreach (var e in Project.ObjectContent)
            {
                e.Factory = factory;
                e.Insert();
            }
        }
    }
}

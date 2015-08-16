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
        public string ProjectFileName;
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
            EditorStates states = new EditorStates(this.Version,null);

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

        public Project MakeCppProject(string title, string version)
        {
            // Initialise un projet
            Project project = new Project(title, version);

            //
            // Params Syntaxes
            //

            project.AddParamSyntax(new ParamSyntax(
                @"^(?:\s*)(?:description[s]?)?\:\n(?<content>(?!\n{2,})(?:.|\n[^\n])*)",
                @"^(?:[\n\s]*)(?<content>[^\n]+)",
                @"description")
            );

            project.AddParamSyntax(new ParamSyntax(
                @"^(?:\s*)(?:exemple|example|sample?)?\:\n(?<content>(?!\n{2,})(?:.|\n[^\n])*)",
                @"(?<content>[^\0]*)",
                @"exemple")
            );

            project.AddParamSyntax(new ParamSyntax(
                @"^(?:\s*)(?:param[eè]tre[s]|parameter[s]?)?\:\n(?<content>(?!\n{2,})(?:.|\n[^\n])*)",
                @"^(?:[\n\s]*)(?<content>[^\n]+)",
                @"param")
            );

            project.AddParamSyntax(new ParamSyntax(
                @"^(?:\s*)(?:remarque[s]?|remark[s]?)?\:\n(?<content>(?!\n{2,})(?:.|\n[^\n])*)",
                @"^(?:[\n\s]*)(?<content>[^\n]+)",
                @"remark")
            );

            project.AddParamSyntax(new ParamSyntax(
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
            project.AddObjectSyntax(objSyntax);

            // Structure
            objSyntax = new ObjectSyntax(
                @"^(?:\s*\/\*\*)(?:[\n\s]+)(?<description>[^\n]*)[\n]+(?<content>(?:[^*]|\*[^\/])*)(?:\*\/)(?:[\n\s]*)typedef(?:[\n\s]+)struct(?:[\n\s]+)(?<name>[A-Za-z_]+)(?:[\n\s]*)\{",
                @"^(?:\s*)\@(?<type>[A-Za-z]+)(?:\s+)(?<content>[^@])+",
                @"struct",
                @"Structure de données");
            // objSyntax.ParamSyntax = new Collection<ParamSyntax>();
            project.AddObjectSyntax(objSyntax);

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
            else
                this.States = new EditorStates(this.Version,String.Empty);

            return true;
        }

        public void ScanObjects()
        {
            project.ObjectContent.Clear();
            foreach (var s in project.SearchParams)
            {
                AddObjects(s.InputDir, s.InputFilter, s.Recursive);
            }
        }

        public void AddObjects(string inputDir, string inputFilter, bool bRecursive)
        {
            // Liste des objets trouvés
            List<ObjectContent> objets = new List<ObjectContent>();

            // Liste les fichiers
            if (inputDir.StartsWith(@".\"))
                inputDir = this.ProjectFilePath + @"\" + inputDir.Substring(2);
            else if (inputDir.StartsWith(@".."))
                inputDir = this.ProjectFilePath + @"\" + inputDir;
            string[] srcPaths = Directory.GetFiles(inputDir, inputFilter, (bRecursive == true ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));

            // Scan les fichiers
            foreach (var filePath in srcPaths)
            {
                Console.WriteLine(String.Format("Scan file: {0}", filePath));
                //Log(String.Format("Scan file: {0}", filePath));
                string relativeFileName = filePath.Substring(inputDir.Length);
                string text = string.Empty;
                using (StreamReader streamReader = new StreamReader(filePath, Encoding.UTF8))
                {
                    text = streamReader.ReadToEnd().Replace("\r\n", "\n");
                }

                // Scan les objets
                foreach (ObjectSyntax curSyntax in project.ObjectSyntax)
                {
                    ScanFile(text, relativeFileName, curSyntax, objets);
                }

                // Log
                Console.WriteLine(String.Format("{0} objets traités", objets.Count));
            }

            // Ajoute les objets au projet
            foreach (var o in objets)
                project.AddObjectContent(o);
        }

        /// <summary>
        /// Scan un fichier à la recherche d'objets
        /// </summary>
        /// <param name="text">Texte du code à analyser</param>
        /// <param name="filePath">Chemin d'accès relatif au fichier analysé</param>
        /// <param name="syntax">Syntaxe utilisé pour scaner le texte</param>
        void ScanFile(string text, string filePath, ObjectSyntax syntax, List<ObjectContent> objList)
        {
            // Convertie en expression reguliere
            Regex content = new Regex(syntax.ContentRegEx, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            Regex param = new Regex(syntax.ParamRegEx, RegexOptions.Multiline | RegexOptions.IgnoreCase);

            MatchCollection matches = content.Matches(text);
            foreach (Match match in matches)
            {
                // Initialise l'objet
                ObjectContent o = new ObjectContent();
                o.ObjectType = syntax.ObjectType;
                o.Filename = filePath;
                o.Position = match.Index;
                o.Id = Guid.NewGuid().ToString("N");

                // Extrer les paramètres implicite de l'expression régulière
                foreach (string groupName in content.GetGroupNames())
                {
                    if (groupName != "content" && groupName != "0")
                    {
                        o.AddParamContent(new ParamContent(Guid.NewGuid().ToString("N"), groupName, match.Groups[groupName].Value));
                        //Log(String.Format("\tAdd param '{0}' as '{1}'", groupName, match.Groups[groupName].Value));
                    }
                }

                // Recherche des paramètres dans le contenu de l'objet
                string objet_text = match.Groups["content"].Value;

                // Extrer les groupes de parametres
                foreach (ParamSyntax g in project.ParamSyntax)
                {
                    // Convertie en expression reguliere
                    Regex pContent = new Regex(g.ContentRegEx, RegexOptions.Multiline | RegexOptions.IgnoreCase);
                    Regex pParam = new Regex(g.ParamRegEx, RegexOptions.Multiline | RegexOptions.IgnoreCase);

                    MatchCollection pMatches = pContent.Matches(objet_text);
                    foreach (Match pMatch in pMatches)
                    {
                        MatchCollection gParamMatches = pParam.Matches(pMatch.Groups["content"].Value);
                        foreach (Match paramMatch in gParamMatches)
                        {
                            o.AddParamContent(new ParamContent(Guid.NewGuid().ToString("N"), g.ParamType, paramMatch.Groups["content"].Value));
                        }
                    }
                }

                // Extrer les parametres d'objet
                MatchCollection paramMatches = param.Matches(objet_text);
                foreach (Match paramMatch in paramMatches)
                {
                    o.AddParamContent(new ParamContent(Guid.NewGuid().ToString("N"), paramMatch.Groups["type"].Value, paramMatch.Groups["content"].Value));
                }

                // Ajoute à la liste des objets
                objList.Add(o);
            }
        }

        /// <summary>
        /// Importe des syntaxes d'objets depuis un dossier
        /// </summary>
        /// <param name="path"></param>
        public void ImportSyntaxDirectory(string path)
        {
            // Scan les objets
            if (Directory.Exists(path))
            {
                string[] groupsPaths = Directory.GetFiles(path, "*");
                foreach (var syntaxFile in groupsPaths)
                {
                    using (StreamReader streamReader = new StreamReader(syntaxFile, Encoding.UTF8))
                    {
                        try
                        {
                            ObjectSyntax syntax = new ObjectSyntax();
                            syntax.ObjectType = Path.GetFileNameWithoutExtension(syntaxFile);
                            syntax.ContentRegEx = streamReader.ReadLine();
                            syntax.ParamRegEx = streamReader.ReadLine();
                            syntax.ObjectDesc = String.Empty;
                            project.AddObjectSyntax(syntax);
                            Console.WriteLine("Add syntax object " + syntax.ObjectType);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ignore object file '" + syntaxFile + "'. " + ex.Message);
                        }
                        streamReader.Close();
                    }
                }
            }

            // Scan les groupes
            if (Directory.Exists(path + @"\groups"))
            {
                string[] groupsPaths = Directory.GetFiles(path + @"\groups", "*");
                foreach (var syntaxFile in groupsPaths)
                {
                    using (StreamReader streamReader = new StreamReader(syntaxFile, Encoding.UTF8))
                    {
                        try
                        {
                            ParamSyntax syntax = new ParamSyntax();
                            syntax.ParamType = Path.GetFileNameWithoutExtension(syntaxFile);
                            syntax.ContentRegEx = streamReader.ReadLine();
                            syntax.ParamRegEx = streamReader.ReadLine();
                            project.AddParamSyntax(syntax);
                            Console.WriteLine("Add syntax param " + syntax.ParamType);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ignore param file '" + syntaxFile + "'. " + ex.Message);
                        }
                        streamReader.Close();
                    }
                }
            }
        }

        public void Export(IEntityFactory factory)
        {
            Project.Factory = factory;
            //Supprime la version existante
            Project.Delete();
            //Insert les données
            Project.Insert();
            foreach (var e in Project.ObjectContent)
            {
                e.Factory = factory;
                e.Insert();
                foreach (var p in e.ParamContent)
                {
                    p.Factory = factory;
                    p.Insert();
                }
            }
        }

        public void Import(IEntityFactory factory)
        {
            Project.Factory = factory;
            //Supprime la version existante
            Project.Load();
            Project.LoadObjectContent();
            //Insert les données
            foreach (var e in Project.ObjectContent)
            {
                e.LoadParamContent();
            }
        }
    }
}

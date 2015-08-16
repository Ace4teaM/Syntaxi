using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AppModel.Entity;
using AppModel.Interface;

namespace app
{
    class Program
    {
        static int Main(string[] args)
        {
            App app = new App();
            int ret = app.Run(args);
            //Console.ReadKey();
            return ret;
        }

        public class App : IApp
        {
            public class AppArguments
            {
                // Dossier a scanner en entrée
                public string inputDir = @".\src";
                // Dossier de sortie
                public string outputFile = @"doc.xml";
                // Filtre de sélection en entrée
                public string inputFilter = @"*";
                // Dossier des définitions d'objets
                public string defDir = @".\objects";
                // Recherche récursive ?
                public bool recursive = false;
                // Version du projet cible
                public string version = "1.0";
                // Titre du probjet cible
                public string title = "Default";
                // Action a entreprendre
                public string action = null;
                // Fichier du projet en entrée
                public string inputProjectFile = null;
                // Dossier des objets de syntaxes
                public string syntaxDir = null;

                /// <summary>
                /// Lit les argument depuis la ligne de commande
                /// </summary>
                /// <param name="args">Ligne de commande</param>
                public void ReadArguments(string[] args)
                {
                    for (int c = 0; c < args.Count(); c++)
                    {
                        string arg = args[c];

                        if (arg[0] == '-')
                        {
                            switch (arg[1])
                            {
                                case 'a':
                                    action = args[++c];
                                    break;
                                case 'p':
                                    inputProjectFile = args[++c];
                                    break;
                                case 'i':
                                    inputDir = args[++c];
                                    break;
                                case 'o':
                                    outputFile = args[++c];
                                    break;
                                case 'f':
                                    inputFilter = args[++c];
                                    break;
                                case 'd':
                                    defDir = args[++c];
                                    break;
                                case 'v':
                                    version = args[++c];
                                    break;
                                case 't':
                                    title = args[++c];
                                    break;
                                case 'r':
                                    recursive = true;
                                    break;
                                case 's':
                                    syntaxDir = args[++c];
                                    break;
                                default:
                                    c++;
                                    break;
                            }
                        }
                    }
                }
            }

            // Projet en cours
            Project project;

            public int Run(string[] args)
            {
                // Lit les arguments
                AppArguments options = new AppArguments();
                options.ReadArguments(args);

                // Action ?
                if (String.IsNullOrEmpty(options.action))
                {
                    Console.WriteLine("Aucune action de définit");
                    return 1;
                }

                // Initialise le projet
                if (!String.IsNullOrEmpty(options.inputProjectFile) && (options.action != "init"))
                {
                    // Charge le projet
                    LoadProject(options.inputProjectFile);
                }
                else
                {
                    // Initialise un nouveau projet
                    options.inputProjectFile = @".\" + options.title + ".prj";
                    project = new Project(options.title,options.version);
                }

                // action
                switch (options.action.ToLower())
                {
                    // Réinitialise le projet
                    case "init":
                        project = new Project(options.title,options.version);
                        break;
                    // Réinitialise le projet
                    case "add_cpp_syntax":
                        project = AddCppSyntax();
                        break;
                    // Réinitialise le projet
                    case "import_syntax":
                        ImportSyntaxDirectory(options.syntaxDir);
                        break;
                    // Ajoute des paramétres de recherche
                    case "add":
                        AddSearch(options.inputDir, options.inputFilter, options.recursive);
                        break;
                    // Scan les données
                    case "scan":
                        ScanObjects();
                        break;
                    // Exporte les données
                    case "toxml":
                        //ExportObjectsAsXML(options.outputFile);
                        break;
                    // Exporte les données
                    case "print":
                        PrintObjects();
                        break;
                    default:
                        Console.WriteLine("Action inconnue");
                        return 1;
                }

                // Sauvegarde le projet
                SaveProject(options.inputProjectFile);

                return 0;
            }

            public void PrintObjects()
            {
                Console.WriteLine(project.ObjectContent.Count+" objects:");

                foreach (var o in project.ObjectContent)
                {
                    Console.WriteLine(o);
                    foreach (var p in o.ParamContent)
                    {
                        Console.WriteLine(p);
                    }
                    Console.WriteLine();
                }
            }

            public Project AddCppSyntax()
            {
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
                    "Fonction");
                /*objSyntax.ParamSyntax = new Collection<ParamSyntax>(
                    project.ParamSyntax.Where(p=>p.ParamType == "description" || p.ParamType == "return").ToArray()
                );*/
                project.ObjectSyntax.Add(objSyntax);

                // Structure
                objSyntax = new ObjectSyntax(
                    @"^(?:\s*\/\*\*)(?:[\n\s]+)(?<description>[^\n]*)[\n]+(?<content>(?:[^*]|\*[^\/])*)(?:\*\/)(?:[\n\s]*)typedef(?:[\n\s]+)struct(?:[\n\s]+)(?<name>[A-Za-z_]+)(?:[\n\s]*)\{",
                    @"^(?:\s*)\@(?<type>[A-Za-z]+)(?:\s+)(?<content>[^@])+",
                    @"struct",
                    "Structure de données");
               // objSyntax.ParamSyntax = new Collection<ParamSyntax>();
                project.ObjectSyntax.Add(objSyntax);
                
                return project;
            }
            
            public void SaveProject(String Filename)
            {
                // Sauvegarde le projet
                FileStream file = File.Open(Filename, FileMode.OpenOrCreate);
                BinaryWriter writer = new BinaryWriter(file);
                project.WriteBinary(writer);
                writer.Close();
                file.Close();
            }

            public void LoadProject(String Filename)
            {
                // Sauvegarde le projet
                FileStream file = File.Open(Filename, FileMode.Open);
                BinaryReader reader = new BinaryReader(file);
                project = new Project();
                project.ReadBinary(reader);
                reader.Close();
                file.Close();
            }

            public void InitialiseProject()
            {
                throw new NotImplementedException();
            }

            public void AddSearch(string inputDir, string inputFilter, bool bRecursive)
            {
                project.SearchParams.Add(new SearchParams(inputDir, inputFilter, bRecursive));
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
                    project.ObjectContent.Add(o);

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
                            o.ParamContent.Add(new ParamContent(Guid.NewGuid().ToString("N"),groupName,match.Groups[groupName].Value));
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
                                o.ParamContent.Add(new ParamContent(Guid.NewGuid().ToString("N"), g.ParamType, paramMatch.Groups["content"].Value));
                            }
                        }
                    }

                    // Extrer les parametres d'objet
                    MatchCollection paramMatches = param.Matches(objet_text);
                    foreach (Match paramMatch in paramMatches)
                    {
                        o.ParamContent.Add(new ParamContent(Guid.NewGuid().ToString("N"), paramMatch.Groups["type"].Value, paramMatch.Groups["content"].Value));
                    }

                    // Ajoute à la liste des objets
                    objList.Add(o);
                }
            }

            /// <summary>
            /// Importe des syntaxes d'objets depuis un dossier
            /// </summary>
            /// <param name="path"></param>
            void ImportSyntaxDirectory(string path)
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

            /// <summary>
            /// Exporte les objets dans un document XML
            /// </summary>
            /// <param name="fileName">Nom du fichier XML</param>
            /// <param name="title">Titre de la librairie</param>
            /// <param name="version">Version de la librairie</param>
            /// <param name="objets">Objets à exporter</param>
            /*static void ExportToXML(string fileName, string title, string version, List<Object> objets)
            {
                // initialise le document
                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.Load(fileName);
                }
                catch (Exception)
                {
                    XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    doc.AppendChild(docNode);

                    XmlNode rootNode = doc.CreateElement("root");
                    doc.AppendChild(rootNode);
                }

                // obtient le noeud de la librairie existante
                XmlNode libNode = doc.DocumentElement.SelectSingleNode(String.Format("lib[@title='{0}' and @version='{1}']", title, version));
                if (libNode == null)
                {
                    XmlNode rootNode = doc.CreateElement("root");
                    doc.AppendChild(rootNode);

                    libNode = doc.CreateElement("lib");
                    rootNode.AppendChild(libNode);
                    AppendAttribute(doc, libNode, "title", title);
                    AppendAttribute(doc, libNode, "version", version);
                }

                // ajoute les objets
                foreach (var o in objets)
                {
                    XmlNode objNode = doc.CreateElement("object");
                    AppendAttribute(doc, objNode, "filename", o.fileName);
                    AppendAttribute(doc, objNode, "type", o.type);
                    AppendAttribute(doc, objNode, "position", o.position.ToString());
                    AppendAttribute(doc, objNode, "id", o.id);
                    //
                    foreach (var p in o.objParams)
                    {
                        XmlNode paramNode = doc.CreateElement("param");
                        AppendAttribute(doc, paramNode, "name", p.name);
                        paramNode.AppendChild(doc.CreateTextNode(p.value));
                        objNode.AppendChild(paramNode);
                    }
                    libNode.AppendChild(objNode);
                }

                doc.Save(fileName);
            }*/

        }
    }
}

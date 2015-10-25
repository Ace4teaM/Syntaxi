using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AppModel.Entity;
using AppModel.Interface;
using AppModel;

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

        public class App
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
                // Groupe
                public string groupName = null;

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
                                case 'g':
                                    groupName = args[++c];
                                    break;
                                default:
                                    c++;
                                    break;
                            }
                        }
                    }
                }
            }

            // Application
            public AppModel.Model appModel = new AppModel.Model();

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
                    appModel.LoadProject(options.inputProjectFile);
                }
                else
                {
                    // Initialise un nouveau projet
                    options.inputProjectFile = @".\" + options.title + ".prj";
                    appModel.project = new Project(options.title, options.version);
                }

                // action
                switch (options.action.ToLower())
                {
                    // Réinitialise le projet
                    case "init":
                        appModel.project = new Project(options.title, options.version);
                        appModel.project.Model = appModel;
                        break;
                    // Réinitialise le projet
                    case "add_cpp_syntax":
                        appModel.AddCppSyntax();
                        break;
                    // Réinitialise le projet
                    case "import_syntax":
                        appModel.ImportSyntaxDirectory(options.syntaxDir);
                        break;
                    // Ajoute des paramétres de recherche
                    case "add":
                        appModel.AddSearch(options.groupName, options.inputDir, options.inputFilter, options.recursive);
                        break;
                    // Scan les données
                    case "scan":
                        ScanObjects(ProjectFilePath(options.inputProjectFile));
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
                appModel.SaveProject(options.inputProjectFile);

                return 0;
            }

            public void PrintObjects()
            {
                Console.WriteLine(appModel.project.ObjectContent.Count() + " objects:");

                foreach (var o in appModel.project.ObjectContent.ToList())
                {
                    Console.WriteLine(o);
                    foreach (var p in o.ParamContent.ToList())
                    {
                        Console.WriteLine(p);
                    }
                    Console.WriteLine();
                }
            }
            
            public string ProjectFilePath(string ProjectFileName)
            {
                if (ProjectFileName == null)
                    return null;
                int index = ProjectFileName.LastIndexOf(@"\");
                if (index < 0)
                    return null;
                return ProjectFileName.Substring(0, ProjectFileName.Length - (ProjectFileName.Length - index));
            }

            public void ScanObjects(string ProjectFilePath)
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

                    // Fix les chemins relatifs
                    if (s.InputDir.StartsWith(@".\"))
                        search.InputDir = ProjectFilePath + @"\" + s.InputDir.Substring(2);
                    else if (s.InputDir.StartsWith(@".."))
                        search.InputDir = ProjectFilePath + @"\" + s.InputDir;
                    else
                        search.InputDir = s.InputDir;

                    //
                    appModel.AddObjects(search);
                }
            }
        
        }
    }
}

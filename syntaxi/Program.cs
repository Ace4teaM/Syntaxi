using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace syntaxi
{
    /// <summary>
    /// Groupe de paramètres
    /// </summary>
    public class ParamGroup
    {
        public ParamGroup(string type, Regex value, Regex param)
        {
            this.type = type;
            this.value = value;
            this.param = param;
        }
        public string type;
        public Regex value,param;
    }
    /// <summary>
    /// Paramètre
    /// </summary>
    public class Param
    {
        public Param(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
        public string name;
        public string value;
    }
    /// <summary>
    /// Objet
    /// </summary>
    public class Object
    {
        public string type;
        public string fileName;
        public int position;
        public string id;
        public List<Param> objParams = new List<Param>();
    }
    /// <summary>
    /// Syntaxe d'objet
    /// </summary>
    public class Syntax
    {
        public Syntax(string type,Regex content, Regex param)
        {
            this.type = type;
            this.content = content;
            this.param = param;
        }
        public Syntax()
        {
            this.content = null;
            this.param = null;
        }
        public string type;
        public Regex content, param;
        public static Regex persitant_param;
    }
    /// <summary>
    /// Programme
    /// </summary>
    class Program
    {
        public class AppArguments
        {
            // args..
            public string inputDir = @".\src";
            public string outputFile = @"doc.xml";
            public string inputFilter = @"*";
            public string defDir = @".\objects";
            public bool recursive = false;
            public string version = "1.0";
            public string title = "Default";
            public string action = null;

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
                            default:
                                c++;
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Lit les paramètres
        /// </summary>
        /// <param name="text">Texte de l'objet</param>
        /// <param name="o">Objet parent</param>
        /// <param name="reg_param">Syntaxe d'un paramètre</param>
        static void ReadParam(string text, Object o, Regex reg_param)
        {
            //parametres
            MatchCollection paramMatches = reg_param.Matches(text);
            foreach (Match paramMatch in paramMatches)
            {
//                Console.WriteLine(String.Format("Find param {0} as {1}", paramMatch.Groups["type"].Value, paramMatch.Groups["content"].Value));
                o.objParams.Add(new Param(paramMatch.Groups["type"].Value, paramMatch.Groups["content"].Value));
            }
        }
        /// <summary>
        /// Lit un groupe de paramètres
        /// </summary>
        /// <param name="text">Texte de l'objet</param>
        /// <param name="o">Objet parent</param>
        /// <param name="group">Définit du group</param>
        static void ReadGroupParam(string text, Object o, ParamGroup group)
        {
            MatchCollection matches = group.value.Matches(text);
            foreach (Match match in matches)
            {
//                Console.WriteLine(String.Format("Find group {0} as:\n{1}", group.type, match.Groups["content"].Value));
                //parametres
                MatchCollection paramMatches = group.param.Matches(match.Groups["content"].Value);
                foreach (Match paramMatch in paramMatches)
                {
//                    Console.WriteLine(String.Format("Find param {0} as {1}", group.type, paramMatch.Groups["content"].Value));
                    o.objParams.Add(new Param(group.type, paramMatch.Groups["content"].Value));
                }
            }
        }
        /// <summary>
        /// Ajoute un attribut à un élément XML
        /// </summary>
        /// <param name="doc">Document parent</param>
        /// <param name="node">Noeud de l'élément</param>
        /// <param name="name">Nom de l'attribut</param>
        /// <param name="value">Valeur de l'attribut</param>
        static void AppendAttribute(XmlDocument doc, XmlNode node, string name, string value)
        {
            XmlAttribute att = doc.CreateAttribute(name);
            att.Value = value;
            node.Attributes.Append(att);
        }
        /// <summary>
        /// Exporte les objets dans un document XML
        /// </summary>
        /// <param name="fileName">Nom du fichier XML</param>
        /// <param name="title">Titre de la librairie</param>
        /// <param name="version">Version de la librairie</param>
        /// <param name="objets">Objets à exporter</param>
        static void ExportToXML(string fileName, string title, string version, List<Object> objets)
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
        }

        /// <summary>
        /// Lit les objets d'un code source
        /// </summary>
        /// <param name="text">Texte du code à analyser</param>
        /// <param name="filePath">Chemin d'accès relatif au fichier analysé</param>
        /// <param name="syntax">Syntaxe utilisé pour scaner le texte</param>
        /// <param name="groups">Syntaxe des groupes de paramètre utilisés pour scaner le texte</param>
        /// <param name="objets">Liste reçevant les nouveaux objets</param>
        static void MakeObject(string text, string filePath, Syntax syntax, ParamGroup[] groups, List<Object> objets)
        {
            MatchCollection matches = syntax.content.Matches(text);
            foreach (Match match in matches)
            {
                Object o = new Object();
                o.type = syntax.type;
                o.fileName = filePath;
                o.position = match.Index;
                o.id = Guid.NewGuid().ToString("N");
                string objet_text = match.Groups["content"].Value;
//                Console.WriteLine(String.Format("Find in {0}", objet_text));
                Log(String.Format("Begin object {0}", o.type));
                //ajoute les groupes comme parametres
                foreach (string groupName in syntax.content.GetGroupNames())
                {
                    if (groupName != "content" && groupName != "0")
                    {
                        o.objParams.Add(new Param(groupName, match.Groups[groupName].Value));
                        Log(String.Format("\tAdd param '{0}' as '{1}'", groupName, match.Groups[groupName].Value));
                    }
                }
                //groupe de parametres
                foreach (ParamGroup g in groups)
                    ReadGroupParam(objet_text, o, g);
                //parametres
                ReadParam(objet_text, o, syntax.param);

                objets.Add(o);
            }
        }
        static void ReadObjectSyntax(string syntaxFile, ref Syntax syntax)
        {
            using (StreamReader streamReader = new StreamReader(syntaxFile, Encoding.UTF8))
            {
                syntax.type = Path.GetFileNameWithoutExtension(syntaxFile);
                syntax.content = new Regex(streamReader.ReadLine(), RegexOptions.Multiline | RegexOptions.IgnoreCase);
                syntax.param = new Regex(streamReader.ReadLine(), RegexOptions.Multiline | RegexOptions.IgnoreCase);
                streamReader.Close();
            }
        }

        /// <summary>
        /// Ajoute le contenu de la documentation à une librairie existante
        /// </summary>
        /// <param name="options">Options de la ligne de commande</param>
        static void Action_Add(AppArguments options)
        {
            // Scan les groupes
            List<ParamGroup> groups = new List<ParamGroup>();
            string[] groupsPaths = Directory.GetFiles(options.defDir+@"\groups", "*");
            foreach (var syntaxFile in groupsPaths)
            {
                Syntax curGroup = new Syntax();
                ReadObjectSyntax(syntaxFile, ref curGroup);
                groups.Add(new ParamGroup(curGroup.type, curGroup.content, curGroup.param));
            }

            // lit les fichiers sources
            List<Object> objets = new List<Object>();
            string[] srcPaths = Directory.GetFiles(options.inputDir, options.inputFilter, (options.recursive == true ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));
            foreach (var filePath in srcPaths)
            {
                Console.WriteLine(String.Format("Scan file: {0}", filePath));
                Log(String.Format("Scan file: {0}", filePath));
                string relativeFileName = filePath.Substring(options.inputDir.Length);
                string text = string.Empty;
                using (StreamReader streamReader = new StreamReader(filePath, Encoding.UTF8))
                {
                    text = streamReader.ReadToEnd().Replace("\r\n", "\n");
                }

                // Scan les objets
                int objCnt = objets.Count;
                Syntax curSyntax = new Syntax();
                string[] filePaths = Directory.GetFiles(options.defDir, "*");
                foreach (var syntaxFile in filePaths)
                {
                    ReadObjectSyntax(syntaxFile, ref curSyntax);
                    MakeObject(text, relativeFileName, curSyntax, groups.ToArray(), objets);
                }
                objCnt = objets.Count - objCnt;

                Console.WriteLine(String.Format("{0} objets traités", objCnt));
            }
            ExportToXML(options.outputFile, options.title, options.version, objets);
        }

        /// <summary>
        /// Initialise la librairie
        /// </summary>
        /// <param name="options">Options de la ligne de commande</param>
        static void Action_Init(AppArguments options)
        {
            // initialise le document
            XmlDocument doc = new XmlDocument();
            try 
	        {
                doc.Load(options.outputFile);
	        }
	        catch (Exception)
	        {
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(docNode);

                XmlNode rootNode = doc.CreateElement("root");
                doc.AppendChild(rootNode);
	        }

            // supprime le noeud de la librairie existante
            XmlNode libNode = doc.DocumentElement.SelectSingleNode(String.Format("lib[@title='{0}' and @version='{1}']", options.title, options.version));
            if (libNode != null)
                doc.DocumentElement.RemoveChild(libNode);

            // initialise le noeud de la librairie
            libNode = doc.CreateElement("lib");
            doc.DocumentElement.AppendChild(libNode);
            AppendAttribute(doc, libNode, "title", options.title);
            AppendAttribute(doc, libNode, "version", options.version);

            // sauvegarde les modifs
            doc.Save(options.outputFile);
        }
        static void Log(string text)
        {
        }
        /// <summary>
        /// Point d'entrée
        /// </summary>
        /// <param name="args">Arguments de la ligne de commande</param>
        static void Main(string[] args)
        {
            // lit les arguments
            AppArguments options = new AppArguments();
            options.ReadArguments(args);

            if (options.action == null)
            {
                Console.WriteLine("Action non définit");
                return;
            }

            switch (options.action.ToLower())
            {
                case "init":
                    Action_Init(options);
                    break;
                case "add":
                    Action_Add(options);
                    break;
                default:
                    Console.WriteLine("Action inconnue");
                    break;
            }
        }

    }
}

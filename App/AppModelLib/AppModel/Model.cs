/*
 * Implémente les fonctions métier du model de données
 * Ce script est partagé entre les applications utilisant le model AppModel
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AppModel.Entity;
using AppModel.Interface;
using Lib;

namespace AppModel
{
    public class Model : EntitiesModel, IAppModel
    {
        // version interne du model de fichier
        // incrémenté à chaque modification du model
        private Int32 version = 1;

        // Projet en cours
        public Project project;

        public override IEntity CreateEntity(string Name)
        {
            Type t = Type.GetType("AppModel.Entity." + Name + ", AppModel");
            IEntity entity = Activator.CreateInstance(t) as IEntity;
            entity.Model = this;

            return entity;
        }

        public IEntity CreateEntity<T>() where T : IEntity, new()
        {
            IEntity entity = new T();
            entity.Model = this;

            return entity;
        }

        // Modification d'une entité
        public override void Update(IEntity entity)
        {
            // Valide les données
            IEntityValidable v = entity as IEntityValidable;
            if (v != null)
            {
                v.Validate();
            }

            // base
            base.Update(entity);
        }

        // Suppression d'une entité
        public override void Delete(IEntity entity)
        {
            // base
            base.Delete(entity);
        }

        // Création d'une entité
        public override void Create(IEntity entity)
        {
            // Valide les données
            IEntityValidable v = entity as IEntityValidable;
            if (v != null)
            {
                v.Validate();
            }

            // base
            base.Create(entity);
        }

        /*
        public override void RemoveEntity(IEntity entity)
        {
            if (entity is ObjectContent)
            {
                ObjectContent e = entity as ObjectContent;
                project.RemoveObjectContent(e);
            }
            if (entity is ParamContent)
            {
                ParamContent e = entity as ParamContent;
                e.ObjectContent.RemoveParamContent(e);
            }
            if (entity is DatabaseSource)
            {
                DatabaseSource e = entity as DatabaseSource;
                project.RemoveDatabaseSource(e);
            }

            this.Objs.Remove(entity);
        }
        */
        public void AddCppSyntax()
        {
            //
            // Params Syntaxes
            //

            project.AddParamSyntax(new ParamSyntax(
                @"^(?:\s*)(?:description[s]?)?\:\n(?<content>(?!\n{2,})(?:.|\n[^\n])*)",
                @"^(?:[\n\s]*)(?<content>[^\n]+)",
                @"description",
                @"cpp")
            );

            project.AddParamSyntax(new ParamSyntax(
                @"^(?:\s*)(?:exemple|example|sample?)?\:\n(?<content>(?!\n{2,})(?:.|\n[^\n])*)",
                @"(?<content>[^\0]*)",
                @"exemple",
                @"cpp")
            );

            project.AddParamSyntax(new ParamSyntax(
                @"^(?:\s*)(?:param[eè]tre[s]|parameter[s]?)?\:\n(?<content>(?!\n{2,})(?:.|\n[^\n])*)",
                @"^(?:[\n\s]*)(?<content>[^\n]+)",
                @"param",
                @"cpp")
            );

            project.AddParamSyntax(new ParamSyntax(
                @"^(?:\s*)(?:remarque[s]?|remark[s]?)?\:\n(?<content>(?!\n{2,})(?:.|\n[^\n])*)",
                @"^(?:[\n\s]*)(?<content>[^\n]+)",
                @"remark",
                @"cpp")
            );

            project.AddParamSyntax(new ParamSyntax(
                @"^(?:\s*)(?:retourne|return)?\:\n(?<content>(?!\n{2,})(?:.|\n[^\n])*)",
                @"^(?:[\n\s]*)(?<content>[^\n]+)",
                @"return",
                @"cpp")
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
                @"Fonction",
                @"cpp");
            project.AddObjectSyntax(objSyntax);

            // Structure
            objSyntax = new ObjectSyntax(
                @"^(?:\s*\/\*\*)(?:[\n\s]+)(?<description>[^\n]*)[\n]+(?<content>(?:[^*]|\*[^\/])*)(?:\*\/)(?:[\n\s]*)typedef(?:[\n\s]+)struct(?:[\n\s]+)(?<name>[A-Za-z_]+)(?:[\n\s]*)\{",
                @"^(?:\s*)\@(?<type>[A-Za-z]+)(?:\s+)(?<content>[^@])+",
                @"struct",
                @"Structure de données",
                @"cpp");
            project.AddObjectSyntax(objSyntax);
        }

        public void ReadFileSignature(BinaryReader reader)
        {
            if (reader.BaseStream.Length < sizeof(char) * 3 + sizeof(Int32))
                throw new ApplicationException("Invalid file size");
            char[] fileMagic;
            Int32 fileVersion = 0;
            fileMagic = reader.ReadChars(3);
            fileVersion = reader.ReadInt32();

            if (!(fileMagic[0] == 'S' && fileMagic[1] == 'X' && fileMagic[2] == 'I'))
                throw new ApplicationException("Invalid file signature");

            if (fileVersion != this.version)
                throw new ApplicationException("Invalid file version");
        }

        public void WriteFileSignature(BinaryWriter writer)
        {
            writer.Write('S');
            writer.Write('X');
            writer.Write('I');
            writer.Write(this.version);
        }

        public void SaveProject(String Filename)
        {
            FileStream file = File.Open(Filename, FileMode.OpenOrCreate);
            BinaryWriter writer = new BinaryWriter(file);

            // Ecrit la signature
            WriteFileSignature(writer);

            // Sauvegarde le projet
            project.WriteBinary(writer);
            writer.Close();
            file.Close();
        }

        public void LoadProject(String Filename)
        {
            FileStream file = File.Open(Filename, FileMode.Open);
            BinaryReader reader = new BinaryReader(file);

            // Vérifie la signature
            ReadFileSignature(reader);

            // Charge le projet
            project = new Project();
            this.Add(project);
            project.ReadBinary(reader,null);
            reader.Close();
            file.Close();
        }

        public void AddSearch(string groupName,string inputDir, string inputFilter, bool bRecursive)
        {
            project.AddSearchParams(new SearchParams(inputDir, inputFilter, bRecursive, groupName));
        }

        public void AddObjects(SearchParams search)
        {
            // Liste des objets trouvés
            List<ObjectContent> objets = new List<ObjectContent>();

            // Liste les fichiers
            string[] srcPaths = Directory.GetFiles(search.InputDir, search.InputFilter, (search.Recursive == true ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));

            // Scan les fichiers
            foreach (var filePath in srcPaths)
            {
                Console.WriteLine(String.Format("Scan file: {0}", filePath));
                //Log(String.Format("Scan file: {0}", filePath));
                string relativeFileName = filePath.Substring(search.InputDir.Length);
                string text = string.Empty;
                using (StreamReader streamReader = new StreamReader(filePath, Encoding.UTF8))
                {
                    text = streamReader.ReadToEnd().Replace("\r\n", "\n");
                }

                // Scan les objets
                foreach (ObjectSyntax curSyntax in project.ObjectSyntax.Where(p => p.GroupName.ToLower() == search.GroupName.ToLower()).ToList())
                {
                    ScanFile(curSyntax.GroupName, text, relativeFileName, curSyntax, objets);
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
        /// <param name="groupName">Nom du groupe de syntaxe à scanner</param>
        /// <param name="text">Texte du code à analyser</param>
        /// <param name="filePath">Chemin d'accès relatif au fichier analysé</param>
        /// <param name="syntax">Syntaxe utilisé pour scaner le texte</param>
        public void ScanFile(string groupName,string text, string filePath, ObjectSyntax syntax, List<ObjectContent> objList)
        {
            // Convertie en expression reguliere
            Regex content = new Regex(syntax.ContentRegEx, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            Regex param = new Regex(syntax.ParamRegEx, RegexOptions.Multiline | RegexOptions.IgnoreCase);

            MatchCollection matches = content.Matches(text);
            foreach (Match match in matches)
            {
                // Initialise l'objet
                ObjectContent o = new ObjectContent();
                this.project.AddObjectContent(o);
//                this.Add(o);
                o.ObjectType = syntax.ObjectType;
                o.Filename = filePath;
                o.Position = match.Index;
                o.Id = Guid.NewGuid().ToString("N");

                // Extrer les paramètres implicite de l'expression régulière
                foreach (string regexGroupName in content.GetGroupNames())
                {
                    if (regexGroupName != "content" && regexGroupName != "0")
                    {
                        o.AddParamContent(new ParamContent(Guid.NewGuid().ToString("N"), regexGroupName, match.Groups[regexGroupName].Value));
                        //Log(String.Format("\tAdd param '{0}' as '{1}'", groupName, match.Groups[groupName].Value));
                    }
                }

                // Recherche des paramètres dans le contenu de l'objet
                string objet_text = match.Groups["content"].Value;

                // Extrer les groupes de parametres
                foreach (ParamSyntax g in project.ParamSyntax.Where(p => p.GroupName.ToLower() == groupName.ToLower()).ToList())
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
            string groupName = Path.GetFileNameWithoutExtension(path);

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
                            this.Add(syntax);
                            syntax.ObjectType = Path.GetFileNameWithoutExtension(syntaxFile);
                            syntax.ContentRegEx = streamReader.ReadLine();
                            syntax.ParamRegEx = streamReader.ReadLine();
                            syntax.ObjectDesc = String.Empty;
                            syntax.GroupName = groupName;
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
                            this.Add(syntax);
                            syntax.ParamType = Path.GetFileNameWithoutExtension(syntaxFile);
                            syntax.ContentRegEx = streamReader.ReadLine();
                            syntax.ParamRegEx = streamReader.ReadLine();
                            syntax.GroupName = groupName;
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
            project.Factory = factory;
            //Supprime la version existante
            project.Delete();
            //Insert les données
            project.Insert();
            foreach (var e in project.ObjectContent)
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
            project.Factory = factory;
            //Supprime la version existante
            project.Load();
            project.LoadObjectContent();
            //Insert les données
            foreach (var e in project.ObjectContent)
            {
                e.LoadParamContent();
            }
        }


        /// <summary>
        /// Exporte les objets dans un document XML
        /// </summary>
        /// <param name="fileName">Nom du fichier XML</param>
        /// <param name="title">Titre de la librairie</param>
        /// <param name="version">Version de la librairie</param>
        /// <param name="objets">Objets à exporter</param>
        /*public static void ExportToXML(string fileName, string title, string version, List<Object> objets)
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

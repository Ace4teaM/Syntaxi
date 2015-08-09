using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppModel.Entity;

namespace editor
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public Project project;
        public Project Project {
            get { return project; }
            set { 
                project = value;
                if (project != null && MainWindow != null)
                    MainWindow.Title = "Syntaxi - " + Project.Name + " [" + Project.Version + "]";
            }
        }

        public Project MakeCppProject(string title, string version)
        {
            // Initialise un projet
            Project project;
            project = new Project(title, version);

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
    }
}

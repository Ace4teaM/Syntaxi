using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AppModel.Entity;
using EditorModel.Entity;
using Lib;

namespace editor.ModelView
{
    class VueObjectSyntax : ViewModelBase
    {
        App app = Application.Current as App;
        public VueObjectSyntax(Project project)
        {
            this.Project = project;
        }
        //
        private Project project;
        public Project Project { get { return project; } set { project = value; OnPropertyChanged("Project"); } }
        //
        private ObjectSyntax curObjectSyntax;
        public ObjectSyntax CurObjectSyntax {
            get { return curObjectSyntax; }
            set {
                curObjectSyntax = value;
                if (curObjectSyntax == null)
                {
                    CurCodeExample = null;
                }
                else
                {
                    CurCodeExample = app.States.EditorSampleCode.Where(p => p.ObjectSyntaxType == CurObjectSyntax.ObjectType).FirstOrDefault();
                }
                OnPropertyChanged("CurObjectSyntax");
            }
        }
        //
        private EditorSampleCode curCodeExample;
        public EditorSampleCode CurCodeExample { get { return curCodeExample; } set { curCodeExample = value; OnPropertyChanged("CurCodeExample"); } }

    }
}

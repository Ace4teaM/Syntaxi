using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppModel.Entity;
using Lib;

namespace editor.ModelView
{
    class VueObjectSyntax : ViewModelBase
    {
        public VueObjectSyntax(Project project)
        {
            this.Project = project;
        }
        //
        private Project project;
        public Project Project { get { return project; } set { project = value; OnPropertyChanged("Project"); } }
        //
        private ObjectSyntax curObjectSyntax;
        public ObjectSyntax CurObjectSyntax { get { return curObjectSyntax; } set { curObjectSyntax = value; OnPropertyChanged("CurObjectSyntax"); } }
    }
}

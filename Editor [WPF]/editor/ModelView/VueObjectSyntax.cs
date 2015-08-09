using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppModel.Entity;

namespace editor.ModelView
{
    class VueObjectSyntax
    {
        public VueObjectSyntax(Project project)
        {
            this.Project = project;
        }
        Project Project { get; set; }
        ObjectSyntax CurObjectSyntax { get; set; }
    }
}

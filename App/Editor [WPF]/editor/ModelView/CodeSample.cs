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
    class CodeSample : ViewModelBase
    {
        editor.App app = Application.Current as editor.App;
        public CodeSample()
        {
            this.states = app.States;
        }
        public CodeSample(string objectType) : this()
        {
            if (app.States != null)
                CurEditorSampleCode = app.States.EditorSampleCode.Where(p => p.ObjectSyntaxType == objectType).FirstOrDefault();
        }
        //
        private EditorStates states;
        public EditorStates States { get { return states; } set { states = value; OnPropertyChanged("States"); } }
        //
        private EditorSampleCode curEditorSampleCode;
        public EditorSampleCode CurEditorSampleCode { get { return curEditorSampleCode; } set { curEditorSampleCode = value; OnPropertyChanged("CurEditorSampleCode"); } }
    }
}

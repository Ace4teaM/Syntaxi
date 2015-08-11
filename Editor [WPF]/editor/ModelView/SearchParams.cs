using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AppModel.Entity;
using editor.Lib;
using EditorModel.Entity;
using Lib;

namespace editor.ModelView
{
    class SearchParams : ViewModelBase
    {
        App app = Application.Current as App;
        public SearchParams()
        {
        }

        //
        private EditorSampleCode curEditorSampleCode;
        public EditorSampleCode CurEditorSampleCode {
            get { return curEditorSampleCode; }
            set {
                curEditorSampleCode = value;
                OnPropertyChanged("CurEditorSampleCode");
            }
        }
        //
        private ObjectSyntax curObjectSyntax;
        public ObjectSyntax CurObjectSyntax {
            get { return curObjectSyntax; }
            set {
                curObjectSyntax = value;
                if (curObjectSyntax != null)
                    CurEditorSampleCode = app.States.EditorSampleCode.Where(p => p.ObjectSyntaxType == curObjectSyntax.ObjectType).FirstOrDefault();
                OnPropertyChanged("CurObjectSyntax");
            }
        }
        //
        public Collection<ObjectSyntax> ObjectSyntaxList
        {
            get { return app.Project.ObjectSyntax; }
        }
        //
        public Collection<ObjectContent> ObjectContentList
        {
            get { return app.Project.ObjectContent; }
        }

        //-----------------------------------------------------------------------------------------
        // Commandes
        //-----------------------------------------------------------------------------------------
        #region Commands
        #region SaveProject
        private ICommand openInputDir;
        public ICommand OpenInputDir
        {
            get
            {
                if (this.openInputDir == null)
                    this.openInputDir = new DelegateCommand(() =>
                    {
                        app.SaveProject();
                    });

                return this.openInputDir;
            }
        }
        #endregion
        #endregion
    }
}

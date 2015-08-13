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
    class VueEditor : ViewModelBase
    {
        App app = Application.Current as App;
        public VueEditor()
        {
            if (app.Project!=null)
                ObjectContentList = new ObservableCollection<ObjectContent>(app.Project.ObjectContent);
        }

        //
        public string ProjectName
        {
            get { return app.Project.Name; }
        }
        //
        public string ProjectVersion
        {
            get { return app.Project.Version; }
        }
        //
        public string ProjectPath
        {
            get { return app.ProjectFileName; }
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
        private ObservableCollection<ObjectContent> objectContentList;
        public ObservableCollection<ObjectContent> ObjectContentList
        {
            get { return objectContentList; }
            set
            {
                objectContentList = value;
                OnPropertyChanged("ObjectContentList");
            }
        }
        //
        private ObjectContent curObjectContent;
        public ObjectContent CurObjectContent
        {
            get { return curObjectContent; }
            set
            {
                curObjectContent = value;
                if (curObjectContent != null)
                    CurParamContentList = curObjectContent.ParamContent;
                OnPropertyChanged("CurObjectContent");
            }
        }
        //
        private Collection<ParamContent> curParamContentList;
        public Collection<ParamContent> CurParamContentList
        {
            get { return curParamContentList; }
            set
            {
                curParamContentList = value;
                OnPropertyChanged("CurParamContentList");
            }
        }
        //
        public Collection<SearchParams> SearchParamsList
        {
            get { return app.Project.SearchParams; }
        }
        //
        private SearchParams curSearchParams;
        public SearchParams CurSearchParams
        {
            get { return curSearchParams; }
            set
            {
                curSearchParams = value;
                OnPropertyChanged("CurSearchParams");
            }
        }

        //-----------------------------------------------------------------------------------------
        // Commandes
        //-----------------------------------------------------------------------------------------
        #region Commands
        #region SaveProject
        private ICommand saveProject;
        public ICommand SaveProject
        {
            get
            {
                if (this.saveProject == null)
                    this.saveProject = new DelegateCommand(() =>
                    {
                        app.SaveProject();
                    });

                return this.saveProject;
            }
        }
        #endregion
        #region ScanObjects
        private ICommand scanObjects;
        public ICommand ScanObjects
        {
            get
            {
                if (this.scanObjects == null)
                    this.scanObjects = new DelegateCommand(() =>
                    {
                        app.ScanObjects();
                        ObjectContentList = new ObservableCollection<ObjectContent>(app.Project.ObjectContent);
                        CurObjectContent = ObjectContentList.FirstOrDefault();
                    });

                return this.scanObjects;
            }
        }
        #endregion
        #region ExportToDatabase
        private ICommand exportToDatabase;
        public ICommand ExportToDatabase
        {
            get
            {
                if (this.exportToDatabase == null)
                    this.exportToDatabase = new DelegateCommand(() =>
                    {
                        /*SqlServerFactory factory = new SqlServerFactory();
                        factory.SetConnection(@"Server=THOMAS-PC\SQLSERVEREXPRESS;Database=syntaxi;Trusted_Connection=True;");
                        //factory.SetConnection(@"Server=BDE-PORT\SQLSERVER2012;Database=syntaxi;Trusted_Connection=True;");
                        app.Export(sqlFactory);

                        SqlOdbcFactory factory = new SqlOdbcFactory();
                        factory.SetConnection(@"DSN=Syntaxi;");
                        app.Export(factory);*/

                        SqlPostgresFactory factory = new SqlPostgresFactory();
                        factory.SetConnection(@"server=217.70.189.220;Port=5432;Database=syntaxi;User Id=****;Password=****;POOLING=False;");
                        app.Export(factory);
                    });

                return this.exportToDatabase;
            }
        }
        #endregion
        #region ImportFromDatabase
        private ICommand importFromDatabase;
        public ICommand ImportFromDatabase
        {
            get
            {
                if (this.importFromDatabase == null)
                    this.importFromDatabase = new DelegateCommand(() =>
                    {
                        SqlOdbcFactory factory = new SqlOdbcFactory();
                        factory.SetConnection(@"DSN=Syntaxi;");
                        app.Import(factory);
                        ObjectContentList = new ObservableCollection<ObjectContent>(app.Project.ObjectContent);
                        CurObjectContent = ObjectContentList.FirstOrDefault();
                    });

                return this.importFromDatabase;
            }
        }
        #endregion
        /*#region ApplyChanges
        private ICommand applyChanges;
        public ICommand ApplyChanges
        {
            get
            {
                if (this.applyChanges == null)
                    this.applyChanges = new DelegateCommand(() =>
                    {
                        List<ObjectContent> deleted = new List<ObjectContent>();
                        foreach (var e in this.ObjectContentList)
                        {
                            switch (e.EntityState)
                            {
                                case EntityState.Added:
                                    e.Project = app.Project;
                                    foreach (var p in e.ParamContent)
                                    {
                                        switch (e.EntityState)
                                        {
                                            case EntityState.Added:
                                                e.Project = app.Project;
                                                break;
                                            case EntityState.Deleted:
                                                deleted.Add(e);
                                                break;
                                        }
                                    }
                                    break;
                                case EntityState.Deleted:
                                    deleted.Add(e);
                                    break;
                            }
                        }
                    });

                return this.applyChanges;
            }
        }
        #endregion*/
        #endregion
    }
}

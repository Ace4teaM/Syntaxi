using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AppModel.Entity;
using AppModel.Domain;
using EditorModel.Entity;
using Lib;
using System.IO;
using System.Xml;
using Event;

namespace editor.ModelView
{
    class VueEditor : ViewModelBase, IEventProcess
    {
        editor.App app = Application.Current as editor.App;
        public VueEditor()
        {
            InitUI();
        }

        //-----------------------------------------------------------------------------------------
        // Méthodes
        //-----------------------------------------------------------------------------------------
        #region Methods
        public void InitUI()
        {
            if (app.Project == null)
            {
                ObjectSyntaxList = new ObservableCollection<ObjectSyntax>();
                ObjectContentList = new ObservableCollection<ObjectContent>();
                SearchParamsList = new ObservableCollection<SearchParams>();
                DatabaseSourceList = new ObservableCollection<DatabaseSource>();
            }
            else
            {
                ObjectSyntaxList = new ObservableCollection<ObjectSyntax>(app.Project.ObjectSyntax);
                ObjectContentList = new ObservableCollection<ObjectContent>(app.Project.ObjectContent);
                SearchParamsList = new ObservableCollection<SearchParams>(app.Project.SearchParams);
                DatabaseSourceList = new ObservableCollection<DatabaseSource>(app.Project.DatabaseSource);
            }
        }
        #endregion

        //-----------------------------------------------------------------------------------------
        // Propriétés
        //-----------------------------------------------------------------------------------------
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
        public ObservableCollection<ObjectSyntax> objectSyntaxList;
        public ObservableCollection<ObjectSyntax> ObjectSyntaxList
        {
            get { return objectSyntaxList; }
            set
            {
                objectSyntaxList = value;
                OnPropertyChanged("ObjectSyntaxList");
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
                if (curObjectSyntax != null)
                    ParamSyntaxList = new ObservableCollection<ParamSyntax>(app.Project.ParamSyntax.Where(p => p.GroupName.ToLower() == CurObjectSyntax.GroupName.ToLower()).ToArray());
                OnPropertyChanged("CurObjectSyntax");
                OnPropertyChanged("ParamSyntaxList");
            }
        }
        //
        public ObservableCollection<ParamSyntax> paramSyntaxList;
        public ObservableCollection<ParamSyntax> ParamSyntaxList
        {
            get { return paramSyntaxList; }
            set
            {
                paramSyntaxList = value;
                OnPropertyChanged("ParamSyntaxList");
            }
        }
        //
        private ObjectSyntax curParamSyntax;
        public ObjectSyntax CurParamSyntax
        {
            get { return curParamSyntax; }
            set
            {
                curParamSyntax = value;
                OnPropertyChanged("CurParamSyntax");
            }
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
                    CurParamContentList = new ObservableCollection<ParamContent>(curObjectContent.ParamContent);
                OnPropertyChanged("CurObjectContent");
            }
        }
        //
        private ObservableCollection<ParamContent> curParamContentList;
        public ObservableCollection<ParamContent> CurParamContentList
        {
            get { return curParamContentList; }
            set
            {
                curParamContentList = value;
                OnPropertyChanged("CurParamContentList");
            }
        }
        //
        private ObservableCollection<SearchParams> searchParamsList;
        public ObservableCollection<SearchParams> SearchParamsList
        {
            get { return searchParamsList; }
            set
            {
                searchParamsList = value;
                OnPropertyChanged("SearchParamsList");
            }
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

        //
        private ObservableCollection<DatabaseSource> databaseSourceList;
        public ObservableCollection<DatabaseSource> DatabaseSourceList
        {
            get { return databaseSourceList; }
            set
            {
                databaseSourceList = value;
                OnPropertyChanged("DatabaseSourceList");
            }
        }
        //
        private DatabaseSource curDatabaseSource;
        public DatabaseSource CurDatabaseSource
        {
            get { return curDatabaseSource; }
            set
            {
                curDatabaseSource = value;
                OnPropertyChanged("CurDatabaseSource");
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
                        //ObjectContentList = new ObservableCollection<ObjectContent>(app.Project.ObjectContent);
                        OnPropertyChanged("ObjectContentList");
                        CurObjectContent = ObjectContentList.FirstOrDefault();
                    });

                return this.scanObjects;
            }
        }
        #endregion
        #region ImportSyntaxDirectory
        private ICommand importSyntaxDirectory;
        public ICommand ImportSyntaxDirectory
        {
            get
            {
                if (this.importSyntaxDirectory == null)
                    this.importSyntaxDirectory = new DelegateCommand(() =>
                    {
                        WPFFolderBrowser.WPFFolderBrowserDialog dialog = new WPFFolderBrowser.WPFFolderBrowserDialog();
                        dialog.InitialDirectory = app.ProjectFilePath;
                        if (dialog.ShowDialog() == true)
                        {
                            app.appModel.ImportSyntaxDirectory(dialog.FileName);
                            OnPropertyChanged("ObjectSyntaxList");
                        }
                    });

                return this.importSyntaxDirectory;
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
                        // Aucune source de données ?
                        if(app.Project.DatabaseSource.Count == 0)
                        {
                            if(MessageBox.Show("Aucune source de données n'est configurée. Atteindre la page de configuration ?","Configurer une source de données",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            return;
                        }

                        // Aucune source de données par défaut
                        DatabaseSource source = app.Project.DatabaseSource.Where(p => (p.Id == app.States.SelectedDatabaseSourceId)).SingleOrDefault();
                        if (app.States.SelectedDatabaseSourceId == null || source == null)
                        {
                            MessageBox.Show("Veuillez sélectionner une source de données.", "Source de données");
                            // a implementer avec EditWnd...
                            return;
                        }

                        try
                        {
                            // Initialise la factory
                            IEntityFactory factory;
                            switch (source.Provider)
                            {
                                case DatabaseProvider.Odbc:
                                    factory = new SqlOdbcFactory(app.appModel);
                                    ((SqlOdbcFactory)factory).SetConnection(source.ConnectionString);
                                    break;
                                case DatabaseProvider.PostgreSQL:
                                    factory = new SqlPostgresFactory(app.appModel);
                                    ((SqlPostgresFactory)factory).SetConnection(source.ConnectionString);
                                    break;
                                case DatabaseProvider.SqlServer:
                                    factory = new SqlServerFactory(app.appModel);
                                    ((SqlServerFactory)factory).SetConnection(source.ConnectionString);
                                    break;
                                default:
                                    MessageBox.Show("La source de données ne correspond pas à une source valide.", "Source de données");
                                    return;
                            }

                            app.appModel.Export(factory);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
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
                        try
                        {
                            SqlOdbcFactory factory = new SqlOdbcFactory(app.appModel);
                            factory.SetConnection(@"DSN=Syntaxi;");
                            app.appModel.Import(factory);
                            //ObjectContentList = new ObservableCollection<ObjectContent>(app.Project.ObjectContent);
                            OnPropertyChanged("ObjectContentList");
                            CurObjectContent = ObjectContentList.FirstOrDefault();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    });

                return this.importFromDatabase;
            }
        }
        #endregion
        #endregion

        //-----------------------------------------------------------------------------------------
        // Evénements
        //-----------------------------------------------------------------------------------------
        #region IEventProcess
        // Traite les événements
        public void ProcessEvent(object from, object _this, IEvent e)
        {
            //
            // Change
            // Action après le changement d'une entité
            //
            if (e is EntityChangeEvent)
            {
                EntityChangeEvent ev = e as EntityChangeEvent;
                // Concerne ce model ?
                if (ev.Model == app.appModel)
                {
                    if (ev.BaseEvent is EntityCreateEvent)
                    {
                        if (ev.Entity is ObjectContent)
                        {
                            if (ObjectContentList.Contains(ev.Entity) == false)
                                ObjectContentList.Add(ev.Entity as ObjectContent);
                        }
                        if (ev.Entity is ParamContent)
                        {
                            if (ObjectContentList.Contains(ev.Entity) == false)
                                ObjectContentList.Add(ev.Entity as ObjectContent);
                            CurParamContentList = new ObservableCollection<ParamContent>(curObjectContent.ParamContent);
                        }
                        if (ev.Entity is DatabaseSource)
                        {
                            OnPropertyChanged("CurDatabaseSource");
                        }
                    }
                    if (ev.BaseEvent is EntityDeleteEvent)
                    {
                        if (ev.Entity is ObjectContent)
                        {
                            OnPropertyChanged("ObjectContentList");
                        }
                        if (ev.Entity is ParamContent)
                        {
                            CurParamContentList = new ObservableCollection<ParamContent>(curObjectContent.ParamContent);
                        }
                        if (ev.Entity is DatabaseSource)
                        {
                            OnPropertyChanged("CurDatabaseSource");
                        }
                    }
                }
            }

            //
            // Pré-Create
            // Préprare la création d'une nouvelle entité (avant édition des champs)
            //
            if (e is EntityPreCreateEvent)
            {
                EntityPreCreateEvent ev = e as EntityPreCreateEvent;

                // Alloue l'entité
                if (ev.Entity == null)
                {
                    ev.Entity = app.appModel.CreateEntity(ev.EntityName);
                }

                // Ajoute l'instance au model
                app.appModel.Add(ev.Entity);

                // Affecte le status de création
                ev.Entity.EntityState = EntityState.Added;

                // Initialise les données par défaut
                if (ev.Entity is IEntityPersistent)
                {
                    IEntityPersistent p = ev.Entity as IEntityPersistent;
                    //Affecte la source de données à la nouvelle entitée
                    p.Factory = app.appModel.project.Factory;
                }

                if (ev.Entity is ObjectContent)
                {
                    ObjectContent entity = ev.Entity as ObjectContent;
                    // Ajoute au projet en cours
                    app.Project.AddObjectContent(entity);
                    // Génére un identifiant unique
                    entity.Id = Guid.NewGuid().ToString("N");
                    foreach (var p in entity.ParamContent)
                        p.Id = Guid.NewGuid().ToString("N");
                }

                if (ev.Entity is ParamContent)
                {
                    ParamContent entity = ev.Entity as ParamContent;
                    CurObjectContent.AddParamContent(entity);
                    // Génére un identifiant unique
                    entity.Id = Guid.NewGuid().ToString("N");
                }

                if (ev.Entity is DatabaseSource)
                {
                    DatabaseSource entity = ev.Entity as DatabaseSource;
                    app.Project.AddDatabaseSource(entity);

                    if (app.States.SelectedDatabaseSourceId == null)
                        app.States.SelectedDatabaseSourceId = app.Project.DatabaseSource.First().Id;
                }

                // Actualise l'interface
                app.ProcessEvent(this, this, new EntityChangeEvent(e, ev.Entity, app.appModel));
            }

            //
            // Create
            // Crée la nouvelle entité (après édition des champs)
            //
            if (e is EntityCreateEvent)
            {
                EntityCreateEvent ev = e as EntityCreateEvent;
                if (ev.Entity != null)
                {
                    // Valide le status de création
                    ev.Entity.EntityState = EntityState.Unmodified;

                    // Actualise l'interface
                    app.ProcessEvent(this, this, new EntityChangeEvent(e, ev.Entity, app.appModel));
                }
            }

            //
            // Pré-Update
            // Les données de l'entité vont êtres modifiées
            //
            if (e is EntityPreUpdateEvent)
            {
                EntityPreUpdateEvent ev = e as EntityPreUpdateEvent;
                // si l'entité fait partie du model
                if (ev.Entity != null && app.appModel.Contains(ev.Entity))
                {
                    // Préviens l'application
                    app.ProcessEvent(this, this, new EntityChangeEvent(e, ev.Entity, app.appModel));
                }
            }

            //
            // Update
            // Les données de l'entité sont modifiées
            //
            if (e is EntityUpdateEvent)
            {
                EntityUpdateEvent ev = e as EntityUpdateEvent;
                // si l'entité fait partie du model
                if (ev.Entity != null && app.appModel.Contains(ev.Entity))
                {
                    // Test la validité des champs
                    if (ev.Entity is IEntityValidable)
                    {
                        string error;
                        IEntityValidable v = ev.Entity as IEntityValidable;
                        if (!v.IsValid(out error))
                        {
                            app.ProcessException(new ApplicationException(error));
                            return;
                        }
                    }

                    // Valide le status de création
                    ev.Entity.EntityState = EntityState.Unmodified;

                    // Actualise l'interface
                    app.ProcessEvent(this, this, new EntityChangeEvent(e, ev.Entity, app.appModel));
                }
            }

            //
            // Delete
            // Supprime l'entité 
            //
            if (e is EntityDeleteEvent)
            {
                EntityDeleteEvent ev = e as EntityDeleteEvent;
                // si l'entité fait partie du model
                if (ev.Entity != null && app.appModel.Contains(ev.Entity))
                {
                    // Supprime du model
                    app.appModel.Remove(ev.Entity);

                    // Actualise l'interface
                    app.ProcessEvent(this, this, new EntityChangeEvent(e, ev.Entity, app.appModel));
                }
            }

            // Implémente la gestion du copier coller
            EventProcess.ProcessCopyPasteEvents(app, this, app.appModel, from, _this, e);
        }

        #endregion
    }
}

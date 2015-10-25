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
        public VueEditor()
        {
            // Abonne le controleur aux changements d'états
            app.appModel.NotifyRegister(this);

            // Prepare les données de l'interface
            UpdateUI();
        }

        ~VueEditor()
        {
            // Abonne le controleur aux changements d'états
            app.appModel.NotifyUnRegister(this);
        }

        //-----------------------------------------------------------------------------------------
        // Méthodes
        //-----------------------------------------------------------------------------------------
        #region Methods

        // Initialise les collections pour le Binding
        public void UpdateUI()
        {
            // Initialise les collections pour le Binding
            objectSyntaxList = new ObservableCollection<ObjectSyntax>(app.Project.ObjectSyntax);
            objectContentList = new ObservableCollection<ObjectContent>(app.Project.ObjectContent);
            searchParamsList = new ObservableCollection<SearchParams>(app.Project.SearchParams);
            databaseSourceList = new ObservableCollection<DatabaseSource>(app.Project.DatabaseSource);
            OnPropertyChanged("ObjectSyntaxList");
            OnPropertyChanged("ObjectContentList");
            OnPropertyChanged("SearchParamsList");
            OnPropertyChanged("DatabaseSourceList");
        }

        // Modification d'une entité
        // (Lors d'un événement 'EntityChangeEvent')
        public void UpdateEntityList<T>(IEvent baseEvent, ObservableCollection<T> list, T e)
        {
            if (baseEvent is EntityDeleteEvent && list.Contains(e) == true)
                list.Remove(e);

            if (baseEvent is EntityCreateEvent && list.Contains(e) == false)
                list.Add(e);
        }

        public void OnEntityChange(IModel model, IEntity entity, IEvent baseEvent)
        {
            // Actualise les collections
            if (entity is ObjectSyntax)
                UpdateEntityList<ObjectSyntax>(baseEvent, ObjectSyntaxList, entity as ObjectSyntax);
            if (entity is ParamSyntax)
                UpdateEntityList<ParamSyntax>(baseEvent, ParamSyntaxList, entity as ParamSyntax);
            if (entity is ObjectContent)
                UpdateEntityList<ObjectContent>(baseEvent, ObjectContentList, entity as ObjectContent);
            if (entity is SearchParams)
                UpdateEntityList<SearchParams>(baseEvent, SearchParamsList, entity as SearchParams);
            if (entity is DatabaseSource)
                UpdateEntityList<DatabaseSource>(baseEvent, DatabaseSourceList, entity as DatabaseSource);
            if (entity is ParamContent && (entity as ParamContent).ObjectContent == CurObjectContent)
                UpdateEntityList<ParamContent>(baseEvent, CurParamContentList, entity as ParamContent);
        }
        #endregion

        //-----------------------------------------------------------------------------------------
        // Données
        //-----------------------------------------------------------------------------------------
        #region Data
        private editor.App app = Application.Current as editor.App;
        //UI 
        private EditorSampleCode curEditorSampleCode;
        private ObjectSyntax curObjectSyntax;
        private ObservableCollection<ParamSyntax> paramSyntaxList;
        private ObjectSyntax curParamSyntax;
        private ObjectContent curObjectContent;
        private ObservableCollection<ParamContent> curParamContentList;
        private DatabaseSource curDatabaseSource;
        private ObservableCollection<ObjectSyntax> objectSyntaxList;
        private ObservableCollection<ObjectContent> objectContentList;
        private ObservableCollection<SearchParams> searchParamsList;
        private ObservableCollection<DatabaseSource> databaseSourceList;
        #endregion

        //-----------------------------------------------------------------------------------------
        // Propriétés
        //-----------------------------------------------------------------------------------------
        #region Properties
        // Nom du projet
        public string ProjectName
        {
            get { return app.Project.Name; }
        }

        // Version du projet
        public string ProjectVersion
        {
            get { return app.Project.Version; }
        }

        // Emplacement du projet
        public string ProjectPath
        {
            get { return app.ProjectFileName; }
        }

        // Code d'exemple actuel
        public EditorSampleCode CurEditorSampleCode {
            get { return curEditorSampleCode; }
            set {
                curEditorSampleCode = value;
                OnPropertyChanged("CurEditorSampleCode");
            }
        }

        // Liste des syntaxes d'objets
        public ObservableCollection<ObjectSyntax> ObjectSyntaxList
        {
            get { return objectSyntaxList; }
            set {
                objectSyntaxList = value;
                OnPropertyChanged("ObjectSyntaxList");
            }
        }

        // Syntaxe d'objet sélectionnée
        public ObjectSyntax CurObjectSyntax {
            get { return curObjectSyntax; }
            set {
                curObjectSyntax = value;
                if (curObjectSyntax != null)
                {
                    // Sélectionne le code d'exemple associé
                    CurEditorSampleCode = app.States.EditorSampleCode.Where(p => p.ObjectSyntaxType == curObjectSyntax.ObjectType).FirstOrDefault();
                    // Sélectionne les paramètres du même groupe
                    ParamSyntaxList = new ObservableCollection<ParamSyntax>(app.Project.ParamSyntax.Where(p => p.GroupName.ToLower() == CurObjectSyntax.GroupName.ToLower()).ToArray());
                }
                else
                {
                    CurEditorSampleCode = null;
                    ParamSyntaxList = null;
                }
                OnPropertyChanged("CurObjectSyntax");
            }
        }

        // Liste des syntaxes de paramètres
        public ObservableCollection<ParamSyntax> ParamSyntaxList
        {
            get { return paramSyntaxList; }
            set
            {
                paramSyntaxList = value;
                OnPropertyChanged("ParamSyntaxList");
            }
        }

        // Syntaxe de paramètre sélectionnée
        public ObjectSyntax CurParamSyntax
        {
            get { return curParamSyntax; }
            set
            {
                curParamSyntax = value;
                OnPropertyChanged("CurParamSyntax");
            }
        }

        // Liste des contenus d'objets
        public ObservableCollection<ObjectContent> ObjectContentList
        {
            get { return objectContentList; }
            set
            {
                objectContentList = value;
                OnPropertyChanged("ObjectContentList");
            }
        }

        // Contenu d'objet sélectionné
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

        // Liste des contenus de paramètres de l'objet sélectionné
        public ObservableCollection<ParamContent> CurParamContentList
        {
            get { return curParamContentList; }
            set
            {
                curParamContentList = value;
                OnPropertyChanged("CurParamContentList");
            }
        }

        // Liste des objets de recherche 
        public ObservableCollection<SearchParams> SearchParamsList
        {
            get { return searchParamsList; }
            set
            {
                searchParamsList = value;
                OnPropertyChanged("SearchParamsList");
            }
        }

        // Objet de recherche sélectionnée
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

        // Liste des sources de données
        public ObservableCollection<DatabaseSource> DatabaseSourceList
        {
            get { return databaseSourceList; }
            set
            {
                databaseSourceList = value;
                OnPropertyChanged("DatabaseSourceList");
            }
        }

        // Source de données sélectionnée
        public DatabaseSource CurDatabaseSource
        {
            get { return curDatabaseSource; }
            set
            {
                curDatabaseSource = value;
                OnPropertyChanged("CurDatabaseSource");
            }
        }
        #endregion

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
                        //UpdateUI();
                        //ObjectContentList = new ObservableCollection<ObjectContent>(app.Project.ObjectContent);
                        //OnPropertyChanged("ObjectContentList");
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
                        if(app.Project.DatabaseSource.Count() == 0)
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
            // Model Change
            // Après le changement du model
            //
            if (e is ModelChangeEvent)
            {
                ModelChangeEvent ev = e as ModelChangeEvent;
                //Actualise l'interface
                UpdateUI();
                // Notifie les vues
                this.NotifyEvent(e);
            }

            //
            // Entity Change
            // Après le changement d'une entité
            //
            if (e is EntityChangeEvent)
            {
                EntityChangeEvent ev = e as EntityChangeEvent;
                //Actualise l'interface
                OnEntityChange(ev.Model, ev.Entity, ev.BaseEvent);
                // Notifie les vues
                this.NotifyEvent(e);
            }

            //
            // Pré-Create
            // Préprare la création d'une nouvelle entité (avant édition des champs)
            //
            if (e is EntityPreCreateEvent)
            {
                EntityPreCreateEvent ev = e as EntityPreCreateEvent;

                // Allocation
                if (ev.Entity == null)
                {
                    ev.Entity = app.appModel.CreateEntity(ev.EntityName);
                }

                // Affecte le status de création
                ev.Entity.EntityState = EntityState.Added;

                // Initialise les données par défaut
                if (ev.Entity is IEntityPersistent)
                {
                    IEntityPersistent p = ev.Entity as IEntityPersistent;
                    //Affecte la source de données à la nouvelle entitée
                    p.Factory = app.appModel.project.Factory;
                }

                if (ev.Entity is ObjectSyntax)
                {
                    ObjectSyntax entity = ev.Entity as ObjectSyntax;
                    // Ajoute au projet en cours
                    app.Project.AddObjectSyntax(entity);
                }


                if (ev.Entity is ParamSyntax)
                {
                    ParamSyntax entity = ev.Entity as ParamSyntax;
                    // Ajoute au projet en cours
                    app.Project.AddParamSyntax(entity);
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

                if (ev.Entity is SearchParams)
                {
                    SearchParams entity = ev.Entity as SearchParams;
                    app.Project.AddSearchParams(entity);
                }

                if (ev.Entity is DatabaseSource)
                {
                    DatabaseSource entity = ev.Entity as DatabaseSource;
                    app.Project.AddDatabaseSource(entity);

                    if (app.States.SelectedDatabaseSourceId == null)
                        app.States.SelectedDatabaseSourceId = app.Project.DatabaseSource.First().Id;
                }

                // Ajoute l'instance au model (notifie le controleur)
                app.appModel.Add(ev.Entity);
            }

            // Implémente la gestion du copier coller
            EventProcess.ProcessCopyPasteEvents(app, this, app.appModel, from, _this, e);

            /*

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
            }*/
        }

        #endregion
    }
}

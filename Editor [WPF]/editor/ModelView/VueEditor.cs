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

namespace editor.ModelView
{
    class VueEditor : ViewModelBase
    {
        App app = Application.Current as App;
        public VueEditor()
        {
            ListObjectContent();
        }

        public void ListObjectContent()
        {
            if (app.Project != null)
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

        //
        public Collection<DatabaseSource> DatabaseSourceList
        {
            get { return app.Project.DatabaseSource; }
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
                        ObjectContentList = new ObservableCollection<ObjectContent>(app.Project.ObjectContent);
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
                            app.ImportSyntaxDirectory(dialog.FileName);
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

                        // Initialise la factory
                        IEntityFactory factory;
                        switch (source.Provider)
                        {
                            case DatabaseProvider.Odbc:
                                factory = new SqlOdbcFactory();
                                ((SqlOdbcFactory)factory).SetConnection(source.ConnectionString);
                                break;
                            case DatabaseProvider.PostgreSQL:
                                factory = new SqlPostgresFactory();
                                ((SqlPostgresFactory)factory).SetConnection(source.ConnectionString);
                                break;
                            case DatabaseProvider.SqlServer:
                                factory = new SqlServerFactory();
                                ((SqlServerFactory)factory).SetConnection(source.ConnectionString);
                                break;
                            default:
                                MessageBox.Show("La source de données ne correspond pas à une source valide.", "Source de données");
                                return;
                        }

                        app.Export(factory);

                        /*SqlServerFactory factory = new SqlServerFactory();
                        factory.SetConnection(@"Server=THOMAS-PC\SQLSERVEREXPRESS;Database=syntaxi;Trusted_Connection=True;");
                        //factory.SetConnection(@"Server=BDE-PORT\SQLSERVER2012;Database=syntaxi;Trusted_Connection=True;");
                        app.Export(sqlFactory);

                        SqlOdbcFactory factory = new SqlOdbcFactory();
                        factory.SetConnection(@"DSN=Syntaxi;");
                        app.Export(factory);

                        SqlPostgresFactory factory = new SqlPostgresFactory();
                        factory.SetConnection(@"server=217.70.189.220;Port=5432;Database=syntaxi;User Id=****;Password=****;POOLING=False;");
                        */
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
        #region EntityChange
        private ICommand entityChange;
        public ICommand EntityChange
        {
            get
            {
                if (this.entityChange == null)
                    this.entityChange = new DelegateCommand(() =>
                    {
                        IEntityPersistent entity = ((DelegateCommand)this.entityChange).GetParam() as IEntityPersistent;
                        if (entity == null)
                            return;

                        if (entity.EntityState == EntityState.Added)
                        {
                            if (entity is ObjectContent)
                            {
                                ObjectContent e = entity as ObjectContent;
                                // Ajoute au projet en cours
                                app.Project.AddObjectContent(e);
                                // Regénére des identifiants uniques pour la base de données
                                e.Id = Guid.NewGuid().ToString("N");
                                foreach(var p in e.ParamContent)
                                    p.Id = Guid.NewGuid().ToString("N");
                                // Ajoute à l'interface
                                if (this.ObjectContentList.Contains(e) == false)
                                    this.ObjectContentList.Add(e);
                            }
                            if (entity is ParamContent)
                            {
                                ParamContent e = entity as ParamContent;
                                CurObjectContent.AddParamContent(e);
                                e.Id = Guid.NewGuid().ToString("N");
                            }
                            if (entity is DatabaseSource)
                            {
                                DatabaseSource e = entity as DatabaseSource;
                                app.Project.AddDatabaseSource(e);

                                if (app.States.SelectedDatabaseSourceId == null)
                                    app.States.SelectedDatabaseSourceId = app.Project.DatabaseSource.First().Id;
                            }
                        }

                        if (entity.EntityState == EntityState.Deleted)
                        {
                            if (entity is ObjectContent)
                            {
                                ObjectContent e = entity as ObjectContent;
                                app.Project.RemoveObjectContent(e);
                            }
                            if (entity is ParamContent)
                            {
                                ParamContent e = entity as ParamContent;
                                CurObjectContent.RemoveParamContent(e);
                            }
                            if (entity is DatabaseSource)
                            {
                                DatabaseSource e = entity as DatabaseSource;
                                app.Project.RemoveDatabaseSource(e);
                            }
                        }

                        // accuse de la modification
                        entity.EntityState = EntityState.Unmodified;
                    });

                return this.entityChange;
            }
        }
        #endregion
        #region EntityCopy
        private ICommand entityCopy;
        public ICommand EntityCopy
        {
            get
            {
                if (this.entityCopy == null)
                    this.entityCopy = new DelegateCommand(() =>
                    {
                        IEntity entity = ((DelegateCommand)this.entityCopy).GetParam() as IEntity;
                        if (entity is IEntitySerializable)
                        {
                            IEntitySerializable serial = entity as IEntitySerializable;
                            /*using (MemoryStream memStream = new MemoryStream())
                            {
                                BinaryWriter writer = new BinaryWriter(memStream);
                                // Serialise le nom de la classe
                                writer.Write(serial.GetType().Name);
                                // Serialise les données
                                serial.WriteBinary(writer);
                                Clipboard.SetData("Entity", memStream.ToArray());
                            }
                            */
                            string xml = serial.ToXml(null);
                            Clipboard.SetText(xml);
                        }
                    });

                return this.entityCopy;
            }
        }
        #endregion
        #region EntityPaste
        private ICommand entityPaste;
        public ICommand EntityPaste
        {
            get
            {
                if (this.entityPaste == null)
                    this.entityPaste = new DelegateCommand(() =>
                    {
                        if (Clipboard.ContainsText())
                        {
                            // initialise le document XML
                            XmlDocument doc = new XmlDocument();
                            string xml = Clipboard.GetText();//Clipboard.GetData("text/xml") as string
                            if (xml == null)
                                return;
                            doc.LoadXml(xml);

                            //obtient le premier élément
                            XmlNode n = doc.DocumentElement.FirstChild;
                            while (n != null)
                            {
                                if(n.NodeType != XmlNodeType.Element)
                                {
                                    n = n.NextSibling;
                                    continue;
                                }

                                XmlElement e = n as XmlElement;
                                IEntity insertEntity = null;
                                if (e.LocalName == "Project")
                                {
                                    Project p = new Project();
                                    p.EntityState = EntityState.Added;
                                    p.FromXml(e);
                                    insertEntity = p;
                                    Console.WriteLine("import Project");
                                }
                                else if (e.LocalName == "ObjectContent")
                                {
                                    ObjectContent p = new ObjectContent();
                                    p.EntityState = EntityState.Added;
                                    p.FromXml(e);
                                    insertEntity = p;
                                    Console.WriteLine("import ObjectContent");
                                }
                                else
                                {
                                    Console.WriteLine("unknown element " + e.LocalName);
                                }

                                // Notify l'insertion de la nouvelle entité
                                if (EntityChange.CanExecute(insertEntity))
                                    EntityChange.Execute(insertEntity);

                                //suivant
                                n = n.NextSibling;
                            }

                        }
                        /*
                        if (Clipboard.ContainsData("Entity"))
                        {
                            byte[] data = Clipboard.GetData("Entity") as byte[];
                            if (data == null)
                                return;
                            using (MemoryStream memStream = new MemoryStream(data))
                            {
                                BinaryReader reader = new BinaryReader(memStream);
                                // Déserialise le nom de la classe
                                string type = reader.ReadString();
                                Console.WriteLine(type);
                                // Déserialise les données
                                // serial.ReadBinary(reader);
                            }
                        }*/
                    });

                return this.entityPaste;
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

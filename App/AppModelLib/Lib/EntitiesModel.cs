using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Event;

namespace Lib
{
    public class EntitiesModel : EventManager, IModel
    {
        protected IDictionary<int, IEntity> entities;//liste des objets et des clés correspondantes
        protected List<IEntityAssociation> associations;//liste des objets et des clés correspondantes
        protected int curKey = 0;// clé en cours

        public ICollection<IEntity> Objs { get { return entities.Values; } }
        public ICollection<IEntityAssociation> Assoc { get { return associations; } }

        public EntitiesModel()
        {
            entities = new Dictionary<int, IEntity>();
            associations = new List<IEntityAssociation>();
        }

        ///
        /// Logique métier
        ///

        public virtual void Update(IEntity entity)
        {
		    entity.EntityState = EntityState.Unmodified;
		    this.NotifyEvent(new EntityChangeEvent(new EntityUpdateEvent(entity),entity,this));
	    }

        public virtual void Delete(IEntity entity)
        {
            entity.EntityState = EntityState.Deleted;

            // Supprime du model
            this.Remove(entity);

            this.NotifyEvent(new EntityChangeEvent(new EntityDeleteEvent(entity),entity,this));
        }

        public virtual void Create(IEntity entity)
        {
            entity.EntityState = EntityState.Unmodified;

            // Ajoute au model
            this.Add(entity);

            this.NotifyEvent(new EntityChangeEvent(new EntityCreateEvent(entity),entity,this));
        }

        ///
        /// Références
        ///

        public virtual IEntity CreateEntity(string entityName)
        {
            throw new NotImplementedException();
        }

        public IEntity Add(IEntity entity)
        {
            if (entities.Values.Contains(entity) == true)
                return entity;
            entities.Add(curKey++, entity);
            entity.Model=this;
            return entity;
        }

        public IEntity Add(IEntity entity, int key)
        {
            if (entities.Values.Contains(entity) == true)
                return entity;
            entities.Add(key, entity);
            entity.Model=this;
            return entity;
        }

        public void Remove(int key)
        {
            IEntity entity = GetObjectByKey(key);
            if (entity != null)
            {
                entities.Remove(key);
                entity.Model = null;
                //this.NotifyEvent(new EntityChangeEvent(new EntityRemoveEvent(entity), entity, this));
            }

            // Supprime les associations
            List<IEntityAssociation> tmp = this.Assoc.Where(p => p.A == entity || p.B == entity).ToList();
            foreach (var a in tmp)
            {
                this.Assoc.Remove(a);
            }
        }

        public void Remove(IEntity entity)
        {
            int key = GetObjectKey(entity);
            if (key >= 0)
            {
                entities.Remove(key);
                entity.Model = null;
                //this.NotifyEvent(new EntityChangeEvent(new EntityRemoveEvent(entity), entity, this));
            }

            // Supprime les associations
            List<IEntityAssociation> tmp = this.Assoc.Where(p => p.A == entity || p.B == entity).ToList();
            foreach (var a in tmp)
            {
                this.Assoc.Remove(a);
            }
        }

        public IEntity GetReference(IEntity entity)
        {
            // Recherche par instance
            if (entities.Values.Contains(entity))
                return entity;

            // Recherche par identifiant
            if (entity is IEntityPersistent)
            {
                IEntityPersistent persEntity = entity as IEntityPersistent;
                foreach (IEntityPersistent eref in entities.Values.OfType<IEntityPersistent>())
                {
                    if (eref.CompareIdentifier(persEntity) == true)
                    {
                        return eref;
                    }
                }
            }

            // Ajoute l'objet à la liste
            return Add(entity);
        }


        public int GetObjectKey(IEntity entity)
        {
            try
            {
                return entities.Where(p => p.Value == entity).First().Key;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public IEntity GetObjectByKey(int key)
        {
            try
            {
                return entities.Where(p => p.Key == key).First().Value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IModel Clone()
        {
            EntitiesModel m = new EntitiesModel();
            //clone les objets mais conserve les identifiants
            foreach (IEntity e in this.entities.Values)
            {
                m.Add(e.Clone(), GetObjectKey(e));
            }
            return m;
        }

        public bool Contains(IEntity entity)
        {
            return this.entities.Values.Contains(entity);
        }

    }
}

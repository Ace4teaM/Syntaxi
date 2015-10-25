using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class EntitiesModel : IModel
    {
        protected IDictionary<int, IEntity> entities;//liste des objets et des clés correspondantes
        protected int curKey = 0;// clé en cours

        public ICollection<IEntity> Objs { get { return entities.Values; } }

        public EntitiesModel()
        {
            entities = new Dictionary<int, IEntity>();
        }

        public virtual IEntity CreateEntity(string entityName)
        {
            throw new NotImplementedException();
        }

        public IEntity Add(IEntity entity)
        {
            entities.Add(curKey++, entity);
            return entity;
        }

        public IEntity Add(IEntity entity, int key)
        {
            entities.Add(key, entity);
            return entity;
        }

        public void Remove(int key)
        {
            entities.Remove(key);
        }

        public void Remove(IEntity entity)
        {
            int key = GetObjectKey(entity);
            if (key >= 0)
                entities.Remove(key);
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

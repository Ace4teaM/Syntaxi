using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib;

namespace Lib
{
    public class EntityReferences<T> where T : IEntityPersistent
    {
        //cache des entités
        private List<T> references = new List<T>();
        public List<T> References { get { return references; } }

        // private List<EntityState> changes = new List<EntityState>();//modifs des entités
        //  public List<EntityState> Changes { get { return changes; } }//modifs des entités

        private Dictionary<IEntityPersistent, EntityState> changes = new Dictionary<IEntityPersistent, EntityState>();
        public Dictionary<IEntityPersistent, EntityState> Changes { get { return changes; } }//modifs des entités

        // Obtient l'état d'une entité
        public EntityState GetState(IEntityPersistent entity)
        {
            if (this.Changes.ContainsKey(entity))
                return this.Changes[entity];

            return EntityState.Unmodified;
        }

        // Modifie l'état d'une entité
        public void SetState(IEntityPersistent entity, EntityState state)
        {
            if (state == EntityState.Unmodified)
            {
                if (this.Changes.ContainsKey(entity))
                    this.Changes.Remove(entity);
                return;
            }

            if (!this.Changes.ContainsKey(entity))
                this.Changes.Add(entity, state);
            else
                this.Changes[entity] = state;
        }

        // recherche une entité dans les references
        public T GetReference(T e)
        {
            if (references.Contains(e))
                return e;
            foreach (T eref in references.OfType<T>())
            {
                if (eref.CompareIdentifier(e) == true)
                {
                    //eref.CopyFrom(e);
                    return eref;
                }
            }
            references.Add(e);
            Changes.Add(e, EntityState.Added);
            return e;
        }

        /* recherche une entité dans les references*/
        public E GetReference<E>(E e) where E : T
        {
            if (references.Contains(e))
                return e;
            foreach (E eref in references.OfType<E>())
            {
                if (eref.CompareIdentifier(e) == true)
                {
                    //eref.CopyFrom(e);
                    return eref;
                }
            }
            references.Add(e);
            return e;
        }
    }

}

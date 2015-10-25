using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lib;

namespace Event
{
    /// <summary>
    /// Evénement transmit par le model après la suppression d'une entité
    /// </summary>
    public class EntityDeleteEvent : IEvent
    {
        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public EntityDeleteEvent(IEntity entity)
        {
            this.Entity = entity;
            this.PrevState = entity.EntityState;
        }

        // entité concernée
        public IEntity Entity;
        public EntityState PrevState;
    }
}

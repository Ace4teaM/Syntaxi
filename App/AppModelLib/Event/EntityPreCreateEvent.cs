using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lib;

namespace Event
{
    /// <summary>
    /// Evénement transmit au model-vue lors de la création d'une entité.
    /// Implémente la logique d'interaction de la vue (associations, ...)
    /// </summary>
    public class EntityPreCreateEvent : IEvent
    {
        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public EntityPreCreateEvent(IEntity entity)
        {
            this.Entity = entity;
            this.EntityName = entity.EntityName;
        }

        public EntityPreCreateEvent(string entityName)
        {
            this.Entity = null;
            this.EntityName = entityName;
        }

        public IEntity Entity;
        public string EntityName;
    }
}

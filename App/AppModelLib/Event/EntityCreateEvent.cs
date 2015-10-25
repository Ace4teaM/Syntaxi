using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lib;

namespace Event
{
    /// <summary>
    /// Evénement transmit par le model après la création d'une entité
    /// </summary>
    public class EntityCreateEvent : IEvent
    {
        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public EntityCreateEvent(IEntity entity)
        {
            this.Entity = entity;
        }

        public IEntity Entity;
    }
}

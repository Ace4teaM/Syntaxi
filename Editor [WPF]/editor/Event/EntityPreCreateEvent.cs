using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib;

namespace Event
{
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
        }

        public IEntity Entity;
    }
}

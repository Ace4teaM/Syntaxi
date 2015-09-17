using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib;

namespace Event
{
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
        }

        // entité concernée
        public IEntity Entity;
    }
}

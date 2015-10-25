using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib;

namespace Event
{
    /// <summary>
    /// Evénement transmit par le model après la modification d'une entité
    /// </summary>
    public class EntityUpdateEvent : IEvent
    {
        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public EntityUpdateEvent(IEntity entity)
        {
            this.Entity = entity;
        }

        // Référence de l'entité modifiée
        public IEntity Entity;
    }
}

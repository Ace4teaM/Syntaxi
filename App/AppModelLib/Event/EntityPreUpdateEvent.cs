using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lib;

namespace Event
{
    /// <summary>
    /// Evénement transmit au model-vue lors de la modification d'une entité.
    /// Implémente la logique d'interaction de la vue (associations, ...)
    /// </summary>
    public class EntityPreUpdateEvent : IEvent
    {
        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public EntityPreUpdateEvent(IEntity entity)
        {
            this.Entity = entity;
        }

        // Référence de l'entité modifiée
        public IEntity Entity;
    }
}

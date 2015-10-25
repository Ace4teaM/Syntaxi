using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lib;

namespace Event
{
    /// <summary>
    /// Evénement transmit par le model lors de la modification d'une entité
    /// Cet événement transporte les sous événements de type EntityCreateEvent, EntityUpdateEvent, EntityDeleteEvent
    /// </summary>
    public class EntityChangeEvent : IEvent
    {
        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public EntityChangeEvent(IEvent baseEvent, IEntity entity, IModel model)
        {
            this.Entity = entity;
            this.Model = model;
            this.BaseEvent = baseEvent;
        }
        public IModel Model;
        public IEntity Entity;
        public IEvent BaseEvent;
    }
}

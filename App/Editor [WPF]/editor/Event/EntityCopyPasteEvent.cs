using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib;

namespace Event
{
    /// <summary>
    /// Evénement transmit par la vue au model-vue lors des opérations de copier/coller
    /// </summary>
    public enum EntityCopyPasteEventType
    {
        Copy,
        Paste
    }

    public class EntityCopyPasteEvent : IEvent
    {
        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public EntityCopyPasteEvent(EntityCopyPasteEventType type)
        {
            this.Type = type;
        }

        public EntityCopyPasteEvent(EntityCopyPasteEventType type, IEntity entity) : this(type)
        {
            Entities.Add(entity);
        }

        public EntityCopyPasteEvent(EntityCopyPasteEventType type, IEntity[] entities) : this(type)
        {
            Entities.AddRange(entities);
        }

        public void AddEntity(IEntity e){
            Entities.Add(e);
        }

        public bool IsEmpty()
        {
            return (Entities.Count == 0);
        }

        public List<IEntity> Entities = new List<IEntity>();
        public EntityCopyPasteEventType Type;
    }
}

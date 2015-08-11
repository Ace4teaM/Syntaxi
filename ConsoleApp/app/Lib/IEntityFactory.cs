using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib
{
    public interface IEntityFactory
    {
        string Name { get; }
        EntityState GetState(IEntity entity);
        void SetState(IEntity entity, EntityState state);
        IEntity GetReference(IEntity e);
        IEnumerable Factory<T>() where T : IEntity, new();
        void Commit(IEntity[] entities);
        List<IEntity> GetReferences();
    }
}
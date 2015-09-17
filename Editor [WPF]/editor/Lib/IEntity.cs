using System;

namespace Lib
{
    public delegate void EntityCallback(IEntity entity);
    
    public interface IEntity
    {
        string EntityName { get; }
        EntityState EntityState { get; set; }
        IEntity Clone();
    }
}
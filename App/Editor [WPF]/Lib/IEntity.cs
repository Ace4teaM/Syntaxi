using System;

namespace Lib
{
    public delegate void EntityCallback(IEntity entity);
    
    public interface IEntity
    {
        string EntityName { get; }
        EntityState EntityState { get; set; }
        IModel Model { get; set; }
        IEntity Clone();
        IEntity Copy(IEntity dst,IEntity src);
    }
}
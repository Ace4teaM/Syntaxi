using System;

namespace Lib
{
    public interface IEntityAssociation
    {
        IEntity A { get; }
        IEntity B { get; }
        IModel Model { get; set; }
    }
}
﻿using System;
using System.Text;

namespace Lib
{
    public interface IEntityValidable : IEntity
    {
        bool IsValid();
        bool CheckField(string propertyName, out string errorCode);
    }
}
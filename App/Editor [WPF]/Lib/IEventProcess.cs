using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib
{
    public interface IEventProcess
    {
        void ProcessEvent(object from,object _this,IEvent e);
    }
}
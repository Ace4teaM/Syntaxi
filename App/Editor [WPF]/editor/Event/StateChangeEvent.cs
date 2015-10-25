using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib;

namespace Event
{
    /// <summary>
    /// Evénement de base d'un changement d'état
    /// </summary>
    public class StateChangeEvent : IEvent
    {
        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public StateChangeEvent(IStateChange state)
        {
            this.State = state;
        }

        public IStateChange State;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib;

namespace Event
{
    /// <summary>
    /// Evénement transmit par le model lors d'un changement dans le model
    /// Cet événement est utilisé lorsque la vue doit réinitialisé la globalité de son interface
    /// </summary>
    public class ModelChangeEvent : IEvent
    {
        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public ModelChangeEvent(IModel model)
        {
            this.Model = model;
        }
        public IModel Model;
    }
}

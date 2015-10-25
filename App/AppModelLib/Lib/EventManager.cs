using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib
{
    public class EventManager : IEventManager
	{
        public EventManager()
        {
            notify = new List<IEventProcess>();
        }
        /// <summary>
        /// Abonne un objet aux notifications
        /// </summary>
        /// <param name="proc">Objet de base IEventProcess</param>
        public void NotifyRegister(IEventProcess proc)
        {
            if(notify.Contains(proc) == false)
                notify.Add(proc);
        }
        /// <summary>
        /// Désabonne un objet notifié
        /// </summary>
        /// <param name="proc">Objet de base IEventProcess</param>
        public void NotifyUnRegister(IEventProcess proc)
        {
            if (notify.Contains(proc) == true)
                notify.Remove(proc);
        }
        /// <summary>
        /// Notifie un événement
        /// </summary>
        /// <param name="e">Evénement à notifier</param>
        public void NotifyEvent(IEvent e)
        {
            foreach (var i in notify)
            {
                i.ProcessEvent(this, this, e);
            }
        }

        private List<IEventProcess> notify;
    }
}

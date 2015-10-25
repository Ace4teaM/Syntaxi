using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib
{
    public interface IEventManager
    {
		/// <summary>
		/// Abonne un objet aux notifications
		/// </summary>
		/// <param name="proc">Objet de base IEventProcess</param>
		 void NotifyRegister(IEventProcess proc);
		/// <summary>
		/// Désabonne un objet notifié
		/// </summary>
		/// <param name="proc">Objet de base IEventProcess</param>
		 void NotifyUnRegister(IEventProcess proc);
		/// <summary>
		/// Notifie un événement
		/// </summary>
		/// <param name="e">Evénement à notifier</param>
		 void NotifyEvent(IEvent e);
    }
}
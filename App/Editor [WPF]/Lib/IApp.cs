using System;

namespace Lib
{
    public interface IApp : IEventProcess
    {
        /// <summary>
        /// Traite une exception utilisateur retournée par l’application
        /// </summary>
        /// <param name="ex">Exception</param>
        void ProcessException(Exception ex);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib
{
    public interface IStateChange
    {
        /// <summary>
        /// Titre indicatif du changement d'état
        /// </summary>
        string Title { get; }
        /// <summary>
        /// Applique la modification
        /// </summary>
        /// <param name="app"></param>
        void Apply(IEventProcess app);
        /// <summary>
        /// Annule la modification
        /// </summary>
        /// <param name="app"></param>
        void UnApply(IEventProcess app);
    }
}
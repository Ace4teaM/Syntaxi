using System;
using System.Text;

namespace Lib
{
    public interface IEntityValidable : IEntity
    {
        /// <summary>
        /// Valide les champs
        /// </summary>
        /// <returns></returns>
        void Validate();
        /// <summary>
        /// Test la validité d'un champ
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        bool CheckField(string propertyName, out string errorCode);
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Lib
{
    // Etat de modification
    public enum EntityState
    {
        Unmodified, // Valeur par défaut
        Modified,
        Added,
        Deleted
    }

    public interface IEntityPersistent : IEntity
    {
        /// <summary>
        /// Obtient l'instance d'interface avec la base de données
        /// </summary>
        IEntityFactory Factory { get; set; }
        /// <summary>
        /// Nom de la table correspondante à l’entité
        /// </summary>
        string TableName { get; }
        /// <summary>
        /// Insert l'entité en base de données
        /// </summary>
        /// <param name="add_params">Nom des paramètres additionnels à passer à la requête SQL</param>
        /// <param name="add_values">Valeur des paramètres additionnels à passer à la requête SQL</param>
        /// <remarks>Initialise la clé primaire si elle est une séquence</remarks>
        void Insert(string add_params = "", string add_values = "");
        /// <summary>
        /// Actualise les champs en base de données
        /// </summary>
        /// <param name="add_params">Nom des paramètres additionnels à passer à la requête SQL</param>
        /// <returns></returns>
        int Update(string add_params = "");
        /// <summary>
        /// Supprime l'entité en base de données
        /// </summary>
        /// <returns></returns>
        int Delete();
        /// <summary>
        /// Charge les relations d'entités
        /// </summary>
        void Load();
        /// <summary>
        /// Obtient l’identificateur primaire
        /// </summary>
        /// <returns></returns>
        string[] GetPrimaryIdentifier();
        /// <summary>
        /// Compare l'identifiant primaire avec une autre entité
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        bool CompareIdentifier(IEntityPersistent e);
        /// <summary>
        /// Rase l'identifiant primaire
        /// </summary>
        void RaiseIdentity();
        /// <summary>
        /// Extrait l'identifiant primaire d'un résultat de requête
        /// </summary>
        /// <param name="reader"></param>
        void PickIdentity(object reader);
        /// <summary>
        /// Extrait les propriétés d'un résultat de requête
        /// </summary>
        /// <param name="reader">Objet de résultats</param>
        void PickProperties(object reader);
        /// <summary>
        /// Charge une relation d'entités
        /// </summary>
        /// <param name="name">Nom de la relation</param>
        /// <returns>Entité ou collection d'entités chargés</returns>
        object LoadAssociations(string name);
    }
}
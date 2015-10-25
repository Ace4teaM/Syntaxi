using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib
{
    /// <summary>
    /// IModel manage une collection d'entités identifiables par une clé unique.
    /// Seul les objets clonables peuvent être insérés dans un model
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Crée une entité
        /// </summary>
        IEntity CreateEntity(string entityName);
        /// <summary>
        /// Liste des références
        /// </summary>
        ICollection<IEntity> Objs { get; }
        /// <summary>
        /// Clone le model et les entités qu'il contient
        /// </summary>
        /// <returns></returns>
        IModel Clone();
        /// <summary>
        /// Applique la logique métier lors de la modification d'une entité
        /// </summary>
        /// <param name="entity">Entité de référence</param>
        void Update(IEntity entity);
        /// <summary>
        /// Applique la logique métier lors de la suppression d'une entité
        /// </summary>
        /// <param name="entity">Entité de référence</param>
        void Delete(IEntity entity);
        /// <summary>
        /// Applique la logique métier lors de la création d'une entité
        /// </summary>
        /// <param name="entity">Entité de référence</param>
        void Create(IEntity entity);
        /// <summary>
        /// Ajoute une entité à la liste des références
        /// </summary>
        /// <param name="entity">Entité de référence</param>
        /// <returns>Entité de référence</returns>
        /// <remarks>La clé de référence est générée automatiquement</remarks>
        IEntity Add(IEntity entity);
        /// <summary>
        /// Ajoute une entité à la liste des références
        /// </summary>
        /// <param name="entity">Entité de référence</param>
        /// <param name="key">Clé de référence</param>
        /// <returns>Entité de référence</returns>
        IEntity Add(IEntity entity, int key);
        /// <summary>
        /// Supprime une entité par sa clé de référence
        /// </summary>
        /// <param name="key">Clé de référence</param>
        void Remove(int key);
        /// <summary>
        /// Supprime une entité par sa référence
        /// </summary>
        /// <param name="entity">Entité de référence</param>
        void Remove(IEntity entity);
        /// <summary>
        /// Obtient une référence de l'objet, si celui-ci n'existe pas il est automatiquement ajouté à la liste
        /// </summary>
        /// <param name="entity">Entité recherchée</param>
        /// <returns>Entité de référence</returns>
        /// <remarks>Si l'entité passée en argument est de type IEntityPersistent alors la recherche porte également sur son identifiant.</remarks>
        IEntity GetReference(IEntity entity);
        /// <summary>
        /// Obtient la clé d'un objet
        /// </summary>
        /// <param name="entity">Entité recherchée</param>
        /// <returns>Clé trouvée, -1 si introuvable</returns>
        int GetObjectKey(IEntity entity);
        /// <summary>
        /// Obtient un objet par sa clé
        /// </summary>
        /// <param name="key">Clé de l'objet</param>
        /// <returns>Entité trouvée, null si introuvable</returns>
        IEntity GetObjectByKey(int key);
        /// <summary>
        /// Vérifie si le model contient une référence de l'objet donné
        /// </summary>
        /// <param name="key">Entité de référence</param>
        /// <remarks>Cette fonction test uniquement la référence mémoire de l'objet et non pas son identifiant dans le cas d'une entité persistante. Cette fonction est utile par exemple pour vérifier si une entité pointée par un événement fait parti du model.</remarks>
        /// <returns>true si l'entité est trouvée, sinon false</returns>
        bool Contains(IEntity entity);
    }
}
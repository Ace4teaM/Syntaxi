using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Lib
{
    public interface IEntityFactory
    {
        /// <summary>
        /// Obtient le model de données contenant les références des entités
        /// </summary>
        IModel Model { get; set; }
        /// <summary>
        /// Obtient le nom du fournisseur d'accès
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Obtient une référence d'entité
        /// </summary>
        /// <param name="e">Entité de base</param>
        /// <returns>Référence de l'entité</returns>
        IEntityPersistent GetReference(IEntityPersistent e);
        /// <summary>
        /// Enumère les références en base de données
        /// </summary>
        /// <typeparam name="T">Type d'entité concerné</typeparam>
        /// <param name="query">Requête de sélection, si null toutes les entrées sont énumérées</param>
        /// <returns>Enumeration</returns>
        IEnumerable Factory<T>(string query) where T : IEntityPersistent, new();
        //void Commit(IEntityPersistent[] entities);
        /// <summary>
        /// Obtient une liste des références du model de données
        /// </summary>
        /// <returns>Liste des références</returns>
        List<IEntityPersistent> GetReferences();
        /// <summary>
        /// Exécute une requête SQL et retourne le premier champ de la première colonne trouvée
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Donné du champ, null si aucun</returns>
        object QueryScalar(string query);
        /// <summary>
        /// Exécute une requête SQL sans retourner de résultat
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        int Query(string query);
        /// <summary>
        /// Exécute une requête SQL et appel une fonction de callback pour lire les données
        /// </summary>
        /// <param name="query">Requête SQL</param>
        /// <param name="act">Fonction de rappel</param>
        void Query(string query, Func<DbDataReader, int> act);
        /// <summary>
        /// Initialise les propriétés d'un objet générique avec les données d'une requête SQL
        /// </summary>
        /// <param name="query">Requête SQL</param>
        /// <param name="obj">Objet à initialisé</param>
        void QueryObject(string query, object obj);
        /// <summary>
        /// Retourne une entité de référence initialisée avec les données d'une requête SQL
        /// </summary>
        /// <typeparam name="T">Type de base IEntityPersistent</typeparam>
        /// <param name="query">Requête SQL</param>
        /// <returns>Référence de l'entité initialisée avec les données de la requête</returns>
        IEntityPersistent QueryEntity<T>(string query) where T : IEntityPersistent, new();
        /// <summary>
        /// Converti un type natif en type SQL
        /// </summary>
        /// <param name="value">Donné à convertir</param>
        /// <returns>Chaine contenant la donnée convertie utilisable dans une requête SQL</returns>
        string ParseType(object value);
    }
}
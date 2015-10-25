/*
   Interface IAppModel
   
   !!Attention!!
   Ce code source est généré automatiquement, toutes modifications seront perdues
   
*/

using System;
using Lib;
using AppModel.Entity;
using AppModel.Domain;

namespace AppModel.Interface
{
    /// <summary>
    /// Implémente la défintion de l'interface
    /// </summary>

    public interface IAppModel
    {
         #region Methods
         
         /// <summary>
         /// Sauvegarde le projet dans un fichier
         /// </summary>
         void SaveProject (String Filename);
         
         /// <summary>
         /// Charge le projet depuis un fichier
         /// </summary>
         void LoadProject (String Filename);
         
         /// <summary>
         /// Ajoute les objets d'un répertoire
         /// </summary>
         void AddObjects (SearchParams search);
         
         /// <summary>
         /// Exporte le objets dans une base de données
         /// </summary>
         void Export (IEntityFactory factory);
         
         /// <summary>
         /// Importe les objets depuis une base de données
         /// </summary>
         void Import (IEntityFactory factory);
         
         /// <summary>
         /// Importe des objets de syntaxe depuis un répertoire
         /// </summary>
         void ImportSyntaxDirectory (string path);
         #endregion // Methods
     }
}
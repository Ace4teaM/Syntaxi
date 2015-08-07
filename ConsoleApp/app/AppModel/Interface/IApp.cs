/*
   Interface IApp
   
   !!Attention!!
   Ce code source est généré automatiquement, toutes modifications seront perdues
   
*/

using System;
using AppModel.Entity;

namespace AppModel.Interface
{
    /// <summary>
    /// Implémente la défintion de l'interface
    /// </summary>

    public interface IApp
    {
         #region Methods
         // 
         void SaveProject (String Filename);
         // 
         void LoadProject (String Filename);
         // 
         void InitialiseProject ();
         // 
         void AddObjects (String inputDir, String inputFilter, bool bRecursive);
         #endregion // Methods
     }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib
{
    public interface IStateManager
    {
        /// <summary>
        /// Obtient le model de données
        /// </summary>
        IModel Model { get; }
        /// <summary>
        /// Débute l’enregistrement des modifications sur un model
        /// </summary>
        /// <param name="app"></param>
        void Begin(IModel app);
        /// <summary>
        /// Applique toutes les modifications apportées depuis l’appel à Begin
        /// </summary>
        void Apply();
        /// <summary>
        /// Annule toutes les modifications apportées depuis l’appel à Begin
        /// </summary>
        void Cancel();
        /// <summary>
        /// Annule la modification et recule le curseur de modification
        /// </summary>
        void Next();
        /// <summary>
        /// Applique la modification et avance le curseur de modification
        /// </summary>
        void Back();
        /// <summary>
        /// Obtient la position du curseur de modification
        /// </summary>
        /// <returns></returns>
        int GetPosition();
        /// <summary>
        /// Déplace le curseur de modification
        /// </summary>
        /// <param name="indice"></param>
        /// <returns></returns>
        bool MoveTo(int indice);
        /// <summary>
        /// Ajoute un changement à la liste
        /// </summary>
        /// <param name="change"></param>
        void Add(IStateChange change);
    }
}
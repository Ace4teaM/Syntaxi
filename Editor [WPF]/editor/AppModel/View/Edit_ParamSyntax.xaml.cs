﻿/*
   Vue d'édition de la classe ParamSyntax

   !!Attention!!
   Ce code source est généré automatiquement, toutes modifications sera perdue
   
*/

using System;
using System.Windows;
using System.Windows.Controls;
namespace AppModel.View
{
    /// <summary>
    /// Logique d'interaction pour Edit_ParamSyntax.xaml
    /// </summary>
    public partial class Edit_ParamSyntax : UserControl
    {
        public Edit_ParamSyntax()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------------------
        // Proprietes
        //-----------------------------------------------------------------------------------------
        #region Properties
        #region EnabledItems
        /// <summary>
        /// Définit les éléments actifs du dialogue
        /// </summary>
        public static readonly DependencyProperty EnabledItemsProperty = DependencyProperty.Register("EnabledItems", typeof(string), typeof(Edit_ParamSyntax), new PropertyMetadata(string.Empty, EnabledItemsChanged));
        public string EnabledItems
        {
            get { return (string)base.GetValue(EnabledItemsProperty); }
            set { base.SetValue(EnabledItemsProperty, value); }
        }

        private static void EnabledItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as Edit_ParamSyntax;

             string[] _args = me.EnabledItems.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);

             // desactive tout
             me.itemGroup_ContentRegEx.IsEnabled = false;
             me.itemGroup_ParamRegEx.IsEnabled = false;
             me.itemGroup_ParamType.IsEnabled = false;
             me.itemGroup_GroupName.IsEnabled = false;             

             // active les éléments demandés
             foreach (string s in _args)
             {
                 switch (s.Trim())
                 {
                     case "ContentRegEx":
                         me.itemGroup_ContentRegEx.IsEnabled = true;
                         break;
                     case "ParamRegEx":
                         me.itemGroup_ParamRegEx.IsEnabled = true;
                         break;
                     case "ParamType":
                         me.itemGroup_ParamType.IsEnabled = true;
                         break;
                     case "GroupName":
                         me.itemGroup_GroupName.IsEnabled = true;
                         break;
               }
            }
        }
        #endregion
        #region VisibleItems
        /// <summary>
        /// Définit les éléments visibles du dialogue
        /// </summary>
        public static readonly DependencyProperty VisibleItemsProperty = DependencyProperty.Register("VisibleItems", typeof(string), typeof(Edit_ParamSyntax), new PropertyMetadata(string.Empty, VisibleItemsChanged));
        public string VisibleItems
        {
            get { return (string)base.GetValue(VisibleItemsProperty); }
            set { base.SetValue(VisibleItemsProperty, value); }
        }

        private static void VisibleItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as Edit_ParamSyntax;

             string[] _args = me.VisibleItems.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

             // desactive tout
             me.itemGroup_ContentRegEx.Visibility = Visibility.Collapsed;
             me.itemGroup_ParamRegEx.Visibility = Visibility.Collapsed;
             me.itemGroup_ParamType.Visibility = Visibility.Collapsed;
             me.itemGroup_GroupName.Visibility = Visibility.Collapsed;             


             // active les éléments demandés
             foreach (string s in _args)
             {
                 switch (s.Trim())
                 {
                     case "ContentRegEx":
                         me.itemGroup_ContentRegEx.Visibility = Visibility.Visible;
                         // place l'élément en bas de la pile (permet le tri par visibilité)
                         me.itemGroups.Children.Remove(me.itemGroup_ContentRegEx);
                         me.itemGroups.Children.Add(me.itemGroup_ContentRegEx);
                         break;
                     case "ParamRegEx":
                         me.itemGroup_ParamRegEx.Visibility = Visibility.Visible;
                         // place l'élément en bas de la pile (permet le tri par visibilité)
                         me.itemGroups.Children.Remove(me.itemGroup_ParamRegEx);
                         me.itemGroups.Children.Add(me.itemGroup_ParamRegEx);
                         break;
                     case "ParamType":
                         me.itemGroup_ParamType.Visibility = Visibility.Visible;
                         // place l'élément en bas de la pile (permet le tri par visibilité)
                         me.itemGroups.Children.Remove(me.itemGroup_ParamType);
                         me.itemGroups.Children.Add(me.itemGroup_ParamType);
                         break;
                     case "GroupName":
                         me.itemGroup_GroupName.Visibility = Visibility.Visible;
                         // place l'élément en bas de la pile (permet le tri par visibilité)
                         me.itemGroups.Children.Remove(me.itemGroup_GroupName);
                         me.itemGroups.Children.Add(me.itemGroup_GroupName);
                         break;
              }
            }
        }
        #endregion
        #region Editable
        /// <summary>
        /// Indique si les champs sont éditables
        /// </summary>
        private bool editable;
        public bool Editable
        {
            get { return editable; }
            set {
               this.editable = value;
               
                // rend les éléments visibles
                this.itemGroup_ContentRegEx.Visibility = Visibility.Visible;
                this.itemGroup_ParamRegEx.Visibility = Visibility.Visible;
                this.itemGroup_ParamType.Visibility = Visibility.Visible;
                this.itemGroup_GroupName.Visibility = Visibility.Visible;                

                // active / desactive l'édition
               this.itemGroup_ContentRegEx.IsEnabled = value;
               this.itemGroup_ParamRegEx.IsEnabled = value;
               this.itemGroup_ParamType.IsEnabled = value;
               this.itemGroup_GroupName.IsEnabled = value;

            }
        }
        #endregion
        #endregion
    }
}
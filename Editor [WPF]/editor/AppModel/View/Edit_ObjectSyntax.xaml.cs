/*
   Vue d'édition de la classe ObjectSyntax

   !!Attention!!
   Ce code source est généré automatiquement, toutes modifications sera perdue
   
*/

using System;
using System.Windows;
using System.Windows.Controls;
namespace AppModel.View
{
    /// <summary>
    /// Logique d'interaction pour Edit_ObjectSyntax.xaml
    /// </summary>
    public partial class Edit_ObjectSyntax : UserControl
    {
        public Edit_ObjectSyntax()
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
        public static readonly DependencyProperty EnabledItemsProperty = DependencyProperty.Register("EnabledItems", typeof(string), typeof(Edit_ObjectSyntax), new PropertyMetadata(string.Empty, EnabledItemsChanged));
        public string EnabledItems
        {
            get { return (string)base.GetValue(EnabledItemsProperty); }
            set { base.SetValue(EnabledItemsProperty, value); }
        }

        private static void EnabledItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as Edit_ObjectSyntax;

             string[] _args = me.EnabledItems.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);

             // desactive tout
             me.itemGroup_ContentRegEx.IsEnabled = false;
             me.itemGroup_ParamRegEx.IsEnabled = false;
             me.itemGroup_ObjectType.IsEnabled = false;
             me.itemGroup_ObjectDesc.IsEnabled = false;             

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
                     case "ObjectType":
                         me.itemGroup_ObjectType.IsEnabled = true;
                         break;
                     case "ObjectDesc":
                         me.itemGroup_ObjectDesc.IsEnabled = true;
                         break;
               }
            }
        }
        #endregion
        #region VisibleItems
        /// <summary>
        /// Définit les éléments visibles du dialogue
        /// </summary>
        public static readonly DependencyProperty VisibleItemsProperty = DependencyProperty.Register("VisibleItems", typeof(string), typeof(Edit_ObjectSyntax), new PropertyMetadata(string.Empty, VisibleItemsChanged));
        public string VisibleItems
        {
            get { return (string)base.GetValue(VisibleItemsProperty); }
            set { base.SetValue(VisibleItemsProperty, value); }
        }

        private static void VisibleItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as Edit_ObjectSyntax;

             string[] _args = me.VisibleItems.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

             // desactive tout
             me.itemGroup_ContentRegEx.Visibility = Visibility.Collapsed;
             me.itemGroup_ParamRegEx.Visibility = Visibility.Collapsed;
             me.itemGroup_ObjectType.Visibility = Visibility.Collapsed;
             me.itemGroup_ObjectDesc.Visibility = Visibility.Collapsed;             


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
                     case "ObjectType":
                         me.itemGroup_ObjectType.Visibility = Visibility.Visible;
                         // place l'élément en bas de la pile (permet le tri par visibilité)
                         me.itemGroups.Children.Remove(me.itemGroup_ObjectType);
                         me.itemGroups.Children.Add(me.itemGroup_ObjectType);
                         break;
                     case "ObjectDesc":
                         me.itemGroup_ObjectDesc.Visibility = Visibility.Visible;
                         // place l'élément en bas de la pile (permet le tri par visibilité)
                         me.itemGroups.Children.Remove(me.itemGroup_ObjectDesc);
                         me.itemGroups.Children.Add(me.itemGroup_ObjectDesc);
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
                this.itemGroup_ObjectType.Visibility = Visibility.Visible;
                this.itemGroup_ObjectDesc.Visibility = Visibility.Visible;                

                // active / desactive l'édition
               this.itemGroup_ContentRegEx.IsEnabled = value;
               this.itemGroup_ParamRegEx.IsEnabled = value;
               this.itemGroup_ObjectType.IsEnabled = value;
               this.itemGroup_ObjectDesc.IsEnabled = value;

            }
        }
        #endregion
        #endregion
    }
}
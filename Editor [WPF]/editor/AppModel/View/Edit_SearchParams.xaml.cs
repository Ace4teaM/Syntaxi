/*
   Vue d'édition de la classe SearchParams

   !!Attention!!
   Ce code source est généré automatiquement, toutes modifications sera perdue
   
*/

using System;
using System.Windows;
using System.Windows.Controls;
namespace AppModel.View
{
    /// <summary>
    /// Logique d'interaction pour Edit_SearchParams.xaml
    /// </summary>
    public partial class Edit_SearchParams : UserControl
    {
        public Edit_SearchParams()
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
        public static readonly DependencyProperty EnabledItemsProperty = DependencyProperty.Register("EnabledItems", typeof(string), typeof(Edit_SearchParams), new PropertyMetadata(string.Empty, EnabledItemsChanged));
        public string EnabledItems
        {
            get { return (string)base.GetValue(EnabledItemsProperty); }
            set { base.SetValue(EnabledItemsProperty, value); }
        }

        private static void EnabledItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as Edit_SearchParams;

             string[] _args = me.EnabledItems.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);

             // desactive tout
             me.itemGroup_InputDir.IsEnabled = false;
             me.itemGroup_InputFilter.IsEnabled = false;
             me.itemGroup_Recursive.IsEnabled = false;             

             // active les éléments demandés
             foreach (string s in _args)
             {
                 switch (s.Trim())
                 {
                     case "InputDir":
                         me.itemGroup_InputDir.IsEnabled = true;
                         break;
                     case "InputFilter":
                         me.itemGroup_InputFilter.IsEnabled = true;
                         break;
                     case "Recursive":
                         me.itemGroup_Recursive.IsEnabled = true;
                         break;
               }
            }
        }
        #endregion
        #region VisibleItems
        /// <summary>
        /// Définit les éléments visibles du dialogue
        /// </summary>
        public static readonly DependencyProperty VisibleItemsProperty = DependencyProperty.Register("VisibleItems", typeof(string), typeof(Edit_SearchParams), new PropertyMetadata(string.Empty, VisibleItemsChanged));
        public string VisibleItems
        {
            get { return (string)base.GetValue(VisibleItemsProperty); }
            set { base.SetValue(VisibleItemsProperty, value); }
        }

        private static void VisibleItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as Edit_SearchParams;

             string[] _args = me.VisibleItems.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

             // desactive tout
             me.itemGroup_InputDir.Visibility = Visibility.Collapsed;
             me.itemGroup_InputFilter.Visibility = Visibility.Collapsed;
             me.itemGroup_Recursive.Visibility = Visibility.Collapsed;             


             // active les éléments demandés
             foreach (string s in _args)
             {
                 switch (s.Trim())
                 {
                     case "InputDir":
                         me.itemGroup_InputDir.Visibility = Visibility.Visible;
                         // place l'élément en bas de la pile (permet le tri par visibilité)
                         me.itemGroups.Children.Remove(me.itemGroup_InputDir);
                         me.itemGroups.Children.Add(me.itemGroup_InputDir);
                         break;
                     case "InputFilter":
                         me.itemGroup_InputFilter.Visibility = Visibility.Visible;
                         // place l'élément en bas de la pile (permet le tri par visibilité)
                         me.itemGroups.Children.Remove(me.itemGroup_InputFilter);
                         me.itemGroups.Children.Add(me.itemGroup_InputFilter);
                         break;
                     case "Recursive":
                         me.itemGroup_Recursive.Visibility = Visibility.Visible;
                         // place l'élément en bas de la pile (permet le tri par visibilité)
                         me.itemGroups.Children.Remove(me.itemGroup_Recursive);
                         me.itemGroups.Children.Add(me.itemGroup_Recursive);
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
                this.itemGroup_InputDir.Visibility = Visibility.Visible;
                this.itemGroup_InputFilter.Visibility = Visibility.Visible;
                this.itemGroup_Recursive.Visibility = Visibility.Visible;                

                // active / desactive l'édition
               this.itemGroup_InputDir.IsEnabled = value;
               this.itemGroup_InputFilter.IsEnabled = value;
               this.itemGroup_Recursive.IsEnabled = value;

            }
        }
        #endregion
        #endregion
    }
}
/*
   Vue d'édition de la classe ParamContent

   !!Attention!!
   Ce code source est généré automatiquement, toutes modifications sera perdue
   
*/

using System;
using System.Windows;
using System.Windows.Controls;
namespace AppModel.View
{
    /// <summary>
    /// Logique d'interaction pour Edit_ParamContent.xaml
    /// </summary>
    public partial class Edit_ParamContent : UserControl
    {
        public Edit_ParamContent()
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
        public static readonly DependencyProperty EnabledItemsProperty = DependencyProperty.Register("EnabledItems", typeof(string), typeof(Edit_ParamContent), new PropertyMetadata(string.Empty, EnabledItemsChanged));
        public string EnabledItems
        {
            get { return (string)base.GetValue(EnabledItemsProperty); }
            set { base.SetValue(EnabledItemsProperty, value); }
        }

        private static void EnabledItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as Edit_ParamContent;

             string[] _args = me.EnabledItems.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);

             // desactive tout
             me.itemGroup_Id.IsEnabled = false;
             me.itemGroup_ParamName.IsEnabled = false;
             me.itemGroup_ParamValue.IsEnabled = false;             

             // active les éléments demandés
             foreach (string s in _args)
             {
                 switch (s.Trim())
                 {
                     case "Id":
                         me.itemGroup_Id.IsEnabled = true;
                         break;
                     case "ParamName":
                         me.itemGroup_ParamName.IsEnabled = true;
                         break;
                     case "ParamValue":
                         me.itemGroup_ParamValue.IsEnabled = true;
                         break;
               }
            }
        }
        #endregion
        #region VisibleItems
        /// <summary>
        /// Définit les éléments visibles du dialogue
        /// </summary>
        public static readonly DependencyProperty VisibleItemsProperty = DependencyProperty.Register("VisibleItems", typeof(string), typeof(Edit_ParamContent), new PropertyMetadata(string.Empty, VisibleItemsChanged));
        public string VisibleItems
        {
            get { return (string)base.GetValue(VisibleItemsProperty); }
            set { base.SetValue(VisibleItemsProperty, value); }
        }

        private static void VisibleItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as Edit_ParamContent;

             string[] _args = me.VisibleItems.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

             // desactive tout
             me.itemGroup_Id.Visibility = Visibility.Collapsed;
             me.itemGroup_ParamName.Visibility = Visibility.Collapsed;
             me.itemGroup_ParamValue.Visibility = Visibility.Collapsed;             


             // active les éléments demandés
             foreach (string s in _args)
             {
                 switch (s.Trim())
                 {
                     case "Id":
                         me.itemGroup_Id.Visibility = Visibility.Visible;
                         // place l'élément en bas de la pile (permet le tri par visibilité)
                         me.itemGroups.Children.Remove(me.itemGroup_Id);
                         me.itemGroups.Children.Add(me.itemGroup_Id);
                         break;
                     case "ParamName":
                         me.itemGroup_ParamName.Visibility = Visibility.Visible;
                         // place l'élément en bas de la pile (permet le tri par visibilité)
                         me.itemGroups.Children.Remove(me.itemGroup_ParamName);
                         me.itemGroups.Children.Add(me.itemGroup_ParamName);
                         break;
                     case "ParamValue":
                         me.itemGroup_ParamValue.Visibility = Visibility.Visible;
                         // place l'élément en bas de la pile (permet le tri par visibilité)
                         me.itemGroups.Children.Remove(me.itemGroup_ParamValue);
                         me.itemGroups.Children.Add(me.itemGroup_ParamValue);
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
                this.itemGroup_Id.Visibility = Visibility.Visible;
                this.itemGroup_ParamName.Visibility = Visibility.Visible;
                this.itemGroup_ParamValue.Visibility = Visibility.Visible;                

                // active / desactive l'édition
               this.itemGroup_Id.IsEnabled = false; //<< par defaut les identifiants ne sont pas editable
               this.itemGroup_ParamName.IsEnabled = value;
               this.itemGroup_ParamValue.IsEnabled = value;

            }
        }
        #endregion
        #endregion
    }
}
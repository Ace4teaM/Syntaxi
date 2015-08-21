/*
   Vue d'édition de la classe DatabaseSource

   !!Attention!!
   Ce code source est généré automatiquement, toutes modifications sera perdue
   
*/

using System;
using System.Windows;
using System.Windows.Controls;
namespace AppModel.View
{
    /// <summary>
    /// Logique d'interaction pour Edit_DatabaseSource.xaml
    /// </summary>
    public partial class Edit_DatabaseSource : UserControl
    {
        public Edit_DatabaseSource()
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
        public static readonly DependencyProperty EnabledItemsProperty = DependencyProperty.Register("EnabledItems", typeof(string), typeof(Edit_DatabaseSource), new PropertyMetadata(string.Empty, EnabledItemsChanged));
        public string EnabledItems
        {
            get { return (string)base.GetValue(EnabledItemsProperty); }
            set { base.SetValue(EnabledItemsProperty, value); }
        }

        private static void EnabledItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as Edit_DatabaseSource;

             string[] _args = me.EnabledItems.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);

             // desactive tout
             me.itemGroup_Id.IsEnabled = false;
             me.itemGroup_Provider.IsEnabled = false;
             me.itemGroup_ConnectionString.IsEnabled = false;             

             // active les éléments demandés
             foreach (string s in _args)
             {
                 switch (s.Trim())
                 {
                     case "Id":
                         me.itemGroup_Id.IsEnabled = true;
                         break;
                     case "Provider":
                         me.itemGroup_Provider.IsEnabled = true;
                         break;
                     case "ConnectionString":
                         me.itemGroup_ConnectionString.IsEnabled = true;
                         break;
               }
            }
        }
        #endregion
        #region VisibleItems
        /// <summary>
        /// Définit les éléments visibles du dialogue
        /// </summary>
        public static readonly DependencyProperty VisibleItemsProperty = DependencyProperty.Register("VisibleItems", typeof(string), typeof(Edit_DatabaseSource), new PropertyMetadata(string.Empty, VisibleItemsChanged));
        public string VisibleItems
        {
            get { return (string)base.GetValue(VisibleItemsProperty); }
            set { base.SetValue(VisibleItemsProperty, value); }
        }

        private static void VisibleItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as Edit_DatabaseSource;

             string[] _args = me.VisibleItems.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

             // desactive tout
             me.itemGroup_Id.Visibility = Visibility.Collapsed;
             me.itemGroup_Provider.Visibility = Visibility.Collapsed;
             me.itemGroup_ConnectionString.Visibility = Visibility.Collapsed;             


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
                     case "Provider":
                         me.itemGroup_Provider.Visibility = Visibility.Visible;
                         // place l'élément en bas de la pile (permet le tri par visibilité)
                         me.itemGroups.Children.Remove(me.itemGroup_Provider);
                         me.itemGroups.Children.Add(me.itemGroup_Provider);
                         break;
                     case "ConnectionString":
                         me.itemGroup_ConnectionString.Visibility = Visibility.Visible;
                         // place l'élément en bas de la pile (permet le tri par visibilité)
                         me.itemGroups.Children.Remove(me.itemGroup_ConnectionString);
                         me.itemGroups.Children.Add(me.itemGroup_ConnectionString);
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
                this.itemGroup_Provider.Visibility = Visibility.Visible;
                this.itemGroup_ConnectionString.Visibility = Visibility.Visible;                

                // active / desactive l'édition
               this.itemGroup_Id.IsEnabled = false; //<< par defaut les identifiants ne sont pas editable
               this.itemGroup_Provider.IsEnabled = value;
               this.itemGroup_ConnectionString.IsEnabled = value;

            }
        }
        #endregion
        #endregion
    }
}
/*
   Vue d'édition de la classe ObjectContent

   !!Attention!!
   Ce code source est généré automatiquement, toutes modifications sera perdue
   
*/

using System;
using System.Windows;
using System.Windows.Controls;
namespace AppModel.View
{
    /// <summary>
    /// Logique d'interaction pour Edit_ObjectContent.xaml
    /// </summary>
    public partial class Edit_ObjectContent : UserControl
    {
        public Edit_ObjectContent()
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
        public static readonly DependencyProperty EnabledItemsProperty = DependencyProperty.Register("EnabledItems", typeof(string), typeof(Edit_ObjectContent), new PropertyMetadata(string.Empty, EnabledItemsChanged));
        public string EnabledItems
        {
            get { return (string)base.GetValue(EnabledItemsProperty); }
            set { base.SetValue(EnabledItemsProperty, value); }
        }

        private static void EnabledItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as Edit_ObjectContent;

             string[] _args = me.EnabledItems.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);

             // desactive tout
             me.itemGroup_Id.IsEnabled = false;
             me.itemGroup_ObjectType.IsEnabled = false;
             me.itemGroup_Filename.IsEnabled = false;
             me.itemGroup_Position.IsEnabled = false;             

             // active les éléments demandés
             foreach (string s in _args)
             {
                 switch (s.Trim())
                 {
                     case "Id":
                         me.itemGroup_Id.IsEnabled = true;
                         break;
                     case "ObjectType":
                         me.itemGroup_ObjectType.IsEnabled = true;
                         break;
                     case "Filename":
                         me.itemGroup_Filename.IsEnabled = true;
                         break;
                     case "Position":
                         me.itemGroup_Position.IsEnabled = true;
                         break;
               }
            }
        }
        #endregion
        #region VisibleItems
        /// <summary>
        /// Définit les éléments visibles du dialogue
        /// </summary>
        public static readonly DependencyProperty VisibleItemsProperty = DependencyProperty.Register("VisibleItems", typeof(string), typeof(Edit_ObjectContent), new PropertyMetadata(string.Empty, VisibleItemsChanged));
        public string VisibleItems
        {
            get { return (string)base.GetValue(VisibleItemsProperty); }
            set { base.SetValue(VisibleItemsProperty, value); }
        }

        private static void VisibleItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as Edit_ObjectContent;

             string[] _args = me.VisibleItems.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

             // desactive tout
             me.itemGroup_Id.Visibility = Visibility.Collapsed;
             me.itemGroup_ObjectType.Visibility = Visibility.Collapsed;
             me.itemGroup_Filename.Visibility = Visibility.Collapsed;
             me.itemGroup_Position.Visibility = Visibility.Collapsed;             


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
                     case "ObjectType":
                         me.itemGroup_ObjectType.Visibility = Visibility.Visible;
                         // place l'élément en bas de la pile (permet le tri par visibilité)
                         me.itemGroups.Children.Remove(me.itemGroup_ObjectType);
                         me.itemGroups.Children.Add(me.itemGroup_ObjectType);
                         break;
                     case "Filename":
                         me.itemGroup_Filename.Visibility = Visibility.Visible;
                         // place l'élément en bas de la pile (permet le tri par visibilité)
                         me.itemGroups.Children.Remove(me.itemGroup_Filename);
                         me.itemGroups.Children.Add(me.itemGroup_Filename);
                         break;
                     case "Position":
                         me.itemGroup_Position.Visibility = Visibility.Visible;
                         // place l'élément en bas de la pile (permet le tri par visibilité)
                         me.itemGroups.Children.Remove(me.itemGroup_Position);
                         me.itemGroups.Children.Add(me.itemGroup_Position);
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
                this.itemGroup_ObjectType.Visibility = Visibility.Visible;
                this.itemGroup_Filename.Visibility = Visibility.Visible;
                this.itemGroup_Position.Visibility = Visibility.Visible;                

                // active / desactive l'édition
               this.itemGroup_Id.IsEnabled = false; //<< par defaut les identifiants ne sont pas editable
               this.itemGroup_ObjectType.IsEnabled = value;
               this.itemGroup_Filename.IsEnabled = value;
               this.itemGroup_Position.IsEnabled = value;

            }
        }
        #endregion
        #endregion
    }
}
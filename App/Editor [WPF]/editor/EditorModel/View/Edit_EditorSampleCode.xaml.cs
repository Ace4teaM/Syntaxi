/*
   Vue d'édition de la classe EditorSampleCode

   !!Attention!!
   Ce code source est généré automatiquement, toutes modifications sera perdue
   
*/

using System;
using System.Windows;
using System.Windows.Controls;
namespace EditorModel.View
{
    /// <summary>
    /// Logique d'interaction pour Edit_EditorSampleCode.xaml
    /// </summary>
    public partial class Edit_EditorSampleCode : UserControl
    {
        public Edit_EditorSampleCode()
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
        public static readonly DependencyProperty EnabledItemsProperty = DependencyProperty.Register("EnabledItems", typeof(string), typeof(Edit_EditorSampleCode), new PropertyMetadata(string.Empty, EnabledItemsChanged));
        public string EnabledItems
        {
            get { return (string)base.GetValue(EnabledItemsProperty); }
            set { base.SetValue(EnabledItemsProperty, value); }
        }

        private static void EnabledItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as Edit_EditorSampleCode;

             string[] _args = me.EnabledItems.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);

             // desactive tout
             me.itemGroup_Text.IsEnabled = false;
             me.itemGroup_ObjectSyntaxType.IsEnabled = false;             

             // active les éléments demandés
             foreach (string s in _args)
             {
                 switch (s.Trim())
                 {
                     case "Text":
                         me.itemGroup_Text.IsEnabled = true;
                         break;
                     case "ObjectSyntaxType":
                         me.itemGroup_ObjectSyntaxType.IsEnabled = true;
                         break;
               }
            }
        }
        #endregion
        #region VisibleItems
        /// <summary>
        /// Définit les éléments visibles du dialogue
        /// </summary>
        public static readonly DependencyProperty VisibleItemsProperty = DependencyProperty.Register("VisibleItems", typeof(string), typeof(Edit_EditorSampleCode), new PropertyMetadata(string.Empty, VisibleItemsChanged));
        public string VisibleItems
        {
            get { return (string)base.GetValue(VisibleItemsProperty); }
            set { base.SetValue(VisibleItemsProperty, value); }
        }

        private static void VisibleItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as Edit_EditorSampleCode;

             string[] _args = me.VisibleItems.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

             // desactive tout
             me.itemGroup_Text.Visibility = Visibility.Collapsed;
             me.itemGroup_ObjectSyntaxType.Visibility = Visibility.Collapsed;             


             // active les éléments demandés
             foreach (string s in _args)
             {
                 switch (s.Trim())
                 {
                     case "Text":
                         me.itemGroup_Text.Visibility = Visibility.Visible;
                         // place l'élément en bas de la pile (permet le tri par visibilité)
                         me.itemGroups.Children.Remove(me.itemGroup_Text);
                         me.itemGroups.Children.Add(me.itemGroup_Text);
                         break;
                     case "ObjectSyntaxType":
                         me.itemGroup_ObjectSyntaxType.Visibility = Visibility.Visible;
                         // place l'élément en bas de la pile (permet le tri par visibilité)
                         me.itemGroups.Children.Remove(me.itemGroup_ObjectSyntaxType);
                         me.itemGroups.Children.Add(me.itemGroup_ObjectSyntaxType);
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
                this.itemGroup_Text.Visibility = Visibility.Visible;
                this.itemGroup_ObjectSyntaxType.Visibility = Visibility.Visible;                

                // active / desactive l'édition
               this.itemGroup_Text.IsEnabled = value;
               this.itemGroup_ObjectSyntaxType.IsEnabled = value;

            }
        }
        #endregion
        #endregion
    }
}
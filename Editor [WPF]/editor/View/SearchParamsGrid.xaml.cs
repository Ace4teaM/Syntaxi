﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AppModel.View;

namespace editor.View
{
    /// <summary>
    /// Logique d'interaction pour SearchParamsGrid.xaml
    /// </summary>
    public partial class SearchParamsGrid : DataGrid
    {
        public SearchParamsGrid()
        {
            InitializeComponent();
        }

        private void PropertiesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window wnd = new Window();
            wnd.Content = new EditView();
            wnd.Owner = Application.Current.MainWindow;
            wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            wnd.WindowStyle = WindowStyle.ToolWindow;
            wnd.Title = "Recherche";
            wnd.Width = 400;

            Edit_SearchParams view = new Edit_SearchParams();
            view.DataContext = this.SelectedItem;
            ((EditView)wnd.Content).AddView(view);

            wnd.ShowDialog();
        }
    }
}

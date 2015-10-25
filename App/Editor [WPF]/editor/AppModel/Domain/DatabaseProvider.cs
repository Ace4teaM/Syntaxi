/*
   Domaine de valeurs DatabaseProvider

   !!Attention!!
   Ce code source est généré automatiquement depuis PowerDesigner, toutes modifications risques d'être perdues
   
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Data;

namespace AppModel.Domain
{
    /*
   public static class DatabaseProvider
   {
      public const int Odbc = 0;
      public const int SqlServer = 1;
      public const int PostgreSQL = 2;
   }*/
   public class DatabaseProviderConverter : IValueConverter
   {
   
     public static Dictionary<string, int> ItemsSource = new Dictionary<string, int>()
     {
        {"ODBC",  0},
        {"Sql Server",  1},
        {"PostgreSQL",  2}
     };
      
     public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
     {
         if (value == null)
             return null;
   
         int val;
         if (int.TryParse(value.ToString(), out val) == false)
             return null;
   
         switch (val)
         {
             case DatabaseProvider.Odbc:
                 return "ODBC";
             case DatabaseProvider.SqlServer:
                 return "Sql Server";
             case DatabaseProvider.PostgreSQL:
                 return "PostgreSQL";
         }
   
         return null;
     }
     
     public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
     {
         if (value == null)
             return null;
   
         switch (value.ToString())
         {
             case "ODBC":
                 return DatabaseProvider.Odbc;
             case "Sql Server":
                 return DatabaseProvider.SqlServer;
             case "PostgreSQL":
                 return DatabaseProvider.PostgreSQL;
         }
         return null;
     }
   }
}
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
   public static class DatabaseProvider
   {
      public const string ODBC = "Odbc";
      public const string Postgres = "Postgre SQL";
      public const string SqlServer = "Sql Server";
   }
   
   public class DatabaseProviderConverter : IValueConverter
   {
     public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
     {
         if (value as string == null)
             return null;
   
         switch (value as string)
         {
             case DatabaseProvider.ODBC:
                 return "Odbc";
             case DatabaseProvider.Postgres:
                 return "Postgre SQL";
             case DatabaseProvider.SqlServer:
                 return "Sql Server";
         }
   
         return null;
     }
     public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
     {
         if (value as string == null)
             return null;
   
         switch (value as string)
         {
             case "Odbc":
                 return DatabaseProvider.ODBC;
             case "Postgre SQL":
                 return DatabaseProvider.Postgres;
             case "Sql Server":
                 return DatabaseProvider.SqlServer;
         }
         return null;
     }
   }
}
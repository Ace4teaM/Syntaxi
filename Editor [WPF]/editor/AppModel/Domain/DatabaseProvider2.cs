/*
   Domaine de valeurs DatabaseProvider2

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
   public enum DatabaseProvider2 : int
   {
      ODBC = 0,
      SqlServer = 1,
      Postgres = 2
   }
   
   public class DatabaseProvider2Converter : IValueConverter
   {
     public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
     {
         DatabaseProvider2? format = value as DatabaseProvider2?;
         if (format == null)
         {
             return null;
         }
   
         return (int)format;
     }
     public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
     {
         DatabaseProvider2 val;
         if(value!=null && Enum.TryParse(value.ToString(), true, out val))
             return val;
         return null;
     }
   }
}
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
    public enum DatabaseProvider : int
    {
      ODBC = 0,
      Postgres = 1,
      SqlServer = 2
    }
    
    
    public class DatabaseProviderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DatabaseProvider? format = value as DatabaseProvider?;
            if (format == null)
            {
                return null;
            }

            return (int)format;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int parseValue;
            if (false == int.TryParse(value.ToString(), out parseValue))
                return null;

            return (DatabaseProvider)parseValue;
        }
    }
}
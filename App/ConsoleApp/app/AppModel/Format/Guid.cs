/*
   Format de données Guid

   !!Attention!!
   Ce code source est généré automatiquement depuis PowerDesigner, toutes modifications risques d'être perdues
   
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AppModel.Format
{
      public static class Guid
      {
            /// <summary>
            /// GUID
            /// </summary>
            /// <param name="str">Chaine à valider</param>
            /// <returns>True si le champ est valide, sinon False</returns>
            public static bool Validate(string str, ref string msg)
            {
               System.Text.RegularExpressions.Regex myRegex = new Regex(@"^([A-Za-z0-9\-]+)$");
               if (!myRegex.IsMatch(str))
               {
                   msg = "INVALID_GUID_FORMAT";
                   //Result.last = new Result().Failed("INVALID_GUID_FORMAT");
                   return false;
               }
               msg = String.Empty;
               return true;
            }
      }
}
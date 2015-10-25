﻿/*
   Format de données Name

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
      public static class Name
      {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="str">Chaine à valider</param>
            /// <returns>True si le champ est valide, sinon False</returns>
            public static bool Validate(string str, ref string msg)
            {
               System.Text.RegularExpressions.Regex myRegex = new Regex(@"^([A-Za-z0-9_-]+)$");
               if (!myRegex.IsMatch(str))
               {
                   msg = "INVALID_NAME_FORMAT";
                   //Result.last = new Result().Failed("INVALID_NAME_FORMAT");
                   return false;
               }
               msg = String.Empty;
               return true;
            }
      }
}
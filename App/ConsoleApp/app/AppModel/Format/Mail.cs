﻿/*
   Format de données Mail

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
      public static class Mail
      {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="str">Chaine à valider</param>
            /// <returns>True si le champ est valide, sinon False</returns>
            public static bool Validate(string str, ref string msg)
            {
               System.Text.RegularExpressions.Regex myRegex = new Regex(@"^([\w]+)@([\w]+)\.([\w]+)$");
               if (!myRegex.IsMatch(str))
               {
                   msg = "INVALID_MAIL_FORMAT";
                   //Result.last = new Result().Failed("INVALID_MAIL_FORMAT");
                   return false;
               }
               msg = String.Empty;
               return true;
            }
      }
}
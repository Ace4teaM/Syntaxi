using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Entity;

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            Project project;

            project = new Project("Webframework","1.8");
            project.SearchParams.Add( new SearchParams(@"C:\Users\developpement\Documents\GitHub\Webframework\sql","*.sql",true) );

            project.SearchParams.Add( new SearchParams(@"C:\Users\developpement\Documents\GitHub\Webframework\wfw\php","*.php",true) );
            project.SearchParams.Add( new SearchParams(@"C:\Users\developpement\Documents\GitHub\Webframework\wfw\ctrl","*.php",true) );

            project.SearchParams.Add( new SearchParams(@"C:\Users\developpement\Documents\GitHub\Webframework-User-Module\sql\postgresql","func.sql",true) );
            project.SearchParams.Add( new SearchParams(@"C:\Users\developpement\Documents\GitHub\Webframework-User-Module\wfw-1.8\lib\user","*.php",true) );
            project.SearchParams.Add( new SearchParams(@"C:\Users\developpement\Documents\GitHub\Webframework-User-Module\wfw-1.8\ctrl\user","*.php",true) );
            
            project.SearchParams.Add( new SearchParams(@"C:\Users\developpement\Documents\GitHub\Webframework-IO-Module\sql\postgresql","func.sql",true) );
            project.SearchParams.Add( new SearchParams(@"C:\Users\developpement\Documents\GitHub\Webframework-IO-Module\wfw-1.8\lib\io","*.php",true) );
            project.SearchParams.Add( new SearchParams(@"C:\Users\developpement\Documents\GitHub\Webframework-IO-Module\wfw-1.8\ctrl\io","*.php",true) );
        }
    }
}

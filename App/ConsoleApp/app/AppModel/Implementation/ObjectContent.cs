using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lib;

namespace AppModel.Implementation
{
    class ObjectContent : AppModel.Entity.ObjectContent
    {
        public override void Insert(Dictionary<string, object> addParams = null)
        {
            SqlFactory db = Factory as SqlFactory;
            string add_query = "";

            // Association Project
            if (Project != null)
            {
                Dictionary<string, object> assParams = Project.GetPrimaryIdentifier();
                for (int i = 0; i < assParams.Count; i++)
                    add_query += String.Format(", @", assParams.ElementAt(i).Key, SqlFactory.ParseType(assParams.ElementAt(i).Value));
            }

            // Format de la chaine
            if (addParams != null)
            {
                for (int i = 0; i < addParams.Count; i++)
                    add_query += String.Format(", @", addParams.ElementAt(i).Key, SqlFactory.ParseType(addParams.ElementAt(i).Value));
            }

            string query = String.Format(
                "exec InsertContent {0},{1},{2},{3}{4}",
                SqlFactory.ParseType(Id), SqlFactory.ParseType(ObjectType), SqlFactory.ParseType(Filename), SqlFactory.ParseType(Position), add_query
            );

            // Execute
            db.Query(query);

        }
    }
}

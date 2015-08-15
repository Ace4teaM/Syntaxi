using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace editor.AppModel.Extend
{
    public partial class DatabaseSource
    {
        // Source par défault dans l'application ?
        public bool IsDefault{
            get
            {
                App app = Application.Current as App;
                return (
                    app.Project != null
                    && app.States != null
                    && app.States.SelectedDatabaseSourceId != null
                    && app.Project.DatabaseSource.Where(p => (p.Id == app.States.SelectedDatabaseSourceId)).SingleOrDefault() != null
               );
            }
        }
    }
}

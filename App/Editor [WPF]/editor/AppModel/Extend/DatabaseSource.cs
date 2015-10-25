using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using editor;

namespace AppModel.Entity
{
    public partial class DatabaseSource
    {
        // Source par défault dans l'application ?
        public bool IsDefault{
            get
            {
                editor.App app = Application.Current as editor.App;
                return (
                    app.Project != null
                    && app.States != null
                    && app.States.SelectedDatabaseSourceId != null
                    && app.States.SelectedDatabaseSourceId == this.Id
               );
            }
        }
    }
}

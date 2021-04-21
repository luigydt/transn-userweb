using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosWeb
{
    public class NuevoEventArgs : EventArgs // Clase creada extendida de la clase EventArgs para crear un evento con una informacion concreta a la hora de crear el evento.
    {
        public string delegaciones { get; set; }
        public NuevoEventArgs(string value)
        {
            delegaciones = value;
        }

    }

    public class CerrarEventArgs : EventArgs // Clase creada extendida de la clase EventArgs para crear un evento con una informacion concreta a la hora de crear el evento.
    {
        public List<String> dataRow {get;set;}

        public CerrarEventArgs(List<String> dataRow)
        {
            this.dataRow = dataRow;
        }
    }
}

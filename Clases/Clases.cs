using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosWeb
{    

    public class Tipo
    {
        public string id { get; set; }
        public string Nombre { get; set; }
        public Tipo (string id, string Nombre)
        {
            this.id = id;
            this.Nombre = Nombre;
        }        
    }
    //public class Delegacion
    //{
    //    public string id { get; set; }
    //    public string Nombre { get; set; }
    //    public Delegacion(string id, string Nombre)
    //    {
    //        this.id = id;
    //        this.Nombre = Nombre;
    //    }
    //}

}

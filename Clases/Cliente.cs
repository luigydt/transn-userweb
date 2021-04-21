using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UsuariosWeb
{   
    
    public class Cliente// Clase que se crea a partir de la informacion de una fila de la tabla ttuserweb, es decir, cada variable es un columna de la tabla, 
    {
        public string userID;
        public string codigoPick;
        public string password;
        public string nombre;
        public string delegaciones;
        public string tipo;
        public string nif;
        public string ient;
        public int listadoXLS;
        public int listadoFacturasXLS;
        public int facturaXLS;
        public string ip;
        public string delegacionVGG;
        public string clienteVGG;
        public string catalogoVGG;
        public string emailContacto;
        public string emailEnviosPendientes;
        public int id;
    }

    
}

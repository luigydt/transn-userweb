using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;
using MySql.Data.MySqlClient;
using System.Data;
using UsuariosWeb.Clases;

namespace UsuariosWeb
{
    class WebUser
    {
        String strConexion;
        private const string PATH = @"configuraciones.db"; // Direccion de 
        static Configuracion datosConexion;        
        MySqlConnection sqlConexion;
        

        public WebUser()
        {
            datosConexion = cargarConfiguracion(); // utilizamos este metodo de la Clase WebUser el cual nos devuelve una Configuracion y la instancia al crear un Web User
            strConexion = datosConexion.stringConexion(); // utiliza el metodo StringConexion(), que nos devuelve un string que utlizamos para la propiedad de la clase MysqlConettion 
            sqlConexion = new MySqlConnection(strConexion);//Instanciamos esta Clase MySqlConnection.        

        }

        public void insertClienteWeb(Cliente cliente)//metoto público que recibe como parámetro un Cliente y los Inserta en la tabla ttwebuser
        {
           
            String insertString; 
            insertString = "INSERT INTO usuariostt_web (userid,codigo_pick,password,nombre,tipo,delegaciones,nif,ient,listado_xls,listadofacturas_xls,factura_xls,ip,delegacionVGG,clienteVGG,catalogoVGG,emailContacto,emailEnviosPendientes)" +
                "VALUES(@p_userID, @p_codigoPick, @p_password, @p_nombre, @p_tipo, @p_delegaciones, @p_nif, @p_ient, @p_listadoXLS, @p_listadoFacturasXLS, @p_facturaXLS, @p_ip, @p_delegacionVGG, @p_clienteVGG, @p_catalogoVGG, @p_emailContacto, @p_emailEnviosPendientes)";
            MySqlCommand cmd_Alta = new MySqlCommand();  // Command que nozs permite realizar la INSERT 
            cmd_Alta.CommandType = CommandType.Text;
            cmd_Alta.CommandText = insertString;
            cmd_Alta.Connection = sqlConexion;


            //parametros Insert (cada uno con el Type, Size, Directiom y Name)

            MySqlParameter p_userId = new MySqlParameter();
            p_userId.MySqlDbType = MySqlDbType.VarChar;
            p_userId.Size = 15;
            p_userId.Direction = ParameterDirection.Input;
            p_userId.ParameterName = "@p_userID";

            MySqlParameter p_codigoPick = new MySqlParameter();
            p_codigoPick.MySqlDbType = MySqlDbType.VarChar;
            p_codigoPick.Size = 12;
            p_codigoPick.Direction = System.Data.ParameterDirection.Input;
            p_codigoPick.ParameterName = "@p_codigoPick";

            MySqlParameter p_password = new MySqlParameter();
            p_password.MySqlDbType = MySqlDbType.VarChar;
            p_password.Size = 15;
            p_password.Direction = System.Data.ParameterDirection.Input;
            p_password.ParameterName = "@p_password";

            MySqlParameter p_nombre = new MySqlParameter();
            p_nombre.MySqlDbType = MySqlDbType.VarChar;
            p_nombre.Size = 50;
            p_nombre.Direction = System.Data.ParameterDirection.Input;
            p_nombre.ParameterName = "@p_nombre";

            MySqlParameter p_tipo = new MySqlParameter();
            p_tipo.MySqlDbType = MySqlDbType.VarChar;
            p_tipo.Size = 5;
            p_tipo.Direction = System.Data.ParameterDirection.Input;
            p_tipo.ParameterName = "@p_tipo";

            MySqlParameter p_delgaciones = new MySqlParameter();
            p_delgaciones.MySqlDbType = MySqlDbType.VarChar;
            p_delgaciones.Size = 70;
            p_delgaciones.Direction = System.Data.ParameterDirection.Input;
            p_delgaciones.ParameterName = "@p_delegaciones";

            MySqlParameter p_nif = new MySqlParameter();
            p_nif.MySqlDbType = MySqlDbType.VarChar;
            p_nif.Size = 400;
            p_nif.Direction = System.Data.ParameterDirection.Input;
            p_nif.ParameterName = "@p_nif";

            MySqlParameter p_ient = new MySqlParameter();
            p_ient.MySqlDbType = MySqlDbType.VarChar;
            p_ient.Size = 400;
            p_ient.Direction = System.Data.ParameterDirection.Input;
            p_ient.ParameterName = "@p_ient";

            MySqlParameter p_listadoXLS = new MySqlParameter();
            p_listadoXLS.MySqlDbType = MySqlDbType.Int16;
            p_listadoXLS.Direction = System.Data.ParameterDirection.Input;
            p_listadoXLS.ParameterName = "@p_listadoXLS";

            MySqlParameter p_listadoFacturasXLS = new MySqlParameter();
            p_listadoFacturasXLS.MySqlDbType = MySqlDbType.Int16;
            p_listadoFacturasXLS.Direction = System.Data.ParameterDirection.Input;
            p_listadoFacturasXLS.ParameterName = "@p_listadoFacturasXLS";

            MySqlParameter p_facturaXLS = new MySqlParameter();
            p_facturaXLS.MySqlDbType = MySqlDbType.Int16;
            p_facturaXLS.Direction = System.Data.ParameterDirection.Input;
            p_facturaXLS.ParameterName = "@p_facturaXLS";

            MySqlParameter p_ip = new MySqlParameter();
            p_ip.MySqlDbType = MySqlDbType.VarChar;
            p_ip.Size = 50;
            p_ip.Direction = System.Data.ParameterDirection.Input;
            p_ip.ParameterName = "@p_ip";

            MySqlParameter p_delegacionVGG = new MySqlParameter();
            p_delegacionVGG.MySqlDbType = MySqlDbType.VarChar;
            p_delegacionVGG.Size = 50;
            p_delegacionVGG.Direction = System.Data.ParameterDirection.Input;
            p_delegacionVGG.ParameterName = "@p_delegacionVGG";

            MySqlParameter p_clienteVGG = new MySqlParameter();
            p_clienteVGG.MySqlDbType = MySqlDbType.VarChar;
            p_clienteVGG.Size = 50;
            p_clienteVGG.Direction = System.Data.ParameterDirection.Input;
            p_clienteVGG.ParameterName = "@p_clienteVGG";

            MySqlParameter p_catalogoVGG = new MySqlParameter();
            p_catalogoVGG.MySqlDbType = MySqlDbType.VarChar;
            p_catalogoVGG.Size = 50;
            p_catalogoVGG.Direction = System.Data.ParameterDirection.Input;
            p_catalogoVGG.ParameterName = "@p_catalogoVGG";

            MySqlParameter p_emailContacto = new MySqlParameter();
            p_emailContacto.MySqlDbType = MySqlDbType.VarChar;
            p_emailContacto.Size = 150;
            p_emailContacto.Direction = System.Data.ParameterDirection.Input;
            p_emailContacto.ParameterName = "@p_emailContacto";

            MySqlParameter p_emailEnviosPendientes = new MySqlParameter();
            p_emailEnviosPendientes.MySqlDbType = MySqlDbType.VarChar;
            p_emailEnviosPendientes.Size = 250;
            p_emailEnviosPendientes.Direction = System.Data.ParameterDirection.Input;
            p_emailEnviosPendientes.ParameterName = "@p_emailEnviosPendientes";


            //Añadimos los parametros en orden correspondiente al string del Command
            cmd_Alta.Parameters.Add(p_userId);
            cmd_Alta.Parameters.Add(p_codigoPick);
            cmd_Alta.Parameters.Add(p_password);
            cmd_Alta.Parameters.Add(p_nombre);
            cmd_Alta.Parameters.Add(p_tipo);
            cmd_Alta.Parameters.Add(p_delgaciones);
            cmd_Alta.Parameters.Add(p_nif);
            cmd_Alta.Parameters.Add(p_ient);
            cmd_Alta.Parameters.Add(p_listadoXLS);
            cmd_Alta.Parameters.Add(p_listadoFacturasXLS);
            cmd_Alta.Parameters.Add(p_facturaXLS);
            cmd_Alta.Parameters.Add(p_ip);
            cmd_Alta.Parameters.Add(p_delegacionVGG);
            cmd_Alta.Parameters.Add(p_clienteVGG);
            cmd_Alta.Parameters.Add(p_catalogoVGG);
            cmd_Alta.Parameters.Add(p_emailContacto);
            cmd_Alta.Parameters.Add(p_emailEnviosPendientes);

            //Añadimos la propiedad Value a cada parametro, el cual corresponde a cada variable del Cliente.
            cmd_Alta.Parameters[0].Value = cliente.userID;
            cmd_Alta.Parameters[1].Value = cliente.codigoPick;
            cmd_Alta.Parameters[2].Value = cliente.password;
            cmd_Alta.Parameters[3].Value = cliente.nombre;
            cmd_Alta.Parameters[4].Value = cliente.tipo;
            cmd_Alta.Parameters[5].Value = cliente.delegaciones;
            cmd_Alta.Parameters[6].Value = cliente.nif;
            cmd_Alta.Parameters[7].Value = cliente.ient;
            cmd_Alta.Parameters[8].Value = cliente.listadoXLS;
            cmd_Alta.Parameters[9].Value = cliente.listadoFacturasXLS;
            cmd_Alta.Parameters[10].Value = cliente.facturaXLS;
            cmd_Alta.Parameters[11].Value = cliente.ip;
            cmd_Alta.Parameters[12].Value = cliente.delegacionVGG;
            cmd_Alta.Parameters[13].Value = cliente.clienteVGG;
            cmd_Alta.Parameters[14].Value = cliente.catalogoVGG;
            cmd_Alta.Parameters[15].Value = cliente.emailContacto;
            cmd_Alta.Parameters[16].Value = cliente.emailEnviosPendientes;


            using (sqlConexion)//Utilizamos la conexion (SqlConexion), utilizando using cierra automaticamente esta conexion
            {
                sqlConexion.Open();//abrimos la conexion
                try
                {
                    cmd_Alta.ExecuteNonQuery();//metodo ExecuteNonQuery que ejecuta el commando con la base de Datos
                }
                catch (Exception e)
                {

                }

            }
        }

        public static Configuracion cargarConfiguracion()// Metodo public static que nos permite crear una coleccion dentro de nuestra Base de Datos local LiteDB 
        {
            using (var db = new LiteDatabase(PATH))
            {
                var configuracion = db.GetCollection<Configuracion>("configuracion"); //variable que representa la coleccion de la clase Configuracion con el nombre "configuracion"

                var results = configuracion.Query().Where(x => x.Id == 1).FirstOrDefault(); // Linq que nos permite buscar una configuracion con el id == 1, 
                if (results == null)// Si no encuentra ninguno(la primera vez que instalamos la aplicacion) creara uno nuevo vacio y lo añadira a la lista 
                {
                    results = new Configuracion { host = "", port = "", stringPwd = "", Id = 1, dataBase = "", usuario = "" , mensaje= new List<string>()};
                    configuracion.Insert(results);
                }

                return results;// devuelve esta Configuracion sea vacia si es la primera vez, o una que ya este configurada cuando actualizamos esta informacion
                
            }

        }
        public static void cargarDelegaciones()// Metodo public static que nos permite crear una coleccion dentro de nuestra Base de Datos local LiteDB 
        {
            using (var db = new LiteDatabase(PATH))
            {
                var delegacion = db.GetCollection<Delegacion>("delegaciones"); //variable que representa la coleccion de la clase Configuracion con el nombre "configuracion"

                var results = delegacion.Query().Where( x=> x.id == 1).FirstOrDefault(); // Linq que nos permite buscar una configuracion con el id == 1, 
                if (results == null)// Si no encuentra ninguno(la primera vez que instalamos la aplicacion) creara uno nuevo vacio y lo añadira a la lista 
                {
                    results = new Delegacion { id = 1,  numDelegacion="201",nomDelegacion = "Irún" };
                    delegacion.Insert(results);
                    results = new Delegacion { id = 2, numDelegacion = "202", nomDelegacion = "Zaragoza" };
                    delegacion.Insert(results);
                    results = new Delegacion { id = 3, numDelegacion = "203", nomDelegacion = "Bilbao" };
                    delegacion.Insert(results);
                    results = new Delegacion { id = 4, numDelegacion = "204", nomDelegacion = "Galicia" };
                    delegacion.Insert(results);                    
                } 
            }

        }

        public static List<Delegacion> getDelegaciones()//Metodo estatico que devuelve una lista de delegaciones 
        {
            List<Delegacion> nueva = new List<Delegacion>();
            using (var db = new LiteDatabase(PATH))
            {
                var delegacion = db.GetCollection<Delegacion>("delegaciones"); //variable que representa la coleccion de la clase Configuracion con el nombre "configuracion"
                nueva = delegacion.Query().ToList(); 
            }

            return nueva;
        }

        public static List<String> GetMensaje() // metodo Static que nos devuelve la variable List<string> de la Configuracion
        {
            using (var db = new LiteDatabase(PATH))
            {
                var configuracion = db.GetCollection<Configuracion>("configuracion");

                var results = configuracion.Query().Where(x => x.Id == 1).FirstOrDefault();//Ya que solo utilizamos una la cual vamos actualizando  solo utilizamos un Id (1)
                
                return results.mensaje;// por tanto devolvemos la variable mensaje que es la List<string>
                
            }
        }

        public static bool existeConfiguracion()//devuelve true si existe alguna configuracion con un usuario real, si no false;
        {
            using (var db = new LiteDatabase(PATH))
            {
                var config = db.GetCollection<Configuracion>("configuracion");
                var res = config.Query().Where(x => x.usuario == null).FirstOrDefault();
                if (res == null)
                {
                    return false;
                }
            }
            return true;
        }     
        public static void actualizarDelegaciones(List<Delegacion> lista)//Metodo estatico que pide como parametro una lista de Delegaciones para actualizar las delegaciones en la base de datos local
        {
            using (var db = new LiteDatabase(PATH))
            {
                var delegacion = db.GetCollection<Delegacion>("delegaciones");
                delegacion.DeleteAll();//Limpiamos todas las delegaciones y añadimos todas las de la lista
                foreach (Delegacion s in lista)
                {                     
                    delegacion.Insert(s); 
                }
            }
        }
        public static void guardarConfiguracion(Configuracion configuracion)//Actualiza una configuracion en la DB para los datos y  utiliza como parametro una Configuracion
        {
            using (var db = new LiteDatabase(PATH))
            {
                var config = db.GetCollection<Configuracion>("configuracion"); 

                config.Update(configuracion); // Utilizamos el metodo Update de la libreria LiteDataBase para actualizar la informacion 
            }
        }
        public DataTable filtroTabla(string nombre,string codigo,string nif)// Metoto publico que pide como parametros un nombre, un codigo pick y un nif(filtros de busqueda) y devuelve un DataTable con la informacion de esta tabla.
        {
            DataTable nuevo = new DataTable();
            MySqlCommand cmdFiltro = new MySqlCommand();
            cmdFiltro.CommandType = CommandType.Text;
            cmdFiltro.CommandText = "SELECT id,codigo_pick ,nombre,nif,tipo FROM usuariostt_web " +
                                    "WHERE nombre LIKE '%"+nombre+"%' OR NULL AND codigo_pick LIKE '%"+codigo+"%' OR NULL AND nif LIKE '%"+nif+"%' OR NULL";
            cmdFiltro.Connection = sqlConexion;

            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(cmdFiltro);
            sqlDataAdapter.Fill(nuevo);

            return nuevo;
        }

        public Cliente clienteEdit(int id)// Metodo public que pide como parametro un id(int) y realiza una búsqueda con ese Id en la base de datos.
        {
            Cliente nuevo = new Cliente();
            MySqlCommand cmdCliente = new MySqlCommand();
            cmdCliente.CommandType = CommandType.Text;
            cmdCliente.CommandText = "SELECT * FROM usuariostt_web WhERE id= @p_id";
            cmdCliente.Connection = sqlConexion;

            MySqlParameter p_id = new MySqlParameter();
            p_id.MySqlDbType = MySqlDbType.Int16;
            p_id.Direction = System.Data.ParameterDirection.Input;
            p_id.ParameterName = "@p_id";

            cmdCliente.Parameters.Add(p_id);
            p_id.Value = id;

            MySqlDataReader lector;//datareader para leer la informacion del Command
            using(sqlConexion)
            {
                sqlConexion.Open();
                lector = cmdCliente.ExecuteReader();
                while (lector.Read())//Bucle para leer el lector el cual solo tendra una Fila, por tanto recorremos el lector que almacena esta informacion en forma de Array[]
                {
                    nuevo.userID = lector[0] as string; //utilizamos as para que pueda utilizar el valor Default de la clase string en caso de ser Null
                    nuevo.codigoPick = lector[1] as string;
                    nuevo.password = lector[2] as string;
                    nuevo.nombre = lector[3] as string;
                    nuevo.tipo = lector[4] as string;
                    nuevo.delegaciones = lector[5] as string;
                    nuevo.nif = lector[6] as string;
                    nuevo.ient = lector[7] as string;
                    nuevo.listadoXLS = (lector[8] as int?).GetValueOrDefault(); // metodo que nos permite devolver un valor default para ese campo en caso de ser null
                    nuevo.listadoFacturasXLS = (lector[9] as int?).GetValueOrDefault();
                    nuevo.facturaXLS = (lector[10] as int?).GetValueOrDefault();
                    nuevo.ip = lector[11] as string;
                    nuevo.delegacionVGG = lector[12] as string;
                    nuevo.clienteVGG = lector[13] as string;
                    nuevo.catalogoVGG = lector[14] as string;
                    nuevo.emailContacto = lector[15] as string;
                    nuevo.emailEnviosPendientes = lector[16] as string;
                    nuevo.id = (lector[17] as int?).GetValueOrDefault();
                }
            }
            

            return nuevo;
        }

        public bool userIDCheck(string userID)// metodo public booleano que utiliza un parametro string(userID) que busca este id y devuelve true o false en caso de encontrarlo
        {
            MySqlCommand cmdUserID = new MySqlCommand();
            cmdUserID.Connection = sqlConexion;
            cmdUserID.CommandType = CommandType.Text;
            cmdUserID.CommandText = "SELECT COUNT(*) FROM usuariostt_web WHERE userid= @p_id2";

            MySqlParameter p_userID = new MySqlParameter();
            p_userID.Size = 15;
            p_userID.MySqlDbType = MySqlDbType.VarChar;
            p_userID.Direction = ParameterDirection.Input;
            p_userID.ParameterName ="@p_id2";
            cmdUserID.Parameters.Add(p_userID);

            p_userID.Value = userID;
           

            using(sqlConexion)
            {
                sqlConexion.Open();
                int c = Int32.Parse(cmdUserID.ExecuteScalar().ToString());
                if (c > 0) 
                {
                    return true;
                }
            }
            return false;
        }

        public void actualizarUserWeb(Cliente cliente)//Metodo public que nos permite Actualizar(Update) una fila de la base de datos y que utiliza como parámetro un Cliente.
        {
            MySqlCommand cmdActualizarUser = new MySqlCommand();
            cmdActualizarUser.CommandType = CommandType.Text;
            cmdActualizarUser.CommandText = " UPDATE usuariostt_web SET userid = @p_userID, codigo_pick = @p_codigoPick, password = @p_password,nombre = @p_nombre,tipo = @p_tipo,delegaciones = @p_delegaciones,nif = @p_nif,ient = @p_ient, listado_xls = @p_listadoXLS,listadofacturas_xls = @p_listadoFacturasXLS,factura_xls = @p_facturaXLS,ip = @p_ip,delegacionVGG = @p_delegacionesVGG,clienteVGG = @p_clienteVGG,catalogoVGG = @p_catalogoVGG, emailContacto = @p_emailContacto,emailEnviosPendientes = @p_emailEnviosPendientes WHERE id = @p_id3;";

            cmdActualizarUser.Connection = sqlConexion;
            //MySqlTransaction mySqlTransaction = null;

            MySqlParameter p_userId1 = new MySqlParameter();
            p_userId1.MySqlDbType = MySqlDbType.VarChar;
            p_userId1.Size = 15;
            p_userId1.Direction = System.Data.ParameterDirection.Input;
            p_userId1.ParameterName = "@p_userID";

            MySqlParameter p_codigoPick1 = new MySqlParameter();
            p_codigoPick1.MySqlDbType = MySqlDbType.VarChar;
            p_codigoPick1.Size = 12;
            p_codigoPick1.Direction = System.Data.ParameterDirection.Input;
            p_codigoPick1.ParameterName = "@p_codigoPick";

            MySqlParameter p_password1 = new MySqlParameter();
            p_password1.MySqlDbType = MySqlDbType.VarChar;
            p_password1.Size = 15;
            p_password1.Direction = System.Data.ParameterDirection.Input;
            p_password1.ParameterName = "@p_password";

            MySqlParameter p_nombre1 = new MySqlParameter();
            p_nombre1.MySqlDbType = MySqlDbType.VarChar;
            p_nombre1.Size = 50;
            p_nombre1.Direction = System.Data.ParameterDirection.Input;
            p_nombre1.ParameterName = "@p_nombre";

            MySqlParameter p_tipo1 = new MySqlParameter();
            p_tipo1.MySqlDbType = MySqlDbType.VarChar;
            p_tipo1.Size = 5;
            p_tipo1.Direction = System.Data.ParameterDirection.Input;
            p_tipo1.ParameterName = "@p_tipo";

            MySqlParameter p_delgaciones1 = new MySqlParameter();
            p_delgaciones1.MySqlDbType = MySqlDbType.VarChar;
            p_delgaciones1.Size = 70;
            p_delgaciones1.Direction = System.Data.ParameterDirection.Input;
            p_delgaciones1.ParameterName = "@p_delegaciones";

            MySqlParameter p_nif1 = new MySqlParameter();
            p_nif1.MySqlDbType = MySqlDbType.VarChar;
            p_nif1.Size = 400;
            p_nif1.Direction = System.Data.ParameterDirection.Input;
            p_nif1.ParameterName = "@p_nif";

            MySqlParameter p_ient1 = new MySqlParameter();
            p_ient1.MySqlDbType = MySqlDbType.VarChar;
            p_ient1.Size = 400;
            p_ient1.Direction = System.Data.ParameterDirection.Input;
            p_ient1.ParameterName = "@p_ient";

            MySqlParameter p_listadoXLS1 = new MySqlParameter();
            p_listadoXLS1.MySqlDbType = MySqlDbType.Int16;
            p_listadoXLS1.Direction = System.Data.ParameterDirection.Input;
            p_listadoXLS1.ParameterName = "@p_listadoXLS";

            MySqlParameter p_listadoFacturasXLS1 = new MySqlParameter();
            p_listadoFacturasXLS1.MySqlDbType = MySqlDbType.Int16;
            p_listadoFacturasXLS1.Direction = System.Data.ParameterDirection.Input;
            p_listadoFacturasXLS1.ParameterName = "@p_listadoFacturasXLS";

            MySqlParameter p_facturaXLS1 = new MySqlParameter();
            p_facturaXLS1.MySqlDbType = MySqlDbType.Int16;
            p_facturaXLS1.Direction = System.Data.ParameterDirection.Input;
            p_facturaXLS1.ParameterName = "@p_facturaXLS";

            MySqlParameter p_ip1 = new MySqlParameter();
            p_ip1.MySqlDbType = MySqlDbType.VarChar;
            p_ip1.Size = 50;
            p_ip1.Direction = System.Data.ParameterDirection.Input;
            p_ip1.ParameterName = "@p_ip";

            MySqlParameter p_delegacionVGG1 = new MySqlParameter();
            p_delegacionVGG1.MySqlDbType = MySqlDbType.VarChar;
            p_delegacionVGG1.Size = 50;
            p_delegacionVGG1.Direction = System.Data.ParameterDirection.Input;
            p_delegacionVGG1.ParameterName = "@p_delegacionesVGG";

            MySqlParameter p_clienteVGG1 = new MySqlParameter();
            p_clienteVGG1.MySqlDbType = MySqlDbType.VarChar;
            p_clienteVGG1.Size = 50;
            p_clienteVGG1.Direction = System.Data.ParameterDirection.Input;
            p_clienteVGG1.ParameterName = "@p_clienteVGG";

            MySqlParameter p_catalogoVGG1 = new MySqlParameter();
            p_catalogoVGG1.MySqlDbType = MySqlDbType.VarChar;
            p_catalogoVGG1.Size = 50;
            p_catalogoVGG1.Direction = System.Data.ParameterDirection.Input;
            p_catalogoVGG1.ParameterName = "@p_catalogoVGG";

            MySqlParameter p_emailContacto1 = new MySqlParameter();
            p_emailContacto1.MySqlDbType = MySqlDbType.VarChar;
            p_emailContacto1.Size = 150;
            p_emailContacto1.Direction = System.Data.ParameterDirection.Input;
            p_emailContacto1.ParameterName = "@p_emailContacto";

            MySqlParameter p_emailEnviosPendientes1 = new MySqlParameter();
            p_emailEnviosPendientes1.MySqlDbType = MySqlDbType.VarChar;
            p_emailEnviosPendientes1.Size = 250;
            p_emailEnviosPendientes1.Direction = System.Data.ParameterDirection.Input;
            p_emailEnviosPendientes1.ParameterName = "@p_emailEnviosPendientes";

            MySqlParameter p_id1 = new MySqlParameter();
            p_id1.MySqlDbType = MySqlDbType.Int32;
            p_id1.Direction = System.Data.ParameterDirection.Input;
            p_id1.ParameterName = "@p_id3";


            cmdActualizarUser.Parameters.Add(p_userId1);
            cmdActualizarUser.Parameters.Add(p_codigoPick1);
            cmdActualizarUser.Parameters.Add(p_password1);
            cmdActualizarUser.Parameters.Add(p_nombre1);
            cmdActualizarUser.Parameters.Add(p_tipo1);
            cmdActualizarUser.Parameters.Add(p_delgaciones1);
            cmdActualizarUser.Parameters.Add(p_nif1);
            cmdActualizarUser.Parameters.Add(p_ient1);
            cmdActualizarUser.Parameters.Add(p_listadoXLS1);
            cmdActualizarUser.Parameters.Add(p_listadoFacturasXLS1);
            cmdActualizarUser.Parameters.Add(p_facturaXLS1);
            cmdActualizarUser.Parameters.Add(p_ip1);
            cmdActualizarUser.Parameters.Add(p_delegacionVGG1);
            cmdActualizarUser.Parameters.Add(p_clienteVGG1);
            cmdActualizarUser.Parameters.Add(p_catalogoVGG1);
            cmdActualizarUser.Parameters.Add(p_emailContacto1);
            cmdActualizarUser.Parameters.Add(p_emailEnviosPendientes1);
            cmdActualizarUser.Parameters.Add(p_id1);

            cmdActualizarUser.Parameters[0].Value = cliente.userID;
            cmdActualizarUser.Parameters[1].Value = cliente.codigoPick;
            cmdActualizarUser.Parameters[2].Value = cliente.password;
            cmdActualizarUser.Parameters[3].Value = cliente.nombre;
            cmdActualizarUser.Parameters[4].Value = cliente.tipo;
            cmdActualizarUser.Parameters[5].Value = cliente.delegaciones;
            cmdActualizarUser.Parameters[6].Value = cliente.nif;
            cmdActualizarUser.Parameters[7].Value = cliente.ient;
            cmdActualizarUser.Parameters[8].Value = cliente.listadoXLS;
            cmdActualizarUser.Parameters[9].Value = cliente.listadoFacturasXLS;
            cmdActualizarUser.Parameters[10].Value = cliente.facturaXLS;
            cmdActualizarUser.Parameters[11].Value = cliente.ip;
            cmdActualizarUser.Parameters[12].Value = cliente.delegacionVGG;
            cmdActualizarUser.Parameters[13].Value = cliente.clienteVGG;
            cmdActualizarUser.Parameters[14].Value = cliente.catalogoVGG;
            cmdActualizarUser.Parameters[15].Value = cliente.emailContacto;
            cmdActualizarUser.Parameters[16].Value = cliente.emailEnviosPendientes;
            cmdActualizarUser.Parameters[17].Value = cliente.id;

            
            sqlConexion.Open();
            
            try
            {
                cmdActualizarUser.ExecuteNonQuery();
                
            }
            catch (MySqlException ex)
            {
                ex.ToString();
                
            }
            finally
            {
                sqlConexion.Close();                
            }
            

        } 
        
        public bool checkConexion()
        {
            using(sqlConexion)
            {
                try
                {
                    sqlConexion.Open();
                    if(sqlConexion.State == ConnectionState.Open)
                    {
                        return true;
                    }
                }
                catch
                {
                    
                }
            }

            return false;
        }

        public string datosUsuario(int id)// Metodo public que devuelve un String con la informacion de usuario,password utilizando como parametro el ID de ese Cliente.
        {
            StringBuilder sb = new StringBuilder();// String builder que nos permite crear un string con ambos datos separados por un ';' y que nos permite utilizar el metodo Split
            MySqlCommand cmdUser = new MySqlCommand();
            cmdUser.CommandType = CommandType.Text;
            cmdUser.CommandText = "SELECT userid, password FROM usuariostt_web WHERE id=@p_id";
            cmdUser.Connection = sqlConexion;

            cmdUser.Parameters.Add(new MySqlParameter("p_id", id));

            using(sqlConexion)
            {
                sqlConexion.Open();
                MySqlDataReader lectorUser = cmdUser.ExecuteReader();
                while(lectorUser.Read())
                {
                    string s = lectorUser.GetValue(1) as string;
                    if (s != null)
                    {
                        sb.Append(lectorUser.GetString(0));
                        sb.Append(";");
                        sb.Append(lectorUser.GetString(1));
                    }
                    else
                    {
                        sb.Append(lectorUser.GetString(0));
                        sb.Append(";");
                        sb.Append("NO PASS,NULL");
                    }
                    
                    

                    return sb.ToString();
                }
            }           

            return null;
        }

        public string usuarioID(string userID)// Metodo public String que devuelve el id de una Fila utilizando el parametro userID, ya que este es UK(Unique Key)
        {
            MySqlCommand cmdUserID = new MySqlCommand();
            cmdUserID.CommandType = CommandType.Text;
            cmdUserID.CommandText = "SELECT id FROM usuariostt_web WHERE userid=@p_user";
            cmdUserID.Connection = sqlConexion;

            cmdUserID.Parameters.Add(new MySqlParameter("p_user",MySqlDbType.VarChar,15));
            cmdUserID.Parameters[0].Value = userID;

            using (sqlConexion)
            {
                sqlConexion.Open();
                MySqlDataReader l = cmdUserID.ExecuteReader();

                while(l.Read())
                {
                    return l.GetValue(0).ToString();
                }
            }

            return null;
        }

    }
}

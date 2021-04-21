using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UsuariosWeb.Clases;

namespace UsuariosWeb
{
    public partial class frmConfig : MaterialForm
    {
        Configuracion configuracion; // Clase Configuracion que es la que se utiliza tanto para cargar la informacion como para actualizarla en el caso de darse.
        
        public frmConfig()
        {
            InitializeComponent();
            configuracion = WebUser.cargarConfiguracion(); // al iniciar el Form utilizamos el metodo static que nos devuelve una configuracion o crea una nueva en caso de no existir ninguna
            
        }

        private void frmConfig_Load(object sender, EventArgs e)//evento Load que carga los datos de configuracion en las cajas de texto
        {
            txtDataBase.Text = configuracion.dataBase;
            txtHost.Text = configuracion.host;
            txtPassword.Text = configuracion.stringPwd;
            txtPuerto.Text = configuracion.port;
            txtUsuario.Text = configuracion.usuario;
            
            llenarList(WebUser.getDelegaciones());
            if (configuracion.mensaje.Count != 0)// si la variable mensajes(List<string>) no esta vacia escribe el mensaje en el Textbox 
            {
                StringBuilder sb = new StringBuilder(); // Utilizamos un string builder para personalizar el mensaje utilzando rtf/ansi que nos permite utilizar codigo Html 

                sb.AppendLine(@"{\rtf1\ansi");
                foreach (string s in WebUser.GetMensaje())// bucle que recorre cada linea del mensaje y la añade a la clase Stringbuilder y añade una linea en formato html
                {
                    sb.AppendLine(s);
                    sb.Append(@" \line ");
                }
                sb.Replace("URL", @"\b URL \b0 ");// reemplaza la palabra URL por otra que nos permite poner en Bold esta parte del texto
                sb.Replace("Usuario", @"\b Usuario\b0 ");
                sb.Replace("Password", @"\b Password \b0 ");


                txtMensaje.Rtf = sb.ToString();// propiedad RTF del RichTextBox que nos permite utilizar  RTF/Ansi
            }
            else//Si la lista de mensajes esta vacia, crearemos un mensaje "predeterminado" utilizando la propiedad ForeColor del TextBox y poniendola en Brown para que quede notorio que no es un mensaje guardado en la Configuracón
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"{\rtf1\ansi");
                sb.Append(" Hola, los datos para revisar el tracking son los siguientes:");
                sb.Append(@" \line ");
                sb.Append(@" \line ");
                sb.Append(@"\b URL: \b0 ");
                sb.Append("http://extranet.transnatur.com/extranet/conectar.aspx");
                sb.Append(@" \line ");
                sb.Append(@"\b Usuario: \b0 [usuario]");
                sb.Append(@" \line ");
                sb.Append(@"\b Password: \b0 [password] ");
                sb.Append(@" \line "); sb.Append(@" \line ");
                sb.Append("Saludos,");

                txtMensaje.Rtf = sb.ToString();
                txtMensaje.ForeColor = Color.Brown;
            }




        }

        private void btnAceptar_Click(object sender, EventArgs e)//guarda o actualiza la informacion de configuracion que esta en las cajas de texto y cierra el form
        {
            configuracion.host = txtHost.Text;
            configuracion.dataBase = txtDataBase.Text;
            configuracion.port = txtPuerto.Text;
            configuracion.stringPwd = txtPassword.Text;
            configuracion.usuario = txtUsuario.Text;

            configuracion.mensaje.Clear();// al actualizar la informacion limpiamos la lista de mensajes 
            if (!string.IsNullOrWhiteSpace(txtMensaje.Text)) // si lo que hay en la TextBox del mensaje no esta vacio o solo formado por espacios en blanco 
            {
                foreach (string s in txtMensaje.Lines) // añadimos cada linea del TextBox a la List<string> de mensajes.
                {
                    configuracion.mensaje.Add(s);
                }
            }

            WebUser.guardarConfiguracion(configuracion); // actualiazamos la informacion por el metodo statico guardarConfiguracion de webUser que realiza el Update de
            WebUser.actualizarDelegaciones(crearLista());
            this.Close();// cerramos el form.
        }

        private void llenarList(List<Delegacion> delegs) // Añade filas a la ListView a partir de una List de delegaciones 
        {
            foreach (Delegacion s in delegs)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Text = s.numDelegacion;
                listViewItem.SubItems.Add(s.nomDelegacion);
                lvDelegaciones.Items.Add(listViewItem);
            }
        }

        private void btnAnadir_Click(object sender, EventArgs e)
        {
            
            int sw = 0;
            foreach (ListViewItem s in lvDelegaciones.Items)
            {
                if (s.Text.CompareTo(txtNumDelegacion.Text)==0)
                {
                    sw++;
                    s.ForeColor = Color.Red;       
                    
                }
                else
                    s.ForeColor = Color.Black;
            }
            if (sw == 0 && !string.IsNullOrEmpty(txtNumDelegacion.Text))
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Text = txtNumDelegacion.Text;
                listViewItem.SubItems.Add(txtNombreDelegacion.Text);
                lvDelegaciones.Items.Add(listViewItem);

                txtNombreDelegacion.Text = "";
                txtNumDelegacion.Text = "";
                txtNumDelegacion.Focus();
            }

        }

        private void btnQuitar_Click(object sender, EventArgs e) //Evento click del boton Quitar que nos permite borrar un elemento de la listView
        {
            if(lvDelegaciones.SelectedItems.Count>0)
                lvDelegaciones.SelectedItems[0].Remove();
        }

        private List<Delegacion> crearLista() // Crea una lista de Delegaciones desde cada elemento/fila de la ListView
        {
            List<Delegacion> nueva = new List<Delegacion>();

            foreach(ListViewItem lv  in lvDelegaciones.Items)
            {
                Delegacion delegacion = new Delegacion();
                delegacion.id = lv.Index+1;
                delegacion.numDelegacion = lv.Text;
                delegacion.nomDelegacion = lv.SubItems[1].Text;

                nueva.Add(delegacion);
            }

            return nueva;
        }

        private void txtNumDelegacion_KeyDown(object sender, KeyEventArgs e)//Evento del 2 TextBox que nos permite realizar la misma accion que el boton Añadir
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.btnAnadir_Click(this, new EventArgs());
            }
        }

        private void lvDelegaciones_KeyDown(object sender, KeyEventArgs e)//Evento KeyDown del listView que nos permite borrar de la lista el elemento seleccionado dandole a la tecla Suprimir
        {
            if(e.KeyCode == Keys.Delete)
            {
                if(lvDelegaciones.SelectedItems.Count>0)
                {
                    lvDelegaciones.SelectedItems[0].Remove();
                }
            }
        }

        private void lvDelegaciones_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            
        }

     
    }
}

using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using UsuariosWeb.Clases;

namespace UsuariosWeb
{
    public partial class frmPrincipal : MaterialForm
    {
        DataTable t_userWeb = new DataTable();// variable dataTable utilizada para el DataSource del DataGridView que devuelve los datos de Búsqueda2

        WebUser webUser; //Clase webUser que contiene todos los metodos back que conectan con la base de datos y la LiteDB local para almanecar los datos de configuracion
        FormWebUser formWeb= new FormWebUser();//variable formWebUser que se abre para dar de alta un cliente nuevo.
        FormWebUser formWebClonar = new FormWebUser();// variable formWeb user que se abre para clonar un cliente seleccionado del grid.
        public frmPrincipal()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;                    
            materialSkinManager.ColorScheme = new ColorScheme(
              Primary.Blue600, Primary.Blue700,
              Primary.Blue800, Accent.LightBlue400,
              TextShade.WHITE
              );

            formWeb.onCerrar += FormWeb_onCerrar;//metodos para los eventos onCerrar del fromwebuser
            formWebClonar.onCerrar += FormWeb_onCerrar;
            webUser = new WebUser();

        }

        private void FormPrincipal_Load(object sender, EventArgs e)//al cargar el Form añade los tooltips utilizado en los controles Buttton
        {            
            ttAnadir.SetToolTip(btnBuscar, "Buscar");
            ttClonar.SetToolTip(btnClonar, "Clonar User Seleccionado");
            ttNuevo.SetToolTip(btnNuevo, "Nuevo User");
            ttHelp.SetToolTip(btnHelp,"Manual de Usuario ");
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)//metodo del button Buscar que al hacer click busca por los filtros de las cajas de Texto
        {
            
            if (WebUser.existeConfiguracion())//metodo statico de la clase WebUser que determina si existe una configuraciondeconexion correcta y si no lo es nos abre un Dialog para configurar los datos deconexion
            {
                string message = "Aceptar para configurar";
                string caption = "No existe Configuracion de Conexion";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {

                    frmConfig nuevo = new frmConfig();
                    nuevo.ShowDialog();
                }
            }
            else//si existe 
            {
                gridWebUser.Rows.Clear();//limpia el grid 
                if (txtCodigo.Text.Length != 0 || txtNIF.Text.Length != 0 || txtNombre.Text.Length != 0)//determina si los campos de Búsqueda contienen algun tipo de filtro
                {
                    try//try para la busqueda en la base d datos
                    {
                        t_userWeb = webUser.filtroTabla(txtNombre.Text, txtCodigo.Text, txtNIF.Text);// utiliza el metodo filtroTabla de la clase WebUser que nos devuelve un dataTable con los datos de los usuarios buscados 
                        foreach (DataRow dr in t_userWeb.Rows)//Bucle que añade una nueva fila al DatagridView por cada fila del dataTable
                        {
                            gridWebUser.Rows.Add(dr.ItemArray);
                        }
                        lblOut.Text = "";//si existen campos de busqueda reinicia el label
                    }
                    catch(Exception ex)//si falla nos aparecera un mensaje con el tipo de fallo 
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    lblOut.Text = "No hay filtros de búsqueda"; //si no se escribe nada en las cajas de texto al buscar nos aparecera un mensaje en el Label indicando que no hay filtros de busqueda

                }
            }
        
        }

        private void gridWebUser_DoubleClick(object sender, EventArgs e)//metodo dobleCLick en el grid
        {            
            {
                if (!gridWebUser.SelectedCells.IsReadOnly)
                {
                    int row = gridWebUser.SelectedCells[0].RowIndex;//indice de la fila seleccionada al hacer dobleclick

                    int id = Int32.Parse(gridWebUser.Rows[row].Cells[0].Value.ToString());//mediante el indice busca en la celda 0 el id de ese user
                    Cliente filtrado = webUser.clienteEdit(id);//utiliza el metodo clientesEdit que nos devuelve la informacion de un cliente y abre el formWebUser con esa informacion para actualizar 
                    FormWebUser form = new FormWebUser();
                    form.AbrirCliente(filtrado);
                }
            }

        }    

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (webUser.checkConexion())
            {
                formWeb.Show();//Abre el fromweb para dar de alta un nuevo UserWeb
                lblOut.Text = "";
            }
            else
            {
                lblOut.Text = "Error de Configuración";
            }

        }

        private void FormWeb_onCerrar(object sender, CerrarEventArgs e)// funcion del evento onCrrar delformWeb 
        {
            gridWebUser.Rows.Clear(); //limpia el grid
            gridWebUser.Rows.Add(new object[] {webUser.usuarioID(e.dataRow[0]),e.dataRow[1], e.dataRow[2], e.dataRow[3], e.dataRow[4]});//añade la informacion que nos deuvelve el evento con los argumentos de CerrarEventArgs que hemos creado que contienen la ifnromacion de ese usuario que hemos dado de alta ,modificado o clonado
            StringBuilder sb = new StringBuilder();        
            
            int id = Int32.Parse(gridWebUser.Rows[0].Cells[0].Value.ToString());//id de la fila que acabamos de añadir 

            if (webUser.datosUsuario(id) != null)//metodo datosUsuario que nos devuelve un string a partir de un id de UserWeb con el usuario y pass 
            {
                if (WebUser.GetMensaje().Count != 0)//Metodo static de la clase Web user que nos devuelve la variable mensaje(List<string>) el cual si contiene algun mensaje creamos la plantilla desde lo que hemos guardado
                {
                    string[] split = webUser.datosUsuario(id).Split(';');
                    sb.AppendLine(@"{\rtf1\ansi");
                    foreach (string s in WebUser.GetMensaje())
                    {
                        sb.AppendLine(s);
                        sb.Append(@" \line ");
                    }

                    sb.Replace("[usuario]", split[0]);
                    sb.Replace("[password]", split[1]);
                    sb.Replace("URL", @"\b URL \b0 ");
                    sb.Replace("Usuario", @"\b Usuario \b0 ");
                    sb.Replace("Password", @"\b Password \b0 ");

                }

                else//en caso de que no encontremos  un mensaje guardado, creara el mensaje pretedeterminado con un aviso para poder configurar ese mensaje.
                {

                    string[] split = webUser.datosUsuario(id).Split(';');
                    sb.Append(@"{\rtf1\ansi");
                    sb.Append(@"(Mensaje predeterminado, ve a Configuración para modificar)");
                    sb.Append(@" \line ");
                    sb.Append(@" \line ");
                    sb.Append("Hola, los datos para revisar el tracking son los siguientes:");
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



                    sb.Replace("[usuario]", split[0]);
                    sb.Replace("[password]", split[1]);

                }


            }
            richTextBox1.Rtf = sb.ToString();


        }

        private void btnClonar_Click(object sender, EventArgs e)//funcion que apartir de una fila selecionada en el grid nos crea una plantilla del formwebUser con unos datos creados a partir de ese usuario
        {
            if (gridWebUser.SelectedCells.Count > 0)//filtra que haya algun campo seleccionado
            {
                int row = gridWebUser.SelectedCells[0].RowIndex;//fila seleccionada 
               
                    int id = Int32.Parse(gridWebUser.Rows[row].Cells[0].Value.ToString());//id de esa fila 
                    Cliente filtrado = webUser.clienteEdit(id);//informacion de ese cliente
                    
                    formWebClonar.ClonarCliente(filtrado); //metodo clonarCliente() que abre un formWebUser con los datos de ese cliente              
              
            }
            else//si no se selecciona nigun campo aparecera un mensaje que nos indica el error.
            {
                MessageBox.Show("Selecciona un ID");
            }
        }

        private void gridWebUser_CellMouseEnter(object sender, DataGridViewCellEventArgs e)// metodo asociado al evento mouse enter que crea un tooltip al pasar el raton por encima de una celda y nos dice que al hacer dobleclick podemos Editar esa usuario.
        {
            string s = "Doble Click Editar";
            DataGridView n = (DataGridView)sender;            
            foreach(DataGridViewRow d in n.Rows)
            {
                foreach(DataGridViewCell dgvc in d.Cells)
                {
                    dgvc.ToolTipText = s;
                }
            }
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)//metodo asociado al evento KeyDown que crea el metodo buscarClick cuando le damos a la tecla Enter mientras estamos en unas Textbox conretas
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar_Click(this, new EventArgs { });
            }
        }

        private void gridWebUser_CellClick(object sender, DataGridViewCellEventArgs e)//metodo que crea un mensaje a partir de un usuario que hacemos click
        {
            StringBuilder sb = new StringBuilder();//clase stringBuilder para crear mensaje
            if (gridWebUser.Rows.Count>0)//filtro para determinar si existe alguna fila 
            {
                int row = gridWebUser.SelectedCells[0].RowIndex;
                int id = Int32.Parse(gridWebUser.Rows[row].Cells[0].Value.ToString());//id de la fila seleccionada 

                if(webUser.datosUsuario(id)!=null)//metodo datosUsuario que nos devuelve un string a partir de un id de UserWeb con el usuario y pass 
                {
                    if (WebUser.GetMensaje().Count != 0)//Metodo static de la clase Web user que nos devuelve la variable mensaje(List<string>) el cual si contiene algun mensaje creamos la plantilla desde lo que hemos guardado
                    {
                        string[] split = webUser.datosUsuario(id).Split(';');
                        sb.AppendLine(@"{\rtf1\ansi");
                        foreach (string s in WebUser.GetMensaje())
                        {
                            sb.AppendLine(s);
                            sb.Append(@" \line ");
                        }

                        sb.Replace("[usuario]", split[0]);
                        sb.Replace("[password]", split[1]);
                        sb.Replace("URL", @"\b URL \b0 ");
                        sb.Replace("Usuario", @"\b Usuario \b0 ");
                        sb.Replace("Password", @"\b Password \b0 ");
                        
                    }

                    else//en caso de que no encontremos  un mensaje guardado, creara el mensaje pretedeterminado con un aviso para poder configurar ese mensaje.
                    {

                        string[] split = webUser.datosUsuario(id).Split(';');
                        sb.Append(@"{\rtf1\ansi");
                        sb.Append(@"(Mensaje predeterminado, ve a Configuración para modificar)");
                        sb.Append(@" \line ");
                        sb.Append(@" \line ");
                        sb.Append("Hola, los datos para revisar el tracking son los siguientes:");
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
                       
                        

                        sb.Replace("[usuario]", split[0]);
                        sb.Replace("[password]", split[1]);

                    }

                    
                }
                richTextBox1.Rtf = sb.ToString();
            }



            
       
        }        

        private void btnConfigurar_Click(object sender, EventArgs e) //Evento click del boton Config que abre un formulario de Configuracion
        {
            frmConfig frmConfig = new frmConfig();
            frmConfig.ShowDialog();
        }

        private void frmPrincipal_Activated(object sender, EventArgs e)//Evento Activated del form que utilizamos para reinicializar la clase WebUser
        {
            webUser = new WebUser();
        }

        private void button1_Click(object sender, EventArgs e)//Evento Click del boton Ayuda, que abre el PDF con el manual de Usuario
        {
            System.Diagnostics.Process.Start(@"O:\ProgramasComunes\UsuariosWeb\Documentos\UsuariosWeb(Manual).pdf");
        }
       
    }
}

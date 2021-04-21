using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using MaterialSkin.Controls;
using MaterialSkin;
using System.Collections.Generic;

namespace UsuariosWeb
{
    public delegate void CambiarTxt(TextBox textBox,string texto); // delegado que utilizamos para interactuar con el form cuando sucede el evento del otro form 
    public delegate void Cerrar(Object sender, CerrarEventArgs e); // delegado que utilizamos para el evento con los argumentos extendidos de la clase EventArgs
    public partial class FormWebUser : MaterialForm
    {
        frmDelegaciones open = new frmDelegaciones(); // creamos 2 frmDelegaciones para asociados a cada apartado delegaciones del form
        frmDelegaciones open2 = new frmDelegaciones();
        public event Cerrar onCerrar; // evento onCerrar para enviar informacion mediante este evento 
        WebUser webUser = new WebUser(); // Clase web user que utilizamos para el Behind que nos permite atacar la BaseDeDatos
        int idUpdate=0; // variable int ID que nos ayuda a determinar si el form activo va a realizar una Insert o un Update 

        public FormWebUser()
        {            
            InitializeComponent();
            onCerrar += (x, y) => { };//Funcion lambda vacia para inicializar el evento 

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            open.onEnviar += Open_onEnviar;// asignamos una funcion concreta al evento
            open2.onEnviar += Open2_onEnviar;
            cboxTipo.DataSource = tipos(); // datasource para el ComboBox Tipo
            cboxCatalogoVGG.DataSource = nombres(); // datasource para el ComboBox CatalogoVGG
            cboxTipo.DisplayMember = "Nombre";
            cboxTipo.ValueMember = "id";
        }

        private void Open2_onEnviar(object sender, NuevoEventArgs args)//funcion  del evento onEnviar
        {
            CambiarTxt f = cambiarTxtFunction; // funcion delegada que interactua con el Form 
            txtDelegacionesVGG.BeginInvoke(f, new object[] { txtDelegacionesVGG, args.delegaciones });// metodo begininvoke que utiliza la funcion delegada Cambiartxt y que utiliza como parametros un TextBox y un string proveniento de los argumentos de la Clase NuevaEventArgs
        }

        private void Open_onEnviar(object sender, NuevoEventArgs args)
        {
            CambiarTxt f = cambiarTxtFunction;
            txtDelegaciones.BeginInvoke(f, new object[] { txtDelegaciones, args.delegaciones });
        }

        private void cambiarTxtFunction(TextBox textBox, string texto)// funcion que utlizamos para el delegado que asigna a la propieda Text del TextBox el string que pasamos como parametro
        {
            textBox.Text = texto;
        }    

        private void crearCliente(Cliente cliente) // Funcion privada que crea un Cliente a partir de las TextBox.Text del form
        {
            cliente.userID = textBox(txtID);
            cliente.codigoPick = textBox(txtCodigoPick);
            cliente.password = textBox(txtPass);
            cliente.nombre = textBox(txtNombre);
            cliente.tipo = cboxTipo.SelectedValue.ToString();
            cliente.delegaciones = textBox(txtDelegaciones);
            cliente.nif = textBox(txtCIF);
            cliente.ient = textBox(txtIent);
            cliente.listadoXLS = chequeado(chboxListado);
            cliente.listadoFacturasXLS = chequeado(chboxListadoFacturas);
            cliente.facturaXLS = chequeado(chboxFactura);
            cliente.ip = textBox(txtIP);
            cliente.delegacionVGG = textBox(txtDelegacionesVGG);
            cliente.clienteVGG = textBox(txtClienteVGG);
            cliente.catalogoVGG = cboxCatalog(cboxCatalogoVGG);
            cliente.emailContacto = textBox(txtEmailContacto);
            cliente.emailEnviosPendientes = textBox(txtEmailPendientes);            
        }

        private int chequeado(CheckBox checkBox)//metodo privado que devuelve un numero dependiendo del estado del CheckBox que pasamos como parametro 
        {
            if (checkBox.Checked)
                return 1;

            return 0;
        }

        private string textBox(TextBox textBox) //metodo privado que devuelve un string dependiendo de si la TextBox utilizada como parametro contiene algun tipo de informacion en la propiedad Text, si no devuelve null
        {
            if (textBox.TextLength > 0)
            {
                return textBox.Text;
            }
            return null;
        }

        private string cboxCatalog(ComboBox cbox)//Metodo privado que devuelve null o el string del item seleccionado de un ComboBox que pasamos como parametro
        {
            if (cbox.SelectedItem == null)
            {
                return null;
            }
            else
                return cbox.SelectedItem.ToString();
        }
        private void comboBoxSelect(ComboBox comboBox, string tipo) // metodo privado que utiliza un ComboBox y un String el cual proviene de una terminologia concreta de la DB y muestra unos valores concretos en este Combobox
        {
            if (tipo.CompareTo("C") == 0)
                comboBox.SelectedIndex = 0;
            else if (tipo.CompareTo("X") == 0)
                comboBox.SelectedIndex = 1;
            else if (tipo.CompareTo("T") == 0)
                comboBox.SelectedIndex = 1;
        }

        private void check(CheckBox checkBox,int n) //metodo privado que asigna a la propiedad Checked el valor true del Combobox que pasamos como parametro dependiendo del otro parametro int
        {
            if (n == 1)
                checkBox.Checked = true;
        }

        private string[] nombres() // metodo que devuelve los nombres que  utilizaremos en el comboBox CatalogoVGG
        {
            string[] nuevo = { "APPIA_IRUN", "APPIA_TNB", "APPIA_VIG", "ADP_SQL", "APPIA_ADP_SQL", "APPIA_SQL" };
            return nuevo;
        }

        private Tipo[] tipos() 
        {
            Tipo[] nuevo = new Tipo[]
            {
            new Tipo("C","Cliente"),
            new Tipo("X","Intermediario"),
            new Tipo("T","Delegación")
            };
            return nuevo;
        }   
        
        public void AbrirCliente(Cliente cliente)// metodo publico de la clase frmUser que abre el form y muestra la informacion de un Cliente parametrizado con un ID concreto para llevar a cabo la Actualizacion de la informacion
        {
            this.Show();
            txtID.Text = cliente.userID;
            txtCodigoPick.Text = cliente.codigoPick;
            txtPass.Text = cliente.password;
            txtNombre.Text = cliente.nombre;
            comboBoxSelect(cboxTipo, cliente.tipo);
            txtDelegaciones.Text = cliente.delegaciones;
            txtCIF.Text = cliente.nif;
            txtIent.Text = cliente.ient;
            txtIP.Text = cliente.ip;
            txtClienteVGG.Text = cliente.clienteVGG;
            cboxCatalogoVGG.SelectedItem = cboxCatal(cliente.catalogoVGG);
            txtDelegacionesVGG.Text = cliente.delegacionVGG;
            txtEmailContacto.Text = cliente.emailContacto;
            txtEmailPendientes.Text = cliente.emailEnviosPendientes;
            check(chboxListado, cliente.listadoXLS);
            check(chboxFactura, cliente.facturaXLS);
            check(chboxListadoFacturas, cliente.listadoFacturasXLS);
            //btnAlta.Enabled = false;
            //btnActualizar.Enabled = true;
            idUpdate = cliente.id;

            lblTitle.Text = "EDITAR";
        }

        private string cboxCatal(string s)
        {
            if (s==null)
            {
                return null;
            }
            return s.ToUpper();
        }



        public void ClonarCliente(Cliente cliente)//metodo public de la clase frmUser que abre el form y muestra la informacion de un Cliente parametrizado sin un ID para lelvar a cabo una Insert partiendo de otro cliente que  utilizamos como plantilla   
        {            
            this.Show();
            RandomPass r = new RandomPass();
            

            txtCodigoPick.Text = cliente.codigoPick;
            txtPass.Text = cliente.password;
            txtNombre.Text = cliente.nombre;
            comboBoxSelect(cboxTipo, cliente.tipo);
            txtDelegaciones.Text = cliente.delegaciones;
            txtCIF.Text = cliente.nif;
            txtIent.Text = cliente.ient;
            txtIP.Text = cliente.ip;
            txtClienteVGG.Text = cliente.clienteVGG;
            cboxCatalogoVGG.SelectedItem = cboxCatal(cliente.catalogoVGG);
            txtDelegacionesVGG.Text = cliente.delegacionVGG;
            txtEmailContacto.Text = cliente.emailContacto;
            txtEmailPendientes.Text = cliente.emailEnviosPendientes;
            check(chboxListado, cliente.listadoXLS);
            check(chboxFactura, cliente.facturaXLS);
            check(chboxListadoFacturas, cliente.listadoFacturasXLS);            
            txtID.Text = r.randomName(cliente.nombre);
            txtID.ForeColor = Color.Red;

            lblTitle.Text = "CLONAR";


        }       

        private void btnDelegaciones_Click_1(object sender, EventArgs e)// Abre el frmDelegaciones
        {            
            open.Location = new Point(1325, 450);
            open.AbrirDelegaciones(txtDelegaciones.Text); 
        }

        private void btnDelegacionesVGG_Click(object sender, EventArgs e)
        {
            open2.Location = new Point(1150, 660);
            open2.AbrirDelegaciones(txtDelegacionesVGG.Text);
        }

        private void txtID_TextChanged(object sender, EventArgs e) // Evento que se genera al cambiar el texto de un determinado TextBox 
        {
            txtID.ForeColor = Color.Black; // al editar el Text de un TextBox este cambia la propiedad ForeColor a Black. 
        }      
        private void materialButton1_Click_1(object sender, EventArgs e)// Evento Click que utiliza la clase RandomPass y el metodo AutoPasss para generar una contraseña aleatoria.
        {
            RandomPass random = new RandomPass();
            txtPass.Text = random.AutoPass();
        }

        private void btnAceptar_Click(object sender, EventArgs e) // Evento click del boton Aceptar que realiza un Alta Nueva o Actualiza información dependiendo de la variable idUpdate
        {
            if(idUpdate!=0) // si al crear el form asignamos un valor al idUpdate nos indica que se trata de un cliente concreto con un id concreto, por tanto llevara a cabo una Modificacion
            {
                Cliente clienteActualizar = new Cliente();
                crearCliente(clienteActualizar);
                clienteActualizar.id = idUpdate;
                try
                {
                    webUser.actualizarUserWeb(clienteActualizar);
                    MessageBox.Show("Modificado");                   
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo completar");
                    txtID.Focus();
                }
            }
            else// en el caso de que esta variable sea 0 ya se trate de un Cliente Nuevo, o proveniente de una Clonación llevara a cabo un Alta.
            {
                if (!webUser.userIDCheck(txtID.Text))// comprueba mediante el metodoidCheck que existe este id de usuario ya que se trata de una clave unica y si no existe crea el cliente y y manda esta informacion mediante el evento onCerrar
                {
                    Cliente nuevo = new Cliente();
                    crearCliente(nuevo);
                    //try
                    //{
                        webUser.insertClienteWeb(nuevo);// lleva a cabo la Insert
                        MessageBox.Show("Alta realizada con éxito");
                        List<String> lista = new List<string>();
                        lista.Add(nuevo.userID);
                        lista.Add(nuevo.codigoPick); 
                        lista.Add(nuevo.nombre);
                        lista.Add(nuevo.nif);
                        lista.Add(nuevo.tipo);                        

                        onCerrar(this,new CerrarEventArgs(lista));
                        this.Close();
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.Message);
                    //    txtID.Focus();
                    //}
                }
                else
                {
                    MessageBox.Show("Ya existe ese UserID");// si ya existe este campo de clave UK nos lo indicara mediante este mensaje.
                    txtID.Focus();
                }
                
            }
        }

        private void FormWebUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
  
}

using MaterialSkin.Controls;
using System;
using System.Text;
using System.Windows.Forms;
using UsuariosWeb.Clases;

namespace UsuariosWeb
{
    public delegate void NuevoEvento(Object sender, NuevoEventArgs args); // delegado que utilizamos para crear un evento concreto para mandar la información al otro form almacenada en la clase extendida de EventArgs
    public partial class frmDelegaciones : MaterialForm
    {

        public string delegaciones { get; set; } // propiedad/variable string que utilizamos para mandar la informacion mediant el evento.
        public event NuevoEvento onEnviar;        
        public frmDelegaciones()
        {
            InitializeComponent();
            onEnviar += (x, y) => { }; // funcion lambda vacia que inicializa el NuvoEvento onEnviar
            WebUser.cargarDelegaciones();
        }
        private void FormDelegaciones_Load(object sender, EventArgs e) // Carga la Lista de delegaciones desde las delegaciones guardadas en la Base de Datos Local
        {
            foreach (Delegacion s in WebUser.getDelegaciones())//bucle que recorre la List de Delegaciones y las añade al chboxlist
            {
                chboxLista.Items.Add(s.numDelegacion + "-" + s.nomDelegacion);
            }
        }      

        private void btnAceptar_Click(object sender, EventArgs e)//Funcion del evento Click para el control btnAceptar 
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < chboxLista.CheckedItems.Count; i++) // recorre la lista de chechBox del form que tienen la propiedad Check a true 
            {
                string[] num = chboxLista.CheckedItems[i].ToString().Split('-'); //Separa cada item de la lista mediante el metodo split para enviar solamente el numero de la Delegación 
                builder.Append(num[0]);
                builder.Append(","); //vamos creando un string compuesto por el numero de la delegación y una coma
            }
            if(builder.Length>0)
            {
                builder.Remove(builder.Length - 1, 1); // si existen letras en el builder, es decir, hemos seleccionado algunas checkbox quitamos la ultima coma del stringbuilder 
                delegaciones = builder.ToString(); 
            }
            else
            {
                delegaciones = null; // si no tenemos nada seleccionado asignamos el valor null a delegaciones para que no de fallo. 
            }
            
            onEnviar(this, new NuevoEventArgs(delegaciones)); // realizamos el evento onEnviar mandando el string que hemos creado y asignado a delegaciones para utilizarlo en el form principal.
            this.Close();
        }
        public void AbrirDelegaciones(string del) // Metodo public que utilizamos para abrir el formulario utilizando como parametro un string que filtra los checkbox de nuestro form.
        {
            chboxLista.Items.Clear();//Limpia la chboxlist
            limpiarCheckList();//
            foreach (Delegacion s in WebUser.getDelegaciones())//recorre las delegaciones de la base de datos local y las añade al chboxlist 
            {
                chboxLista.Items.Add(s.numDelegacion+"-"+s.nomDelegacion);
            }
            if (!string.IsNullOrWhiteSpace(del)) //si el string que pasamos como parametro es != null o esta formado por espacios en blanco 
            {
                string[] split = del.Split(',');//dividimos la palabra que estara separada por ','(comas)
                if (split.Length > 1)//si se divide en mas de 1 elemento
                {
                    foreach (string s in split)//recorremos las distintas palabras del array split, y dependiendo de si ese numero de delegacion esta en la lista, cambiamos a true el estado Check del chexbox
                    {
                        for (int i = 0; i < chboxLista.Items.Count; i++)
                        {
                            if (chboxLista.GetItemText(chboxLista.Items[i]).Contains(s))
                            {
                                chboxLista.SetItemChecked(i, true);
                            }                            
                        }
                    }
                }
                else if (del.Length>2)
                {
                    for (int i = 0; i < chboxLista.Items.Count; i++)
                    {
                        if (chboxLista.GetItemText(chboxLista.Items[i]).Contains(del))
                        {
                            chboxLista.SetItemChecked(i, true);
                        }
                    }
                }
            }
            else
            {

            }

            this.ShowDialog();

        }  
        
        private void limpiarCheckList()// metodo privado que deja en false, todos los checkbox del form
        {
            for(int i = 0;i<chboxLista.Items.Count;i++)
            {
                chboxLista.SetItemChecked(i, false);
            }
        }
        
        private void frmDelegaciones_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
    
}

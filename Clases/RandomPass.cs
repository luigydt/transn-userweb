using System;
using System.Text;


namespace UsuariosWeb
{
    class RandomPass 
    {
        private string RandomString()// Metodo privado que devuelve un string creado de manera aleatoria para la generacion de Contraseñas 
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < 3; i++)// Bucle de 4 repeticiones que en cada repeticion crea un Char a partir de la clase MathRandom y la añade al StringBuilder para crear el string
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            
            return builder.ToString().ToLower();// devolvemos este string builder convertido a minusculas 
        }

        private int RandomNumber()// metodo privado que devuelve un int utilzando la clase random que devuelve un numero desde 0 hasta 99999 
        {           
            Random random = new Random();            
            int num = random.Next(99999);
            return num;
        }

        public string AutoPass() // metodo public que devuelve un string concatenando los metodos anteriores para crear una unica password compuesta de numeros y letras generados de manera aleatoria
        {
            string chars = RandomString();
            int numbers = RandomNumber();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(chars);
            stringBuilder.Append(numbers);

            return stringBuilder.ToString(); ;
        }

        public string randomName(string s) //Metodo public que devuelve un string creado a partir de un algoritmo creado que va recorriendo las distintas palabras por las que esta compuesto el string separadas por espacios y que  utilizamos para crear una sugerencia de nombredeusuario a partir de un nombre de cliente.
        {
            StringBuilder sb = new StringBuilder();
            string[] split = s.ToLower().Split(' ');

            sb.Append(quitarSignos(split[0]).ToLower());

            if (split.Length >= 1)
            {
                for (int i = 1; i < split.Length; i++)
                {
                    if (quitarSignos(split[i]).Length > 2)
                    {
                        sb.Append(quitarSignos(split[i]).Substring(0, 3));
                    }
                    else
                    {
                        sb.Append(quitarSignos(split[i]).Substring(0, quitarSignos(split[i]).Length));
                    }

                }
            }

            return sb.ToString();
            
        }

        public string quitarSignos(string s)// metodo que devuelve un string a partir de uno que pedimos como parametro el cual queremos quitar cualquier signo , es decir solo queremos que este compuesto por letras y numeros 
        {
            StringBuilder sb = new StringBuilder();

            foreach(char c in s)
            {
                if(char.IsDigit(c) || char.IsLetter(c) ) // recorre cada char del string y dependiendo de si es digito o letra la añadimos al stringbuilder, si no, la dejamos pasar.
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().ToLower(); // devolvemos este string en minusculas
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEUCrtPortBelly.Clases
{
    public class Correos
    {
        private string correo;
        private string asunto;
        private string mensaje;

        public string Correo
        {
            get { return correo; }   // get method
            set { correo = value; }  // set method
        }
        public string Asunto
        {
            get { return asunto; }
            set { asunto = value; }
        }
        public string Mensaje
        {
            get { return mensaje; }
            set { mensaje = value; }
        }
    }
}

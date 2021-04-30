using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.ConexionServicios
{
    public class conexionServidor
    {
        public string url { get; set; }
        public conexionServidor() {
            this.url = "https://localhost:44308/";
        }

    }
}

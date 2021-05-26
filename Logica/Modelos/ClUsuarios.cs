using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Modelos
{
    public class ClUsuarios
    {
        public virtual long idUser { get; set; }

        public virtual long? idInscrito { get; set; }
        public virtual string Usuario { get; set; }
        public virtual string Password { get; set; }
        public virtual string Nombres { get; set; }
        public virtual string Apellidos { get; set; }
        public virtual int tipoUser { get; set; }
    }
}

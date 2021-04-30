using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Modelos
{
    public class ClPrueba
    {
        public virtual long IdPrueba { get; set; }
        public virtual long IdInscrito { get; set; }
        public virtual long? IdHistorialPuntaje { get; set; }
        public virtual double? PunjatePrueba { get; set; }
        public virtual string FechaPrueba { get; set; } 

    }
}

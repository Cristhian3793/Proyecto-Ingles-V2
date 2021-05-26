using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Modelos
{
  public  class ClNivelesInscrito
    {
        public virtual long IDNIVELESTUDIANTE { get; set; }
        public virtual long? IDNIVEL { get; set; }
        public virtual long? IDESTADONIVEL { get; set; }
        public virtual long? IDINSCRITO { get; set; }

        public virtual long? IDPRUEBAUBICACION { get; set;}
        public virtual string FECHAREGISTRO { get; set; }

        public virtual int PRUEBA { get; set; }
        public virtual int ESTADONIVEL { get; set; }
        public virtual long IDPERIODOINSCRIPCION { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Modelos
{
    public class ClLicenciasEstudiante
    {
        public virtual long IDLICENCIAESTUDIANTE { get; set; }
        public virtual long IDLICENCIA { get; set; }
        public virtual long IDINSCRITO { get; set; }
        public virtual long IDNIVELESTUDIANTE { get; set; }

    }
}

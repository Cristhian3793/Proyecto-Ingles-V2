using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Modelos
{
    public class ClNivel
    {
        public virtual long idNivel { get; set; }
        public virtual long? idEstadoNivel { get; set; }
        public virtual long? idLibro { get; set; }
        public virtual long? idTipoNivel { get; set; }
        public virtual long idCurso { get; set; }
        public virtual string codNivel { get; set; }
        public virtual string nomNivel { get; set; }
        public virtual string descNivel { get; set; }
        public virtual double costoNIvel { get; set; }
    }
}

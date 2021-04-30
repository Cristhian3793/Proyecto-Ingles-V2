using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Modelos
{
    public class ClPeriodoInscripcion
    {
        public virtual int IdPeriodoInscripcion { get; set; }
        public virtual string Periodo { get; set; }
        public virtual int AnoLectivo { get; set; }
        public virtual string CodPeriodoInscripcion { get; set; }
        public virtual DateTime FechaInicio { get; set; }
        public virtual DateTime FechaFin { get; set; }
        public virtual int EstadoPeriodo { get; set; }
    }
}

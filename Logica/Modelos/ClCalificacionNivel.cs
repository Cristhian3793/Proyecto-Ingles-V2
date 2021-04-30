using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Modelos
{
    public class ClCalificacionNivel
    {
        public virtual long idCalificacionNivel { get; set; }
        public virtual long idNivel { get; set; }
        public virtual double calificacionNivelDesde { get; set; }

        public virtual double calificacionNivelHasta { get; set; }
    }
}

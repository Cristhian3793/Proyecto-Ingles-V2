using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Modelos
{
    public class ClHorarios
    {
        public virtual int IdHorarios { get; set; }
        public virtual int IdCurso { get; set; }
        public virtual int IdDiaHorario { get; set; }
        public virtual string HoraInicioHorario { get; set; }
        public virtual string HoraFinHorario { get; set; }

    }
}

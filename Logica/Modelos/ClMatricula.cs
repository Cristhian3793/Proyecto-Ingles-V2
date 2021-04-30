using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Modelos
{
    public class ClMatricula
    {
        public int IdMatricula { get; set; }
        public int IdTipoDescuento { get; set; }
        public int IdInscrito { get; set; }
        public int IdEstudiante { get; set; }
        public string NumAutorizacion { get; set; }
        public string FechaRegMatricula { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Modelos
{
    public class ClEstadoEstudiante
    {
        public virtual int IdEstadoEstudiante {get;set;}

        public virtual int CodEstadoEstu { get; set; }
        public virtual string DescEstEstudiante { get; set; }
    }
}

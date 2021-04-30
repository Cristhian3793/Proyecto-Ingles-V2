using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Modelos
{
    public class ClEstadoNivel
    {
        public virtual int IdEstadoNivel { get; set; }
        public virtual string DescEstadoNivel { get; set; }

        public virtual List<ClNivel> nivel { get; set; }




    }
}

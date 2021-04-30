using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Modelos
{
    public class ClCurso
    {
        public virtual int IdCurso { get; set; }

        public virtual string CodCurso { get; set; }
        public virtual string DescCurso { get; set; }
        public virtual string  FechaCreacionCurso { get; set; }

    }
}

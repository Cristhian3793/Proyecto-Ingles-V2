using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Modelos
{
    public class ClNota
    {
        public virtual long IDNOTA { get; set;}
        public virtual long IDINSCRITO { get; set; }
        public virtual long IDNIVEL { get; set; }
        public virtual long IDTEMA { get; set; }
        public virtual double CALIFICACION { get; set; }
        public virtual int ESTADO { get; set; }
        public virtual long IDNIVELESTUDINTE { get; set; }
    }
}

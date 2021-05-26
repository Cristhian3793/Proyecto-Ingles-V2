using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Modelos
{
    public class ClMatricula
    {
        public virtual long IDNIVELESTUDIANTE { get; set; }
        public virtual long IDINSCRITO { get; set; }
        public virtual string NUMDOCINSCRITO { get; set; }
        public virtual long IDNIVEL { get; set; }
        public virtual int IDESTADONIVEL { get; set; }
        public virtual string CODNIVEL { get; set; }
        public virtual string NOMNIVEL { get; set; }
        public virtual int PRUEBA { get; set; }

    }
}

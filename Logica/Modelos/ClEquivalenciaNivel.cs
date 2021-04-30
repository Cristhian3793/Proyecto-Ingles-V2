using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Modelos
{
    public class ClEquivalenciaNivel
    {
        public virtual int IdEquivalenciaNivel { get; set; }
        public virtual long idNivelAut { get; set; }
        public virtual long idNivelPro { get; set; }

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Modelos
{
    public class ClLicenciaCambridge
    {
        public virtual long IdLicencia { get; set; }
        public virtual long? IdLibro { get; set; }
        public virtual string Licencia { get; set; }        
        public virtual int EstadoLicencia { get; set; }      
        public virtual DateTime FechaEmision { get; set; }
    }
}

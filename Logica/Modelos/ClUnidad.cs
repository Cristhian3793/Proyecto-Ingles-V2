using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Modelos
{
    public class ClUnidad
    {
        public virtual long idNomUnidad { get; set; }
        public virtual long idNivel { get; set; }
        public virtual string codNomUnidad { get; set; }
        public virtual string NomUnidad { get; set; }
        public virtual string desNomUnidad { get; set; }
    }
}

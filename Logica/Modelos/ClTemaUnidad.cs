using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Modelos
{
    public class ClTemaUnidad
    {
        public virtual long idTemaUnidad {get; set;}
        public virtual long idNomUnidad { get; set; }
        public virtual string codTemaUnidad { get; set; }
        public virtual string descTemaUnidad { get; set; }
    }
}

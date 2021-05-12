using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Logica.Modelos;
namespace Logica.Mapping
{
   public class mapPrueba:ClassMap<ClPrueba>
    {
        public mapPrueba()
        {
            Id(x => x.IdPrueba).Column("IDPRUEBA");
            Map(x => x.IdInscrito).Column("IDINSCRITO");
            Map(x => x.IdHistorialPuntaje).Column("IDHISTORIALPUNTAJE");
            Map(x => x.PunjatePrueba).Column("PUNTAJEPRUEBA");
            Map(x => x.FechaPrueba).Column("FECHAPRUEBA");
            Map(x => x.IDNIVELESTUDIANTE).Column("IDNIVELESTUDIANTE");
            
            Table("PRUEBA");
        }

    }
}

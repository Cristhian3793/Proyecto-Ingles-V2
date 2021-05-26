using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Logica.Modelos;
namespace Logica.Mapping
{
    class mapNivelesInscrito:ClassMap<ClNivelesInscrito>
    {
        public mapNivelesInscrito()
        {
            Id(x => x.IDNIVELESTUDIANTE).Column("IDNIVELESTUDIANTE");
            Map(x => x.IDNIVEL).Column("IDNIVEL");
            Map(x => x.IDESTADONIVEL).Column("IDESTADONIVEL");
            Map(x => x.IDINSCRITO).Column("IDINSCRITO");
            Map(x => x.IDPRUEBAUBICACION).Column("IDPRUEBAUBICACION");
            Map(x => x.FECHAREGISTRO).Column("FECHAREGISTRO");
            Map(x => x.PRUEBA).Column("PRUEBA");
            Map(x => x.ESTADONIVEL).Column("ESTADONIVEL");
            Map(x => x.IDPERIODOINSCRIPCION).Column("IDPERIODOINSCRIPCION"); 
            Table("NIVELESINSCRITO");
        }
    }
}

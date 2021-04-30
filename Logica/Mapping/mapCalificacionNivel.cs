using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Logica.Modelos;
namespace Logica.Mapping
{
    public class mapCalificacionNivel:ClassMap<ClCalificacionNivel>
    {
        public mapCalificacionNivel() {
            Id(x => x.idCalificacionNivel).Column("IDCALIFICACIONNIVEL");
            Map(x => x.idNivel).Column("IDNIVEL");
            Map(x => x.calificacionNivelDesde).Column("CALIFICACIONDESDE");
            Map(x => x.calificacionNivelHasta).Column("CALIFICACIONHASTA");
            Table("CALIFICACION_NIVEL");
        }
    }
}

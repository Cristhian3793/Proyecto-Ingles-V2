using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Logica.Modelos;
namespace Logica.Mapping
{
    public class mapPeriodoInscripcion:ClassMap<ClPeriodoInscripcion>
    {
        public mapPeriodoInscripcion() {
            Id(x => x.IdPeriodoInscripcion).Column("IDPERIODOINSCRIPCION");
            Map(x => x.Periodo).Column("PERIODO");
            Map(x => x.AnoLectivo).Column("ANOLECTIVO");
            Map(x => x.CodPeriodoInscripcion).Column("CODPERIODOINSCRIPCION");
            Map(x => x.FechaInicio).Column("FECHAINICIO");
            Map(x => x.FechaFin).Column("FECHAFIN");
            Map(x => x.EstadoPeriodo).Column("ESTADOPERIODO");
            Table("PERIODO_INSCRIPCION");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Logica.Modelos;
namespace Logica.Mapping
{
    public class mapEstadoPeriodo:ClassMap<ClEstadoPeriodo>
    {
        public mapEstadoPeriodo()
        {
            Id(x => x.IDESTADOPERIODO).Column("IDESTADOPERIODO");
            Map(x => x.DESCESTADOPERIODO).Column("DESCESTADOPERIODO");
            Map(x => x.CODESTADOPERIODO).Column("CODESTADOPERIODO");
            Table("ESTADO_PERIODO");

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Logica.Modelos;
namespace Logica.Mapping
{
    class mapDiasHorario :ClassMap<ClDiasHorarios>
    {
        public mapDiasHorario()
        {
            Id(x => x.IdDiaHorario).Column("IDDIAHORARIO");
            Map(x => x.DiaHorario).Column("DIAHORARIO");
            Table("DIAS_HORARIOS");

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Logica.Modelos;
namespace Logica.Mapping
{
    class mapHorarios :ClassMap<ClHorarios>
    {
        public mapHorarios()
        {
            Id(x => x.IdHorarios).Column("IDHORARIOS");
            Map(x => x.IdCurso).Column("IDCURSO");
            Map(x => x.IdDiaHorario).Column("IDDIAHORARIO");
            Map(x => x.HoraInicioHorario).Column("HORAINICIOHORARIO");
            Map(x => x.HoraFinHorario).Column("HORAFINHORARIO");
            Table("HORARIOS");
        }
    }
}

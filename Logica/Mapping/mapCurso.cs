using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Logica.Modelos;
namespace Logica.Mapping
{
    public class mapCurso: ClassMap<ClCurso>
    {
        public mapCurso()
        {
            Id(x => x.IdCurso).Column("IDCURSO");
            Map(x => x.CodCurso).Column("CODCURSO");
            Map(x => x.DescCurso).Column("DESCCURSO");
            Map(x => x.FechaCreacionCurso).Column("FECHACREACIONCURSO");
            Table("CURSO");
        }

    }
}

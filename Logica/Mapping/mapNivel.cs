using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Logica.Modelos;

namespace Logica.Mapping
{
    public class mapNivel: ClassMap<ClNivel>
    {
        public mapNivel() {
            Id(x => x.idNivel).Column("IDNIVEL");
            Map(x => x.idEstadoNivel).Column("IDESTADONIVEL");
            Map(x => x.idLibro).Column("IDLIBRO");
            Map(x => x.idTipoNivel).Column("IDTIPONIVEL");
            Map(x => x.idCurso).Column("IDCURSO");
            Map(x => x.codNivel).Column("CODNIVEL");
            Map(x => x.nomNivel).Column("NOMNIVEL");
            Map(x => x.descNivel).Column("DESCNIVEL");
            Map(x => x.costoNIvel).Column("COSTONIVEL");
            Table("NIVEL");
        }
    }
}
     
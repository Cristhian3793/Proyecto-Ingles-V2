using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Logica.Modelos;
namespace Logica.Mapping
{
    public class mapTipoNivel:ClassMap<ClTipoNivel>
    {
        public mapTipoNivel() {
            Id(x => x.idtipoNivel).Column("IDTIPONIVEL");
            Map(x => x.descTipoNivel).Column("DESCTIPONIVEL");
            Map(x => x.codTipoNivel).Column("CODTIPONIVEL");
            Table("TIPO_NIVEL");
        }
    }
}

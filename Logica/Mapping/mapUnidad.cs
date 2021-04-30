using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Logica.Modelos;
namespace Logica.Mapping
{
    public class mapUnidad:ClassMap<ClUnidad>
    {
        public mapUnidad() {
            Id(x => x.idNomUnidad).Column("IDNOMUNIDAD");
            Map(x=>x.idNivel).Column("IDNIVEL");
            Map(x => x.codNomUnidad).Column("CODNOMUNIDAD");
            Map(x => x.NomUnidad).Column("NOMUNIDAD");
            Map(x => x.desNomUnidad).Column("DESCNOMUNIDAD");
            Table("NOMBREUNIDAD");

        }
    }
}

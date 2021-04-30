using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Logica.Modelos;
namespace Logica.Mapping
{
    class mapTemaUnidad:ClassMap<ClTemaUnidad>
    {
        public mapTemaUnidad() {
            Id(x => x.idTemaUnidad).Column("IDTEMAUNIDAD");
            Map(x => x.idNomUnidad).Column("IDNOMUNIDAD");
            Map(x => x.codTemaUnidad).Column("CODTEMAUNIDAD");
            Map(x => x.descTemaUnidad).Column("DESCTEMAUNIDAD");
            Table("TEMAUNIDAD");
        }
    }
}

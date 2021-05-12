using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Logica.Modelos;
namespace Logica.Mapping
{
   public class mapEstadoPrueba :ClassMap<ClEstadoPrueba>
    {
        public mapEstadoPrueba()
        {
            Id(x => x.IdEstadoPrueba).Column("IDESTADOPRUEBA");
            Map(x => x.CodEstadoPrueba).Column("CODESTADOPRUEBA");
            Map(x => x.DescEstadoPrueba).Column("DESCESTADOPRUEBA");
            Table("ESTADO_PRUEBA");


        }
    }
}

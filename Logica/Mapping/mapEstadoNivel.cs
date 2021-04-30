using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Logica.Modelos;
namespace Logica.Mapping
{
    public class mapEstadoNivel:ClassMap<ClEstadoNivel>
    {
    public mapEstadoNivel()
    {
            Id(x => x.IdEstadoNivel).Column("IDESTADONIVEL");
            Map(x => x.DescEstadoNivel).Column("DESCESTADONIVEL");
            //HasMany<ClNivel>(x => x.IdEstadoNivel).KeyColumn("IDESTADONIVEL");
            Table("ESTADO_NIVEL");


        }
}
}
 
 
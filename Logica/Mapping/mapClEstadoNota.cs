using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Logica.Modelos;
namespace Logica.Mapping
{
    public class mapClEstadoNota:ClassMap<ClEstadoNota>
    {
        public mapClEstadoNota()
        {
            Id(x => x.CODESTADONOTA).Column("CODESTADONOTA");
            Map(x => x.DESESTADONOTA).Column("DESESTADONOTA");
            Map(x => x.IDESTADONOTA).Column("IDESTADONOTA");
            Table("ESTADO_NOTA");
        }

    }
}

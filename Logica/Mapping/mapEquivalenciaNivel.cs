using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Logica.Modelos;

namespace Logica.Mapping
{
    public class mapEquivalenciaNivel : ClassMap<ClEquivalenciaNivel>
    {
        public mapEquivalenciaNivel()
        {
            Id(x => x.IdEquivalenciaNivel).Column("IDNIVELEQUIVALENCIA");
            Map(x => x.idNivelAut).Column("IDNIVELAUT");
            Map(x => x.idNivelPro).Column("IDNIVELPRO");   
            Table("EQUIVALENCIA_NIVEL");
        }
    }
}

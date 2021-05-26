using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
using FluentNHibernate.Mapping;
namespace Logica.Mapping
{
    public class mapNotas :ClassMap<ClNota>
    {
        public mapNotas()
        {

            Id(x => x.IDNOTA).Column("IDNOTA");
            Map(x => x.IDINSCRITO).Column("IDINSCRITO");
            Map(x => x.IDNIVEL).Column("IDNIVEL");
            Map(x => x.IDTEMA).Column("IDTEMA");
            Map(x => x.CALIFICACION).Column("CALIFICACION");
            Map(x => x.ESTADO).Column("ESTADO");
            Map(x => x.IDNIVELESTUDINTE).Column("IDNIVELESESTUDIANTE");
            
            Table("NOTAS");

        }
    }
}

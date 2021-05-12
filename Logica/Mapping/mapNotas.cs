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
            Map(x => x.UNIT_1).Column("UNIT_1");
            Map(x => x.DONE_1).Column("DONE_1");
            Map(x => x.UNIT_2).Column("UNIT_2");
            Map(x => x.DONE_2).Column("DONE_2");
            Map(x => x.UNIT_3).Column("UNIT_3");
            Map(x => x.DONE_3).Column("DONE_3");
            Map(x => x.CHECK_POINT).Column("CHECK_POINT");
            Map(x => x.UNIT_4).Column("UNIT_4");
            Map(x => x.DONE_4).Column("DONE_4");
            Map(x => x.UNIT_5).Column("UNIT_5");
            Map(x => x.DONE_5).Column("DONE_5");
            Map(x => x.UNIT_6).Column("UNIT_6");
            Map(x => x.DONE_6).Column("DONE_6");
            Map(x => x.ESTADO).Column("ESTADO");
            Table("NOTAS");

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
using FluentNHibernate.Mapping;
namespace Logica.Mapping
{
    class mapNivelPogramado : ClassMap<ClNivelesProgramado>
    {
        public mapNivelPogramado() {

                Id(x => x.idNIvelProgramado).Column("IDNIVELPRO");
                Map(x => x.idNivel).Column("IDNIVEL");
                Table("NIVELES_PROGRAMADO");
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Logica.Modelos;
namespace Logica.Mapping
{
    public class mapNivelAutonomo : ClassMap<ClNivelesAutonomos>
    {
        public mapNivelAutonomo(){
            Id(x => x.idNIvelAutonomo).Column("IDNIVELAUT");
            Map(x => x.idNivel).Column("IDNIVEL");
            Table("NIVELES_AUTONOMO");       
        }
    }
}

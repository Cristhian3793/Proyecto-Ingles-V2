using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Logica.Modelos;
using FluentNHibernate.Mapping;
namespace Logica.Mapping
{
    class mapLicenciasEstudiante : ClassMap<ClLicenciasEstudiante>
    {
        public mapLicenciasEstudiante()
        {
            Id(x => x.IDLICENCIAESTUDIANTE).Column("IDLICENCIAESTUDIANTE");
            Map(x => x.IDLICENCIA).Column("IDLICENCIA");
            Map(x => x.IDINSCRITO).Column("IDINSCRITO");
            Map(x => x.IDNIVELESTUDIANTE).Column("IDNIVELESTUDIANTE");
            Table("LICENCIASESTUDIANTE");

        }
    

    }
}

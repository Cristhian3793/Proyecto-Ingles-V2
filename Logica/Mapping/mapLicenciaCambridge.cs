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
    class mapLicenciaCambridge :ClassMap<ClLicenciaCambridge>
    {
        public mapLicenciaCambridge() {
            Id(x => x.IdLicencia).Column("IDLICENCIA");
            Map(x => x.IdLibro).Column("IDLIBRO");
            Map(x => x.Licencia).Column("LICENCIA");
            Map(x => x.EstadoLicencia).Column("ESTADOLICENCIA");
            Map(x => x.FechaEmision).Column("FECHAEMISION");
            Table("LICENCIAS_CAMBRIDGE");
        }
    }
}

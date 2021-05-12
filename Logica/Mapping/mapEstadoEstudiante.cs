
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Logica.Modelos;
namespace Logica.Mapping
{
    class mapEstadoEstudiante : ClassMap<ClEstadoEstudiante>
    {
        public mapEstadoEstudiante()
        {
            Id(x => x.IdEstadoEstudiante).Column("IDESTADOESTUDIANTE");
            Map(x => x.CodEstadoEstu).Column("CODESTADOESTU");
            Map(x => x.DescEstEstudiante).Column("DESCESTESTUDIANTE");
            Table("ESTADO_ESTUDIANTE");


        }
    }
}

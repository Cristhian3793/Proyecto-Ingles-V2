using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
using FluentNHibernate.Mapping;
using NHibernate;
namespace Logica.Mapping
{
    public class mapPreguntasRespuestas : ClassMap<ClPreguntasRespuestas>
    {
        public mapPreguntasRespuestas() {
            Id(x => x.IDPREGUNTA).Column("IDPREGUNTA");
            Map(x => x.PREGUNTA).Column("PREGUNTA");
            Map(x => x.RESPUESTA).Column("RESPUESTA");
            Table("PREGUNTASYRESPUESTAS");
        }
    }
}

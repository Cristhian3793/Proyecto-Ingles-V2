using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Logica.Modelos;
namespace Logica.Mapping
{
    class mapTipoEstudiante: ClassMap<ClTipoEstudiante>
    {
        public mapTipoEstudiante()
        {
            Id(x => x.IdTipoEstudiante).Column("IDTIPOESTUDIANTE");
            Map(x => x.DescTipoEstudiante).Column("DESCRTIPOESTUDIANTE");
            Table("TIPO_ESTUDIANTE");
        }
    }
}

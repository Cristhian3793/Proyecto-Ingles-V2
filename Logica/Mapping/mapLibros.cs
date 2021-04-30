using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Logica.Modelos;
namespace Logica.Mapping
{
    public class mapLibros :ClassMap<CLLibros>
    {
        public mapLibros() {
            Id(x => x.idLibro).Column("IDLIBRO");
            Map(x => x.codLibro).Column("CODLIBRO");
            Map(x => x.descLibro).Column("DESCLIBRO");
            Table("LIBROS");
        }
    }
}

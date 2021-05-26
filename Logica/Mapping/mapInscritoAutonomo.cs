using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Logica.Modelos;
namespace Logica.Mapping
{
    public class mapInscritoAutonomo: ClassMap<ClInscritoAutonomo>
    {
        public mapInscritoAutonomo()
        {
            Id(x => x.IdInscrito).Column("IDINSCRITO");
            Map(x => x.IdTipoDocumento).Column("IDTIPODOCUMENTO");
            Map(x => x.IdTipoEstudiante).Column("IDTIPOESTUDIANTE");
            Map(x => x.IdEstadoEstudiante).Column("IDESTADOESTUDIANTE");
            Map(x => x.NombreInscrito).Column("NOMBINSCRITO");
            Map(x => x.ApellidoInscrito).Column("APELLIINSCRITO");
            Map(x => x.NumDocInscrito).Column("NUMDOCINSCRITO").Not.Nullable();
            Map(x => x.CeluInscrito).Column("CELUINSCRITO");
            Map(x => x.TelefInscrito).Column("TELEFINSCRITO");
            Map(x => x.DirecInscrito).Column("DIRECINSCRITO");
            Map(x => x.EmailInscrito).Column("EMAILINSCRITO");
            Map(x => x.FechaRegistro).Column("FECHAREGISTRO");
            Map(x => x.EstadoPrueba).Column("ESTADOPRUEBA");
            Map(x => x.InformacionCurso).Column("INFORMACIONCURSO");
            Table("INSCRITO_AUTONOMO");
        }
    }
}

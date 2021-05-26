using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Modelos
{
    public class ClInscritoAutonomo
    {
        public virtual long IdInscrito { get; set;}
        public virtual long IdTipoDocumento { get; set; }
        public virtual int IdTipoEstudiante { get; set; }
        public virtual int IdEstadoEstudiante { get; set; }
        public virtual string NombreInscrito { get; set; }
        public virtual string ApellidoInscrito { get; set; }
        public virtual string NumDocInscrito { get; set; }
        public virtual string CeluInscrito { get; set; }
        public virtual string TelefInscrito { get; set; }
        public virtual string DirecInscrito { get; set; }
        public virtual string EmailInscrito { get; set; }
        public virtual string FechaRegistro { get; set; }
        public virtual int EstadoPrueba { get; set; }
        public virtual string InformacionCurso { get; set; }
    }
}

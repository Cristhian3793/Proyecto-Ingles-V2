using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
namespace Logica.ModelosDAO
{
    interface TipoEstudianteDAO
    {
        IList<ClTipoEstudiante> getTipoEstudiante();
    }
}

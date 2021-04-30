using Logica.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.ModelosDAO
{
    interface CalificacionNivelDAO
    {
        IList<ClCalificacionNivel> getCalificacionNivel();
        void InsertarCalificacionNivel(ClCalificacionNivel cur);
        string eliminarCalificacionNivel(long id);
        bool actualizarCalificacionNivel(ClCalificacionNivel cur, long id);
        IList<ClCalificacionNivel> getCalificacionNivelXId(long id);
    }
}

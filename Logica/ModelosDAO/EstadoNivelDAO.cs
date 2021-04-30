using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
namespace Logica.ModelosDAO
{
    interface EstadoNivelDAO
    {
        IList<ClEstadoNivel> getEstadoNivel();
        void InsertarEstadoNivel(ClEstadoNivel cur);
        string eliminarEstadoNivel(int codigo);
        bool actualizarEstadoNivel(ClEstadoNivel cur, int codigo);
        IList<ClEstadoNivel> getEstadoNivelxId(int codigo);
    }
}

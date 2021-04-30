using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
namespace Logica.ModelosDAO
{
    interface TipoNivelDAO
    {
        IList<ClTipoNivel> getTipoNivel();
        void InsertarTipoNivel(ClTipoNivel tipNivel);
        string eliminarTipoNivel(int idTipoNivel);
        bool actualizarTipoNivel(ClTipoNivel tipNivel, int idTipoNivel);
        IList<ClTipoNivel> getTipoNivelxId(int idTipoNivel);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
namespace Logica.ModelosDAO
{
    interface EquivalenciaNivelDAO
    {
        IList<ClEquivalenciaNivel> getEquivalenciaNivel();
        void InsertarEquivalenciaNivel(ClEquivalenciaNivel equiniv);
        string eliminarEquivalenciaNivel(long idEqui);
        bool actualizarEquivalenciaNivel(ClEquivalenciaNivel equiniv, long idEqui);
        IList<ClEquivalenciaNivel> getEquivalenciaNivelxCodigo(long idEqui);
    }
}

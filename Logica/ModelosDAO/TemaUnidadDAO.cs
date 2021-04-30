using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
namespace Logica.ModelosDAO
{
    public interface TemaUnidadDAO
    {
        IList<ClTemaUnidad> getTemasUnidad();
        void InsertarTemaUnidad(ClTemaUnidad unidad);
        string eliminarTemaUnidad(long id);
        bool actualizarTemaUnidad(ClTemaUnidad unidad, long id);
        IList<ClTemaUnidad> getTemaUnidadxIdNivel(long id);
    }
}

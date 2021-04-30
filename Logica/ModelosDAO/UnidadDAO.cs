using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
namespace Logica.ModelosDAO
{
    interface UnidadDAO
    {
        IList<ClUnidad> getUnidades();
        void InsertarUnidad(ClUnidad unidad);
        string eliminarUnidad(long id);
        bool actualizarUnidad(ClUnidad unidad, long id);
        IList<ClUnidad> getUnidadxIdNivel(long id);
    }
}

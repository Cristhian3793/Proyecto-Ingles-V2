using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
namespace Logica.ModelosDAO
{
    interface PruebaDAO
    {
        IList<ClPrueba> getPrueba();
        void InsertarPrueba(ClPrueba cur);
        string eliminarPrueba(long codigo);
        bool actualizarPrueba(ClPrueba cur, long codigo);
        IList<ClPrueba> getPruebaXCodigo(long codigo);
    }
}

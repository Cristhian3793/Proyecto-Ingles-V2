using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
namespace Logica.ModelosDAO
{
    interface PeriodoInscripcion
    {
        IList<ClPeriodoInscripcion> getPeriodoInscripcion();
        void InsertarPeriodoInscripcion(ClPeriodoInscripcion perInsc);
        string eliminarPeriodoInscripcion(int idPeriodo);
        bool actualizarPeriodoInscripcion(ClPeriodoInscripcion perInsc, int idPeriodo);
        IList<ClPeriodoInscripcion> getPeriodoInscripcionXid(int idPeriodo);

    }
}

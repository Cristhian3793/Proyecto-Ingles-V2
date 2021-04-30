using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
namespace Logica.ModelosDAO
{
    interface DiasHorariosDAO
    {
        IList<ClDiasHorarios> getDiaHorario();
        void InsertarDiaHorario(ClDiasHorarios diah);
        string eliminarDiaHorario(int id);
        bool actualizarDiaHorario(ClDiasHorarios diah, int id);
        IList<ClDiasHorarios> getDiaHorarioXId(int id);
    }
}

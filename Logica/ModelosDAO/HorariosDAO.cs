using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
namespace Logica.ModelosDAO
{
    interface HorariosDAO
    {
        IList<ClHorarios> getHorario();
        void InsertarHorario(ClHorarios horario);
        string eliminarHorario(int id);
        bool actualizarHorario(ClHorarios horario, int id);
        IList<ClHorarios> getHorarioXCurso(int idcurso);
    }
}

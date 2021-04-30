using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
namespace Logica.ModelosDAO
{
    interface NivelesProgramadosDAO
    {
        IList<ClNivelesProgramado> getNivelProgramado();
        void InsertarNivelProgramado(ClNivelesProgramado niv);
        string eliminarNivelProgramado(long idNIvel);
        bool actualizarNivelprogramado(ClNivelesProgramado cur, long idNIvel);
        IList<ClNivelesProgramado> getNivelProgramadoxId(long idNivel);
    }
}

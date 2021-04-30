using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
namespace Logica.ModelosDAO
{
    interface NivelesAutonomosDAO
    {
        IList<ClNivelesAutonomos> getNivelAutonomo();
        void InsertarNivelAutonomo(ClNivelesAutonomos niv);
        string eliminarNivelAutonomo(long idNIvel);
        bool actualizarNivelAutonomo(ClNivelesAutonomos cur, long idNIvel);
        IList<ClNivelesAutonomos> getNivelAutonomoxId(long idNivel);
    }
}

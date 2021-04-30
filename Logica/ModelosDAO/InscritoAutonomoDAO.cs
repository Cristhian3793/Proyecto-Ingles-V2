using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
namespace Logica.ModelosDAO
{
    interface InscritoAutonomoDAO
    {
        IList<ClInscritoAutonomo> getInscritosAutonomos();
        void InsertarInscritoAutonomo(ClInscritoAutonomo insA);
        string eliminarInscritoAutonomo(long id);
        bool actualizarInscritoAutonomo(ClInscritoAutonomo insA,long idInscrito);
        IList<ClInscritoAutonomo> getInscritoAXNumDoc(string numdoc);

        bool actualizarNivel(ClInscritoAutonomo insA, long idInscrito);

    }
}

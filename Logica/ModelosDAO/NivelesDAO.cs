using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
using NHibernate;
namespace Logica.ModelosDAO
{
    interface NivelesDAO
    {
        IList<ClNivel> getNivel();
        void InsertarNivel(ClNivel cur);
        string eliminarNivel(long idNivel);
        bool actualizarNivel(ClNivel cur, long idnivel);
        IList<ClNivel> getNivelxCod(string codigo);
    }
}

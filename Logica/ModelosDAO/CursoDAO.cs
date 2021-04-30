using Logica.Conexion;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
namespace Logica.ModelosDAO
{
    interface CursoDAO
    {
        IList<ClCurso> getCurso();
        void InsertarCurso(ClCurso cur);
        string eliminarCurso(string codigo);
        bool actualizarCurso(ClCurso cur, string codigo);
        IList<ClCurso> getCursoXCodigo(string codigo);

    }
}

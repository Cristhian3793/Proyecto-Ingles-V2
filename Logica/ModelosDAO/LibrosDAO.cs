using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
namespace Logica.ModelosDAO
{
    interface LibrosDAO
    {
        IList<CLLibros> getLibros();
        void InsertarLibros(CLLibros libro);
        string eliminarLibros(long idLibro);
        bool actualizarLibros(CLLibros insA, long idLibro);
        IList<CLLibros> getLibrosXCod(string cod);
    }
}

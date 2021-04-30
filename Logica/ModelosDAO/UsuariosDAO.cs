using Logica.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.ModelosDAO
{
    interface UsuariosDAO
    {
        IList<ClUsuarios> getUsuarios();
        void InsertarUsuario(ClUsuarios usuario);
        string eliminarUsuario(long id);
        bool actualizarUsuario(ClUsuarios usuario, long id);
        IList<ClUsuarios> getUsuarioxNumDoc(string numDoc);
    }
}

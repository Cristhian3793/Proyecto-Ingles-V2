using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Logica.Modelos;
using Logica.Servicios;
namespace Services.Controllers
{
    public class UsuariosController : ApiController
    {
        ServiciosUsuarios serv = new ServiciosUsuarios();
        [HttpGet]
        public IList<ClUsuarios> GetUsuarios()
        {
            return serv.getUsuarios();
        }
        [HttpPost]
        public void PostUsuario(ClUsuarios usuario)
        {
            serv.InsertarUsuario(usuario);
        }
        [HttpDelete]
        public string DeleteUsuario(long id)
        {
            return serv.eliminarUsuario(id);
        }
        [HttpGet]
        public IList<ClUsuarios> GetUsuarioxDoc(string numdoc)
        {
            return serv.getUsuarioxNumDoc(numdoc);
        }
        [HttpPut]
        public bool UpdateUsuario(ClUsuarios user, long id)
        {
            return serv.actualizarUsuario(user, id);
        }
    }
}

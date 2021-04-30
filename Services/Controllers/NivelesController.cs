using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Logica.Modelos;
using Logica.Servicios;
using NHibernate;
namespace Services.Controllers
{
    public class NivelesController : ApiController
    {
        ServiciosNiveles serv = new ServiciosNiveles();
        [HttpGet]
        public IList<ClNivel> GetNiveles()//obtener todos los niveles
        {
            return serv.getNivel();
        }
        [HttpPost]
        public void PostinsertarNivel(ClNivel niv)//insertar un nivel
        {     serv.InsertarNivel(niv);
        }
        [HttpDelete]
        public string DeleteNivel(long id)//borrar un nivel
        {
            return serv.eliminarNivel(id);
        }
        [HttpGet]
        public IList<ClNivel> GetNivelxCodigo(string codigo)//obtener un nivel x codigo
        {
            return serv.getNivelxCod(codigo);
        }
        [HttpPut]
        public bool UpdateNivel(ClNivel insA, long idnivel)//actualizar nivel
        {
            return serv.actualizarNivel(insA, idnivel);
        }
    }
}

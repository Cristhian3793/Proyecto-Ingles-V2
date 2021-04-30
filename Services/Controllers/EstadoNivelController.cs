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
    public class EstadoNivelController : ApiController
    {
        ServiciosEstadoNivel serv = new ServiciosEstadoNivel();
        [HttpGet]
        public IList<ClEstadoNivel> GetEstadoNivel()//obtener todos los Estados Niveles
        {
            return serv.getEstadoNivel();
        }
        [HttpPost]
        public void PostEstadoNivel(ClEstadoNivel estnivel)//insertar Estados Niveles
        {
            serv.InsertarEstadoNivel(estnivel);
        }
        [HttpDelete]
        public string DeleteEstadoNivel(int idEstNivel)//borrar un Estados Niveles
        {
            return serv.eliminarEstadoNivel(idEstNivel);
        }
        [HttpGet]
        public IList<ClEstadoNivel> GetEstadoNivel(int idEstNivel)//obtener un  Estados Niveles
        {
            return serv.getEstadoNivelxId(idEstNivel);
        }
        [HttpPut]
        public bool UpdateEstadoNivel(ClEstadoNivel estnivel, int idEstNivel)//actualizar Estados Niveles
        {
            return serv.actualizarEstadoNivel(estnivel, idEstNivel);
        }
    }
}

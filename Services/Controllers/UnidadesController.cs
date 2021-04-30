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
    public class UnidadesController : ApiController
    {
        ServiciosUnidad serv = new ServiciosUnidad();
        [HttpGet]
        public IList<ClUnidad> GetUnidad()
        {
            return serv.getUnidades();
        }
        [HttpPost]
        public void PostUnidad(ClUnidad unidad)
        {
            serv.InsertarUnidad(unidad);
        }
        [HttpDelete]
        public string DeleteUnidad(long id)
        {
            return serv.eliminarUnidad(id);
        }
        [HttpGet]
        public IList<ClUnidad> GetUnidadXIdNivel(long id)
        {
            return serv.getUnidadxIdNivel(id);
        }
        [HttpPut]
        public bool UpdateUnidad(ClUnidad unidad, long id)
        {
            return serv.actualizarUnidad(unidad, id);
        }
    }
}

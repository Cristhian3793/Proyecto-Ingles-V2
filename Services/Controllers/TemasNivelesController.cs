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
    public class TemasNivelesController : ApiController
    {
        ServiciosTemaUnidad serv = new ServiciosTemaUnidad();
        [HttpGet]
        public IList<ClTemaUnidad> GetTemaUnidad()
        {
            return serv.getTemasUnidad();
        }
        [HttpPost]
        public void PostTemaUnidad(ClTemaUnidad unidad)
        {
            serv.InsertarTemaUnidad(unidad);
        }
        [HttpDelete]
        public string DeleteTemaUnidad(long id)
        {
            return serv.eliminarTemaUnidad(id);
        }
        [HttpGet]
        public IList<ClTemaUnidad> GetTemaUnidadXIdNivel(long id)
        {
            return serv.getTemaUnidadxIdNivel(id);
        }
        [HttpPut]
        public bool UpdateTemaUnidad(ClTemaUnidad unidad, long id)
        {
            return serv.actualizarTemaUnidad(unidad, id);
        }
   
}
}

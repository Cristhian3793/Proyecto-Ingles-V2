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
    public class PruebaController : ApiController
    {
        ServiciosPrueba serv = new ServiciosPrueba();
        [HttpGet]
        public IList<ClPrueba> GetPrueba() //obtener todos los pruebas
        {
            return serv.getPrueba();
        }
        [HttpPost]
        public void PostinsertarPrueba(ClPrueba cur)//insertar pruebas
        {
            serv.InsertarPrueba(cur);
        }
        [HttpDelete]
        public string DeletePrueba(long codigo)//borrar pruebas
        {
            return serv.eliminarPrueba(codigo);
        }
        [HttpGet]
        public IList<ClPrueba> GetCursoxPrueba(long codigo)//obtener pruebas
        {
            return serv.getPruebaXCodigo(codigo);
        }
        [HttpPut]
        public bool UpdatePrueba(ClPrueba insA, long codigo)//actualizar pruebas
        {
            return serv.actualizarPrueba(insA, codigo);
        }
    }
}

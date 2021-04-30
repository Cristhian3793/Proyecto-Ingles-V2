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
    public class PeriodoInscripcionController : ApiController
    {
        ServiciosPeriodoInscripcion serv = new ServiciosPeriodoInscripcion();
        [HttpGet]
        public IList<ClPeriodoInscripcion> GetPeriodoInscripcion()//obtener todos los Periodos Inscripcion
        {
            return serv.getPeriodoInscripcion();
        }
        [HttpPost]
        public void PostInsertarPeriodoInscripcion(ClPeriodoInscripcion perIns)//insertar un Periodo de Inscripcion
        {
            serv.InsertarPeriodoInscripcion(perIns);
        }
        [HttpDelete]
        public string DeletePeriodoInscripcion(int idper)//borrar un periodo de inscripcion
        {
            return serv.eliminarPeriodoInscripcion(idper);
        }
        [HttpGet]
        public IList<ClPeriodoInscripcion> GetPeriodoInscripcionXId(int idper)//obtener periodo x id
        {
            return serv.getPeriodoInscripcionXid(idper);
        }
        [HttpPut]
        public bool UpdateInscritosAutonomoXCedula(ClPeriodoInscripcion perInsc, int idper)//actualizar PeriodoInscripcion
        {
            return serv.actualizarPeriodoInscripcion(perInsc, idper);
        }
    }
}

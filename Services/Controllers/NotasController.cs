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
    public class NotasController : ApiController
    {
        ServicioNotas serv = new ServicioNotas();
        [HttpGet]
        public IList<ClNota> GetNotas()
        {
            return serv.getNotas();
        }
        [HttpPost]
        public void PostInsertarNotas(ClNota no)
        {
            serv.InsertarNotas(no);
        }
        [HttpDelete]
        public string DeleteNotas(long idnot)
        {
            return serv.eliminarNota(idnot);
        }
        [HttpGet]
        public IList<ClNota> GetNotaxId(long idIns)
        {
            return serv.getNotasxIns(idIns);
        }
        [HttpPut]
        public bool UpdateNotas(ClNota nota, long idNota)
        {
            return serv.actualizarNotas(nota, idNota);
        }
        [HttpPatch]
        public bool UpdateEstadoNotas(ClNota nota, long idNota)
        {
            return serv.actualizarEstadoNotas(nota, idNota);
        }
        
    }
}

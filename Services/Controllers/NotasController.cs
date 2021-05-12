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
        public IList<ClNota> GetNotas() //obtener todos los pruebas
        {
            return serv.getNotas();
        }
        [HttpPost]
        public void PostInsertarNotas(ClNota no)//insertar pruebas
        {
            serv.InsertarNotas(no);
        }
        [HttpDelete]
        public string DeleteNotas(long idnot)//borrar pruebas
        {
            return serv.eliminarNota(idnot);
        }
        [HttpGet]
        public IList<ClNota> GetNotaxId(long idIns)//obtener pruebas
        {
            return serv.getNotasxIns(idIns);
        }
        [HttpPut]
        public bool UpdateNotas(ClNota nota, long idNota)//actualizar pruebas
        {
            return serv.actualizarNotas(nota, idNota);
        }
    }
}

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
    public class InscritoAutonomoController : ApiController
    {
        ServiciosInscritoAutonomo serv = new ServiciosInscritoAutonomo();
        [HttpGet]
        public IList<ClInscritoAutonomo> GetInscritosAutonomos()//obtener todos los inscritos autonomos
        {
            return serv.getInscritosAutonomos();
        }
        [HttpPost]
        public void PostinsertarInscritoAutonomo(ClInscritoAutonomo insA)//insertar un inscrito autonomos
        {
            serv.InsertarInscritoAutonomo(insA);
        }
        [HttpDelete]
        public string DeleteInscritoAutonomo(long id)//borrar un inscrito autonomo
        {
            return serv.eliminarInscritoAutonomo(id);
        }
        [HttpGet]
        public IList<ClInscritoAutonomo> GetGetInscritosAutonomoXDocumento(string numdoc)//obtener inscrito x numdocumento
        {
            return serv.getInscritoAXNumDoc(numdoc);
        }
        [HttpPut]
        public bool UpdateInscritosAutonomo(ClInscritoAutonomo insA, long idInscrito)//actualizar inscrito
        {
            return serv.actualizarInscritoAutonomo(insA, idInscrito);
        }
        //[HttpPatch]
        //public bool UpdateNivel(ClInscritoAutonomo insA, long idInscrito)//actualizar nivel
        //{
        //    return serv.actualizarNivel(insA, idInscrito);
        //}


    }
}

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
    public class NivelesInscritoController : ApiController
    {
        ServiciosNivelesInscrito serv = new ServiciosNivelesInscrito();
        [HttpGet]
        public IList<ClNivelesInscrito> GetNivelesIns()//obtener todos los niveles
        {
            return serv.getNivelIns();
        }
        [HttpPost]
        public void PostinsertarNivelIns(ClNivelesInscrito niv)//insertar un nivel
        {
            serv.InsertarNivelIns(niv);
        }
        [HttpDelete]
        public string DeleteNivel(long id)//borrar un nivel
        {
            return serv.eliminarNivelIns(id);
        }
        [HttpGet]
        public IList<ClNivelesInscrito> GetNivelInscritoxId(long id)//obtener un nivel x codigo
        {
            return serv.getNivelInsxCod(id);
        }
        [HttpPut]
        public bool UpdateNivelIns(ClNivelesInscrito niv, long id)//actualizar nivel
        {
            return serv.actualizarNivelIns(niv, id);
        }
        [HttpPatch]
        public bool UpdateEstadoNivel(ClNivelesInscrito niv, long id)//actualizar nivel
        {
            return serv.actualizarEstadoNivelInscrito(niv, id);
        }
        
    }
}

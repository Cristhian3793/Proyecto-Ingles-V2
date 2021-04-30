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
    public class EquivalenciaNivelController : ApiController
    {
        ServiciosEquivalenciaNivel serv = new ServiciosEquivalenciaNivel();
        [HttpGet]
        public IList<ClEquivalenciaNivel> GetEquivalenciaNIvel()//obtener todos los niveles equivalentes
        {
            return serv.getEquivalenciaNivel();
        }
        [HttpPost]
        public void PostEquivalenciaNivel(ClEquivalenciaNivel equiniv)//insertar un niveles equivalentes
        {
            serv.InsertarEquivalenciaNivel(equiniv);
        }
        [HttpDelete]
        public string DeleteEquivalenciaNivel(long codEquivNivel)//borrar un niveles equivalentes
        {
            return serv.eliminarEquivalenciaNivel(codEquivNivel);
        }
        [HttpGet]
        public IList<ClEquivalenciaNivel> GetEquivalenciaNivel(long codEquivNivel)//obtener un niveles equivalentes
        {
            return serv.getEquivalenciaNivelxCodigo(codEquivNivel);
        }
        [HttpPut]
        public bool UpdateCurso(ClEquivalenciaNivel equiniv, long codEquivNivel)//actualizar un niveles equivalentes
        {
            return serv.actualizarEquivalenciaNivel(equiniv, codEquivNivel);
        }
    }
}

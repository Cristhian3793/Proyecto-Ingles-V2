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
    public class TipoNivelController : ApiController
    {
        ServiciosTipoNivel serv = new ServiciosTipoNivel();
        [HttpGet]
        public IList<ClTipoNivel> GetTipoNivel()//obtener todos los tipos de nivel
        {
            return serv.getTipoNivel();
        }
        [HttpPost]
        public void PostInsertarTipoNivel(ClTipoNivel tipnivel)//insertar un tipo nivel
        {
            serv.InsertarTipoNivel(tipnivel);
        }
        [HttpDelete]
        public string DeleteTipoNivel(int idTipoNivel)//borrar un tipo nivel
        {
            return serv.eliminarTipoNivel(idTipoNivel);
        }
        [HttpGet]
        public IList<ClTipoNivel> GetTipoNivelxid(int idTipoNivel)//obtener un tipo nivel x id
        {
            return serv.getTipoNivelxId(idTipoNivel);
        }
        [HttpPut]
        public bool UpdateTipoNivel(ClTipoNivel tipNIvel, int idTipoNivel)//actualizar tipo nivel
        {
            return serv.actualizarTipoNivel(tipNIvel, idTipoNivel);
        }
    }
}

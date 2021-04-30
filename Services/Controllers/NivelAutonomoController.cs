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
    public class NivelAutonomoController : ApiController
    {
        ServiciosNivelesAutonomos serv = new ServiciosNivelesAutonomos();
        [HttpGet]
        public IList<ClNivelesAutonomos> GetNivelesAutonomos()//obtener todos los niveles
        {
            return serv.getNivelAutonomo();
        }
        [HttpPost]
        public void PostinsertarNivelAutonomo(ClNivelesAutonomos niv)//insertar un nivel
        {
            serv.InsertarNivelAutonomo(niv);
        }
        [HttpDelete]
        public string DeleteNivelAutonomo(long id)//borrar un nivel
        {
            return serv.eliminarNivelAutonomo(id);
        }
        [HttpGet]
        public IList<ClNivelesAutonomos> GetNivelAutonomoxId(long id)//obtener un nivel x codigo
        {
            return serv.getNivelAutonomoxId(id);
        }
        [HttpPut]
        public bool UpdateNivelAutnomo(ClNivelesAutonomos niv, long id)//actualizar nivel
        {
            return serv.actualizarNivelAutonomo(niv, id);
        }
    }
}

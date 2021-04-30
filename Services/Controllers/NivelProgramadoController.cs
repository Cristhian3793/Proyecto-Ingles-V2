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
    public class NivelProgramadoController : ApiController
    {
        ServiciosNivelesProgramado serv = new ServiciosNivelesProgramado();
        [HttpGet]
        public IList<ClNivelesProgramado> GetNivelesProgramado()//obtener todos los niveles
        {
            return serv.getNivelProgramado();
        }
        [HttpPost]
        public void PostinsertarNivelProgramado(ClNivelesProgramado niv)//insertar un nivel
        {
            serv.InsertarNivelProgramado(niv);
        }
        [HttpDelete]
        public string DeleteNivelProgramado(long id)//borrar un nivel
        {
            return serv.eliminarNivelProgramado(id);
        }
        [HttpGet]
        public IList<ClNivelesProgramado> GetNivelProgramadoxId(long id)//obtener un nivel x codigo
        {
            return serv.getNivelProgramadoxId(id);
        }
        [HttpPut]
        public bool UpdateNivelAutnomo(ClNivelesProgramado niv, long id)//actualizar nivel
        {
            return serv.actualizarNivelprogramado(niv, id);
        }
    }
}

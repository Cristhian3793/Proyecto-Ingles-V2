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
    public class CursoController : ApiController
    {
        
        ServiciosCurso serv = new ServiciosCurso();
        [HttpGet]
        public IList<ClCurso> GetCursos()//obtener todos los cursos
        {
            return serv.getCurso();
        }
        [HttpPost]
        public void PostinsertarCurso(ClCurso cur)//insertar un curso
        {
            serv.InsertarCurso(cur);
        }
        [HttpDelete]
        public string DeleteCurso(string codigo)//borrar un curso
        {
            return serv.eliminarCurso(codigo);
        }
        [HttpGet]
        public IList<ClCurso> GetCursoxCodigo(string codigo)//obtener un curso x codigo
        {
            return serv.getCursoXCodigo(codigo);
        }
        [HttpPut]
        public bool UpdateCurso(ClCurso insA, string codigo)//actualizar curso
        {
            return serv.actualizarCurso(insA, codigo);
        }
    }
}

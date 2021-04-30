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
    public class HorariosController : ApiController
    {
        ServiciosHorarios serv = new ServiciosHorarios();
        [HttpGet]
        public IList<ClHorarios> GetHorarios()//obtener todos los horarios
        {
            return serv.getHorario();
        }
        [HttpPost]
        public void PostHorarios(ClHorarios clho)//insertar un horario
        {
            serv.InsertarHorario(clho);
        }
        [HttpDelete]
        public string DeleteHorario(int id)//borrar un horario
        {
            return serv.eliminarHorario(id);
        }
        [HttpGet]
        public IList<ClHorarios> GetHorarioXIdCurso(int idcurso)//obtener un horario
        {
            return serv.getHorarioXCurso(idcurso);
        }
        [HttpPut]
        public bool UpdateHorario(ClHorarios clho, int id)//actualizar horario
        {
            return serv.actualizarHorario(clho, id);
        }
    }
}

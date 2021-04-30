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
    public class DiasHorariosController : ApiController
    {
        ServiciosDiasHorarios serv = new ServiciosDiasHorarios();
        [HttpGet]
        public IList<ClDiasHorarios> getDiaHorario()//obtener todos los dias
        {
            return serv.getDiaHorario();
        }
        [HttpPost]
        public void PostinsertarDiaHorario(ClDiasHorarios diah)//insertar un dia
        {
            serv.InsertarDiaHorario(diah);
        }
        [HttpDelete]
        public string DeleteDiaHorario(int id)//borrar un dia
        {
            return serv.eliminarDiaHorario(id);
        }
        [HttpGet]
        public IList<ClDiasHorarios> GetDiaHorarioxId(int id)//obtener un dia
        {
            return serv.getDiaHorarioXId(id);
        }
        [HttpPut]
        public bool UpdateDiaHorario(ClDiasHorarios diah, int id)//actualizar dia
        {
            return serv.actualizarDiaHorario(diah, id);
        }
    }
}

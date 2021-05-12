using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Logica.Modelos;
using Logica.Servicios;
using Newtonsoft;

namespace Services.Controllers
{
    public class PreguntasRespuestasController : ApiController
    {
        ServiciosPreguntasRespuestas serv = new ServiciosPreguntasRespuestas();
        [HttpGet]
        public IList<ClPreguntasRespuestas> GetPreguntasRespuestas()
        {
            return serv.GetPreguntasRespuestas();
        }
        [HttpPost]
        public void PostPreguntasRespuetas(ClPreguntasRespuestas cl)
        {
            serv.InsertarPregunta(cl);
        }
        [HttpDelete]
        public string DeletePreguntaRespuesta(int id)
        {
            return serv.eliminarPreguntaRespuesta(id);
        }
        [HttpGet]
        public IList<ClPreguntasRespuestas> GetPreguntaRespuesta(int id)
        {
            return serv.GetPreguntasRespuestasxId(id);
        }
        [HttpPut]
        public bool UpdatePreguntaRespuesta(ClPreguntasRespuestas cl, int id)
        {
            return serv.UpdatePreguntaRespuesta(cl, id);
        }
    }
}

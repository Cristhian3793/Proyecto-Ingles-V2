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
    public class LicenciasEstudianteController : ApiController
    {
        ServiciosLicenciasEstudiante serv = new ServiciosLicenciasEstudiante();
        [HttpPost]
        public void PostLicenciaEstudiante(ClLicenciasEstudiante licencia)
        {
            serv.InsertarLicenciaEstudiante(licencia);
        }
        [HttpGet]
        public IList<ClLicenciasEstudiante> getLicenciaEstudiante()
        {
            return serv.getLicenciasEstudiantes();
        }
        [HttpDelete]
        public string DeleteLicenciaEstudiante(long id)
        {
            return serv.eliminarLicenciaEstudiante(id);
        }
        [HttpGet]
        public IList<ClLicenciasEstudiante> GetLicenciasEstudiantexId(long codigo)
        {
            return serv.getLicenciasEstudiantexId(codigo);
        }

    }
}

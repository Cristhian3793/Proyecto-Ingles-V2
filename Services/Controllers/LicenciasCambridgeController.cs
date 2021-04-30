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
    public class LicenciasCambridgeController : ApiController
    {
        ServiciosLicenciasCambridge serv = new ServiciosLicenciasCambridge();
        [HttpPost]
        public void PostLicenciasCambridge(ClLicenciaCambridge licencia)
        {
            serv.InsertarLicenciaCambridge(licencia);
        }
        [HttpGet]
        public IList<ClLicenciaCambridge> getLicenciaCambridge()
        {
            return serv.getLicenciasCambridge();
        }
        [HttpDelete]
        public string DeleteLicencia(long id)
        {
            return serv.eliminarLicenciaCambridge(id);
        }
        [HttpGet]
        public IList<ClLicenciaCambridge> GetLicenciasxCodigo(long codigo)
        {
            return serv.getLicenciasCambridgexId(codigo);
        }
    }
}


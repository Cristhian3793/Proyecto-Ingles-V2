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
    public class LibrosController : ApiController
    {
        ServiciosLibros serv = new ServiciosLibros();
        [HttpGet]
        public IList<CLLibros> GetLibros()
        {
            return serv.getLibros();
        }
        [HttpPost]
        public void PostLibros(CLLibros lib)
        {
            serv.InsertarLibros(lib);
        }
        [HttpDelete]
        public string DeleteLibros(long id)
        {
            return serv.eliminarLibros(id);
        }
        [HttpGet]
        public IList<CLLibros> GetLibrosxCodigo(string codigo)
        {
            return serv.getLibrosXCod(codigo);
        }
        [HttpPut]
        public bool UpdateLibros(CLLibros lib, long id)
        {
            return serv.actualizarLibros(lib, id);
        }
    }
}

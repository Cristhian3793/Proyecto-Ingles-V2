using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Logica.ModelosDAO;
using Logica.Modelos;
using NHibernate;
using Logica.Conexion;
using Logica.Servicios;

namespace Services.Controllers
{
    public class ActualizarMatriculasController : ApiController
    {
        ServiciosInscritoAutonomo servInscrito = new ServiciosInscritoAutonomo();
        ServiciosNivelesInscrito servNivelesInscrito = new ServiciosNivelesInscrito();
        ServiciosNiveles servNivel = new ServiciosNiveles();
        ServiciosPeriodoInscripcion servPeriodos = new ServiciosPeriodoInscripcion();
        [HttpGet]
        public IHttpActionResult GetEstadoMatricula(string numdoc,string codProducto,string periodo)
        {
            IList<ClInscritoAutonomo> inscrito = servInscrito.getInscritosAutonomos();
            IList<ClNivelesInscrito> nivInscrito = servNivelesInscrito.getNivelIns();
            IList<ClNivel> nivel = servNivel.getNivel();
            IList<ClPeriodoInscripcion> periodos = servPeriodos.getPeriodoInscripcion();
            long[] estados = { 0, 5 };
            var query = from a in inscrito 
                        join b in nivInscrito on a.IdInscrito equals b.IDINSCRITO 
                        join c in nivel on b.IDNIVEL equals c.idNivel
                        join d in periodos on b.IDPERIODOINSCRIPCION equals d.IdPeriodoInscripcion
                        where estados.Contains(b.IDESTADONIVEL) && a.NumDocInscrito.Trim()==numdoc && c.codNivel.Trim()==codProducto && d.Periodo.Trim()==periodo.Trim()
                        select new { 
                            IDNIVELESTUDIANTE=b.IDNIVELESTUDIANTE,
                            IDINSCRITO=a.IdInscrito,
                            NUMDOCINSCRITO=a.NumDocInscrito.Trim(),
                            IDNIVEL=b.IDNIVEL,
                            ESTADONIVEL=b.IDESTADONIVEL,
                            CODNIVEL=c.codNivel.Trim(),
                            NOMNIVEL=c.nomNivel.Trim(),
                            PRUEBA=b.PRUEBA,
                            PERIODO=d.Periodo.Trim(),
                        };

            return Ok(query);
        }
        [HttpPatch]
        public bool UpdateEstadoMatricula(ClNivelesInscrito niveles,long idNivel)
        {
            ISession mySesions = SessionFactory.OpenSession;
            bool resp = false;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClNivelesInscrito WHERE IDNIVELESTUDIANTE=: idNivel").SetInt64("idNivel", idNivel);
                        ClNivelesInscrito ins = query.List<ClNivelesInscrito>()[0];
                        ins.IDESTADONIVEL = niveles.IDESTADONIVEL;
                        mySesions.Update(ins);
                        transaction.Commit();
                        resp = true;
                        return resp;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        mySesions.Close();
                    }
                }
            }

        }

    }
}

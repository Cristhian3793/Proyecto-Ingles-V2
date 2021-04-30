
using Logica.Conexion;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
using Logica.ModelosDAO;
namespace Logica.Servicios
{
    public class ServiciosPeriodoInscripcion : PeriodoInscripcion
    {
        //Obtener todos los periodos de Inscripcion
        public IList<ClPeriodoInscripcion> getPeriodoInscripcion()
        {
            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("FROM ClPeriodoInscripcion");
                        IList<ClPeriodoInscripcion> perIns = query.List<ClPeriodoInscripcion>();
                        return perIns;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        mySesion.Flush();
                    }
                }
            }
        }
        //Insertar un Periodo de Inscripcion
        public void InsertarPeriodoInscripcion(ClPeriodoInscripcion perIns)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        mySesions.Save(perIns);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

        }
        public IList<ClPeriodoInscripcion> getPeriodoInscripcionXid(int idPeriodo)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClPeriodoInscripcion WHERE IdPeriodoInscripcion=: idPeriodo")
                       .SetInt32("idPeriodo", idPeriodo);
                        IList<ClPeriodoInscripcion> per = query.List<ClPeriodoInscripcion>();
                        return per;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        mySesions.Flush();
                    }
                }
            }

        }
        public string eliminarPeriodoInscripcion(int idPeriodo)
        {
            ISession mySesionsD = SessionFactory.OpenSession;
            using (mySesionsD)
            {
                using (ITransaction transaction = mySesionsD.BeginTransaction())
                {
                    try
                    {
                        mySesionsD.CreateQuery("DELETE FROM ClPeriodoInscripcion WHERE IdPeriodoInscripcion = :idPeriodo").SetInt32("idPeriodo", idPeriodo).ExecuteUpdate();
                        transaction.Commit();
                        return "eliminado con exito";
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        mySesionsD.Flush();
                    }
                }
            }
        }
        public bool actualizarPeriodoInscripcion(ClPeriodoInscripcion perIns, int idPeriodo)
        {
            ISession mySesions = SessionFactory.OpenSession;
            bool resp = false;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClPeriodoInscripcion WHERE IdPeriodoInscripcion=: idPeriodo").SetInt32("idPeriodo", idPeriodo);
                        ClPeriodoInscripcion perIns_ = query.List<ClPeriodoInscripcion>()[0];
                        perIns_.Periodo = perIns.Periodo;
                        perIns_.AnoLectivo = perIns.AnoLectivo;
                        perIns_.CodPeriodoInscripcion = perIns.CodPeriodoInscripcion;
                        perIns_.FechaInicio = perIns.FechaInicio;
                        perIns_.FechaFin = perIns.FechaFin;
                        perIns_.EstadoPeriodo = perIns.EstadoPeriodo;
                        mySesions.Update(perIns_);
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
                        mySesions.Flush();
                    }
                }
            }

        }


    }
}

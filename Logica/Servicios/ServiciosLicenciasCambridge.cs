using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
using Logica.ModelosDAO;
using NHibernate;
using Logica.Conexion;
namespace Logica.Servicios
{
    public class ServiciosLicenciasCambridge 
    {
        public bool actualizarLicenciaCambridge(ClLicenciaCambridge licencia, long idLicencia)
        {
            ISession mySesions = SessionFactory.OpenSession;
            bool resp = false;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClLicenciaCambridge WHERE IdLicencia=: idLicencia").SetInt64("idLicencia", idLicencia);
                        ClLicenciaCambridge lic = query.List<ClLicenciaCambridge>()[0];
                        lic.Licencia = licencia.Licencia;
                        lic.EstadoLicencia = licencia.EstadoLicencia;
                        lic.IdLibro = licencia.IdLibro;
                        mySesions.Update(lic);
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
        public string eliminarLicenciaCambridge(long idLicencia)
        {
            ISession mySesionsD = SessionFactory.OpenSession;
            //string result = false;
            using (mySesionsD)
            {
                using (ITransaction transaction = mySesionsD.BeginTransaction())
                {
                    try
                    {
                        mySesionsD.CreateQuery("DELETE FROM ClLicenciaCambridge WHERE IdLicencia = :idLicencia").SetInt64("idLicencia", idLicencia).ExecuteUpdate();
                        transaction.Commit();
                        return "eliminado con exito";
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        mySesionsD.Close();
                    }
                }


            }
        }

        public IList<ClLicenciaCambridge> getLicenciasCambridgexId(long IdLicencia)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClLicenciaCambridge WHERE IdLicencia=: IdLicencia")
                       .SetInt64("IdLicencia", IdLicencia);
                        IList<ClLicenciaCambridge> licencia = query.List<ClLicenciaCambridge>();
                        return licencia;
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

        public IList<ClLicenciaCambridge> getLicenciasCambridge()
        {
            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("FROM ClLicenciaCambridge");
                        IList<ClLicenciaCambridge> licencias = query.List<ClLicenciaCambridge>();
                        return licencias;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        mySesion.Close();
                    }
                }
            }
        }

        public void InsertarLicenciaCambridge(ClLicenciaCambridge licencia)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        mySesions.Save(licencia);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally {
                        mySesions.Close();


                    }
                }
            }
        }
    }
}

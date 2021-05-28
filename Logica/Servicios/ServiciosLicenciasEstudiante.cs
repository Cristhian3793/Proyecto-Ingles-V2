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
    public class ServiciosLicenciasEstudiante
    {
        public bool actualizarLicenciaEstudiante(ClLicenciasEstudiante licencia, long idLicenciaEstu)
        {
            ISession mySesions = SessionFactory.OpenSession;
            bool resp = false;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClLicenciasEstudiante WHERE IDLICENCIAESTUDIANTE=: idLicenciaEstu").SetInt64("idLicenciaEstu", idLicenciaEstu);
                        ClLicenciasEstudiante lic = query.List<ClLicenciasEstudiante>()[0];
                        lic.IDLICENCIA = licencia.IDLICENCIA;
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
        public string eliminarLicenciaEstudiante(long idLicenciaEstu)
        {
            ISession mySesionsD = SessionFactory.OpenSession;
            //string result = false;
            using (mySesionsD)
            {
                using (ITransaction transaction = mySesionsD.BeginTransaction())
                {
                    try
                    {
                        mySesionsD.CreateQuery("DELETE FROM ClLicenciasEstudiante WHERE IDLICENCIAESTUDIANTE = :idLicenciaEstu").SetInt64("idLicenciaEstu", idLicenciaEstu).ExecuteUpdate();
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
        public IList<ClLicenciasEstudiante> getLicenciasEstudiantexId(long IdLicencia)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClLicenciasEstudiante WHERE IDLICENCIAESTUDIANTE=: IdLicencia")
                       .SetInt64("IdLicencia", IdLicencia);
                        IList<ClLicenciasEstudiante> licencia = query.List<ClLicenciasEstudiante>();
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
        public IList<ClLicenciasEstudiante> getLicenciasEstudiantes()
        {
            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("FROM ClLicenciasEstudiante");
                        IList<ClLicenciasEstudiante> licencias = query.List<ClLicenciasEstudiante>();
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
        public void InsertarLicenciaEstudiante(ClLicenciasEstudiante licencia)
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
                }
            }
        }
    }
}

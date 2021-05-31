using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.ModelosDAO;
using Logica.Modelos;
using NHibernate;
using Logica.Conexion;

namespace Logica.Servicios
{
    public class ServiciosCalificacionNiveles : CalificacionNivelDAO
    {
        public bool actualizarCalificacionNivel(ClCalificacionNivel niv, long id)
        {
            ISession mySesions = SessionFactory.OpenSession;
            bool resp = false;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClCalificacionNivel WHERE idCalificacionNivel=: id").SetInt64("id", id);
                        ClCalificacionNivel clniv = query.List<ClCalificacionNivel>()[0];
                        clniv.idNivel = niv.idNivel;
                        clniv.calificacionNivelDesde = niv.calificacionNivelDesde;
                        clniv.calificacionNivelHasta = niv.calificacionNivelHasta;
                        mySesions.Update(clniv);
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
        public string eliminarCalificacionNivel(long id)
        {
            ISession mySesionsD = SessionFactory.OpenSession;
            using (mySesionsD)
            {
                using (ITransaction transaction = mySesionsD.BeginTransaction())
                {
                    try
                    {
                        mySesionsD.CreateQuery("DELETE FROM ClCalificacionNivel WHERE idCalificacionNivel=:id").SetInt64("id", id).ExecuteUpdate();
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

        public IList<ClCalificacionNivel> getCalificacionNivel()
        {
            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("FROM ClCalificacionNivel");
                        IList<ClCalificacionNivel> cur = query.List<ClCalificacionNivel>();
                        return cur;
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

        public IList<ClCalificacionNivel> getCalificacionNivelXId(long id)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClCalificacionNivel WHERE idCalificacionNivel=: id")
                       .SetInt64("id", id);
                        IList<ClCalificacionNivel> cur = query.List<ClCalificacionNivel>();
                        return cur;
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

        public void InsertarCalificacionNivel(ClCalificacionNivel cur)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        mySesions.Save(cur);
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

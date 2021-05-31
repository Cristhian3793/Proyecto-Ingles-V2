using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.ModelosDAO;
using Logica.Modelos;
using Logica.Conexion;
using NHibernate;
namespace Logica.Servicios
{
    public class ServiciosEstadoNivel :EstadoNivelDAO
    {
        public IList<ClEstadoNivel> getEstadoNivel()
        {
            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("FROM ClEstadoNivel");
                        IList<ClEstadoNivel> inscritoA = query.List<ClEstadoNivel>();
                        return inscritoA;
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
        public void InsertarEstadoNivel(ClEstadoNivel estNiv)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        mySesions.Save(estNiv);
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
        public IList<ClEstadoNivel> getEstadoNivelxId(int idEstNivel)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClEstadoNivel WHERE IdEstadoNivel=: idEstNivel")
                       .SetInt32("idEstNivel", idEstNivel);
                        IList<ClEstadoNivel> cur = query.List<ClEstadoNivel>();
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
        public string eliminarEstadoNivel(int  idEstNivel)
        {
            ISession mySesionsD = SessionFactory.OpenSession;
            using (mySesionsD)
            {
                using (ITransaction transaction = mySesionsD.BeginTransaction())
                {
                    try
                    {
                        mySesionsD.CreateQuery("DELETE FROM ClEstadoNivel WHERE IdEstadoNivel=:idEstNivel").SetInt32("idEstNivel", idEstNivel).ExecuteUpdate();
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
        public bool actualizarEstadoNivel(ClEstadoNivel estN, int idEstNivel)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClEstadoNivel WHERE IdEstadoNivel=: idEstNivel").SetInt32("idEstNivel", idEstNivel);
                        ClEstadoNivel estNiv = query.List<ClEstadoNivel>()[0];
                        estNiv.DescEstadoNivel = estN.DescEstadoNivel;
                        mySesions.Update(estNiv);
                        transaction.Commit();
                        return true;
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

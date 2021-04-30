using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
using Logica.ModelosDAO;
using Logica.Conexion;
using NHibernate;
namespace Logica.Servicios
{
    public class ServiciosTipoNivel : TipoNivelDAO
    {
        public bool actualizarTipoNivel(ClTipoNivel tipNivel, int idTipoNivel)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClTipoNivel WHERE idTipoNivel=: idTipoNivel").SetInt32("idTipoNivel", idTipoNivel);
                        ClTipoNivel tipNIv = query.List<ClTipoNivel>()[0];
                        tipNIv.descTipoNivel = tipNivel.descTipoNivel;
                        tipNIv.codTipoNivel = tipNivel.codTipoNivel;
                        mySesions.Update(tipNIv);
                        transaction.Commit();
                        return true;
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
        public string eliminarTipoNivel(int idTipoNivel)
        {
            ISession mySesionsD = SessionFactory.OpenSession;
            using (mySesionsD)
            {
                using (ITransaction transaction = mySesionsD.BeginTransaction())
                {
                    try
                    {
                        mySesionsD.CreateQuery("DELETE FROM ClTipoNivel WHERE idTipoNivel=:idTipoNivel").SetInt32("idTipoNivel", idTipoNivel).ExecuteUpdate();
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

        public IList<ClTipoNivel> getTipoNivel()
        {
            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("FROM ClTipoNivel");
                        IList<ClTipoNivel> tipnivel = query.List<ClTipoNivel>();
                        return tipnivel;
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

        public IList<ClTipoNivel> getTipoNivelxId(int idTipoNivel)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClTipoNivel WHERE idtipoNivel=: idTipoNivel")
                       .SetInt32("idTipoNivel", idTipoNivel);
                        IList<ClTipoNivel> cur = query.List<ClTipoNivel>();
                        return cur;
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

        public void InsertarTipoNivel(ClTipoNivel tipNivel)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        mySesions.Save(tipNivel);
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

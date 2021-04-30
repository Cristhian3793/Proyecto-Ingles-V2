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
    public class ServiciosEquivalenciaNivel : EquivalenciaNivelDAO
    {
        public bool actualizarEquivalenciaNivel(ClEquivalenciaNivel equiniv, long IdEquivalenciaNivel)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClEquivalenciaNivel WHERE IdEquivalenciaNivel=: IdEquivalenciaNivel").SetInt64("IdEquivalenciaNivel", IdEquivalenciaNivel);
                        ClEquivalenciaNivel equinive = query.List<ClEquivalenciaNivel>()[0];
                        equinive.idNivelAut = equiniv.idNivelAut;
                        equinive.idNivelPro = equiniv.idNivelPro;
                        mySesions.Update(equinive);
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
        public string eliminarEquivalenciaNivel(long IdEquivalenciaNivel)
        {
            ISession mySesionsD = SessionFactory.OpenSession;
            using (mySesionsD)
            {
                using (ITransaction transaction = mySesionsD.BeginTransaction())
                {
                    try
                    {
                        mySesionsD.CreateQuery("DELETE FROM ClEquivalenciaNivel WHERE IdEquivalenciaNivel=:IdEquivalenciaNivel").SetInt64("IdEquivalenciaNivel", IdEquivalenciaNivel).ExecuteUpdate();
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

        public IList<ClEquivalenciaNivel> getEquivalenciaNivel()
        {
            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("FROM ClEquivalenciaNivel");
                        IList<ClEquivalenciaNivel> equiniv = query.List<ClEquivalenciaNivel>();
                        return equiniv;
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

        public IList<ClEquivalenciaNivel> getEquivalenciaNivelxCodigo(long IdEquivalenciaNivel)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClEquivalenciaNivel WHERE IdEquivalenciaNivel=: IdEquivalenciaNivel")
                       .SetInt64("IdEquivalenciaNivel", IdEquivalenciaNivel);
                        IList<ClEquivalenciaNivel> equiniv  = query.List<ClEquivalenciaNivel>();
                        return equiniv;
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

        public void InsertarEquivalenciaNivel(ClEquivalenciaNivel equiniv)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        mySesions.Save(equiniv);
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

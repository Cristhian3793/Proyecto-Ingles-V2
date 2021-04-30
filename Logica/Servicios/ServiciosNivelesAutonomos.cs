using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Conexion;
using Logica.Modelos;
using Logica.ModelosDAO;
using NHibernate;

namespace Logica.Servicios
{
    public class ServiciosNivelesAutonomos : NivelesAutonomosDAO
    {
        public bool actualizarNivelAutonomo(ClNivelesAutonomos nivel, long idNIvel)
        {
            ISession mySesions = SessionFactory.OpenSession;
            bool resp = false;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {

                        IQuery query = mySesions.CreateQuery("from ClNivelesAutonomos WHERE idNIvelAutonomo=: idNIvel").SetInt64("idNIvel", idNIvel);
                        ClNivelesAutonomos niv = query.List<ClNivelesAutonomos>()[0];
                        niv.idNivel = nivel.idNivel;
                        mySesions.Update(niv);
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

        public string eliminarNivelAutonomo(long idNIvel)
        {
            ISession mySesionsD = SessionFactory.OpenSession;
            using (mySesionsD)
            {
                using (ITransaction transaction = mySesionsD.BeginTransaction())
                {
                    try
                    {
                        mySesionsD.CreateQuery("DELETE FROM ClNivelesAutonomos WHERE idNIvelAutonomo = :idNIvel").SetInt64("idNIvel", idNIvel).ExecuteUpdate();
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

        public IList<ClNivelesAutonomos> getNivelAutonomo()
        {
            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("from ClNivelesAutonomos");
                        IList<ClNivelesAutonomos> niveles = query.List<ClNivelesAutonomos>();
                        return niveles;
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

        public IList<ClNivelesAutonomos> getNivelAutonomoxId(long idNivel)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClNivelesAutonomos WHERE idNIvelAutonomo=: idNivel")
                       .SetInt64("idNivel", idNivel);
                        IList<ClNivelesAutonomos> niv = query.List<ClNivelesAutonomos>();
                        return niv;
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

        public void InsertarNivelAutonomo(ClNivelesAutonomos niv)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        mySesions.Save(niv);
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

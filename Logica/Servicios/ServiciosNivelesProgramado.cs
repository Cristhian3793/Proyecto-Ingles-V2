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
    public class ServiciosNivelesProgramado : NivelesProgramadosDAO
    {
        public bool actualizarNivelprogramado(ClNivelesProgramado nivel, long idNIvel)
        {
            ISession mySesions = SessionFactory.OpenSession;
            bool resp = false;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {

                        IQuery query = mySesions.CreateQuery("from ClNivelesProgramado WHERE idNIvelProgramado=: idNIvel").SetInt64("idNIvel", idNIvel);
                        ClNivelesProgramado niv = query.List<ClNivelesProgramado>()[0];
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
                        mySesions.Close();
                    }
                }
            }
        }

        public string eliminarNivelProgramado(long idNIvel)
        {
            ISession mySesionsD = SessionFactory.OpenSession;
            using (mySesionsD)
            {
                using (ITransaction transaction = mySesionsD.BeginTransaction())
                {
                    try
                    {
                        mySesionsD.CreateQuery("DELETE FROM ClNivelesProgramado WHERE idNIvelProgramado = :idNIvel").SetInt64("idNIvel", idNIvel).ExecuteUpdate();
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

        public IList<ClNivelesProgramado> getNivelProgramado()
        {
            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("from ClNivelesProgramado");
                        IList<ClNivelesProgramado> niveles = query.List<ClNivelesProgramado>();
                        return niveles;
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

        public IList<ClNivelesProgramado> getNivelProgramadoxId(long idNivel)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClNivelesProgramado WHERE idNIvelProgramado=: idNivel")
                       .SetInt64("idNivel", idNivel);
                        IList<ClNivelesProgramado> niv = query.List<ClNivelesProgramado>();
                        return niv;
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

       public  void InsertarNivelProgramado(ClNivelesProgramado niv)
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
                    finally
                    {
                        mySesions.Close();

                    }
                }
            }
        }
    }
}

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
   public class ServiciosDiasHorarios : DiasHorariosDAO
    {
        public bool actualizarDiaHorario(ClDiasHorarios diah, int id)
        {
            ISession mySesions = SessionFactory.OpenSession;
            bool resp = false;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClDiasHorarios WHERE IdDiaHorario=: id").SetInt32("id", id);
                        ClDiasHorarios cldiah = query.List<ClDiasHorarios>()[0];
                        cldiah.DiaHorario = diah.DiaHorario;
                        mySesions.Update(cldiah);
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

        public string eliminarDiaHorario(int id)
        {
            ISession mySesionsD = SessionFactory.OpenSession;
            using (mySesionsD)
            {
                using (ITransaction transaction = mySesionsD.BeginTransaction())
                {
                    try
                    {
                        mySesionsD.CreateQuery("DELETE FROM ClDiasHorarios WHERE IdDiaHorario=:id").SetInt32("id", id).ExecuteUpdate();
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

        public IList<ClDiasHorarios> getDiaHorario()
        {
            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("FROM ClDiasHorarios");
                        IList<ClDiasHorarios> diah = query.List<ClDiasHorarios>();
                        return diah;
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

        public IList<ClDiasHorarios> getDiaHorarioXId(int id)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClDiasHorarios WHERE IdDiaHorario=: id")
                       .SetInt32("id", id);
                        IList<ClDiasHorarios> diah = query.List<ClDiasHorarios>();
                        return diah;
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

        public void InsertarDiaHorario(ClDiasHorarios diah)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        mySesions.Save(diah);
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

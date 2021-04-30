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
    public class ServiciosHorarios : HorariosDAO
    {
        public bool actualizarHorario(ClHorarios horario, int id)
        {
            ISession mySesions = SessionFactory.OpenSession;
            bool resp = false;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClHorarios WHERE IdHorarios=: id").SetInt32("id", id);
                        ClHorarios clho = query.List<ClHorarios>()[0];
                        clho.IdCurso = horario.IdCurso;
                        clho.IdDiaHorario = horario.IdDiaHorario;
                        clho.HoraInicioHorario = horario.HoraInicioHorario;
                        clho.HoraFinHorario = horario.HoraFinHorario;
                        mySesions.Update(clho);
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

        public string eliminarHorario(int id)
        {
            ISession mySesionsD = SessionFactory.OpenSession;
            using (mySesionsD)
            {
                using (ITransaction transaction = mySesionsD.BeginTransaction())
                {
                    try
                    {
                        mySesionsD.CreateQuery("DELETE FROM ClHorarios WHERE IdHorarios=:id").SetInt32("id", id).ExecuteUpdate();
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

        public IList<ClHorarios> getHorario()
        {
            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("FROM ClHorarios");
                        IList<ClHorarios> clHo = query.List<ClHorarios>();
                        return clHo;
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

        public IList<ClHorarios> getHorarioXCurso(int idcurso)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClHorarios WHERE IdCurso=: idcurso")
                       .SetInt32("idcurso", idcurso);
                        IList<ClHorarios> clHo = query.List<ClHorarios>();
                        return clHo;
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

        public void InsertarHorario(ClHorarios horario)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        mySesions.Save(horario);
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

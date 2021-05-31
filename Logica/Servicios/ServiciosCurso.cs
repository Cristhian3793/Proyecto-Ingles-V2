using Logica.Conexion;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
using Logica.ModelosDAO;
namespace Logica.Servicios
{
    public class ServiciosCurso: CursoDAO
    {     //Obtener todos los Cursos
        public IList<ClCurso> getCurso()
        {
            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("FROM ClCurso");
                        IList<ClCurso> cur = query.List<ClCurso>();
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
        //Insertar un Curso
        public void InsertarCurso(ClCurso cur)
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
        public IList<ClCurso> getCursoXCodigo(string codigo)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClCurso WHERE CodCurso=: codigo")
                       .SetString("codigo", codigo);
                        IList<ClCurso> cur = query.List<ClCurso>();
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
        public string eliminarCurso(string codigo)
        {
            ISession mySesionsD = SessionFactory.OpenSession;
            using (mySesionsD)
            {
                using (ITransaction transaction = mySesionsD.BeginTransaction())
                {
                    try
                    {
                        mySesionsD.CreateQuery("DELETE FROM ClCurso WHERE CodCurso=:codigo").SetString("codigo", codigo).ExecuteUpdate();
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
        public bool actualizarCurso(ClCurso cur, string codigo)
        {
            ISession mySesions = SessionFactory.OpenSession;
            bool resp = false;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClCurso WHERE CodCurso=: codigo").SetString("codigo", codigo);
                        ClCurso clcur = query.List<ClCurso>()[0];
                        clcur.DescCurso = cur.DescCurso;
                        clcur.CodCurso = cur.CodCurso;
                        clcur.FechaCreacionCurso = cur.FechaCreacionCurso;
                        mySesions.Update(clcur);
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

    }
}

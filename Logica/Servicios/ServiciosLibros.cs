using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Conexion;
using NHibernate;
using Logica.Modelos;
using Logica.ModelosDAO;
namespace Logica.Servicios
{
   public class ServiciosLibros : LibrosDAO
    {
       public bool actualizarLibros(CLLibros libro, long idLibro)
        {
            ISession mySesions = SessionFactory.OpenSession;
            bool resp = false;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM CLLibros WHERE idLibro=: idLibro").SetInt64("idLibro", idLibro);
                        CLLibros lib = query.List<CLLibros>()[0];
                        lib.codLibro = libro.codLibro;
                        lib.descLibro = libro.descLibro;
                        mySesions.Update(lib);
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
    

        public string eliminarLibros(long idLibro)
        {
            ISession mySesionsD = SessionFactory.OpenSession;
            //string result = false;
            using (mySesionsD)
            {
                using (ITransaction transaction = mySesionsD.BeginTransaction())
                {
                    try
                    {
                        mySesionsD.CreateQuery("DELETE FROM CLLibros WHERE idLibro = :idLibro").SetInt64("idLibro", idLibro).ExecuteUpdate();
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

        public IList<CLLibros> getLibros()
        {
            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("FROM CLLibros");
                        IList<CLLibros> libros = query.List<CLLibros>();
                        return libros;
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

        public IList<CLLibros> getLibrosXCod(string cod)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM CLLibros WHERE codLibro=: cod")
                       .SetString("cod", cod);
                        IList<CLLibros> libro = query.List<CLLibros>();
                        return libro;
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

        public void InsertarLibros(CLLibros libro)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        mySesions.Save(libro);
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

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
    public class ServiciosUsuarios : UsuariosDAO
    {
        public bool actualizarUsuario(ClUsuarios usuario, long id)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClUsuarios WHERE idUser=: id").SetInt64("id", id);
                        ClUsuarios user = query.List<ClUsuarios>()[0];
                        user.Usuario = usuario.Usuario;
                        user.Password= usuario.Password;
                        user.Nombres = usuario.Nombres;
                        user.Apellidos= usuario.Apellidos;
                        mySesions.Update(user);
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

        public string eliminarUsuario(long id)
        {
            ISession mySesionsD = SessionFactory.OpenSession;
            using (mySesionsD)
            {
                using (ITransaction transaction = mySesionsD.BeginTransaction())
                {
                    try
                    {
                        mySesionsD.CreateQuery("DELETE FROM ClUsuarios WHERE idUser=:id").SetInt64("id", id).ExecuteUpdate();
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

        public IList<ClUsuarios> getUsuarios()
        {
            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("FROM ClUsuarios");
                        IList<ClUsuarios> usu = query.List<ClUsuarios>();
                        return usu;
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

        public IList<ClUsuarios> getUsuarioxNumDoc(string numDoc)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClUsuarios WHERE idUser=: numDoc")
                       .SetString("numDoc", numDoc);
                        IList<ClUsuarios> user = query.List<ClUsuarios>();
                        return user;
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

        public void InsertarUsuario(ClUsuarios usuario)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        mySesions.Save(usuario);
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

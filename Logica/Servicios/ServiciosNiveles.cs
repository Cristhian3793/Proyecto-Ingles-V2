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
    public class ServiciosNiveles :NivelesDAO
    {
        //Obtener todos los niveles
        public IList<ClNivel> getNivel()
        {

            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("from ClNivel");
                        IList<ClNivel> licencias = query.List<ClNivel>();
                        return licencias;
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
        //Insertar un nivel
        public void InsertarNivel(ClNivel nivel)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        mySesions.Save(nivel);
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
       public IList<ClNivel> getNivelxCod(string codnivel)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClNivel WHERE codNivel=: codnivel")
                       .SetString("codnivel", codnivel);
                        IList<ClNivel> niv = query.List<ClNivel>();
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
        public string eliminarNivel(long codnivel)
        {
            ISession mySesionsD = SessionFactory.OpenSession;
            using (mySesionsD)
            {
                using (ITransaction transaction = mySesionsD.BeginTransaction())
                {
                    try
                    {
                        mySesionsD.CreateQuery("DELETE FROM ClNivel WHERE idNivel = :codnivel").SetInt64("codnivel", codnivel).ExecuteUpdate();
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
        public bool actualizarNivel(ClNivel cniv, long idNivel)
        {
            ISession mySesions = SessionFactory.OpenSession;
            bool resp = false;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {

                        IQuery query = mySesions.CreateQuery("from ClNivel WHERE idNivel=: idNivel").SetInt64("idNivel", idNivel);
                        ClNivel niv = query.List<ClNivel>()[0];
                        niv.idEstadoNivel= cniv.idEstadoNivel;
                        niv.idTipoNivel = cniv.idTipoNivel;
                        niv.idLibro = cniv.idLibro;
                        niv.idCurso = cniv.idCurso;
                        niv.codNivel = cniv.codNivel;
                        niv.nomNivel = cniv.nomNivel;
                        niv.descNivel = cniv.descNivel;
                        niv.costoNIvel = cniv.costoNIvel;
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

    }
}

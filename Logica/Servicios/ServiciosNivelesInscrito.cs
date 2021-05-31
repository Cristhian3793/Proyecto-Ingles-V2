using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Conexion;
using NHibernate;
using Logica.Modelos;
namespace Logica.Servicios
{
    public class ServiciosNivelesInscrito
    {
        public IList<ClNivelesInscrito> getNivelIns()
        {

            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("from ClNivelesInscrito");
                        IList<ClNivelesInscrito> niveles = query.List<ClNivelesInscrito>();
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
        //Insertar un nivel
        public void InsertarNivelIns(ClNivelesInscrito nivel)
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
        public IList<ClNivelesInscrito> getNivelInsxCod(long idnivel)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClNivelesInscrito WHERE IDNIVELESTUDIANTE=: idnivel")
                       .SetInt64("idnivel", idnivel);
                        IList<ClNivelesInscrito> niv = query.List<ClNivelesInscrito>();
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
        public string eliminarNivelIns(long codnivel)
        {
            ISession mySesionsD = SessionFactory.OpenSession;
            using (mySesionsD)
            {
                using (ITransaction transaction = mySesionsD.BeginTransaction())
                {
                    try
                    {
                        mySesionsD.CreateQuery("DELETE FROM ClNivelesInscrito WHERE IDNIVELESTUDIANTE = :codnivel").SetInt64("codnivel", codnivel).ExecuteUpdate();
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
        public bool actualizarNivelIns(ClNivelesInscrito cniv, long idNivel)
        {
            ISession mySesions = SessionFactory.OpenSession;
            bool resp = false;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {

                        IQuery query = mySesions.CreateQuery("from ClNivelesInscrito WHERE IDNIVELESTUDIANTE=: idNivel").SetInt64("idNivel", idNivel);
                        ClNivelesInscrito niv = query.List<ClNivelesInscrito>()[0];
                        niv.IDNIVEL = cniv.IDNIVEL;
                        niv.IDESTADONIVEL = cniv.IDESTADONIVEL;
                        niv.IDPRUEBAUBICACION = cniv.IDPRUEBAUBICACION;
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
        public bool actualizarEstadoNivelInscrito(ClNivelesInscrito insA, long idNivelInscrito)
        {
            ISession mySesions = SessionFactory.OpenSession;
            bool resp = false;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClNivelesInscrito  WHERE IDNIVELESTUDIANTE=: idNivelInscrito").SetInt64("idNivelInscrito", idNivelInscrito);
                        ClNivelesInscrito ins = query.List<ClNivelesInscrito>()[0];
                        ins.IDESTADONIVEL = insA.IDESTADONIVEL;
                        mySesions.Update(ins);
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

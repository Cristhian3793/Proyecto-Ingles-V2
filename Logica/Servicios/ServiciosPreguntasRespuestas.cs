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
    public class ServiciosPreguntasRespuestas
    {
        public void InsertarPregunta(ClPreguntasRespuestas cl) {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        mySesions.Save(cl);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        public IList<ClPreguntasRespuestas> GetPreguntasRespuestas() { 
            ISession mysession= SessionFactory.OpenSession;

            using (mysession) {
                using (ITransaction transaccion = mysession.BeginTransaction()) {
                    try { 
                     IQuery query = mysession.CreateQuery("from ClPreguntasRespuestas");
                        IList<ClPreguntasRespuestas> lis = query.List<ClPreguntasRespuestas>();
                        return lis;
                    }
                    catch (Exception ex) {
                        throw ex;
                    
                    }
                    finally{
                        mysession.Close();
                    }
                   

                }           
            }        
        }
        public IList<ClPreguntasRespuestas> GetPreguntasRespuestasxId(int id)
        {
            ISession mysession = SessionFactory.OpenSession;

            using (mysession)
            {
                using (ITransaction transaccion = mysession.BeginTransaction())
                {
                    try
                    {

                        IQuery query = mysession.CreateQuery("from ClPreguntasRespuestas where IDPREGUNTA=:id").SetInt64("id",id);
                        IList<ClPreguntasRespuestas> lis = query.List<ClPreguntasRespuestas>();
                        return lis;
                    }
                    catch (Exception ex)
                    {
                        throw ex;

                    }
                    finally
                    {
                        mysession.Close();
                    }
                }
            }
        }
        public bool UpdatePreguntaRespuesta(ClPreguntasRespuestas cl_,int id)
        {
            ISession mysession = SessionFactory.OpenSession;
            bool resp = false;
            using (mysession)
            {
                using (ITransaction transaccion = mysession.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mysession.CreateQuery("from ClPreguntasRespuestas where IDPREGUNTA=:id").SetInt32("id", id);
                        ClPreguntasRespuestas cl = query.List<ClPreguntasRespuestas>()[0];
                        cl.PREGUNTA = cl_.PREGUNTA;
                        cl.RESPUESTA = cl.RESPUESTA;
                        mysession.Update(cl);
                        transaccion.Commit();
                        resp = true;
                        return resp;
                    }
                    catch (Exception ex)
                    {
                        throw ex;

                    }
                    finally
                    {
                        mysession.Close();
                    }
                }
            }
        }
        public string eliminarPreguntaRespuesta(int id)
        {
            ISession mySesionsD = SessionFactory.OpenSession;
            using (mySesionsD)
            {
                using (ITransaction transaction = mySesionsD.BeginTransaction())
                {
                    try
                    {
                        mySesionsD.CreateQuery("DELETE FROM ClPreguntasRespuestas WHERE IDPREGUNTA = :id").SetInt32("id", id).ExecuteUpdate();
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

    }
}

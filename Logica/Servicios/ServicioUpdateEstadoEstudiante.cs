using Logica.Conexion;
using Logica.Modelos;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Servicios
{
    public class ServicioUpdateEstadoEstudiante
    {
        public bool actualizarEstadoEstudiante(ClInscritoAutonomo insA, long idInscrito)
        {
            ISession mySesions = SessionFactory.OpenSession;
            bool resp = false;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClInscritoAutonomo  WHERE IdInscrito=: idInscrito").SetInt64("idInscrito", idInscrito);
                        ClInscritoAutonomo ins = query.List<ClInscritoAutonomo>()[0];
                        ins.IdEstadoEstudiante = insA.IdEstadoEstudiante;
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

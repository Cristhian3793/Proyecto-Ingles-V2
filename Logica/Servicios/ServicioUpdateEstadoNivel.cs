using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Conexion;
using Logica.Modelos;
using NHibernate;
namespace Logica.Servicios
{
   public class ServicioUpdateEstadoNivel
    {
        public bool actualizarEstadoNivel(ClNivelesInscrito insA, long idNIvelestu)
        {
            ISession mySesions = SessionFactory.OpenSession;
            bool resp = false;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClNivelesInscrito  WHERE IDNIVELESTUDIANTE=: idNIvelestu").SetInt64("idNIvelestu", idNIvelestu);
                        ClNivelesInscrito ins = query.List<ClNivelesInscrito>()[0];
                        ins.ESTADONIVEL = insA.ESTADONIVEL;
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

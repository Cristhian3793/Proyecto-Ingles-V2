using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
using Logica.Conexion;
using NHibernate;
namespace Logica.Servicios
{
    public class ServiciosEstadoPeriodo
    {
        public IList<ClEstadoPeriodo> getEstadoPeriodo()
        {
            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("FROM ClEstadoPeriodo");
                        IList<ClEstadoPeriodo> estp = query.List<ClEstadoPeriodo>();
                        return estp;
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.ModelosDAO;
using Logica.Modelos;
using Logica.Conexion;
using NHibernate;
namespace Logica.Servicios
{
    public class ServiciosEstadoPrueba
    {
        public IList<ClEstadoPrueba> getEstadoPrueba()
        {
            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("FROM ClEstadoPrueba");
                        IList<ClEstadoPrueba> estprueba = query.List<ClEstadoPrueba>();
                        return estprueba;
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
    }
}

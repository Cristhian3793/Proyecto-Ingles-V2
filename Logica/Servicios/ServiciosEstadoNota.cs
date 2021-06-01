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
   public  class ServiciosEstadoNota
    {
        public IList<ClEstadoNota> getEstadoNota()
        {
            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("FROM ClEstadoNota");
                        IList<ClEstadoNota> est = query.List<ClEstadoNota>();
                        return est;
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

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
    public class ServiciosEstadoEstudiante
    {
        public IList<ClEstadoEstudiante> getEstadoEstudiante()
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClEstadoEstudiante");
                        IList<ClEstadoEstudiante> inscritoA = query.List<ClEstadoEstudiante>();
                        return inscritoA;
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
    }
}

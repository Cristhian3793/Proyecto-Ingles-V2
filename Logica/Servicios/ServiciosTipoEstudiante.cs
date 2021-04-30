using Logica.Conexion;
using Logica.Modelos;
using Logica.ModelosDAO;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Servicios
{
    public class ServiciosTipoEstudiante : TipoEstudianteDAO
    {
        public IList<ClTipoEstudiante> getTipoEstudiante()
        {
            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("FROM ClTipoEstudiante");
                        IList<ClTipoEstudiante> tipEstudiante = query.List<ClTipoEstudiante>();
                        return tipEstudiante;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Logica.Modelos;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Logica.Mapping;
namespace Logica.Conexion
{
    public class SessionFactory
    {
        private static volatile ISessionFactory iSessionFactory;
        private static object syncRoot = new object();
        public static ISession OpenSession
        {
            get
            {
                if (iSessionFactory == null)
                {
                    lock (syncRoot)
                    {
                        if (iSessionFactory == null)
                        {
                            iSessionFactory = CreateSessionFactory();
                        }
                    }
                }
                return iSessionFactory.OpenSession();
            }
        }
        private static ISessionFactory CreateSessionFactory()
        {
            //var mapper=new Conv
            try
            {
                string connection_string = System.Configuration.ConfigurationManager.AppSettings["connection_string"];
                return Fluently.Configure().
                Database(MsSqlConfiguration.MsSql2012.ConnectionString
                (connection_string))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ClPeriodoInscripcion>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ClInscritoAutonomo>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ClCurso>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ClDiasHorarios>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ClHorarios>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ClNivel>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ClEstadoNivel>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ClEquivalenciaNivel>())           
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ClTipoNivel>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ClPrueba>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ClUnidad>())
                .Mappings(m=>m.FluentMappings.AddFromAssemblyOf<ClLicenciaCambridge>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CLLibros>())
                .Mappings(m=>m.FluentMappings.AddFromAssemblyOf<ClNivelesAutonomos>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ClNivelesProgramado>())
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private static void BuildSchema(NHibernate.Cfg.Configuration config)
        {
        }
        private static AutoPersistenceModel CreateMappings()
        {
            return AutoMap
                .Assembly(System.Reflection.Assembly.GetCallingAssembly())
                .Where(testx => testx.Namespace == "Logica.Modelos");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.ModelosDAO;
using Logica.Modelos;
using NHibernate;
using Logica.Conexion;
namespace Logica.Servicios
{
    public class ServicioNotas
    {
   
        public IList<ClNota> getNotas()
        {
            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("FROM ClNota");
                        IList<ClNota> inscritoA = query.List<ClNota>();
                        return inscritoA;
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

        public void InsertarNotas(ClNota insA)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        mySesions.Save(insA);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        public IList<ClNota> getNotasxIns(long id)
        {
            ISession mySesions = SessionFactory.OpenSession;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClNota WHERE IDINSCRITO=: id").SetInt64
                        ("id", id);
                        IList<ClNota> inscritoA = query.List<ClNota>();
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
        public string eliminarNota(long id)
        {
            ISession mySesionsD = SessionFactory.OpenSession;
            //string result = false;
            using (mySesionsD)
            {
                using (ITransaction transaction = mySesionsD.BeginTransaction())
                {
                    try
                    {
                        mySesionsD.CreateQuery("DELETE FROM ClNota WHERE IDNOTA = :id").SetInt64("id", id).ExecuteUpdate();
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
        /*
            Id(x => x.IDNOTA).Column("IDNOTA");
            Map(x => x.IDINSCRITO).Column("IDINSCRITO");
            Map(x => x.IDNIVEL).Column("IDNIVEL");
            Map(x => x.IDTEMA).Column("IDTEMA");
            Map(x => x.UNIT_1).Column("UNIT_1");
            Map(x => x.DONE_1).Column("DONE_1");
            Map(x => x.UNIT_2).Column("UNIT_2");
            Map(x => x.DONE_2).Column("DONE_2");
            Map(x => x.UNIT_3).Column("UNIT_3");
            Map(x => x.DONE_3).Column("DONE_3");
            Map(x => x.CHECK_POINT).Column("CHECK_POINT");
            Map(x => x.UNIT_4).Column("UNIT_4");
            Map(x => x.DONE_4).Column("DONE_4");
            Map(x => x.UNIT_5).Column("UNIT_5");
            Map(x => x.DONE_5).Column("DONE_5");
            Map(x => x.UNIT_6).Column("UNIT_6");
            Map(x => x.DONE_6).Column("DONE_6");
            Map(x => x.ESTADO).Column("ESTADO");
         
         */

        public bool actualizarNotas(ClNota insA, long idNota)
        {
            ISession mySesions = SessionFactory.OpenSession;
            bool resp = false;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClNota WHERE IDNOTA=: idNota").SetInt64("idNota", idNota);
                        ClNota ins = query.List<ClNota>()[0];
                        ins.UNIT_1 = insA.UNIT_1;
                        ins.DONE_1 = insA.DONE_1;
                        ins.UNIT_2 = insA.UNIT_2;
                        ins.DONE_2 = insA.DONE_2;
                        ins.UNIT_3 = insA.UNIT_3;
                        ins.DONE_3 = insA.DONE_3;
                        ins.CHECK_POINT = insA.CHECK_POINT;
                        ins.UNIT_4 = insA.UNIT_4;
                        ins.DONE_4 = insA.DONE_4;
                        ins.UNIT_5 = insA.UNIT_5;
                        ins.DONE_5 = insA.DONE_5;
                        ins.UNIT_6 = insA.UNIT_6;
                        ins.DONE_6 = insA.DONE_6;
                        ins.ESTADO = insA.ESTADO;
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
                        mySesions.Flush();
                    }
                }
            }
        }
    
    }
}

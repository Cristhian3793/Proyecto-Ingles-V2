using Logica.Conexion;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
using Logica.ModelosDAO;
namespace Logica.Servicios
{
    public class ServiciosInscritoAutonomo : InscritoAutonomoDAO
    {       
        //Obtener todos los Inscritos Autonomos
        public IList<ClInscritoAutonomo> getInscritosAutonomos()
        {
            ISession mySesion = SessionFactory.OpenSession;
            using (mySesion)
            {
                using (ITransaction transaction = mySesion.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesion.CreateQuery("FROM ClInscritoAutonomo");
                        IList<ClInscritoAutonomo> inscritoA = query.List<ClInscritoAutonomo>();                       
                        return inscritoA;
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
        //Insertar un isncrito Autonomo
        public void InsertarInscritoAutonomo(ClInscritoAutonomo insA)
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
        public IList<ClInscritoAutonomo> getInscritoAXNumDoc(string numdoc)
        {
            ISession mySesions = SessionFactory.OpenSession;
             using (mySesions)
             {
                 using (ITransaction transaction = mySesions.BeginTransaction())
                 {
                     try
                     {
                        IQuery query = mySesions.CreateQuery("FROM ClInscritoAutonomo WHERE NumDocInscrito=: numdoc")
                       .SetString("numdoc", numdoc);                          
                        IList<ClInscritoAutonomo> inscritoA = query.List<ClInscritoAutonomo>();
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
        public string eliminarInscritoAutonomo(long id) {
            ISession mySesionsD = SessionFactory.OpenSession;
            //string result = false;
            using (mySesionsD)
            {
                using (ITransaction transaction = mySesionsD.BeginTransaction())
                {
                    try
                    {
                        mySesionsD.CreateQuery("DELETE FROM ClInscritoAutonomo WHERE IDINSCRITO = :id").SetInt64("id", id).ExecuteUpdate();
                        transaction.Commit();
                        return "eliminado con exito";
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally {
                        mySesionsD.Flush();
                    }
                }  
            }
        }
        public bool actualizarInscritoAutonomo(ClInscritoAutonomo insA,long idInscrito)
        {
            ISession mySesions = SessionFactory.OpenSession;
            bool resp = false;
            using (mySesions)
            {
                using (ITransaction transaction = mySesions.BeginTransaction())
                {
                    try
                    {
                        IQuery query = mySesions.CreateQuery("FROM ClInscritoAutonomo WHERE IdInscrito=: idInscrito").SetInt64("idInscrito", idInscrito);
                        ClInscritoAutonomo ins = query.List<ClInscritoAutonomo>()[0];
                        ins.NombreInscrito = insA.NombreInscrito;
                        ins.ApellidoInscrito = insA.ApellidoInscrito;
                        ins.CeluInscrito = insA.CeluInscrito;
                        ins.TelefInscrito = insA.TelefInscrito;
                        ins.DirecInscrito = insA.DirecInscrito;
                        ins.EmailInscrito = insA.EmailInscrito;
                        ins.EstadoPrueba = insA.EstadoPrueba;
                        ins.IdTipoEstudiante = insA.IdTipoEstudiante;
                        ins.InformacionCurso = insA.InformacionCurso;
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
        //public bool actualizarNivel(ClInscritoAutonomo insA, long idInscrito)
        //{
        //    ISession mySesions = SessionFactory.OpenSession;
        //    bool resp = false;
        //    using (mySesions)
        //    {
        //        using (ITransaction transaction = mySesions.BeginTransaction())
        //        {
        //            try
        //            {
        //                IQuery query = mySesions.CreateQuery("FROM ClInscritoAutonomo  WHERE IdInscrito=: idInscrito").SetInt64("idInscrito", idInscrito);
        //                ClInscritoAutonomo ins = query.List<ClInscritoAutonomo>()[0];
        //                ins.IdNivel = insA.IdNivel;
        //                ins.IdEstadoEstudiante = insA.IdEstadoEstudiante;
        //                mySesions.Update(ins);
        //                transaction.Commit();
        //                resp = true;
        //                return resp;
        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }
        //            finally
        //            {
        //                mySesions.Flush();
        //            }
        //        }
        //    }
        //}


    }
}

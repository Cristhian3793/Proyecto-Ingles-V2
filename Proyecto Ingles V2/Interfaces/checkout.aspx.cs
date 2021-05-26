using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Proyecto_Ingles_V2.LoginDb;
using Newtonsoft.Json;
using RestSharp;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Logica.ConexionServicios;
using Logica.Modelos;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class FORMPRUEBAMAESTRO : System.Web.UI.Page
    {
        List<Producto> listaCompras = new List<Producto>();
        static conexionServidor cs = new conexionServidor();
        string url = cs.url.ToString();
        protected String exception = "";
        protected async void Page_Load(object sender, EventArgs e)
        {

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                if (Session["usuario"] == null || (string)Session["usuario"] == "")
                {
                    Response.Redirect("../Login/formLogin.aspx");
                }
                if ((int)Session["TipoUser"] == 1)
                {
                    if (!IsPostBack)
                    {

                        LlenarTarjetas();


                    }
                    lbl_fecha.Text = "Fecha: " + DateTime.Now.ToShortDateString();
                    //listaCompras = (List<Producto>)Session["listaCompras"];
                    string usuario = (string)Session["usuario"];
                    cargarInformacionFactura();
                    listaCompras = await CargarInformacion(usuario);
                    Previsualizar();

                    }             
        }
        private void RevisaEnProceso(string identificacion, string identificacionFactura)
        {


            DataSet ds_consulta = Conexion.BuscarNAV_ds("TransaccionesBotonPagosOnline", "id_stupendo", "where ((cast(convert(varchar, fecha, 103) + ' ' + CAST(hora AS VARCHAR(12)) as datetime) <= dateadd(minute, -2, getdate()))" +
                " and (cedula = '" + identificacion + "' or IdentificacionFactura = '" + identificacionFactura + "')) and estado = 'en proceso'");
            if (ds_consulta.Tables[0].Rows.Count > 0)
            {
                for (int z = 0; z < ds_consulta.Tables[0].Rows.Count; z++)
                {
                    string id;
                    DataSet ds = Conexion.BuscarNAV_ds("[DatosPagoEnLinea]", "url,token,action_url,source_url,cod_aprobacion", "where activo=1");
                    string url = ds.Tables[0].Rows[0]["url"].ToString();
                    var client = new RestClient(url);
                    var request = new RestRequest("v1/payment/results", Method.POST);
                    string men = string.Empty;
                    string campos = string.Empty;
                    try
                    {

                        id = ds_consulta.Tables[0].Rows[z]["id_stupendo"].ToString();

                        request.RequestFormat = DataFormat.Json;
                        request.AddJsonBody(new
                        {
                            //token
                            apiToken = ds.Tables[0].Rows[0]["token"].ToString(),
                            id = id,
                            //si los datos adicionales son null o vacios, no daña la consulta
                            additionalData1 = "",
                            additionalData2 = "",
                            additionalData3 = ""
                        });
                        //Se envia el request
                        RestResponse response = (RestResponse)client.Execute(request);

                        //Se trae la respuesta en un String
                        string respuesta = response.Content;
                        if (!respuesta.Contains("No response"))
                        {
                            //raíz
                            XNode node = JsonConvert.DeserializeXNode(respuesta, "Root");
                            XmlDocument xDoc = new XmlDocument();
                            xDoc.LoadXml(node.ToString());
                            //Nodo de transaction (transacción)
                            XmlNodeList transaction = xDoc.GetElementsByTagName("transaction");
                            //nodo de transaction/result (resultado) 
                            XmlNodeList result = ((XmlElement)transaction[0]).GetElementsByTagName("result");
                            //nodo de transaction/result/code (código)
                            XmlNodeList codeNode = ((XmlElement)result[0]).GetElementsByTagName("code");
                            //nodo de transaction/customParameters (customParameters) 
                            XmlNodeList customparameters = ((XmlElement)transaction[0]).GetElementsByTagName("customParameters");
                            //nodo de transaction/customParameters/Shopper_interes (interes) 
                            // XmlNodeList interes = ((XmlElement)customparameters[0]).GetElementsByTagName("SHOPPER_interes")
                            //tipo de crédito
                            XmlNodeList tipoCredito = ((XmlElement)customparameters[0]).GetElementsByTagName("SHOPPER_TIPOCREDITO");
                            //nodo de transaction/recurring (recurring) 
                            //XmlNodeList recurring = ((XmlElement)transaction[0]).GetElementsByTagName("recurring")
                            //nodo de transaction/recurring/numberOfInstallments (cantidad diferidos) 
                            XmlNodeList numberOfInstallments = ((XmlElement)transaction[0]).GetElementsByTagName("numberOfInstallments");
                            //datos adicionales
                            XmlNodeList adicional_1 = xDoc.GetElementsByTagName("additionalData1");
                            XmlNodeList adicional_2 = xDoc.GetElementsByTagName("additionalData2");
                            XmlNodeList adicional_3 = xDoc.GetElementsByTagName("additionalData3");
                            //resultdetails mas detallados
                            XmlNodeList resultdetails = ((XmlElement)transaction[0]).GetElementsByTagName("resultDetails");
                            //Campo que indica si la transacción fue aprobada o rechazada, esto se detalla en la sección de mensajes.
                            XmlNodeList ResponseAprRec = ((XmlElement)resultdetails[0]).GetElementsByTagName("Response");
                            string respuestaCodigo = "-1";
                            if (ResponseAprRec.Count > 0)
                                respuestaCodigo = ResponseAprRec[0].InnerText;

                            //variables
                            string zerobase = string.Empty;
                            string ivabase = string.Empty;
                            string IVA = string.Empty;
                            string codcli = string.Empty;
                            string cedula = string.Empty;
                            string tipo = string.Empty;
                            string tipo_alumno = string.Empty;
                            string observacion = string.Empty;
                            string niveles = string.Empty;
                            string cert = string.Empty;
                            //se obtiene los datos adicionales de la respuesta
                            if (adicional_1.Count > 0)
                                cedula = adicional_1[0].InnerText;

                            if (adicional_2.Count > 0)
                            {
                                //aditionaldata zerobase|ivabase|IVA
                                string[] adicionalSplit = adicional_2[0].InnerText.Split('|');
                                switch (adicionalSplit.Length.ToString())
                                {
                                    case "1":
                                        observacion = adicionalSplit[0].Trim();
                                        break;
                                    case "3":
                                        zerobase = adicionalSplit[0].Trim();
                                        ivabase = adicionalSplit[1].Trim();
                                        IVA = adicionalSplit[2].Trim();
                                        break;
                                }

                            }

                            if (adicional_3.Count > 0)
                            {
                                string[] adicionalSpli3 = adicional_3[0].InnerText.Split('|');
                                zerobase = adicionalSpli3[0].Trim();
                                ivabase = adicionalSpli3[1].Trim();
                                IVA = adicionalSpli3[2].Trim();
                                codcli = adicionalSpli3[3].Trim();
                                tipo = adicionalSpli3[4].Trim();
                                tipo_alumno = adicionalSpli3[5].Trim();
                                niveles = adicionalSpli3[6].Trim();
                                cert = adicionalSpli3[7].Trim();
                            }
                            //
                            //si el valor de query string STATUS es verdadero y el valor de CODE de la consulta de API

                            if (codeNode[0].InnerText == ds.Tables[0].Rows[0]["cod_aprobacion"].ToString())
                            {
                                XmlNodeList AuthCode = ((XmlElement)resultdetails[0]).GetElementsByTagName("AuthCode");
                                //Número de Referencia
                                XmlNodeList ReferenceNbr = ((XmlElement)resultdetails[0]).GetElementsByTagName("ReferenceNbr");
                                string referenciaAux = ReferenceNbr[0].InnerText;
                                string referencia = referenciaAux.Split('_')[1].ToString().PadLeft(8, '0');
                                //Número de Referencia
                                XmlNodeList BatchNo = ((XmlElement)resultdetails[0]).GetElementsByTagName("BatchNo");
                                string lote = BatchNo[0].InnerText;
                                //Nomenclatura del Banco dueño de la tarjeta utilizada en la transacción
                                XmlNodeList CardType = ((XmlElement)resultdetails[0]).GetElementsByTagName("CardType");

                                //nodo transaction/paymentBrand (marca de la Tarjeta) 
                                XmlNodeList paymentBrandNode = ((XmlElement)transaction[0]).GetElementsByTagName("paymentBrand");
                                string tarjeta = paymentBrandNode[0].InnerText;
                                //Valores posibles MASTER,VISA,DINERS,DISCOVER,AMEX
                                switch (tarjeta)
                                {
                                    case "MASTER":
                                        campos += "D_ttar='MC',";
                                        break;
                                    case "VISA":
                                        campos += "D_ttar='VI',";
                                        break;
                                    case "DINERS":
                                        campos += "D_ttar='DN',";
                                        break;
                                    case "DISCOVER":
                                        campos += "D_ttar='DI',";
                                        break;
                                    case "AMEX":
                                        campos += "D_ttar='AM',";
                                        break;
                                    default:
                                        campos += "D_ttar='OT',";
                                        break;
                                }
                                //nodo transaction/amount (total)
                                XmlNodeList amountNode = ((XmlElement)transaction[0]).GetElementsByTagName("amount");
                                string total = amountNode[0].InnerText;
                                decimal subtotal = Convert.ToDecimal(zerobase) + Convert.ToDecimal(ivabase);
                                //total
                                XmlNodeList TotalAmount = ((XmlElement)resultdetails[0]).GetElementsByTagName("TotalAmount");
                                string montoTotal = TotalAmount[0].InnerText;

                                //valor de los intereses
                                XmlNodeList Interest = ((XmlElement)resultdetails[0]).GetElementsByTagName("Interest");
                                string interes = Interest[0].InnerText;

                                //se debe averiguar que tipo de valores se pueden obtener D_sub (subtotal),	D_Iva (IVA),D_Ice(ICE),D_Int(Intereses),D_Tot(subtotal + intereses=total)
                                campos += "D_sub=" + subtotal.ToString().Replace(",", ".") + ",D_tNo= Secuencial,Estado='Autorizado',D_aut='" + AuthCode[0].InnerText + "',D_cDt='xxx',D_datos='xxx',D_Tipo='P',D_Iva=" + IVA.Replace(",", ".") + ",D_Ice=0.00,D_Int=" + interes.Replace(",", ".") + ",D_Tot=" + montoTotal.Replace(",", ".") + ",lote='" + lote + "',referencia='" + referencia + "', Response_Code='" + respuestaCodigo + "'";
                                if (numberOfInstallments.Count == 0)

                                    campos += ",D_Cre='00',D_Mes='1'";
                                else
                                {
                                    // SHOPPER_TIPOCREDITO 

                                    switch (tipoCredito[0].InnerText)
                                    {
                                        case "01":
                                        case "03":
                                            //01 diferido corriente
                                            //03 Diferido sin Interés                                
                                            campos += ",D_Cre='01',D_Mes='" + numberOfInstallments[0].InnerText + "'";
                                            break;
                                        case "02":
                                            //02 Diferido con Interés 
                                            campos += ",D_Cre='02',D_Mes='" + numberOfInstallments[0].InnerText + "'";
                                            break;
                                        default:
                                            // 00 Transacción corriente
                                            campos += ",D_Cre='00',D_Mes='1'";
                                            break;
                                    }


                                }

                                men = Conexion.ActualizarNAV("TransaccionesBotonPagosOnline", campos, "where  id_stupendo='" + id + "'");
                                if (men == "1")
                                {
                                    DataSet ds_correo = Conexion.BuscarNAV_ds("TransaccionesBotonPagosOnline a  left join dbo.Maestro_PagoOnline  b on a.Secuencial=b.secuencial",
                                    "a.*,b.concepto,b.cantidad,b.codconcepto",
                                    "where a.id_stupendo='" + id + "'");
                                    string concepto = ds_correo.Tables[0].Rows[0]["concepto"].ToString();
                                    string cod_concepto = ds_correo.Tables[0].Rows[0]["codconcepto"].ToString();
                                    string secuencial = ds_correo.Tables[0].Rows[0]["Secuencial"].ToString();
                                    if (concepto.Contains("ESPECIE"))
                                    {
                                        if (tipo != "")
                                        {

                                            Conexion.ActualizarUMAS("CERT_EspecieValorada", "pagado=1", "where id_tracking='" + cert + "'");
                                            if (tipo.Contains("baja"))
                                            {
                                                //Ingreso
                                                DataSet ds_tracking = Conexion.BuscarUMAS_ds("CERT_EspecieValorada", "*", "where id_tracking = '" + cert + "'");
                                                DataSet ds_usuario = Conexion.BuscarUMAS_ds("MT_WebUser", "*", "WHERE usuario = '" + cedula + "' ");
                                                DataSet ds_planestud = Conexion.BuscarUMAS_ds("matricula.mt_alumno", "CODPESTUD, CODCARPR", " WHERE CODCLI='" + ds_tracking.Tables[0].Rows[0]["codcli"].ToString() + "'");
                                                DataSet ds_anoperiodo_actual = Conexion.BuscarUMAS_ds("matricula.MT_PARAME", "ANO, PERIODO", "");
                                                string anio_periodo = ds_anoperiodo_actual.Tables[0].Rows[0]["ANO"].ToString() + "-" + ds_anoperiodo_actual.Tables[0].Rows[0]["PERIODO"].ToString();

                                                string ap_pater = ds_usuario.Tables[0].Rows[0]["AP_Pater"].ToString().Trim();
                                                string ap_mater = ds_usuario.Tables[0].Rows[0]["AP_Mater"].ToString().Trim();
                                                string nombre = ds_usuario.Tables[0].Rows[0]["nombre"].ToString().Trim();
                                                string creador = ap_pater + " " + nombre;
                                                if (creador.Length > 30)
                                                    creador = creador.Substring(0, 30);
                                                string anio = DateTime.Now.Year.ToString();
                                                string id_tipoRequerimiento = "22";
                                                string codcarr = ds_planestud.Tables[0].Rows[0]["CODCARPR"].ToString();
                                                DataSet ds_nivel = Conexion.BuscarUMAS_ds("[ZonaReportes_Ecuador].dbo.SEK_inscripcion_asignaturas", " distinct top 1 *",
                                                    " where ('0'+cedula='" + cedula + "' or cedula='" + cedula + "')  and baja is null and CODCARR='" + codcarr + "' order by anoperiodo desc,nivel desc");
                                                string nivel = string.Empty;
                                                if (ds_nivel.Tables[0].Rows.Count > 0)
                                                {
                                                    nivel = ds_nivel.Tables[0].Rows[0]["nivel"].ToString();
                                                }
                                                observacion = ds_tracking.Tables[0].Rows[0]["observacion"].ToString();
                                                string fecha = DateTime.Now.ToString();

                                                string traking_AAE = string.Empty;

                                                string semestre = Conexion.BuscarUMAS_ds_simple("[matricula].[SemestreDe]('" + ds_anoperiodo_actual.Tables[0].Rows[0]["ANO"].ToString() + "', '" + ds_anoperiodo_actual.Tables[0].Rows[0]["PERIODO"].ToString() + "')").Tables[0].Rows[0][0].ToString();


                                                //se obtiene el ultimo tracking
                                                DataSet ds_ = Conexion.BuscarUMAS_ds("AAE_Solicitud", "top 1 id_tracking,anio", "where anio_periodo='" + ds_anoperiodo_actual.Tables[0].Rows[0]["ANO"].ToString() + "-" + ds_anoperiodo_actual.Tables[0].Rows[0]["PERIODO"].ToString() + "' order by id desc,id_tracking desc");
                                                //se crea el nuevo tracking 
                                                if (ds_.Tables[0].Rows.Count > 0)
                                                {
                                                    traking_AAE = ds_.Tables[0].Rows[0]["id_tracking"].ToString();
                                                    string cont = traking_AAE.Substring(traking_AAE.LastIndexOf("_") + 1);
                                                    if (int.Parse(cont) < 10)
                                                        traking_AAE = "SEK_AAE_" + semestre + "_00";
                                                    else if (int.Parse(cont) < 100)
                                                        traking_AAE = "SEK_AAE_" + semestre + "_0";
                                                    else
                                                        traking_AAE = "SEK_AAE_" + semestre + "_";

                                                    traking_AAE += (int.Parse(cont) + 1).ToString();
                                                }
                                                else
                                                {
                                                    traking_AAE = "SEK_AAE_" + semestre + "_001";
                                                }

                                                string ingreso_base_AAE_solicitud = Conexion.InsertarUMAS("AAE_solicitud", "id_tracking,identificacion,nombre,ap_pater,ap_mater,codcarr,nivel,anio,anio_periodo,fecha_creacion,creador,id_tipoRequerimiento,observacion,estado",
                                                "'" + traking_AAE + "','" + cedula + "','" + nombre + "','" + ap_pater + "','" + ap_mater + "','" + codcarr + "','" + nivel + "','" + anio + "','" + anio_periodo + "','" + fecha + "','" + creador + "'," + id_tipoRequerimiento +
                                                ",'" + observacion + "',0");
                                            }
                                        }
                                        else
                                        {
                                            Conexion.ActualizarUMAS("ESPE_EspecieExt", " pagado='1'", " where secuencial='" + secuencial + "'");


                                        }
                                    }
                                }
                            }
                            else
                            {
                                //cuando el If no es verdadero es decir el STATUS es false o el CODE no es el de APROBADO o PAGADO
                                campos = "Estado = 'Negado', Response_Code='" + respuestaCodigo + "'";

                                men = Conexion.ActualizarNAV("TransaccionesBotonPagosOnline", campos, "where id_stupendo='" + id + "'");
                            }

                        }
                        else
                        {
                            campos = "Estado = 'Negado', Response_Code='-1'";
                            men = Conexion.ActualizarNAV("TransaccionesBotonPagosOnline", campos, "where id_stupendo='" + id + "'");

                        }

                    }
                    catch (Exception ex)
                    {
                        Conexion.Log(ex.Message.Replace("'", "\""), "click actualizar_2.aspx");
                    }
                }
            }
        }
        public async void Previsualizar()
        {
            try
            {
                //detalle , creditos , costo 
                //listaCompras = (List<Producto>)Session["listaCompras"];
                string usuario = (string)Session["usuario"];
                listaCompras  =await CargarInformacion(usuario);

                


                //asignaturas 
                decimal valorPago = 0;

                //inicia grid valores 

                //llena la cabecera
                TableHeaderRow RowTableHead = new TableHeaderRow();
                RowTableHead.TableSection = TableRowSection.TableHeader;
                string[] encabezados = new string[4] { "Cantidad", "Descripción", "Precio Unitario", "Precio" };
                foreach (var y in encabezados)
                {
                    TableHeaderCell CellHead = new TableHeaderCell
                    {
                        HorizontalAlign = HorizontalAlign.Center,
                        BackColor = Color.LightGray,
                        Text = y
                    };
                    RowTableHead.Cells.Add(CellHead);
                }
                grd_Valores.Rows.Add(RowTableHead);
                //llena el detalle de lo que va pagar
                for (int i = 0; i < listaCompras.Count; i++)
                {
                    TableRow RowTable = new TableRow();

                    TableCell Cell_cant = new TableCell
                    {
                        Text = listaCompras[i].cantidad.ToString()
                    };
                    TableCell Cell_desc = new TableCell
                    {
                        Text = listaCompras[i].concepto.ToString()
                    };
                    TableCell Cell_unit = new TableCell
                    {
                        Text = listaCompras[i].importe.ToString(),
                        CssClass = "dolar"
                    };
                    TableCell Cell_precio = new TableCell
                    {
                        Text = Math.Round(Convert.ToDecimal(listaCompras[i].importe) * Convert.ToDecimal(listaCompras[i].cantidad), 2, MidpointRounding.AwayFromZero).ToString(),
                        CssClass = "dolar"
                    };
                    RowTable.Cells.Add(Cell_cant);
                    RowTable.Cells.Add(Cell_desc);
                    RowTable.Cells.Add(Cell_unit);
                    RowTable.Cells.Add(Cell_precio);

                    grd_Valores.Rows.Add(RowTable);

                }
                //termina de llenando los valores totales y subtotales

                decimal totalsinIVA = 0;
                decimal totalconIVA = 0;
                decimal IVA = 0;
                decimal total = 0;

                bool aux = true;
                for (int j = 0; j < listaCompras.Count; j++)
                {
                    DataSet ds = Conexion.BuscarNAV_ds("[UISEK_ECUADOR$Concepto Facturación] AS A INNER JOIN[UISEK_ECUADOR$Tarifa de concepto] AS B ON  A.[Cód_ concepto] = B.[Cód_ concepto] and getdate() between[Fecha inicial] and  dateadd(minute, 1439,[fecha final])", "[Grupo IVA neg_ producto]",
                        "where a.[cód_ concepto]='" + listaCompras[j].codConcepto + "'");
                    switch (ds.Tables[0].Rows[0][0].ToString())
                    {
                        case "IVA12":
                        case "IVA14":
                            totalconIVA += Math.Round(Convert.ToDecimal(listaCompras[j].importe) * Convert.ToDecimal(listaCompras[j].cantidad), 2, MidpointRounding.AwayFromZero);
                            aux = false;
                            break;
                        default:
                            totalsinIVA += Math.Round(Convert.ToDecimal(listaCompras[j].importe) * Convert.ToDecimal(listaCompras[j].cantidad), 2, MidpointRounding.AwayFromZero);
                            aux = false;
                            break;
                    }
                }
                IVA = Math.Round(totalconIVA * Convert.ToDecimal(0.12), 2, MidpointRounding.AwayFromZero);
                total = (totalconIVA + IVA) + totalsinIVA;
                if (aux)
                    valorPago = 0;
                else
                    valorPago = total;


                TableRow RowTable_IvaCero = new TableRow();
                //ingresa el valor base (sin IVA)
                //texto
                TableCell Cell_IvaCero = new TableCell
                {
                    Text = "Total grabado con IVA tarifa 0% =",
                    ColumnSpan = 3,
                    BackColor = Color.LightGray,
                    HorizontalAlign = HorizontalAlign.Right
                };
                RowTable_IvaCero.Cells.Add(Cell_IvaCero);
                //valor
                TableCell Cell_IvaCero_t = new TableCell
                {
                    Text = "" + totalsinIVA,
                    BackColor = Color.LightGray,
                    HorizontalAlign = HorizontalAlign.Right,
                    CssClass = "dolar"
                };
                RowTable_IvaCero.Cells.Add(Cell_IvaCero_t);

                grd_Valores.Rows.Add(RowTable_IvaCero);
                //ingresa el valor base (con IVA)
                //Texto
                TableRow RowTable_Iva12 = new TableRow();
                TableCell Cell_Iva12 = new TableCell
                {
                    Text = "Total grabado con IVA 12% =",
                    ColumnSpan = 3,
                    BackColor = Color.LightGray,
                    HorizontalAlign = HorizontalAlign.Right
                };
                RowTable_Iva12.Cells.Add(Cell_Iva12);
                //Valor
                TableCell Cell_Iva12_t = new TableCell
                {
                    Text = "" + totalconIVA,
                    BackColor = Color.LightGray,
                    HorizontalAlign = HorizontalAlign.Right,
                    CssClass = "dolar"
                };
                RowTable_Iva12.Cells.Add(Cell_Iva12_t);

                grd_Valores.Rows.Add(RowTable_Iva12);
                //IVA
                TableRow RowTable_Iva = new TableRow();
                //Texto
                TableCell Cell_Iva = new TableCell
                {
                    Text = "Importe del IVA =",
                    ColumnSpan = 3,
                    BackColor = Color.LightGray,
                    HorizontalAlign = HorizontalAlign.Right
                };
                RowTable_Iva.Cells.Add(Cell_Iva);
                //Valor
                TableCell Cell_Iva_t = new TableCell
                {
                    Text = "" + IVA,
                    BackColor = Color.LightGray,
                    HorizontalAlign = HorizontalAlign.Right,
                    CssClass = "dolar"
                };
                RowTable_Iva.Cells.Add(Cell_Iva_t);

                grd_Valores.Rows.Add(RowTable_Iva);
                //inseta el total
                TableRow RowTable_Total = new TableRow();
                //texto
                TableCell Cell_Total = new TableCell
                {
                    Text = "Importe Total = ",
                    ColumnSpan = 3,
                    BackColor = Color.LightGray,
                    HorizontalAlign = HorizontalAlign.Right
                };
                RowTable_Total.Cells.Add(Cell_Total);
                //Valor
                TableCell Cell_Total_t = new TableCell
                {
                    Text = "" + total,
                    BackColor = Color.LightGray,
                    HorizontalAlign = HorizontalAlign.Right,
                    CssClass = "dolar"
                };
                RowTable_Total.Cells.Add(Cell_Total_t);

                grd_Valores.Rows.Add(RowTable_Total);

            }
            catch (Exception ex)
            {
                //envía un mesaje de que sucedió un error
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ventana", @"alert('Ventana no disponible'); window.close();", true);
                //registra el error en la tabla de errores del pago de matrículas 
                Conexion.Log(ex.Message.Replace("'", "\""), "checkout.aspx");
            }
        }
        private bool ValidaDifSinI()
        {
            return (listaCompras.Exists(x => x.codConcepto.Contains("SEK1655")) || listaCompras.Exists(x => x.codConcepto.Contains("SEK1656"))
            || listaCompras.Exists(x => x.codConcepto.Contains("SEK1083")) || listaCompras.Exists(x => x.codConcepto.Contains("SEK1084"))
             || listaCompras.Exists(x => x.codConcepto.Contains("SEK1152")) || listaCompras.Exists(x => x.codConcepto.Contains("SEK1610"))
             || listaCompras.Exists(x => x.codConcepto.Contains("SEK1667")));
        }
        private string ListaDifSinInt()
        {
            StringBuilder codProductos = new StringBuilder();
            string listaRespuesta;
            if (ValidaDifSinI())
            {
                List<string> lista = new List<string>
                {
                    "SEK1655",
                    "SEK1656",
                    "SEK1083",
                    "SEK1084",
                    "SEK1152",
                    "SEK1610",
                    "SEK1667"
                };
                foreach (string producto in lista)
                {
                    codProductos.Append("codproducto='" + producto + "' or ");
                }
                listaRespuesta = codProductos.ToString(0, codProductos.ToString().LastIndexOf("or"));
            }
            else
                listaRespuesta = "codProducto='00000'";
            return listaRespuesta;
        }

        protected void chkTerminos_CheckedChanged(object sender, EventArgs e)
        {

            if (chkTerminos.Checked)
            {
                if (txtCedula.Text == "" || txtDireccion.Text == "" || txtEmail.Text == "" || txtNombre.Text == "" || txtTelefono.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('Falta de llenar alguno/s campo/s obligatorio/s');", true);
                    chkTerminos.Checked = false;
                }
                else if (!txtEmail.Text.Contains("@"))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('Por favor llene el correo correctamente.');", true);
                    chkTerminos.Checked = false;
                }
                else
                {
                    txtCedula.Enabled = false;
                    txtDireccion.Enabled = false;
                    txtEmail.Enabled = false;
                    txtNombre.Enabled = false;
                    txtTelefono.Enabled = false;
                    ddlTipo.Enabled = false;
                }
            }
            else
            {
                txtCedula.Enabled = true;
                txtDireccion.Enabled = true;
                txtEmail.Enabled = true;
                txtNombre.Enabled = true;
                txtTelefono.Enabled = true;
                ddlTipo.Enabled = true;
            }
        }

        private string OnServerURL()
        {
            return HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
        }

        protected async void btn_pagar_Click(object sender, EventArgs e)
        {
            
            if (chkTerminos.Checked)
            {
                listaCompras = await CargarInformacion((string)Session["usuario"]);
                string codNivel = listaCompras.Select(x => x.codConcepto).FirstOrDefault();
                List<ClInscritoAutonomo> ins =await getDatosInscrito();
                string cedula = ins.Where(a => a.NumDocInscrito.Trim() == (string)Session["usuario"]).Select(a => a.NumDocInscrito).FirstOrDefault();
                string alumno = ins.Where(a => a.NumDocInscrito.Trim() == (string)Session["usuario"]).Select(a => a.NombreInscrito).FirstOrDefault();


                string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                //Si no venía de un proxy, tomo la ip del visitante 
                if (string.IsNullOrEmpty(ip))
                    ip = Request.ServerVariables["REMOTE_ADDR"];


                string host = HttpContext.Current.Request.Url.Host.ToLower();
                if (host == "localhost")
                    ip = "186.5.25.90";


                //revisa las transacciones mas recientes y las transacciones de esa cédula
                //RevisaEnProceso(cedula, txtCedula.Text);
                DataSet ds_cosnutlaPagosPrevios = Conexion.BuscarNAV_ds("TransaccionesBotonPagosOnline", "count(*)",
                    "where (( cast (convert(varchar ,fecha,103) + ' ' + CAST(hora AS VARCHAR(12)) as datetime)>=dateadd(DAY,-7,getdate())) and origen=1 and (cedula='" + cedula + "' or IdentificacionFactura='" + txtCedula.Text + "')) and estado='autorizado' ");
                if (int.Parse(ds_cosnutlaPagosPrevios.Tables[0].Rows[0][0].ToString()) > 0)
                {
                    bool compra_anterior = false;
                    DataSet ds_transacciones = Conexion.BuscarNAV_ds("TransaccionesBotonPagosOnline", "secuencial",
                    "where (( cast (convert(varchar ,fecha,103) + ' ' + CAST(hora AS VARCHAR(12)) as datetime)>=dateadd(DAY,-7,getdate())) and origen=1 and (cedula='" + cedula + "' or IdentificacionFactura='" + txtCedula.Text + "')) and estado='autorizado' ");

                    foreach (DataRow x in ds_transacciones.Tables[0].Rows)
                    {
                        DataSet ds_productos = Conexion.BuscarNAV_ds("Maestro_PagoOnline", "*", "where secuencial='" + x["secuencial"].ToString() + "'");
                        foreach (DataRow y in ds_productos.Tables[0].Rows)
                        {

                            foreach (Producto z in listaCompras)
                            {
                                if (z.codConcepto.ToString() == y["codconcepto"].ToString())
                                {
                                    compra_anterior = true;
                                    break;
                                }
                            }
                            if (compra_anterior)
                                break;
                        }
                        if (compra_anterior)
                            break;
                    }
                    if (compra_anterior)
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"doblepago();", true);
                    else
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"doblepago_2('" + ds_cosnutlaPagosPrevios.Tables[0].Rows[0][0].ToString() + "');", true);
                }

                //variables para el calculo del importe total 
                decimal totalsinIVA = 0;
                decimal totalconIVA = 0;
                decimal IVA = 0;
                decimal total = 0;
                //calculo de los valores 
                foreach (Producto x in listaCompras)
                {
                    //contra que se confirma el valor de facturacion 
                    DataSet ds = Conexion.BuscarNAV_ds("[UISEK_ECUADOR$Concepto Facturación] AS A INNER JOIN[UISEK_ECUADOR$Tarifa de concepto] AS B ON  A.[Cód_ concepto] = B.[Cód_ concepto] and getdate() between[Fecha inicial] and  dateadd(minute, 1439,[fecha final])", "[Grupo IVA neg_ producto]",
                        "where a.[cód_ concepto]='" + x.codConcepto.Trim() + "'");

                    switch (ds.Tables[0].Rows[0][0].ToString())
                    {
                        case "IVA12":
                        case "IVA14":
                            totalconIVA += Math.Round(Convert.ToDecimal(x.importe) * Convert.ToDecimal(x.cantidad), 2, MidpointRounding.AwayFromZero);
                            break;
                        default:
                            totalsinIVA += Math.Round(Convert.ToDecimal(x.importe) * Convert.ToDecimal(x.cantidad), 2, MidpointRounding.AwayFromZero);
                            break;
                    }
                }
                //IVA
                IVA = Math.Round(totalconIVA * Convert.ToDecimal(0.12), 2, MidpointRounding.AwayFromZero);
                //total de valores 
                total = totalconIVA + totalsinIVA + IVA;

                string id;
                //ingresa la transacción 
               
                        id = Conexion.GuardaTransaccion(cedula, total.ToString().Replace(",", "."), IVA.ToString().Replace(",", "."), txtNombre.Text, ddlTipo.SelectedValue, txtCedula.Text, txtTelefono.Text, txtDireccion.Text, txtEmail.Text, ip, 0);
                //crea un maestro trasacción
                string ingMaestro = Conexion.GuardarMaestro(id, listaCompras);

                //confirma que tanto el id y el ingreso en el maestro transaccion se haya creado 
                if (id != "0" && ingMaestro == "1")
                {
                    try
                    {
                        DataSet ds_datopago = Conexion.BuscarNAV_ds("[DatosPagoEnLinea]", "url,token,action_url,source_url,cod_aprobacion", "where activo=1");
                        string url = ds_datopago.Tables[0].Rows[0]["url"].ToString();

                        var client = new RestClient(url);
                        var request = new RestRequest("v1/payment/checkouts", Method.POST)
                        {
                            RequestFormat = DataFormat.Json
                        };
                        request.AddJsonBody(new
                        {
                            apiToken = ds_datopago.Tables[0].Rows[0]["token"].ToString(),
                            // amount es el valor de los items sumados con iva y sin iva
                            amount = total,
                            name = txtNombre.Text,
                            ip = ip,
                            email = txtEmail.Text,
                            identificationDocId = txtCedula.Text,
                            phone = txtTelefono.Text,
                            street1 = txtDireccion.Text,
                            // iva value es la sumatoria de todos los ivas de los items con iva 
                            ivaValue = IVA,
                            //ivabase es el valor del objeto que debe tener iva pero  sin iva
                            ivaBase = totalconIVA,
                            zeroBase = totalsinIVA,
                            tipValue = 0.00,
                            iceBase = 0.00,
                            irbpnrBase = 0.00,
                            destinyUrl = OnServerURL() + "/Interfaces/pagoresp.aspx",
                            additionalData1 = txtCedula.Text,
                            additionalData2 = totalsinIVA + "|" + totalconIVA + "|" + IVA,
                            additionalData3 = "" + cedula + "|" + alumno + "|" + codNivel + "| ING AUT"
                        });
                        //Se envia el request
                        RestResponse response = (RestResponse)client.Execute(request);// despues de esta linea cargar grid

                        //Se trae la respuesta en un String
                        string respuesta = response.Content;
                        //Raíz
                        XNode node = JsonConvert.DeserializeXNode(respuesta, "Root");
                        XmlDocument xDoc = new XmlDocument();
                        xDoc.LoadXml(node.ToString());
                        //resultado
                        XmlNodeList result = xDoc.GetElementsByTagName("result");
                        //código
                        XmlNodeList codeNode = ((XmlElement)result[0]).GetElementsByTagName("code");
                        string code = codeNode[0].InnerText;
                        //revisa si el código recibido es el correcto
                        switch (code)
                        {
                            case "000.200.100":
                                XmlNodeList idNode = xDoc.GetElementsByTagName("id");
                                string id_stupendo = idNode[0].InnerText;
                                //actualiza el id devuelto por Stupendo
                                Conexion.ActualizarNAV("TransaccionesBotonPagosOnline", "id_stupendo='" + id_stupendo + "'", "where secuencial='" + id + "'");

  

                                pnl_boton.Visible = false;
                                pnl_pagos.Visible = true;
                                string meses = rbl_meses.SelectedValue;
                                DataSet ds_ConfigPago = Conexion.BuscarNAV_ds("VS_PagosOnLine_FormaCreditoTarjeta",
                                    "id,id_banco,Banco,id_tarjeta,Tarjeta,SiglasTarjeta,siglas_largo,id_TipoCredito,nombre,codigo,corriente,conInteres,meses,disp_matricula,disp_carrito,disp_otros,post,disp_inglesautonomo",
                                    "where id='" + meses + "'");

                                if (ds_ConfigPago.Tables[0].Rows.Count > 0)
                                {
                                    DataSet ds_codFormaNav = Conexion.BuscarNAV_ds("FormasPagoOnline", "top 1 Cod_FormaNAV", " where  ID_Banco='" + ds_ConfigPago.Tables[0].Rows[0]["id_banco"].ToString() + "' and CodTarjeta='" + ds_ConfigPago.Tables[0].Rows[0]["SiglasTarjeta"].ToString() + "' " +
                                        "and [Con Interes]='" + ds_ConfigPago.Tables[0].Rows[0]["conInteres"].ToString() + "' and Corriente='" + ds_ConfigPago.Tables[0].Rows[0]["corriente"].ToString() + "' ");
                                    Conexion.ActualizarNAV("TransaccionesBotonPagosOnline", "CodFormaNav='" + ds_codFormaNav.Tables[0].Rows[0][0].ToString() + "'", " where id_stupendo='" + id_stupendo + "'");

                                    if_pagos.Src = "/Interfaces/pagodata.aspx?id=" + id_stupendo + "&total=" + total + "&tipo='" + ds_ConfigPago.Tables[0].Rows[0]["codigo"].ToString() +
                                         "'&dif=" + ds_ConfigPago.Tables[0].Rows[0]["meses"].ToString() + "&brand=" + ds_ConfigPago.Tables[0].Rows[0]["siglas_largo"].ToString();
                                }
                                else
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('El método de pago seleccionado no fue encontrado, por favor comuniquese con administración o envíe un correo a helpdesk@uisek.edu.ec .');", true);
                                break;
                            case "800.800.401":

                                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('El valor ha pagar no es correcto.');", true);
                                break;
                            default:
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('No se pudo concluir el pago. Vuelva a intentarlo en unos minutos.');", true);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('No se pudo concluir el pago. Vuelva a intentarlo en unos minutos.');console.log('" + ex.Message.Replace("'", "\"") + "')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('Error ');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('Acepte para poder continuar.');", true);
            }
        }
        private void LlenarTarjetas()
        {
            ddl_Tarjeta.Items.Clear();
            DataSet ds_Tarjeta = Conexion.BuscarNAV_ds("PagosOnLine_Tarjetas", "distinct nombre,id", "");
            if (ds_Tarjeta.Tables[0].Rows.Count > 0)
            {
                ddl_Tarjeta.DataSource = ds_Tarjeta.Tables[0];
                ddl_Tarjeta.DataTextField = "nombre";
                ddl_Tarjeta.DataValueField = "id";
                ddl_Tarjeta.DataBind();
            }
            ddl_Tarjeta.Items.Insert(0, new ListItem("Seleccione una opción", "0"));
            ddl_Tarjeta.SelectedIndex = 0;
            LlenarBancos(ddl_Tarjeta.SelectedValue);
        }
        private void LlenarBancos(string idTarjeta)
        {
            ddl_Banco.Items.Clear();
            DataSet ds_Banco = Conexion.BuscarNAV_ds("VS_PagosOnLine_TarjetasBanco", "distinct id_banco,Banco", "where id_tarjeta='" + idTarjeta + "'");
            if (ds_Banco.Tables[0].Rows.Count > 0)
            {
                ddl_Banco.DataSource = ds_Banco.Tables[0];
                ddl_Banco.DataTextField = "Banco";
                ddl_Banco.DataValueField = "id_banco";
                ddl_Banco.DataBind();
            }
            ddl_Banco.Items.Insert(0, new ListItem("Seleccione una opción", "0"));
            ddl_Banco.SelectedIndex = 0;
            LlenarTipoCredito(ddl_Tarjeta.SelectedValue, ddl_Banco.SelectedValue);
        }
        private void LlenarTipoCredito(string idTarjeta, string idBanco)
        {
            ddl_TipoCredito.Items.Clear();
            DataSet ds_TipoCredito = Conexion.BuscarNAV_ds("VS_PagosOnLine_FormaCreditoTarjeta a", "distinct codigo,nombre", "where id_tarjeta='" + idTarjeta + "' and id_banco='" + idBanco + "' " +
                "and (disp_InglesAutonomo=1 )");
            if (ds_TipoCredito.Tables[0].Rows.Count > 0)
            {
                ddl_TipoCredito.DataSource = ds_TipoCredito.Tables[0];
                ddl_TipoCredito.DataTextField = "nombre";
                ddl_TipoCredito.DataValueField = "codigo";
                ddl_TipoCredito.DataBind();
            }
            ddl_TipoCredito.Items.Insert(0, new ListItem("Seleccione una opción", "0"));
            ddl_TipoCredito.SelectedIndex = 0;
            LlenarMeses(ddl_Tarjeta.SelectedValue, ddl_Banco.SelectedValue, ddl_TipoCredito.SelectedValue);
        }
        private void LlenarMeses(string idTarjeta, string idBanco, string idTipoCredito)
        {

            rbl_meses.Items.Clear();
            rbl_meses.ClearSelection();

            DataSet ds_Meses = Conexion.BuscarNAV_ds("VS_PagosOnLine_FormaCreditoTarjeta a", "id,meses", "where id_tarjeta='" + idTarjeta + "' and id_banco='" + idBanco + "' and codigo='" + idTipoCredito + "' " +
                "and (disp_InglesAutonomo=1)");
            if (ds_Meses.Tables[0].Rows.Count > 0)
            {
                rbl_meses.DataSource = ds_Meses.Tables[0];
                rbl_meses.DataValueField = "id";
                rbl_meses.DataTextField = "meses";
                rbl_meses.DataBind();
                foreach (ListItem x in rbl_meses.Items)
                    x.Attributes.Add("class", "radio-inline");
                if (ds_Meses.Tables[0].Rows.Count == 1)
                    rbl_meses.SelectedIndex = 0;
            }

        }

        protected void ddl_Tarjeta_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarBancos(ddl_Tarjeta.SelectedValue);
        }

        protected void ddl_Banco_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarTipoCredito(ddl_Tarjeta.SelectedValue, ddl_Banco.SelectedValue);

        }

        protected void ddl_TipoCredito_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarMeses(ddl_Tarjeta.SelectedValue, ddl_Banco.SelectedValue, ddl_TipoCredito.SelectedValue);
        }

        #region Metodos


        public async Task<List<Producto>> CargarInformacion(string usuario)
        {
            Producto pro = new Producto();
            //Inscritos
            List<ClInscritoAutonomo> ins = await ServicioExtraerInscritos();
            List<ClPeriodoInscripcion> per = await ServicioExtraerPeriodos();
            List<ClTipoEstudiante> tipo = await ServicioGetTipoEstudiante();
            List<ClPrueba> prueba = await ServicioExtraerPrueba();
            //NIveles
            List<ClNivel> nivel = await ServicioGetNiveles();
            List<ClEstadoEstudiante> estadoEstu = await ServicioGetEstadoEstudiante();
            List<ClTipoNivel> tiponivel = await ServicioCargarTipoNivel();
            List<ClCurso> curso = await ServicioCargarCursos();
            List<ClNivelesInscrito> nivIns = await ServicioGetNivelesInscritos();


            var infoNivelPago = from a in ins                               
                                join c in nivIns on a.IdInscrito equals c.IDINSCRITO
                                join b in per on c.IDPERIODOINSCRIPCION equals b.IdPeriodoInscripcion
                                join h in prueba on c.IDPRUEBAUBICACION equals h.IdPrueba into NivelesPrueba
                                from i in NivelesPrueba.DefaultIfEmpty()
                                join d in nivel on c.IDNIVEL equals d.idNivel
                                join e in curso on d.idCurso equals e.IdCurso
                                join f in tiponivel on d.idTipoNivel equals f.idtipoNivel
                                join g in estadoEstu on c.IDESTADONIVEL equals g.CodEstadoEstu

                                where a.NumDocInscrito.Trim() == usuario && c.IDESTADONIVEL == 0
                                select new
                                {
                                    IDINSCRITO = a.IdInscrito,
                                    IDNIVEL = c.IDNIVELESTUDIANTE,
                                    NUMDOCINSCRITO = a.NumDocInscrito,
                                    NOMBINSCRITO = a.NombreInscrito,
                                    APELLIINSCRITO = a.ApellidoInscrito,
                                    PERIODO = b.Periodo,
                                    PUNTAJEPRUEBA = NivelesPrueba.Select(x => x.PunjatePrueba).FirstOrDefault(),
                                    Email = a.EmailInscrito,
                                    DESCCURSO = e.DescCurso,
                                    DESCTIPONIVEL = d.descNivel,
                                    NOMNIVEL = d.nomNivel,
                                    DESCNIVEL = d.descNivel,
                                    COSTONIVEL = d.costoNIvel,
                                    CODCURSO = d.codNivel,
                                    IDNIVELESTUDIANTE = c.IDNIVELESTUDIANTE,
                                    IDESTADONIVEL = c.IDESTADONIVEL,
                                    DESCESTADOESTUDIANTE = g.DescEstEstudiante,

                                };
            if (infoNivelPago.Count() > 0)
            {
                listaCompras = new List<Producto>();
                Session["idNivel"] = infoNivelPago.Select(x => x.IDNIVELESTUDIANTE).FirstOrDefault();
                Session["codNivel"]= infoNivelPago.Select(x => x.CODCURSO).FirstOrDefault().Trim().ToString();
                pro.codConcepto = infoNivelPago.Select(x => x.CODCURSO).FirstOrDefault().ToString().Trim();
                pro.concepto = infoNivelPago.Select(x => x.NOMNIVEL).FirstOrDefault();
                pro.importe = Convert.ToDecimal(infoNivelPago.Select(x => x.COSTONIVEL).FirstOrDefault());
                pro.cantidad = 1;
                pro.iva = 0;
                listaCompras.Add(pro);
            }
            else
            {
                Session["idNivel"] = "";
                Session["codNivel"] = "";
                pro.codConcepto = "";
                pro.concepto = "";
                pro.importe = 0;
                pro.cantidad = 0;
                pro.iva = 0;
            }

            return listaCompras;
        }
        #endregion
        #region Servicios
        public async Task<List<ClEstadoEstudiante>> ServicioGetEstadoEstudiante()
        {
            List<ClEstadoEstudiante> compInf = new List<ClEstadoEstudiante>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/EstadoEstudiante");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClEstadoEstudiante>>(empResponse);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClInscritoAutonomo>> getDatosInscrito() {
            List<ClInscritoAutonomo> ins =await ServicioExtraerInscritos();
            return ins;
        }

        public async Task<List<ClInscritoAutonomo>> ServicioExtraerInscritos()
        {
            List<ClInscritoAutonomo> compInf = new List<ClInscritoAutonomo>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/InscritoAutonomo");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClInscritoAutonomo>>(empResponse);
                    return compInf;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClPeriodoInscripcion>> ServicioExtraerPeriodos()
        {
            List<ClPeriodoInscripcion> compInf = new List<ClPeriodoInscripcion>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/PeriodoInscripcion");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClPeriodoInscripcion>>(empResponse);

                }

                return compInf;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClNivelesInscrito>> ServicioGetNivelesInscritos()
        {
            List<ClNivelesInscrito> compInf = new List<ClNivelesInscrito>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/NivelesInscrito");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClNivelesInscrito>>(empResponse);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClTipoEstudiante>> ServicioGetTipoEstudiante()
        {
            List<ClTipoEstudiante> compInf = new List<ClTipoEstudiante>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/TipoEstudiante");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClTipoEstudiante>>(empResponse);

                }

                return compInf;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;
        }
        public async Task<List<ClPrueba>> ServicioExtraerPrueba()
        {
            List<ClPrueba> compInf = new List<ClPrueba>();
            try
            {
                //string url = "http://servicioinglesuisek/";               
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Prueba");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClPrueba>>(empResponse);
                    return compInf;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return compInf;


        }

        public async Task<List<ClNivel>> ServicioGetNiveles()
        {
            List<ClNivel> compInf = new List<ClNivel>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Niveles");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    compInf = JsonConvert.DeserializeObject<List<ClNivel>>(empResponse);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return compInf;
        }

        public async Task<List<ClCurso>> ServicioCargarCursos()
        {
            List<ClCurso> cur = new List<ClCurso>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/Curso");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    cur = JsonConvert.DeserializeObject<List<ClCurso>>(empResponse);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return cur;
        }

        public async Task<List<ClTipoNivel>> ServicioCargarTipoNivel()
        {
            List<ClTipoNivel> cur = new List<ClTipoNivel>();
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                HttpResponseMessage res = await client.GetAsync("api/TipoNivel");
                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    cur = JsonConvert.DeserializeObject<List<ClTipoNivel>>(empResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return cur;
        }
        public async void cargarInformacionFactura() {

            Producto pro = new Producto();
            //Inscritos
            List<ClInscritoAutonomo> ins = await ServicioExtraerInscritos();
            var queryInscritos = from a in ins

                                 where a.NumDocInscrito.Trim().ToString() == (string)Session["usuario"]
                                 select new
                                 {
                                     IDINSCRITO = a.IdInscrito,
                                     NUMDOCINSCRITO = a.NumDocInscrito,
                                     NOMBINSCRITO = a.NombreInscrito,
                                     APELLIINSCRITO = a.ApellidoInscrito,
                                     Email = a.EmailInscrito,
                                     Direccion = a.DirecInscrito,
                                     Telefono = a.TelefInscrito,
                                     Celular = a.CeluInscrito,
                                     TIPODOCUMENTO=a.IdTipoDocumento,//1 cedula 2 pasaporte


                                 };

            string nombre = queryInscritos.Select(x => x.NOMBINSCRITO).FirstOrDefault().ToString().Trim();
            string apellido = queryInscritos.Select(x => x.APELLIINSCRITO).FirstOrDefault().ToString().Trim();
            string nombreCompleto = nombre + " " + apellido;
            txtNombre.Text = nombreCompleto;
            ddlTipo.SelectedValue = queryInscritos.Select(x => x.TIPODOCUMENTO).FirstOrDefault().ToString();
            txtDireccion.Text= queryInscritos.Select(x => x.Direccion).FirstOrDefault().ToString().Trim();
            txtEmail.Text= queryInscritos.Select(x => x.Email).FirstOrDefault().ToString().Trim();
            txtTelefono.Text = queryInscritos.Select(x => x.Celular).FirstOrDefault().ToString().Trim();
            txtCedula.Text= queryInscritos.Select(x => x.NUMDOCINSCRITO).FirstOrDefault().ToString().Trim();
        }

        #endregion

    }
}
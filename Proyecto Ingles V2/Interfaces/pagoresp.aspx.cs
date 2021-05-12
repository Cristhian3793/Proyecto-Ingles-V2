using Logica.ConexionServicios;
using Logica.Modelos;
using Newtonsoft.Json;
using Proyecto_Ingles_V2.LoginDb;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace Proyecto_Ingles_V2.Interfaces
{
    public partial class pagoresp : System.Web.UI.Page
    {
        static conexionServidor cs = new conexionServidor();
        string url = cs.url.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    string status = Request.QueryString["status"];
                    string code = Request.QueryString["code"];
                    string id = Request.QueryString["id"];

                    //datos de prueba Shortcut
                    //status = "true";
                    //code = "000.000.000";
                    //id = "8D96B0055FDE17BB592B7A904C1C26A5.prod01-vm-tx10";

                    //se consulta el API de STUPENDO
                    //se consulta los datos y codigos para el ambiente activo de los pagos en línea 
                    DataSet ds = Conexion.BuscarNAV_ds("[DatosPagoEnLinea]", "url,token,action_url,source_url,cod_aprobacion", "where activo=1");
                    string url = ds.Tables[0].Rows[0]["url"].ToString();
                    var client = new RestClient(url);
                    var request = new RestRequest("v1/payment/results", Method.POST);
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
                    // XmlNodeList interes = ((XmlElement)customparameters[0]).GetElementsByTagName("SHOPPER_interes");
                    //tipo de crédito
                    XmlNodeList tipoCredito = ((XmlElement)customparameters[0]).GetElementsByTagName("SHOPPER_TIPOCREDITO");
                    //nodo de transaction/recurring (recurring) 
                    XmlNodeList recurring = ((XmlElement)transaction[0]).GetElementsByTagName("recurring");
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
                    string men = string.Empty;
                    //se obtiene los datos adicionales de la respuesta
                    if (adicional_1.Count > 0)
                        cedula = adicional_1[0].InnerText;

                    if (adicional_2.Count > 0)
                    {
                        //aditionaldata zerobase|ivabase|IVA
                        string[] adicionalSplit = adicional_2[0].InnerText.Split('|');
                        zerobase = adicionalSplit[0];
                        ivabase = adicionalSplit[1];
                        IVA = adicionalSplit[2];
                    }
                    string campos = string.Empty;

                    //si el valor de query string STATUS es verdadero y el valor de CODE de la consulta de API
                    if (status == "true" && codeNode[0].InnerText == ds.Tables[0].Rows[0]["cod_aprobacion"].ToString())
                    {
                        //código de autorización
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
                        decimal subtotal = Convert.ToDecimal(zerobase.Replace(",", ".")) + Convert.ToDecimal(ivabase);
                        //total
                        XmlNodeList TotalAmount = ((XmlElement)resultdetails[0]).GetElementsByTagName("TotalAmount");
                        string montoTotal = TotalAmount[0].InnerText;

                        //valor de los intereses
                        XmlNodeList Interest = ((XmlElement)resultdetails[0]).GetElementsByTagName("Interest");
                        string interes = Interest[0].InnerText;

                        //se debe averiguar que tipo de valores se pueden obtener D_sub (subtotal),	D_Iva (IVA),D_Ice(ICE),D_Int(Intereses),D_Tot(subtotal + intereses=total)
                        campos += "D_sub=" + subtotal.ToString().Replace(",", ".") + ",D_tNo= Secuencial,Estado='Autorizado',D_aut='" + AuthCode[0].InnerText + "',D_cDt='xxx',D_datos='xxx',D_Tipo='P',D_Iva=" + IVA.Replace(",", ".") + ",D_Ice=0.00,D_Int=" + interes.Replace(",", ".") + ",D_Tot=" + montoTotal.Replace(",", "") + ",lote='" + lote + "',referencia='" + referencia + "', Response_Code='" + respuestaCodigo + "'";
                        if (numberOfInstallments.Count == 0)

                            campos += ",D_Cre='00',D_Mes='1'";
                        else
                        {
                            // SHOPPER_TIPOCREDITO 
                            // 00 Transacción corriente
                            if (numberOfInstallments[0].InnerText == "0")
                                campos += ",D_Cre='00',D_Mes='1'";
                            else
                            {
                                if (tipoCredito[0].InnerText == "03")//03 Diferido sin Interés
                                {
                                    campos += ",D_Cre='01',D_Mes='" + numberOfInstallments[0].InnerText + "'";
                                }
                                else
                                {//02 Diferido con Interés 
                                    campos += ",D_Cre='02',D_Mes='" + numberOfInstallments[0].InnerText + "'";
                                }

                            }
                        }
                    }
                    else
                    {
                        //cuando el If no es verdadero es decir el STATUS es false o el CODE no es el de APROBADO o PAGADO
                        campos = "Estado = 'Negado', Response_Code='" + respuestaCodigo + "'";
                    }
                    //actualizar en la tabla del NAV
                    men = Conexion.ActualizarNAV("TransaccionesBotonPagosOnline", campos, "where id_stupendo='" + id + "'");

                    if (men == "1")
                    {
                        if (campos.Contains("Negado"))
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('Pago no se ha realizado exitosamente, inténtelo nuevamente en unos instantes. En caso de persistir el problema comuníquese con su Banco Emisor.')", true);
                        else
                        {
                            //incluir parte de la matricula
                            //servicio Actualizar estado producto
                            long idnivel = (long)Session["idNivel"];
                            string codNivel=(string)Session["codNivel"];
                            int estado;
                            if (codNivel.Trim().ToString() == "SEK1203")
                            {
                                estado = 2;
                                ActualizarNivelInscrito(idnivel,estado);
                            }
                            else if (codNivel.Trim().ToString() == "SEK1074")
                            {
                                estado = 3;
                                ActualizarNivelInscrito(idnivel, estado);
                            }
                            else if (codNivel.Trim().ToString() == "SEK1205")
                            {
                                estado = 4;
                                ActualizarNivelInscrito(idnivel, estado);
                            }
                            else {
                                estado = 1;
                                ActualizarNivelInscrito(idnivel, estado);
                            }
                           
                            //
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('Pago fue procesado correctamente.')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", @"alert('Pago no se ha realizado exitosamente, inténtelo nuevamente en unos instantes. En caso de persistir el problema comuníquese con su Banco Emisor.')", true);
                    }

                    cargarDatos(id, respuestaCodigo);
                }
                catch (Exception ex)
                {
                    Conexion.Log(ex.Message);
                }


            }


        }
        private void cargarDatos(string id, string respuestaCodigo)
        {
            try
            {
                DataSet ds = Conexion.BuscarNAV_ds("[dbo].[TransaccionesBotonPagosOnline]",
                    "D_tNo,Estado,convert(decimal(19,2),D_Tot) as importe,convert(decimal(19,2),D_Tot) as total,convert(varchar(12),fecha,103)+' '+Hora as fecha," +
                    "case D_ttar when 'DN' then 'DINERS' WHEN 'DI' THEN 'DISCOVER' WHEN 'VI' THEN 'VISA' WHEN 'MC' THEN 'MASTERCARD' WHEN 'AM' THEN 'AMEX' WHEN 'OT' THEN 'OTRA'  else '' end AS tarjeta, CorreoFactura, D_Iva",
                    "where id_stupendo='" + id + "'");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblNumeroOrden.Text = "Número de Orden: " + ds.Tables[0].Rows[0]["D_tNo"].ToString();

                    lnlNumeroAutorizacion.Text = "Número de Autorización: " + ds.Tables[0].Rows[0]["D_tNo"].ToString();
                    lblEstado.Text = "Estado: " + ds.Tables[0].Rows[0]["Estado"].ToString();
                    lblFecha.Text = "Fecha: " + ds.Tables[0].Rows[0]["fecha"].ToString();
                    lblTarjeta.Text = "Tarjeta: " + ds.Tables[0].Rows[0]["tarjeta"].ToString();
                    lblValor.Text = "Total: " + ds.Tables[0].Rows[0]["total"].ToString();

                    if (lblEstado.Text.Contains("APROBADO") || lblEstado.Text.Contains("Aprobado") || lblEstado.Text.Contains("AUTORIZADO") || lblEstado.Text.Contains("Autorizado"))
                    {
                        btnImprimir.Visible = true;

                    }
                    else
                    {
                        lblError.Text = "Detelle Error: " + devuelveCodigo(respuestaCodigo);
                    }
                }
            }
            catch (Exception ex)
            {
                Conexion.Log(ex.Message);
            }
        }
        private string devuelveCodigo(string response)
        {
            string mensaje = string.Empty;
            switch (response)
            {
                case "-1":
                    mensaje = "Error al consultar la respuesta";
                    break;
                case "02":
                    mensaje = "Existe un problema con la tarjeta, favor llamar al Centro de Autorización del banco";
                    break;
                case "03":
                    mensaje = " Establecimiento inválido";
                    break;
                case "04":
                    mensaje = "Retenga la tarjeta y llame(puede haber sido reportada como robada o está utilizando una T.C.no válida)";
                    break;
                case "05":
                    mensaje = "Transacción rechazada por el banco, favor llamar al banco y consultar";
                    break;
                case "07":
                    mensaje = "Retenga la tarjeta y llame";
                    break;
                case "12":
                    mensaje = "Transacción inválida, mensaje general emitido por el banco, la tarjeta no tiene permitido compras por internet";
                    break;
                case "13":
                    mensaje = "Monto inválido";
                    break;
                case "14":
                    mensaje = "Error en el número de tarjeta";
                    break;
                case "15":
                    mensaje = "Error en el número de tarjeta";
                    break;
                case "17":
                    mensaje = "Socio cancelado";
                    break;
                case "19":
                    mensaje = "Transacción rechazada, favor reintente";
                    break;
                case "41":
                    mensaje = "Tarjeta pérdida. Retenga la tarjeta y llame";
                    break;
                case "43":
                    mensaje = "Tarjeta robada. Retenga la tarjeta y llame";
                    break;
                case "51":
                    mensaje = "Fondos insuficientes";
                    break;
                case "54":
                    mensaje = "Tarjeta expirada";
                    break;
                case "57":
                    mensaje = "Transacción inválida o no permitida(diferidos mal enviado o transacción realizada con una tarjeta de débito con restricciones)";
                    break;
                case "61":
                    mensaje = "Monto excede el crédito disponible";
                    break;
                case "62":
                    mensaje = "Tarjeta restringida";
                    break;
                case "76":
                    mensaje = "Cuenta Inválida. En este caso es por el uso de una tarjeta de crédito no válida(Renovaciones)";
                    break;
                case "77":
                    mensaje = "Modalidad inválida. Cuando no tiene autorizado un tipo de crédito(diferido)";
                    break;
                case "79":
                    mensaje = "Vigencia errada(Fecha de caducidad)";
                    break;
                case "80":
                    mensaje = "Establecimiento cancelado";
                    break;
                case "84":
                    mensaje = "Existe un problema con la tarjeta, favor llamar al Centro de Autorización del banco";
                    break;
                case "88 ":
                    mensaje = "Transacción rechazada, favor reintente";
                    break;
                case "89":
                    mensaje = "Terminal inválida";
                    break;
                case "91 ":
                    mensaje = "Entidad fuera de línea";
                    break;
                case "FORMAT_ERROR":
                    mensaje = "Error en la identificación del cliente";
                    break;
                case "ER":
                    mensaje = "Error en los datos de la transacción, favor comuniquese con el comercio";
                    break;
            }
            return mensaje;
        }
        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }
        private void EnviaCorreo(string destinatario, string importe, string IVA, string datos_adicionales, DataSet ds)
        {

            string secuencial = ds.Tables[0].Rows[0]["Secuencial"].ToString();
            DataSet ds_producto = Conexion.BuscarNAV_ds("dbo.DatosAdicionalesPagosOnline", "*", "where secuencial='" + secuencial + "'");
            MailMessage correo = new MailMessage();
            correo.To.Add(destinatario);
            correo.CC.Add(destinatario);
            correo.CC.Add("sistemas@uisek.edu.ec");
            correo.CC.Add("pagosonline@uisek.edu.ec");
            //correo.Bcc.Add("jonathan.lemus@uisek.edu.ec");
            correo.From = new MailAddress("no.reply@uisek.edu.ec", "Pago en línea de la Universidad Internacional SEK", System.Text.Encoding.UTF8);
            correo.Subject = "Correo autogenerado de Matrícula Online";
            correo.SubjectEncoding = System.Text.Encoding.UTF8;
            correo.Body = "Correo de confirmación de Pago en línea de la Universidad Internacional SEK.\n NO RESPONDA A ESTE EMAIL.\n En caso de dudas contacte al departamento técnico.";
            correo.BodyEncoding = System.Text.Encoding.UTF8;


            string body = "<html><head> ";
            body += "<style type=\"text/css\">.style3 { width:30%;  } .style2 {color:red;}.style4 {border:0;}</style>";
            body += "</head>";
            body += "<body class=\"style4\">";
            body += "<form id=\"form1\" runat=\"server\">";
            body += "<div><table class=\"style4\" style=\"width:100%;\">";
            body += "<tr><td colspan=\"2\" style=\"text-align:center; font-size:25px\"><img src='cid:logo'></img></td>";
            body += "<tr><td colspan=\"2\" style=\"text-align:center; font-size:25px\"><b>Pago en l&iacute;nea de la Universidad Internacional SEK.</b></td>";
            body += "<tr><td colspan=\"2\"></td>";
            body += "<tr><td colspan=\"2\"><hr></td>";
            body += "<tr><td colspan=\"2\"></td>";
            body += "<tr><td colspan=\"2\" style=\"text-align:left;\"><b>DATOS PAGO EN L&Iacute;NEA</b></td>";
            body += "<tr><td>Fecha:</td><td>" + DateTime.Now.ToShortDateString() + "</td>";
            body += "<tr><td>Hora:</td><td>" + DateTime.Now.ToShortTimeString() + "</td>";
            body += "<tr><td>Valor Pagado: </td><td>" + importe.Substring(0, importe.Length - 2) + " " + importe.Substring(importe.Length - 2) + "</td>";
            body += "<tr><td>IVA: </td><td>" + IVA.Substring(0, IVA.Length - 2) + " " + IVA.Substring(IVA.Length - 2) + "</td>";
            body += "<tr><td colspan=\"2\"><hr></td>";
            if (datos_adicionales == "1")
            {
                for (int i = 0; i < ds_producto.Tables[0].Rows.Count; i++)
                {
                    string cedula = ds_producto.Tables[0].Rows[i]["identificacion"].ToString();
                    string pater = ds_producto.Tables[0].Rows[i]["ap_pater"].ToString();
                    string mater = ds_producto.Tables[0].Rows[i]["ap_mater"].ToString();
                    string nombres = ds_producto.Tables[0].Rows[i]["nombres"].ToString();
                    string mail = ds_producto.Tables[0].Rows[i]["correo"].ToString();
                    string observacion = ds_producto.Tables[0].Rows[i]["observacion"].ToString();

                    //EnviaCorreo_Unidad_especies(corr_unidad, tracking);

                    body += "<tr><td colspan=\"2\" style=\"text-align:left;\"><b>DATOS PRODUCTO " + (i + 1) + "</b></td>";
                    body += "<tr><td>Cédula:</td><td>" + cedula + "</td>";
                    body += "<tr><td>Nombre:</td><td>" + pater + " " + mater + "," + nombres + "</td>";
                    body += "<tr><td>Correo:</td><td>" + mail + "</td>";
                    //body += "<tr><td>A:</td><td>" + unidad + "</td>";
                    body += "<tr><td>Con la observación:</td><td>" + observacion + "</td>";

                }
            }


            body += "<tr><td></td><td></td>";
            body += "<td class=\"style3\"></td></tr><tr><td class=\"style3\">&nbsp;</td><td class=\"style2\">NO RESPONDA A ESTE EMAIL. </td>";
            body += "<td class=\"style3\">&nbsp;</td></tr> </table></div></form></body></html>";


            System.Net.Mime.ContentType mimeType = new System.Net.Mime.ContentType("text/html");
            // Add the alternate body to the message.
            AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
            LinkedResource logo = new LinkedResource(Server.MapPath("~/images/") + "logo.png");
            logo.ContentId = "logo";
            // Lo incrustamos en la vista HTML...
            alternate.LinkedResources.Add(logo);
            correo.AlternateViews.Add(alternate);
            correo.IsBodyHtml = false;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("no.reply@uisek.edu.ec", Funciones.ConectarMail());
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true; //Esto es para que vaya a través de SSL que es obligatorio con GMail 
            try
            {
                client.Send(correo);
            }
            catch (Exception ex)
            {
                Conexion.Log(ex.Message);
            }
        }

        private void EnviaCorreo_especies(string destinatario, string importe, string IVA, DataSet ds)
        {
            string secuencial = ds.Tables[0].Rows[0]["Secuencial"].ToString();
            //string secuencial = "0004444";

            DataSet ds_especie = Conexion.BuscarUMAS_ds("ESPE_EspecieExt", "*", "where secuencial='" + secuencial + "'");


            MailMessage correo = new MailMessage();
            correo.To.Add(destinatario);
            //correo.CC.Add(destinatario);
            correo.CC.Add("sistemas@uisek.edu.ec");
            correo.CC.Add("pagosonline@uisek.edu.ec");
            //correo.To.Add("juan.baird@uisek.edu.ec");
            correo.From = new MailAddress("no.reply@uisek.edu.ec", "Pago en línea de la Universidad Internacional SEK", System.Text.Encoding.UTF8);
            correo.Subject = "Correo autogenerado de Matrícula Online";
            correo.SubjectEncoding = System.Text.Encoding.UTF8;
            correo.Body = "Correo de confirmación de Pago en línea de la Universidad Internacional SEK.\n NO RESPONDA A ESTE EMAIL.\n En caso de dudas contacte al departamento técnico.";
            correo.BodyEncoding = System.Text.Encoding.UTF8;


            string body = "<html><head> ";
            body += "<style type=\"text/css\">.style3 { width:30%;  } .style2 {color:red;}.style4 {border:0;}</style>";
            body += "</head>";
            body += "<body class=\"style4\">";
            body += "<form id=\"form1\" runat=\"server\">";
            body += "<div><table class=\"style4\" style=\"width:100%;\">";
            body += "<tr><td colspan=\"2\" style=\"text-align:center; font-size:25px\"><img src='cid:logo'></img></td>";
            body += "<tr><td colspan=\"2\" style=\"text-align:center; font-size:25px\"><b>Pago en l&iacute;nea de la Universidad Internacional SEK.</b></td>";
            body += "<tr><td colspan=\"2\"></td>";
            body += "<tr><td colspan=\"2\"><hr></td>";
            body += "<tr><td colspan=\"2\"></td>";
            body += "<tr><td colspan=\"2\" style=\"text-align:left;\"><b>DATOS PAGO EN L&Iacute;NEA</b></td>";
            body += "<tr><td>Fecha:</td><td>" + DateTime.Now.ToShortDateString() + "</td>";
            body += "<tr><td>Hora:</td><td>" + DateTime.Now.ToShortTimeString() + "</td>";
            body += "<tr><td>Valor Pagado: </td><td>" + importe.Substring(0, importe.Length - 2) + " " + importe.Substring(importe.Length - 2) + "</td>";
            body += "<tr><td>IVA: </td><td>" + IVA.Substring(0, IVA.Length - 2) + " " + IVA.Substring(IVA.Length - 2) + "</td>";
            body += "<tr><td colspan=\"2\"><hr></td>";

            for (int i = 0; i < ds_especie.Tables[0].Rows.Count; i++)
            {

                DataSet ds_unidad = Conexion.BuscarUMAS_ds("sek_cargoautoridades", "*", "where cod_unidad='" + ds_especie.Tables[0].Rows[i]["cod_unidad"].ToString() + "'");

                string cedula = ds_especie.Tables[0].Rows[i]["identificacion"].ToString();
                string pater = ds_especie.Tables[0].Rows[i]["ap_pater"].ToString();
                string mater = ds_especie.Tables[0].Rows[i]["ap_mater"].ToString();
                string nombres = ds_especie.Tables[0].Rows[i]["nombres"].ToString();
                string mail = ds_especie.Tables[0].Rows[i]["correo"].ToString();
                //correo y datos para la unidad 
                string unidad = ds_unidad.Tables[0].Rows[0]["unidad"].ToString();
                string corr_unidad = ds_unidad.Tables[0].Rows[0]["correo_PagosVarios"].ToString();
                string tracking = ds_especie.Tables[0].Rows[i]["id_tracking"].ToString();
                string observacion = ds_especie.Tables[0].Rows[i]["observacion"].ToString();

                EnviaCorreo_Unidad_especies(corr_unidad, tracking, cedula, pater, mater, nombres, unidad, observacion, mail);

                body += "<tr><td colspan=\"2\" style=\"text-align:left;\"><b>DATOS ESPECIE VALORADA " + (i + 1) + "</b></td>";
                body += "<tr><td>Cédula:</td><td>" + cedula + "</td>";
                body += "<tr><td>Nombre:</td><td>" + pater + " " + mater + "," + nombres + "</td>";
                body += "<tr><td>Correo:</td><td>" + mail + "</td>";
                body += "<tr><td>A:</td><td>" + unidad + "</td>";
                body += "<tr><td>Con la observación:</td><td>" + observacion + "</td>";

            }
            body += "<tr><td></td><td></td>";
            body += "<td class=\"style3\"></td></tr><tr><td class=\"style3\">&nbsp;</td><td class=\"style2\">NO RESPONDA A ESTE EMAIL. </td>";
            body += "<td class=\"style3\">&nbsp;</td></tr> </table></div></form></body></html>";


            System.Net.Mime.ContentType mimeType = new System.Net.Mime.ContentType("text/html");
            // Add the alternate body to the message.
            AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
            LinkedResource logo = new LinkedResource(Server.MapPath("~/images/") + "logo.png");
            logo.ContentId = "logo";
            // Lo incrustamos en la vista HTML...
            alternate.LinkedResources.Add(logo);
            correo.AlternateViews.Add(alternate);
            correo.IsBodyHtml = false;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("no.reply@uisek.edu.ec", Funciones.ConectarMail());
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true; //Esto es para que vaya a través de SSL que es obligatorio con GMail 
            try
            {
                client.Send(correo);
            }
            catch (Exception ex)
            {
                Conexion.Log(ex.Message);
            }
        }


        private void EnviaCorreo_Unidad_especies(string destinatario, string id_tracking, string cedula, string pater, string mater, string nombres, string unidad, string observacion, string mail)
        {

            //DataSet ds_especie = Conexion.BuscarUMAS_ds("ESPE_EspecieExt", "*", "where id_tracking='" + id_tracking + "'");


            MailMessage correo = new MailMessage();
            correo.To.Add(destinatario);
            //correo.CC.Add(destinatario);
            correo.CC.Add("sistemas@uisek.edu.ec");
            correo.CC.Add("pagosonline@uisek.edu.ec");
            //correo.To.Add("juan.baird@uisek.edu.ec");
            correo.From = new MailAddress("no.reply@uisek.edu.ec", "Pago en línea de la Universidad Internacional SEK", System.Text.Encoding.UTF8);
            correo.Subject = "Correo autogenerado de Matrícula Online";
            correo.SubjectEncoding = System.Text.Encoding.UTF8;
            correo.Body = "Correo de confirmación de Pago en línea de la Universidad Internacional SEK.\n NO RESPONDA A ESTE EMAIL.\n En caso de dudas contacte al departamento técnico.";
            correo.BodyEncoding = System.Text.Encoding.UTF8;


            string body = "<html><head> ";
            body += "<style type=\"text/css\">.style3 { width:30%;  } .style2 {color:red;}.style4 {border:0;}</style>";
            body += "</head>";
            body += "<body class=\"style4\">";
            body += "<form id=\"form1\" runat=\"server\">";
            body += "<div><table class=\"style4\" style=\"width:100%;\">";
            body += "<tr><td colspan=\"2\" style=\"text-align:center; font-size:25px\"><img src='cid:logo'></img></td>";
            body += "<tr><td colspan=\"2\" style=\"text-align:center; font-size:25px\"><b>Pago en l&iacute;nea de la Universidad Internacional SEK.</b></td>";
            body += "<tr><td colspan=\"2\"></td>";
            body += "<tr><td colspan=\"2\"><hr></td>";
            body += "<tr><td colspan=\"2\"></td>";
            body += "<tr><td colspan=\"2\" style=\"text-align:left;\"><b>SOLICITUD DE ESPECIE RECIBIDA</b></td>";
            body += "<tr><td>Cédula:</td><td>" + cedula + "</td>";
            body += "<tr><td>Nombre:</td><td>" + pater + " " + mater + "," + nombres + "</td>";
            body += "<tr><td>Correo:</td><td>" + mail + "</td>";
            body += "<tr><td>A:</td><td>" + unidad + "</td>";
            body += "<tr><td>Con la observación:</td><td>" + observacion + "</td>";


            body += "<tr><td></td><td></td>";
            body += "<td class=\"style3\"></td></tr><tr><td class=\"style3\">&nbsp;</td><td class=\"style2\">NO RESPONDA A ESTE EMAIL. </td>";
            body += "<td class=\"style3\">&nbsp;</td></tr> </table></div></form></body></html>";


            System.Net.Mime.ContentType mimeType = new System.Net.Mime.ContentType("text/html");
            // Add the alternate body to the message.
            AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
            LinkedResource logo = new LinkedResource(Server.MapPath("~/images/") + "logo.png");
            logo.ContentId = "logo";
            // Lo incrustamos en la vista HTML...
            alternate.LinkedResources.Add(logo);
            correo.AlternateViews.Add(alternate);
            correo.IsBodyHtml = false;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("no.reply@uisek.edu.ec", Funciones.ConectarMail());
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true; //Esto es para que vaya a través de SSL que es obligatorio con GMail 
            try
            {
                client.Send(correo);
            }
            catch (Exception ex)
            {
                Conexion.Log(ex.Message);
            }
        }

        private void EnviaCorreo_Unidad_productos(string destinatario, DataSet ds)
        {

            string secuencial = ds.Tables[0].Rows[0]["Secuencial"].ToString();
            DataSet ds_producto = Conexion.BuscarNAV_ds("dbo.DatosAdicionalesPagosOnline", "*", "where secuencial='" + secuencial + "'");


            MailMessage correo = new MailMessage();
            correo.To.Add(destinatario);
            //correo.CC.Add(destinatario);
            correo.CC.Add("sistemas@uisek.edu.ec");
            correo.CC.Add("pagosonline@uisek.edu.ec");
            //correo.To.Add("juan.baird@uisek.edu.ec");
            correo.From = new MailAddress("no.reply@uisek.edu.ec", "Pago en línea de la Universidad Internacional SEK", System.Text.Encoding.UTF8);
            correo.Subject = "Correo autogenerado de Matrícula Online";
            correo.SubjectEncoding = System.Text.Encoding.UTF8;
            correo.Body = "Correo de confirmación de Pago en línea de la Universidad Internacional SEK.\n NO RESPONDA A ESTE EMAIL.\n En caso de dudas contacte al departamento técnico.";
            correo.BodyEncoding = System.Text.Encoding.UTF8;


            string body = "<html><head> ";
            body += "<style type=\"text/css\">.style3 { width:30%;  } .style2 {color:red;}.style4 {border:0;}</style>";
            body += "</head>";
            body += "<body class=\"style4\">";
            body += "<form id=\"form1\" runat=\"server\">";
            body += "<div><table class=\"style4\" style=\"width:100%;\">";
            body += "<tr><td colspan=\"2\" style=\"text-align:center; font-size:25px\"><img src='cid:logo'></img></td>";
            body += "<tr><td colspan=\"2\" style=\"text-align:center; font-size:25px\"><b>Pago en l&iacute;nea de la Universidad Internacional SEK.</b></td>";
            body += "<tr><td colspan=\"2\"></td>";
            body += "<tr><td colspan=\"2\"><hr></td>";
            body += "<tr><td colspan=\"2\"></td>";
            body += "<tr><td colspan=\"2\" style=\"text-align:left;\"><b>SOLICITUD DE PRODUCTO</b></td>";


            string cedula = ds_producto.Tables[0].Rows[0]["identificacion"].ToString();
            string pater = ds_producto.Tables[0].Rows[0]["ap_pater"].ToString();
            string mater = ds_producto.Tables[0].Rows[0]["ap_mater"].ToString();
            string nombres = ds_producto.Tables[0].Rows[0]["nombres"].ToString();
            string mail = ds_producto.Tables[0].Rows[0]["correo"].ToString();

            string observacion = ds_producto.Tables[0].Rows[0]["observacion"].ToString();

            body += "<tr><td>Cédula:</td><td>" + cedula + "</td>";
            body += "<tr><td>Nombre:</td><td>" + pater + " " + mater + "," + nombres + "</td>";
            body += "<tr><td>Correo:</td><td>" + mail + "</td>";
            body += "<tr><td>Con la observación:</td><td>" + observacion + "</td>";


            body += "<tr><td></td><td></td>";
            body += "<td class=\"style3\"></td></tr><tr><td class=\"style3\">&nbsp;</td><td class=\"style2\">NO RESPONDA A ESTE EMAIL. </td>";
            body += "<td class=\"style3\">&nbsp;</td></tr> </table></div></form></body></html>";


            System.Net.Mime.ContentType mimeType = new System.Net.Mime.ContentType("text/html");
            // Add the alternate body to the message.
            AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
            LinkedResource logo = new LinkedResource(Server.MapPath("~/images/") + "logo.png");
            logo.ContentId = "logo";
            // Lo incrustamos en la vista HTML...
            alternate.LinkedResources.Add(logo);
            correo.AlternateViews.Add(alternate);
            correo.IsBodyHtml = false;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("no.reply@uisek.edu.ec", Funciones.ConectarMail());
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true; //Esto es para que vaya a través de SSL que es obligatorio con GMail 
            try
            {
                client.Send(correo);
            }
            catch (Exception ex)
            {
                Conexion.Log(ex.Message);
            }
        }


        //mis funciones
        public async void ActualizarNivelInscrito(long idnivelIns,int estado)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ClNivelesInscrito ins = new ClNivelesInscrito();
                ins.IDESTADONIVEL = estado;
                string uri = "api/NivelesInscrito/" + idnivelIns;
                var myContent = JsonConvert.SerializeObject(ins);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                var method = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(method, uri)
                {
                    Content = stringContent
                };
                response = await client.SendAsync(request);

            }
            catch (TaskCanceledException e)
            {

            }

        }


    }
}
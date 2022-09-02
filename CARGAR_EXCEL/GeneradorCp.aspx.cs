using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Routing;
using CARGAR_EXCEL.Controllers;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using System.Net;
using System.IO;

namespace CARGAR_EXCEL
{
    public partial class GeneradorCp : System.Web.UI.Page
    {
        public FacCpController facLabControler = new FacCpController();
        public List<string> f02 = new List<string>();
        public string fDesde, fHasta, concepto, tipoCobro, tipocomprobante, lugarexpedicion, metodopago33, formadepago, usocfdi, confirmacion, paisresidencia, numtributacion
        , mailenvio, numidentificacion, claveunidad, tipofactoriva, tipofactorret, coditrans, tipofactor, tasatras, codirete, tasarete, relacion, montosoloiva, montoivarete
        , ivadeiva, ivaderet, retderet, conceptoretencion, consecutivoconcepto, claveproductoservicio, valorunitario, importe, descuento, cantidadletra, uuidrel
        , identificador, version, fechapago, monedacpag, tipodecambiocpag, monto, numerooperacion, rfcemisorcuenta, nombrebanco, numerocuentaord, rfcemisorcuentaben, numcuentaben
        , tipocadenapago, certpago, cadenadelpago, sellodelpago, identpag, identdocpago, seriecpag, foliocpag, monedacpagdoc, tipocambiocpag, metododepago, numerodeparcialidad, f03, f04, IdentificadorDelDocumentoPagado, identificaciondpago, serieinvoice, folioscpag, monedascpadgoc, nparcialidades, interiorsaldoanterior, ipagadoisaldoinsoluto, ipagado, isaldoinsoluto, k1, k3, f05, iva, retencion
        , importeSaldoAnterior, importepago, importesaldoinsoluto, total, subt, ivat, rett, cond, tipoc, seriee, folioe, sfolio, idcomprobante, fecha, tmoneda, Tdoc, IdCliente, RFC, Cliente, Pais, Calle, NoExt, NoInt, Colonia, Localidad, Referencia, Municipio, Estado, CP, FechaPago, cantidad, descripcion, RFCbancoEmisor, BancoEmisor, CuentaPago, Total, identificadorDelPago, formadepagocpag, mmonto, if05, if06, iipagado, totaliva, totalisr, foliop, receptorp, MetdodoPagop, uidp, usdf04, TotaldeIva, TotaldeRe, f07, f08, Totalipagado, basecalculado;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["usr_userid"] == null)
            //{
            //    Response.Redirect("Login.aspx");
            //}
            ////lblUserDetails.Text = "Usuario: " + Session["usr_userid"];

            //Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;


            try
            {
                iniciaDatos();
            }
            catch (Exception EX)
            {
                string error = EX.Message;
            }



        }

        public void iniciaDatos()
        {
            //string folio = "40524";
            //string folio = "40658";
            //string folio = "40654";
            string folio = "40693";
            //string folio = "40646";
            //string folio = "40645";
            //string folio = "40643";
            //string folio = "40525";
            //string folio = "40627";
            //string folio = "40557";
            DataTable td = facLabControler.detalleFacturas(folio);
            string datestring = DateTime.Now.ToString("yyyyMMddHHmmss");

            foreach (DataRow row in td.Rows)
            {
                //Asignar valores 01
                idcomprobante = row["IdComprobante"].ToString();
                sfolio = row["SFolio"].ToString();
                seriee = row["Serie"].ToString();
                folioe = row["Folio"].ToString();
                DateTime dt = DateTime.Parse(row["FechaHoraEmision"].ToString());
                fecha = dt.ToString("yyyy'/'MM'/'dd HH:mm:ss");
                subt = row["Subtotal"].ToString();
                ivat = row["TotalImpuestosTrasladados"].ToString();
                rett = row["TotalImpuestosRetenidos"].ToString();
                total = row["Total"].ToString();
                cantidadletra = row["Totalconletra"].ToString();
                formadepago = row["FormaDePago"].ToString();
                cond = row["CondicionesdePago"].ToString();
                metodopago33 = row["MedotoDePago"].ToString();
                tmoneda = row["Moneda"].ToString();
                tipoc = row["Tipodecambio"].ToString();
                tipocomprobante = row["TipodeComprobante"].ToString();
                lugarexpedicion = row["LugardeExpedición"].ToString();

                usocfdi = row["UsoCFDI"].ToString();
                if (usocfdi == "P01")
                {
                    usocfdi = "CP01";
                }
                Tdoc = "FAC";
                //confirmacion = row["Confirmación"].ToString();
                //string f01 = "|01|" + idcomprobante + "|";

                //02-------------------------------------------------------------------------------------------------------------------------

                IdCliente = row["IdReceptor"].ToString();

                RFC = row["RFC"].ToString();
                Cliente = row["Nombre"].ToString();
                Pais = row["Pais"].ToString();
                Calle = row["Calle"].ToString();
                NoExt = row["NumeroExterior"].ToString();
                NoInt = row["NumeroInterior"].ToString();
                Colonia = row["Colonia"].ToString();
                Localidad = row["Localidad"].ToString();
                Referencia = row["Referencia"].ToString();
                Municipio = row["MunicipioDelegacion"].ToString();
                Estado = row["Estado"].ToString();
                CP = row["CódigoPostal"].ToString();
                FechaPago = row["Fechapago"].ToString();
                paisresidencia = row["PaísResidenciaFiscal"].ToString();
                numtributacion = row["NúmeroDeRegistroIdTributacion"].ToString();
                mailenvio = row["CorreoEnvio"].ToString();

                //04-------------------------------------------------------------------------------------------------------------------------
                consecutivoconcepto = row["ConsecutivoConcepto"].ToString();
                claveproductoservicio = row["ClaveProductooServicio"].ToString();
                cantidad = row["Cantidad"].ToString();
                claveunidad = row["ClaveUnidad"].ToString();
                descripcion = row["Descripcion"].ToString();
                valorunitario = row["ValorUnitario"].ToString();
                importe = row["Importe"].ToString();
                descuento = row["Descuento"].ToString();

                //CPAG-------------------------------------------------------------------------------------------------------------------------


                DateTime dtdtt = DateTime.Parse(row["Fechapago"].ToString());
                fechapago = dtdtt.ToString("yyyy'-'MM'-'dd'T'HH:mm:ss");
                //fechapago =
                identificador = row["Identificador"].ToString();
                version = row["version"].ToString();
                //txtFormaPago.Text = row["Formadepagocpag"].ToString();
                monedacpag = row["Monedacpag"].ToString();
                tipodecambiocpag = row["TipoDeCambiocpag"].ToString();
                monto = row["Monto"].ToString();
                numerooperacion = row["NumeroOperacion"].ToString();
                RFCbancoEmisor = row["RFCEmisorCuentaBeneficiario"].ToString();
                BancoEmisor = row["NombreDelBanco"].ToString();
                CuentaPago = row["NumeroCuentaOrdenante"].ToString();
                rfcemisorcuentaben = row["RFCEmisorCuentaBeneficario"].ToString();
                numcuentaben = row["NumerCuentaBeneficiario"].ToString();
                tipocadenapago = row["TipoCadenaPago"].ToString();
                certpago = row["CertificadoPago"].ToString();
                cadenadelpago = row["CadenaDePago"].ToString();
                sellodelpago = row["SelloDePago"].ToString();
                identificadorDelPago = row["IdentificadorDelPago"].ToString();
                formadepagocpag = row["Formadepagocpag"].ToString();
                monedacpag = row["Monedacpag"].ToString();



                //CPAGDOC-----------------------------------------------------------------------------------------------------------------------
                DataTable detalleIdent = facLabControler.getDatosCPAGDOC(identificadorDelPago);
                string uid = "";
                decimal importePagos = 0;
                decimal importePagos2 = 0;
                decimal importePagos3 = 0;
                decimal importePagos4 = 0;
                double ivaa = 0.16;
                double isrr = 0.04;
                decimal totalIva = 0;
                decimal totalIsr = 0;
                decimal basecalculo = 0;
                int contadorPUE = 0;
                int contadorPPD = 0;
                string MetdodoPago = row["MedotoDePago"].ToString();
                //string uidp = "";


                foreach (DataRow rowIdent in detalleIdent.Rows)
                {
                    ////aqui va el codigo en produccion -------------------------------------

                    //    //Primer liena copiada la variable original es folio la cambie pod foliop
                    folio = Regex.Replace(folio.ToString().Replace("TDR", "").Trim(), @"[A-Z]", "");
                    ////segunda linea copiada - receptor lo cambie por receptorp
                    string receptorp = IdCliente.ToString().Trim();
                    ////tercer codigo copiada
                    string serieinvoice = "";
                    if (receptorp.Equals("LIVERPOL") || receptorp.Equals("LIVERDED") || receptorp.Equals("ALMLIVER") || receptorp.Equals("LIVERTIJ") || receptorp.Equals("SFERALIV") || receptorp.Equals("GLOBALIV") || receptorp.Equals("SETRALIV") || receptorp.Equals("FACTUMLV"))
                    {
                        serieinvoice = "TDRL";
                    }
                    else
                    {
                        serieinvoice = rowIdent["Seriecpag"].ToString();
                    }

                    //// cuarta parte del codigo copiado

                    folio = Regex.Replace(folio.ToString().Replace("TDR", "").Trim(), @"[A-Z]", "");
                    if (folio.Length == 7 && folio.StartsWith("99"))
                    {
                        folio = folio.Substring(folio.Length - 6, 6);
                    }
                    else if (folio.Length == 8)
                    {
                        folio = folio.Substring(folio.Length - 7, 7);
                    }
                    folio = folio.Replace("-", "");

                    //// siguiente codigo copiado, la variable MetdodoPago lo cambie por MetdodoPagop



                    DataTable datosMaster = facLabControler.getDatosMaster(folio);
                    if (datosMaster.Rows.Count > 0)
                    {
                        foreach (DataRow rowMaster in datosMaster.Rows)
                        {
                            string invoiceMaster = Regex.Replace(rowMaster[0].ToString(), @"[A-Z]", "");
                            folio = invoiceMaster;
                            var request = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico=" + invoiceMaster + "&serie=" + serieinvoice);
                            var response = (HttpWebResponse)request.GetResponse();
                            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                            MetdodoPago = "PPD";
                            string[] separadas = responseString.Split(',');
                            foreach (string dato in separadas)
                            {
                                if (dato.Contains("uuid"))
                                {
                                    uid = dato.Replace(dato.Substring(0, 8), "").Replace("\"", "").Replace(":", "");
                                }
                                if (serieinvoice != "TDRL")
                                {
                                    if (dato.Contains("xmlDownload"))
                                    {

                                        string xml = dato.Replace(dato.Substring(0, 15), "").Replace("\"", "");
                                        XmlDocument xDoc = new XmlDocument();
                                        xDoc.Load("https://canal1.xsa.com.mx:9050" + xml);
                                        var xmlTexto = xDoc.InnerXml.ToString();
                                        if (xmlTexto.Contains("MetodoPago=\"PPD\""))
                                        {
                                            MetdodoPago = "PPD";
                                            contadorPPD++;
                                            //PopupMsg.Message1 = "La factura es PPD!!";
                                            //PopupMsg.ShowPopUp(0);
                                        }
                                        else if (xmlTexto.Contains("MetodoPago=\"PUE\""))
                                        {
                                            MetdodoPago = "PUE";
                                            contadorPUE++;
                                            //PopupMsg.Message1 = "La factura es PUE!!";
                                            //PopupMsg.ShowPopUp(0);
                                        }
                                    }

                                }
                            }

                        }

                    }
                    else
                    {

                        var request = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico=" + folio + "&serie=" + serieinvoice);
                        var response = (HttpWebResponse)request.GetResponse();
                        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                        MetdodoPago = "PPD";
                        string[] separadas = responseString.Split(',');


                        foreach (string dato in separadas)
                        {
                            if (dato.Contains("uuid"))
                            {
                                uid = dato.Replace(dato.Substring(0, 8), "").Replace("\"", "").Replace(":", "");
                            }
                            if (serieinvoice != "TDRL")
                            {
                                if (dato.Contains("xmlDownload"))
                                {

                                    string xml = dato.Replace(dato.Substring(0, 15), "").Replace("\"", "");
                                    XmlDocument xDoc = new XmlDocument();
                                    xDoc.Load("https://canal1.xsa.com.mx:9050" + xml);
                                    var xmlTexto = xDoc.InnerXml.ToString();
                                    if (xmlTexto.Contains("MetodoPago=\"PPD\""))
                                    {
                                        MetdodoPago = "PPD";
                                        contadorPPD++;
                                        //PopupMsg.Message1 = "La factura es PPD!!";
                                        //PopupMsg.ShowPopUp(0);
                                    }
                                    else if (xmlTexto.Contains("MetodoPago=\"PUE\""))
                                    {
                                        MetdodoPago = "PUE";
                                        contadorPUE++;
                                        //PopupMsg.Message1 = "La factura es PUE!!";
                                        //PopupMsg.ShowPopUp(0);
                                    }
                                }
                            }
                        }
                        if (uid == "" && serieinvoice == "TDRA")
                        {
                            request = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico=" + folio + "&serie=" + "SAEM");
                            response = (HttpWebResponse)request.GetResponse();
                            responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                            separadas = responseString.Split(',');


                            foreach (string dato in separadas)
                            {
                                if (dato.Contains("uuid"))
                                {
                                    uid = dato.Replace(dato.Substring(0, 8), "").Replace("\"", "").Replace(":", "");
                                }
                                if (dato.Contains("xmlDownload"))
                                {

                                    string xml = dato.Replace(dato.Substring(0, 15), "").Replace("\"", "");
                                    XmlDocument xDoc = new XmlDocument();
                                    xDoc.Load("https://canal1.xsa.com.mx:9050" + xml);
                                    var xmlTexto = xDoc.InnerXml.ToString();
                                    if (xmlTexto.Contains("MetodoPago=\"PPD\""))
                                    {
                                        MetdodoPago = "PPD";
                                        contadorPPD++;
                                        //string mensaje = "La factura es PPD!!";
                                        //MessageBox.Show(mensaje);
                                        //PopupMsg.Message1 = "La factura es PPD!!";
                                        //PopupMsg.ShowPopUp(0);
                                    }
                                    else if (xmlTexto.Contains("MetodoPago=\"PUE\""))
                                    {
                                        MetdodoPago = "PUE";
                                        contadorPUE++;
                                        //PopupMsg.Message1 = "La factura es PUE!!";
                                        //PopupMsg.ShowPopUp(0);
                                    }

                                }
                            }
                        }

                    }


                    //aqui termina -----------------------------------------------------------------------------------------------------

                    if (MetdodoPago == "PPD")
                    {
                        importepago = rowIdent["ImportePagado"].ToString();
                        try
                        {
                            importePagos = importePagos + Convert.ToDecimal(importepago);
                            Total = importePagos.ToString();
                        }
                        catch (Exception ex)
                        {
                            string errors = ex.Message;
                        }
                        identificaciondpago = rowIdent["IdentificadorDelPago"].ToString();
                        IdentificadorDelDocumentoPagado = rowIdent["IdentificadorDelDocumentoPagado"].ToString();
                        serieinvoice = rowIdent["Seriecpag"].ToString();
                        folioscpag = rowIdent["Foliocpag"].ToString();
                        monedascpadgoc = rowIdent["Monedacpagdoc"].ToString();
                        nparcialidades = rowIdent["NumeroDeParcialidad"].ToString();
                        interiorsaldoanterior = rowIdent["ImporteSaldoAnterior"].ToString();
                        ipagado = rowIdent["ImportePagado"].ToString();
                        isaldoinsoluto = rowIdent["ImporteSaldoInsoluto"].ToString();

                        try
                        {
                            importePagos4 = importePagos4 + Convert.ToDecimal(ipagado);
                            Totalipagado = importePagos4.ToString();
                        }
                        catch (Exception ex)
                        {
                            string errors = ex.Message;
                        }

                        //aqui comienza el IF
                        if (monedascpadgoc.Trim() == "USD")
                        {
                            usdf04 = "04"                                                   //1-Tipo De Registro
                           + "|" + consecutivoconcepto.Trim()                       //2-Id Receptor
                           + "|" + claveproductoservicio.Trim()                                //3-RFC
                           + "|"                          //4-Nombre
                           + "|" + cantidad.Trim()                           //5-Pais
                           + "|" + claveunidad.Trim()                            //6-Calle
                           + "|"                             //7-Numero Exterior
                           + "|" + descripcion.Trim()                            //8-Numero Interior
                           + "|" + valorunitario.Trim()                         //9-Colonia
                           + "|" + importe.Trim()                        //10-Localidad
                           + "|"                        //11-Referencia
                           + "|"                         //12-Municio/Delegacion
                           + "|" + "01"                          //13-EStado
                           + "| \r\n";
                            f04 += "CPAG20DOC"
                              + "|" + identificaciondpago
                              + "|" + uid
                              + "|" + serieinvoice
                              + "|" + folioscpag
                              + "|" + monedascpadgoc
                              + "|"
                              + "|" + MetdodoPago
                              + "|" + nparcialidades
                              + "|" + interiorsaldoanterior
                              + "|" + ipagado
                              + "|" + "0"
                              + "| \r\n";
                            f07 = "";
                            f08 = "";


                            //    cpagdoc = cpagdoc + ("CPAGDOC"                           //1-Tipo De Registro
                            //      + "|" + identpag                                       //2-IdentificadorDelPago
                            //                                                             //+ "|" + rowIdent["IdentificadorDelDocumentoPagado"].ToString()                            //3-IdentificadorDelDocumentoPagado                                              
                            //      + "|" + uid                                            //3-IdentificadorDelDocumentoPagado                                              
                            //      + "|" + serieinvoice                                   //4-Seriecpag
                            //      + "|" + foliocpag                                      //5-Foliocpag
                            //      + "|" + monedacpagdoc                                  //6-Monedacpag
                            //      + "|" + ""                                             //7-TipoCambiocpagdpc
                            //      + "|" + txtMetodoPago.Text                             //8-MetodoDePago
                            //      + "|" + numerodeparcialidad                            //9-NumeroDeParcialidad
                            //      + "|" + importepago                                    //10-ImporteSaldoAnterior
                            //      + "|" + importepago                                    //11-ImportePagado                                                  
                            //      + "|" + "0"                                            //12 ImporteSaldoInsoluto
                            //      + "| \r\n");
                        }


                        //Aqui termina el IF

                        // Esto va en le ELSE
                        else
                        {


                            DataTable detalleIdentt = facLabControler.getDatosCPAGDOCTRL(identificaciondpago, folioscpag);

                            if (detalleIdentt.Rows.Count > 0)
                            {
                                foreach (DataRow rowIdentt in detalleIdentt.Rows)
                                {
                                    k1 = rowIdentt["K1"].ToString();
                                    k3 = rowIdentt["K3"].ToString();
                                    iva = rowIdentt["IVA"].ToString();
                                    retencion = rowIdentt["RETENCION"].ToString();
                                    iipagado = rowIdentt["ActualApplyToAmount"].ToString();
                                    basecalculo = Convert.ToDecimal(iipagado);
                                    basecalculado = basecalculo.ToString("F");
                                    totalIva = (decimal)(ivaa * Convert.ToDouble(basecalculado));
                                    totaliva = totalIva.ToString("F");
                                    totalIsr = (decimal)(isrr * Convert.ToDouble(basecalculado));
                                    totalisr = totalIsr.ToString("F");

                                    if (iva != "")
                                    {
                                        if05 = "CPAG20DOCIMPRET"
                                        + "|" + k1.Trim()
                                        + "|" + k3.Trim()
                                        + "|" + IdentificadorDelDocumentoPagado.Trim()
                                        + "|" + "001"
                                        + "|" + "Tasa"
                                        + "|" + "0.040000"
                                        + "|" + totalisr
                                        //+ "|" + retencion
                                        + "|" + basecalculado.Trim()
                                        + "| \r\n";
                                    }
                                    if (retencion != "")
                                    {
                                        if06 = "CPAG20DOCIMPTRA"
                                        + "|" + k1.Trim()
                                        + "|" + k3.Trim()
                                        + "|" + IdentificadorDelDocumentoPagado.Trim()
                                        + "|" + "002"
                                        + "|" + "Tasa"
                                        + "|" + "0.160000"
                                        + "|" + totaliva
                                        //+ "|" + iva.Trim()
                                        + "|" + basecalculado.Trim()
                                        + "| \r\n";
                                    }
                                    try
                                    {
                                        importePagos3 = importePagos3 + Convert.ToDecimal(totalisr);
                                        TotaldeRe = importePagos3.ToString();

                                    }
                                    catch (Exception ex)
                                    {
                                        string errors = ex.Message;
                                    }

                                    f07 = "CPAG20IMPRET"
                                    + "|" + identificaciondpago
                                    + "|" + "001"
                                    + "|" + TotaldeRe
                                    + "| \r\n";

                                    try
                                    {
                                        importePagos2 = importePagos2 + Convert.ToDecimal(totaliva);
                                        TotaldeIva = importePagos2.ToString();
                                    }
                                    catch (Exception ex)
                                    {
                                        string errors = ex.Message;
                                    }

                                    f08 = "CPAG20IMPTRA"
                                    + "|" + identificaciondpago
                                    + "|" + "002"
                                    + "|" + "Tasa"
                                    + "|" + "0.160000"
                                    + "|" + TotaldeIva
                                    + "|" + Totalipagado
                                    + "| \r\n";




                                    //if05 = "CPAG20DOCIMPRET"
                                    //    + "|" + k1.Trim()
                                    //    + "|" + k3.Trim()
                                    //    + "|" + iva.Trim()
                                    //    + "| \r\n";

                                    //if06 = "CPAG20DOCIMPTRA"
                                    //    + "|" + k1.Trim()
                                    //    + "|" + k3.Trim()
                                    //    + "|" + retencion
                                    //    + "| \r\n";

                                }
                            }
                            usdf04 = "04"                                                   //1-Tipo De Registro
                           + "|" + consecutivoconcepto.Trim()                       //2-Id Receptor
                           + "|" + claveproductoservicio.Trim()                                //3-RFC
                           + "|"                          //4-Nombre
                           + "|" + cantidad.Trim()                           //5-Pais
                           + "|" + claveunidad.Trim()                            //6-Calle
                           + "|"                             //7-Numero Exterior
                           + "|" + descripcion.Trim()                            //8-Numero Interior
                           + "|" + valorunitario.Trim()                         //9-Colonia
                           + "|" + importe.Trim()                        //10-Localidad
                           + "|"                        //11-Referencia
                           + "|"                         //12-Municio/Delegacion
                           + "|" + "02"                          //13-EStado
                           + "| \r\n";
                            f04 += "CPAG20DOC"
                                 + "|" + identificaciondpago
                                 + "|" + IdentificadorDelDocumentoPagado
                                 + "|"
                                 + "|"
                                 + "|" + monedascpadgoc
                                 + "|"
                                 + "|" + nparcialidades
                                 + "|" + interiorsaldoanterior
                                 + "|" + ipagado
                                 + "|" + isaldoinsoluto
                                 + "|" + "02"
                                 + "| \r\n"
                                 + if05
                                 + if06;

                        }
                        // Hasta aqui va en el ELSE

                        //foreach (DataRow rowIdentt in detalleIdentt.Rows)
                        //{
                        //    k1 = rowIdentt["K1"].ToString();
                        //    k3 = rowIdentt["K3"].ToString();
                        //    iva = rowIdentt["IVA"].ToString();
                        //    retencion = rowIdentt["RETENCION"].ToString();

                        //    if05 = "CPAG20DOCIMPRET"
                        //        + "|" + k1.Trim()
                        //        + "|" + k3.Trim()
                        //        + "|" + iva.Trim()
                        //        + "| \r\n";

                        //    if06 = "CPAG20DOCIMPTRA"
                        //        + "|" + k1.Trim()
                        //        + "|" + k3.Trim()
                        //        + "|" + retencion
                        //        + "| \r\n";

                        //}

                        //f04 += "CPAG20DOC"
                        //+ "|" + identificaciondpago
                        //+ "|" + IdentificadorDelDocumentoPagado
                        //+ "|"
                        //+ "|"
                        //+ "|" + monedascpadgoc
                        //+ "|"
                        //+ "|" + nparcialidades
                        //+ "|" + interiorsaldoanterior
                        //+ "|" + ipagado
                        //+ "|" + isaldoinsoluto
                        //+ "|" + "02"
                        //+ "| \r\n";






                        //"CPAGDOC"                                              //1-Tipo De Registro
                        //          + "|" + identpag                                       //2-IdentificadorDelPago
                        //                                                                 //+ "|" + rowIdent["IdentificadorDelDocumentoPagado"].ToString()                            //3-IdentificadorDelDocumentoPagado                                              
                        //          + "|" + uid                            //3-IdentificadorDelDocumentoPagado                                              
                        //          + "|" + serieinvoice                                      //4-Seriecpag
                        //          + "|" + foliocpag                                      //5-Foliocpag
                        //          + "|" + monedacpagdoc                                  //6-Monedacpag
                        //          + "|" + tipocambiocpag                                 //7-TipoCambiocpagdpc
                        //          + "|" + txtMetodoPago.Text                             //8-MetodoDePago
                        //          + "|" + numerodeparcialidad                            //9-NumeroDeParcialidad
                        //          + "|" + importeSaldoAnterior                           //10-ImporteSaldoAnterior
                        //          + "|" + importepago                                    //11-ImportePagado                                                  
                        //          + "|" + importesaldoinsoluto                           //12 ImporteSaldoInsoluto
                        //          + "| \r\n");
                    }
                }
                if (contadorPPD == 0 && contadorPUE > 0)
                {
                    string msg = "La factura es PUE!! y es libre de todo PPD";
                    ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'La factura es PUE!! y es libre de todo PPD', 'success');", true);
                    //PopupMsg.Message1 = "La factura es PUE!! y es libre de todo PPD";
                    //PopupMsg.ShowPopUp(0);
                }

                DataTable detalleIdent2 = facLabControler.detalleFacturas(folio);

                foreach (DataRow rowIdent2 in detalleIdent2.Rows)
                {
                    identificadorDelPago = rowIdent2["IdentificadorDelPago"].ToString();
                    DateTime dtdtt2 = DateTime.Parse(rowIdent2["Fechapago"].ToString());
                    fechapago = dtdtt2.ToString("yyyy'-'MM'-'dd'T'HH:mm:ss");
                    formadepagocpag = rowIdent2["Formadepagocpag"].ToString();
                    monedacpag = rowIdent2["Monedacpag"].ToString();
                    mmonto = rowIdent2["Monto"].ToString();

                    f03 += "CPAG20PAGO"
                        + "|" + identificadorDelPago
                        + "|" + fechapago
                        + "|" + formadepagocpag
                        + "|" + monedacpag
                        + "|"
                        + "|" + mmonto
                        + "|"
                        + "|"
                        + "|"
                        + "|"
                        + "|"
                        + "|"
                        + "|"
                        + "|"
                        + "|"
                        + "|"
                        + "| \r\n";
                }

                //System.IO.File.WriteAllText(@"C:\Administración\Sistema complemento pago\TxtGenerados\" + datestring + "-Tralix2.txt", f03);
                string f01 = "01"                                                //1.-Tipo De Registro
                + "|" + idcomprobante                                      //2-ID Comprobante
                + "|" + seriee                                      //3-Serie
                + "|" + folioe                                      //4-Foliio 
                + "|" + fecha                 //5-Fecha y Hora De Emision
                + "|" + subt                                        //6-Subtotal
                + "|"                                         //7-Total Impuestos Trasladados
                + "|"                                         //8-Total Impuestos Retenidos
                + "|"                                               //9-Descuentos
                + "|" + total                                       //10-Total
                + "|"                        //11-Total Con Letra
                + "|" + formadepago                       //12-Forma De Pago
                + "|"                                         //13-Condiciones De Pago
                + "|" + metodopago33                                //14-Metodo de Pago
                + "|" + tmoneda                       //15-Moneda
                + "|"                                        //16-Tipo De Cambio
                + "|" + tipocomprobante                             //17-Tipo De Comprobante
                + "|" + lugarexpedicion                             //18-Lugar De Expedicion                                        
                + "|" + usocfdi                                     //19-Uso CFDI
                + "|"                               //20-Confirmacion
                + "|" + Tdoc                                     //21-Tipo documento
                + "|"                               //22-Exportacion
                + "|"                               //23-Aquiere
                + "|"
                + "| \r\n"
                //02-------------------------------------------------------------------------------------------------------------------------
                + "02"                                                   //1-Tipo De Registro
                   + "|" + IdCliente.Trim()                       //2-Id Receptor
                   + "|" + RFC.Trim()                                //3-RFC
                   + "|" + Cliente.Trim()                         //4-Nombre
                   + "|"                            //5-Pais
                   + "|"                             //6-Calle
                   + "|"                            //7-Numero Exterior
                   + "|"                             //8-Numero Interior
                   + "|"                          //9-Colonia
                   + "|"                         //10-Localidad
                   + "|"                        //11-Referencia
                   + "|"                         //12-Municio/Delegacion
                   + "|"                            //13-EStado
                   + "|" + CP.Trim()                              //14-Codigo Postal
                   + "|"                             // paisresidencia                                 //15-Pais de Residecia Fiscal Cuando La Empresa Sea Extrajera
                   + "|"                                   //16-Numero de Registro de ID Tributacion 
                   + "|"
                   + "|" + "601"    //17-Correo de envio                                                    
                   + "| \r\n"
                   + usdf04
                   //04-------------------------------------------------------------------------------------------------------------------------
                   //+ "04"                                                   //1-Tipo De Registro
                   //   + "|" + consecutivoconcepto.Trim()                       //2-Id Receptor
                   //   + "|" + claveproductoservicio.Trim()                                //3-RFC
                   //   + "|"                          //4-Nombre
                   //   + "|" + cantidad.Trim()                           //5-Pais
                   //   + "|" + claveunidad.Trim()                            //6-Calle
                   //   + "|"                             //7-Numero Exterior
                   //   + "|" + descripcion.Trim()                            //8-Numero Interior
                   //   + "|" + valorunitario.Trim()                         //9-Colonia
                   //   + "|" + importe.Trim()                        //10-Localidad
                   //   + "|"                        //11-Referencia
                   //   + "|"                         //12-Municio/Delegacion
                   //   + "|" + "02"                          //13-EStado
                   //   + "| \r\n"
                   //CPAG20-------------------------------------------------------------------------------------------------------------------------
                   + "CPAG20"
                   + "|" + "2.0"                                  //2-Identificador
                   + "| \r\n"
                   //CPAG20TOT-------------------------------------------------------------------------------------------------------------------------
                   + "CPAG20TOT"                         //1-Tipo De Registro
                   + "|"                               //2-TotalRetencionesIVA
                   + "|"                               //3-TotalRetencionesISR                                              
                   + "|"                               //4-TotalRetencionesIEPS
                   + "|"                               //5-TotalTrasladosBaseIVA16
                   + "|"                               //6-TotalTrasladosImpuestoIVA16
                   + "|"                               //7-TotalTrasladosBaseIVA8
                   + "|"                               //8-TotalTrasladosImpuestoIVA8
                   + "|"                               //9-TotalTrasladosBaseIVA0
                   + "|"                               //10-TotalTrasladosImpuestoIVA0
                   + "|"                               //11-TotalTrasladosBaseIVAExento
                   + "|" + Total                       //12-MontoTotalPagos
                   + "| \r\n"
                   + f03
                   + f04
                   + f07
                   + f08;

                //CPAG20DOC-------------------------------------------------------------------------------------------------------------------------






                System.IO.File.WriteAllText(@"C:\Administración\Sistema complemento pago\TxtGenerados\" + datestring + "-TralixGenerador.txt", f01);
                //Console.WriteLine(f01);
            }


        }
    }
}
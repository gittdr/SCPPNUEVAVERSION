using CARGAR_EXCEL.Controllers;
using CARGAR_EXCEL.Models;
using Newtonsoft.Json;
using RestSharp;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace CARGAR_EXCEL
{
    public partial class DetallesFacturas : System.Web.UI.Page
    {
        public facLabController facLabControler = new facLabController();
        //public FacCpController facLabControler = new FacCpController();
        public string fDesde, fHasta, concepto, tipoCobro, tipocomprobante, lugarexpedicion, metodopago33, formadepago, usocfdi, confirmacion, paisresidencia, numtributacion
        , mailenvio, numidentificacion, claveunidad, tipofactoriva, tipofactorret, coditrans, tipofactor, tasatras, codirete, tasarete, relacion, montosoloiva, montoivarete
        , ivadeiva, ivaderet, retderet, conceptoretencion, consecutivoconcepto, claveproductoservicio, valorunitario, importe, descuento, cantidadletra, uuidrel
        , identificador, version, fechapago, monedacpag, tipodecambiocpag, monto, numerooperacion, rfcemisorcuenta, nombrebanco, numerocuentaord, rfcemisorcuentaben, numcuentaben
        , tipocadenapago, certpago, cadenadelpago, sellodelpago, identpag, identdocpago, seriecpag, foliocpag, monedacpagdoc, tipocambiocpag, metododepago, numerodeparcialidad, f03, f04, IdentificadorDelDocumentoPagado, identificaciondpago, serieinvoice, folioscpag, monedascpadgoc, nparcialidades, interiorsaldoanterior, ipagadoisaldoinsoluto, ipagado, isaldoinsoluto, k1, k3, f05, iva, retencion
        , importeSaldoAnterior, importepago, importesaldoinsoluto, total, subt, ivat, rett, cond, tipoc, seriee, folioe, sfolio, idcomprobante, fecha, tmoneda, Tdoc, IdCliente, RFC, Cliente, Pais, Calle, NoExt, NoInt, Colonia, Localidad, Referencia, Municipio, Estado, CP, FechaPago, cantidad, descripcion, RFCbancoEmisor, BancoEmisor, CuentaPago, Total, identificadorDelPago, formadepagocpag, mmonto, if05, if06, iipagado, totaliva, totalisr, foliop, receptorp, MetdodoPagop, uidp, usdf04, TotaldeIva, TotaldeRe, f07, f08, Totalipagado, basecalculado,folio,foliot, foliopagado;

        public bool error = false;

        public string serie;
        public decimal importePagos = 0;
        
        public decimal importePagos2 = 0;
        public decimal importePagos3 = 0;
        public decimal importePagos4 = 0;
        public double ivaa = 0.16;
        public double isrr = 0.04;
        public decimal totalIva = 0;
        public decimal totalIsr = 0;
        public string ejecutar = "Si";
        

        string cpagdoc = "";
        public string escrituraFactura = "", idSucursal = "", idTipoFactura = "", jsonFactura = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            imgFDesde.Visible = false;
            imgFHasta.Visible = false;
            lblFact.Text = Request.QueryString["factura"];
            foliot = Request.QueryString["factura"];
            if (IsPostBack)
            {
                fDesde = txtFechaDesde.Text;
                fHasta = txtFechaHasta.Text;
                concepto = txtConcepto.Text;
                tipoCobro = txtTipoCobro.Text;
                formadepago = txtFormaPago.Text;
                txtFolio.Text = "";
            }

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

            try
            { //try TLS 1.3
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)12288
                                                     | (SecurityProtocolType)3072
                                                     | (SecurityProtocolType)768
                                                     | SecurityProtocolType.Tls;
            }
            catch (NotSupportedException)
            {
                try
                { //try TLS 1.2
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072
                                                         | (SecurityProtocolType)768
                                                         | SecurityProtocolType.Tls;
                }
                catch (NotSupportedException)
                {
                    try
                    { //try TLS 1.1
                        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768
                                                             | SecurityProtocolType.Tls;
                    }
                    catch (NotSupportedException)
                    { //TLS 1.0
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                    }
                }
            }


            // ESTE ES EL CODIGO LISTO 


            folio = "40737";
            serie = "TDRA";

            string datestring = DateTime.Now.ToString("yyyyMMddHHmmss");
            var request2 = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico=" + folio + "&serie=" + serie);
            var response2 = (HttpWebResponse)request2.GetResponse();
            var responseString2 = new StreamReader(response2.GetResponseStream()).ReadToEnd();

            List<ModelFact> separados = JsonConvert.DeserializeObject<List<ModelFact>>(responseString2);

            foreach (var item in separados)
            {
                string UUID = item.xmlDownload;

                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("https://canal1.xsa.com.mx:9050" + UUID);
                var xmlTexto = xDoc.InnerXml.ToString();
                DataSet dataSet1 = new DataSet();
                XmlTextReader xtr = new XmlTextReader(xDoc.OuterXml, XmlNodeType.Element, null);
                dataSet1.ReadXml(xtr);
                DataTable tdw = facLabControler.detalleFacturas(folio);

                //Obtencion de datos-------------------------------------------------------------------------------------------------------------------------

                foreach (DataRow rowa in tdw.Rows)
                {
                    IdCliente = rowa["IdReceptor"].ToString();
                    if (IdCliente == null)
                    {
                        IdCliente = "";
                    }
                    CP = rowa["CódigoPostal"].ToString();
                    consecutivoconcepto = rowa["ConsecutivoConcepto"].ToString();



                }
                foreach (DataRow row in (InternalDataCollectionBase)dataSet1.Tables["Comprobante"].Rows)
                {
                    lugarexpedicion = row["LugarExpedicion"].ToString();
                    tipocomprobante = row["TipoDeComprobante"].ToString();
                    total = row["Total"].ToString();
                    tmoneda = row["Moneda"].ToString();
                    subt = row["SubTotal"].ToString();
                    string Ccertificado = row["Certificado"].ToString();
                    string Cnocertificado = row["NoCertificado"].ToString();
                    string Csello = row["Sello"].ToString();
                    fecha = row["Fecha"].ToString();
                    idcomprobante = row["Folio"].ToString();
                    serie = row["Serie"].ToString();
                }
                foreach (DataRow rows in (InternalDataCollectionBase)dataSet1.Tables["Emisor"].Rows)
                {
                    string Eregimenfiscal = rows["RegimenFiscal"].ToString();
                    string Enombre = rows["Nombre"].ToString();
                    string Erfc = rows["Rfc"].ToString();
                }
                foreach (DataRow rowsr in (InternalDataCollectionBase)dataSet1.Tables["Receptor"].Rows)
                {
                    Cliente = rowsr["Nombre"].ToString();
                    RFC = rowsr["Rfc"].ToString();
                    usocfdi = rowsr["UsoCFDI"].ToString();
                    if (usocfdi == "P01")
                    {
                        usocfdi = "CP01";
                    }
                }
                foreach (DataRow rowsr in (InternalDataCollectionBase)dataSet1.Tables["Conceptos"].Rows)
                {
                    foreach (DataRow rowsrc in (InternalDataCollectionBase)dataSet1.Tables["Concepto"].Rows)
                    {
                        importe = rowsrc["Importe"].ToString();
                        valorunitario = rowsrc["ValorUnitario"].ToString();
                        descripcion = rowsrc["Descripcion"].ToString();
                        claveunidad = rowsrc["ClaveUnidad"].ToString();
                        cantidad = rowsrc["Cantidad"].ToString();
                        claveproductoservicio = rowsrc["ClaveProdServ"].ToString();
                    }
                }
                foreach (DataRow rowsr1 in (InternalDataCollectionBase)dataSet1.Tables["Complemento"].Rows)
                {
                    foreach (DataRow rowsrct in (InternalDataCollectionBase)dataSet1.Tables["TimbreFiscalDigital"].Rows)
                    {
                        string Trfcprovcertif = rowsrct["RfcProvCertif"].ToString();
                        string Tsellosat = rowsrct["SelloSAT"].ToString();
                        string Tsellocfd = rowsrct["SelloCFD"].ToString();
                        string Tnocertidicadosat = rowsrct["NoCertificadoSAT"].ToString();
                        string Tuuid = rowsrct["UUID"].ToString();
                        string Tfechatimbrado = rowsrct["FechaTimbrado"].ToString();
                    }
                    foreach (DataRow rowsrcts in (InternalDataCollectionBase)dataSet1.Tables["Pagos"].Rows)
                    {
                        foreach (DataRow rowsrctp in (InternalDataCollectionBase)dataSet1.Tables["Pago"].Rows)
                        {
                            string Pctabeneficiario = rowsrctp["CtaBeneficiario"].ToString();
                            string Prfcemisorctaben = rowsrctp["RfcEmisorCtaBen"].ToString();
                            //string Pctaordenante = rowsrctp["CtaOrdenante"].ToString();
                            //string Pnombancoordext = rowsrctp["NomBancoOrdExt"].ToString();
                            //string Prfcemisorctaord = rowsrctp["RfcEmisorCtaOrd"].ToString();
                            Total = rowsrctp["Monto"].ToString();
                            monedacpag = rowsrctp["MonedaP"].ToString();
                            formadepagocpag = rowsrctp["FormaDePagoP"].ToString();
                            fechapago = rowsrctp["FechaPago"].ToString();

                            f03 += "CPAG20PAGO"
                                + "|" + idcomprobante.Trim()
                                + "|" + fechapago.Trim()
                                + "|" + formadepagocpag.Trim()
                                + "|" + monedacpag.Trim()
                                + "|"
                                + "|" + Total.Trim()
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
                                + "|";

                            int totalmn = 1;
                            DataSet dataSet2 = new DataSet();
                            int x = 0;
                            foreach (DataRow rowsrctpr in (InternalDataCollectionBase)dataSet1.Tables["DoctoRelacionado"].Rows)
                            {

                                k3 = rowsrctpr["Folio"].ToString();
                                string Dserie = rowsrctpr["Serie"].ToString();
                                isaldoinsoluto = rowsrctpr["ImpSaldoInsoluto"].ToString();
                                ipagado = rowsrctpr["ImpPagado"].ToString();
                                interiorsaldoanterior = rowsrctpr["ImpSaldoAnt"].ToString();
                                nparcialidades = rowsrctpr["NumParcialidad"].ToString();
                                metodopago33 = rowsrctpr["MetodoDePagoDR"].ToString();
                                monedascpadgoc = rowsrctpr["MonedaDR"].ToString();
                                IdentificadorDelDocumentoPagado = rowsrctpr["IdDocumento"].ToString();

                                //int totalr = (int)rowsrctpr["Count"] + 1;
                                //int totalr = (rowsrctpr["Count"] as int?) ?? 0;
                                int totalr = dataSet1.Tables["DoctoRelacionado"].Select("Folio is not null").Length;
                                if (totalmn == totalr)
                                {
                                    if06 = "CPAG20DOCIMPTRA"
                                    + "|" + idcomprobante.Trim()
                                    + "|" + k3.Trim()
                                    + "|" + IdentificadorDelDocumentoPagado.Trim()
                                    + "|" + "002"
                                    + "|" + "Tasa"
                                    + "|" + "0.160000"
                                    + "|" + totaliva
                                    //+ "|" + retencion
                                    + "|" + ipagado.Trim()
                                    + "|";

                                }
                                else
                                {
                                    if06 = "CPAG20DOCIMPTRA"
                                    + "|" + idcomprobante.Trim()
                                    + "|" + k3.Trim()
                                    + "|" + IdentificadorDelDocumentoPagado.Trim()
                                    + "|" + "002"
                                    + "|" + "Tasa"
                                    + "|" + "0.160000"
                                    + "|" + totaliva
                                    //+ "|" + retencion
                                    + "|" + ipagado.Trim()
                                    + "| \r\n";

                                }


                                if (metodopago33 == "PPD")
                                {
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
                                           + "|";

                                        f04 += "CPAG20DOC"
                                             + "|" + idcomprobante.Trim()
                                             + "|" + IdentificadorDelDocumentoPagado.Trim()
                                             + "|"
                                             + "|"
                                             + "|" + monedascpadgoc
                                             + "|"
                                             + "|" + nparcialidades
                                             + "|" + interiorsaldoanterior
                                             + "|" + ipagado
                                             + "|" + isaldoinsoluto
                                             + "|" + "02"
                                             + "| \r\n";
                                        f07 = "";
                                        f08 = "";



                                    }
                                    else
                                    {
                                        totalIva = (decimal)(ivaa * Convert.ToDouble(ipagado));
                                        totaliva = totalIva.ToString("F");
                                        totalIsr = (decimal)(isrr * Convert.ToDouble(ipagado));
                                        totalisr = totalIsr.ToString("F");

                                        if05 = "CPAG20DOCIMPRET"
                                        + "|" + idcomprobante.Trim()
                                        + "|" + k3.Trim()
                                        + "|" + IdentificadorDelDocumentoPagado.Trim()
                                        + "|" + "001"
                                        + "|" + "Tasa"
                                        + "|" + "0.040000"
                                        + "|" + totalisr
                                        //+ "|" + iva.Trim()
                                        + "|" + ipagado.Trim()
                                        + "| \r\n";
                                        //if06 = "CPAG20DOCIMPTRA"
                                        //+ "|" + idcomprobante.Trim()
                                        //+ "|" + k3.Trim()
                                        //+ "|" + IdentificadorDelDocumentoPagado.Trim()
                                        //+ "|" + "002"
                                        //+ "|" + "Tasa"
                                        //+ "|" + "0.160000"
                                        //+ "|" + totaliva
                                        ////+ "|" + retencion
                                        //+ "|" + ipagado.Trim()
                                        //+ "| \r\n";
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
                                        + "|" + idcomprobante.Trim()
                                        + "|" + "001"
                                        + "|" + TotaldeRe
                                        + "|";

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
                                        + "|" + idcomprobante.Trim()
                                        + "|" + "002"
                                        + "|" + "Tasa"
                                        + "|" + "0.160000"
                                        + "|" + TotaldeIva
                                        + "|" + ipagado.Trim()
                                        + "|";

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
                                       + "|";



                                        f04 += "CPAG20DOC"
                                     + "|" + idcomprobante.Trim()
                                     + "|" + IdentificadorDelDocumentoPagado.Trim()
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
                                    totalmn++;








                                }


                            }
                        }
                    }
                }


            }
            if (ejecutar == "Si")
            {
                generarTXTTralix();
            }


            //ESTE CODIGO GENERA EL TXT DE PRUEBA

            //string f01 = "01"
            //    + "|" + idcomprobante.Trim()
            //    + "|" + serie.Trim()
            //    + "|" + idcomprobante.Trim()
            //    + "|" + fecha.Trim()
            //    + "|" + subt.Trim()
            //    + "|"
            //    + "|"
            //    + "|"
            //    + "|" + total.Trim()
            //    + "|"
            //    + "|" + "03"
            //    + "|"
            //    + "|" + metodopago33
            //    + "|" + tmoneda
            //    + "|"
            //    + "|" + tipocomprobante
            //    + "|" + lugarexpedicion
            //    + "|" + usocfdi
            //    + "|"
            //    + "|" + "FAC"
            //    + "|"
            //    + "|"
            //    + "| \r\n"
            //    //02 -------------------
            //    + "02"                                                   //1-Tipo De Registro
            //       + "|" + IdCliente.Trim()                       //2-Id Receptor
            //       + "|" + RFC.Trim()                                //3-RFC
            //       + "|" + Cliente.Trim()                         //4-Nombre
            //       + "|"                            //5-Pais
            //       + "|"                             //6-Calle
            //       + "|"                            //7-Numero Exterior
            //       + "|"                             //8-Numero Interior
            //       + "|"                          //9-Colonia
            //       + "|"                         //10-Localidad
            //       + "|"                        //11-Referencia
            //       + "|"                         //12-Municio/Delegacion
            //       + "|"                            //13-EStado
            //       + "|" + CP.Trim()                              //14-Codigo Postal
            //       + "|"                             // paisresidencia                                 //15-Pais de Residecia Fiscal Cuando La Empresa Sea Extrajera
            //       + "|"                                   //16-Numero de Registro de ID Tributacion 
            //       + "|"
            //       + "|" + "601"    //17-Correo de envio                                                    
            //       + "| \r\n"
            //       //04 ------------------
            //       + usdf04
            //       //CPAG20-------------------------------------------------------------------------------------------------------------------------
            //       + "CPAG20"
            //       + "|" + "2.0"                                  //2-Identificador
            //       + "| \r\n"
            //       //CPAG20TOT-------------------------------------------------------------------------------------------------------------------------
            //       + "CPAG20TOT"                         //1-Tipo De Registro
            //       + "|"                               //2-TotalRetencionesIVA
            //       + "|"                               //3-TotalRetencionesISR                                              
            //       + "|"                               //4-TotalRetencionesIEPS
            //       + "|"                               //5-TotalTrasladosBaseIVA16
            //       + "|"                               //6-TotalTrasladosImpuestoIVA16
            //       + "|"                               //7-TotalTrasladosBaseIVA8
            //       + "|"                               //8-TotalTrasladosImpuestoIVA8
            //       + "|"                               //9-TotalTrasladosBaseIVA0
            //       + "|"                               //10-TotalTrasladosImpuestoIVA0
            //       + "|"                               //11-TotalTrasladosBaseIVAExento
            //       + "|" + Total                       //12-MontoTotalPagos
            //       + "| \r\n"
            //       + f03
            //       + f04
            //       + f07
            //       + f08;

            //System.IO.File.WriteAllText(@"C:\Administración\Sistema complemento pago\TxtGenerados\" + datestring + "-ExportdesdeXml.txt", f01);

            //AQUI TERMINA EL TXT DE PRUEBA

            //DESDE AQUI EMPIZA EL TXT DE PRODUCCION
            DataTable td = facLabControler.detalleFacturas(lblFact.Text);
            Div1.Visible = false;
            //Obtencion de datos------------------------------------------------------------------------------------------------------------------------ -

            foreach (DataRow row in td.Rows)
            {


                //01-------------------------------------------------------------------------------------------------------------------------
                if (txtFolio.Text != row["SFolio"].ToString())
                {
                    txtFechaIniOP.Text = txtFechaIniOP.Text + "\r\n" + row["IdentificadorDelDocumentoPagado"].ToString();
                    FolioUUIDTxt.Text = row["UUIDident"].ToString();
                    txtFolio.Text = row["SFolio"].ToString();
                    DateTime dt = DateTime.Parse(row["FechaHoraEmision"].ToString());
                    txtFechaFactura.Text = dt.ToString("yyyy'/'MM'/'dd HH:mm:ss");

                    sfolio = row["SFolio"].ToString();


                    seriee = row["Serie"].ToString();

                    folioe = row["Folio"].ToString();
                    subt = row["Subtotal"].ToString();
                    ivat = row["TotalImpuestosTrasladados"].ToString();
                    rett = row["TotalImpuestosRetenidos"].ToString();
                    total = row["Total"].ToString();
                    cantidadletra = row["Totalconletra"].ToString();
                    //formadepago = row["FormaDePago"].ToString();
                    cond = row["CondicionesdePago"].ToString();
                    metodopago33 = row["MetodoPago"].ToString();
                    txtMoneda.Text = row["Moneda"].ToString();
                    tipoc = row["Tipodecambio"].ToString();
                    tipocomprobante = row["TipodeComprobante"].ToString();
                    lugarexpedicion = row["LugardeExpedición"].ToString();
                    usocfdi = row["UsoCFDI"].ToString();
                    confirmacion = row["Confirmación"].ToString();

                    //02-------------------------------------------------------------------------------------------------------------------------

                    txtIdCliente.Text = row["IdReceptor"].ToString();

                    txtRFC.Text = row["RFC"].ToString();
                    RFC = row["RFC"].ToString();
                    txtCliente.Text = row["Nombre"].ToString();
                    txtPaís.Text = row["Pais"].ToString();
                    txtCalle.Text = row["Calle"].ToString();
                    txtNoExt.Text = row["NumeroExterior"].ToString();
                    txtNoInt.Text = row["NumeroInterior"].ToString();
                    txtColonia.Text = row["Colonia"].ToString();
                    txtLocalidad.Text = row["Localidad"].ToString();
                    txtReferencia.Text = row["Referencia"].ToString();
                    txtMunicipio.Text = row["MunicipioDelegacion"].ToString();
                    txtEstado.Text = row["Estado"].ToString();
                    txtCP.Text = row["CódigoPostal"].ToString();
                    txtFechaPago.Text = row["Fechapago"].ToString();
                    paisresidencia = row["PaísResidenciaFiscal"].ToString();
                    numtributacion = row["NúmeroDeRegistroIdTributacion"].ToString();
                    mailenvio = row["CorreoEnvio"].ToString();

                    //04-------------------------------------------------------------------------------------------------------------------------

                    consecutivoconcepto = row["ConsecutivoConcepto"].ToString();
                    claveproductoservicio = row["ClaveProductooServicio"].ToString();
                    numidentificacion = row["NumeroIdentificación"].ToString();
                    claveunidad = row["ClaveUnidad"].ToString();
                    txtUnidadMedida.Text = row["ClaveUnidad"].ToString();
                    txtIdConcepto.Text = row["ClaveProductooServicio"].ToString();
                    txtCantidad.Text = row["Cantidad"].ToString();
                    txtMetodoPago.Text = row["MedotoDePago"].ToString();

                    if (concepto == null || concepto.Equals(row["Descripcion"].ToString())) { txtConcepto.Text = row["Descripcion"].ToString(); }
                    else { txtConcepto.Text = concepto; }


                    if (formadepago == null || formadepago.Equals(row["Formadepagocpag"].ToString())) { txtFormaPago.Text = row["Formadepagocpag"].ToString(); }
                    else { txtFormaPago.Text = formadepago; }


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
                    txtRFCbancoEmisor.Text = row["RFCEmisorCuentaBeneficiario"].ToString();
                    txtBancoEmisor.Text = row["NombreDelBanco"].ToString();
                    txtCuentaPago.Text = row["NumeroCuentaOrdenante"].ToString();
                    rfcemisorcuentaben = row["RFCEmisorCuentaBeneficario"].ToString();
                    numcuentaben = row["NumerCuentaBeneficiario"].ToString();
                    tipocadenapago = row["TipoCadenaPago"].ToString();
                    certpago = row["CertificadoPago"].ToString();
                    cadenadelpago = row["CadenaDePago"].ToString();
                    sellodelpago = row["SelloDePago"].ToString();




                    //CPAGDOC-----------------------------------------------------------------------------------------------------------------------
                    DataTable detalleIdent = facLabControler.getDatosCPAGDOC(row["IdentificadorDelPago"].ToString());
                    string uid = "";

                    //foreach (DataRow rowIdent in detalleIdent.Rows)
                    //{
                    //    if (rowIdent["MedotoDePago"].ToString() == "PPD")
                    //    {
                    //        string folio = Regex.Replace(rowIdent["Foliocpag"].ToString(), @"[A-Z]", "");

                    //        string receptor = txtIdCliente.Text.ToString().Trim();
                    //        string serieinvoice = "";
                    //        if (receptor.Equals("LIVERPOL") || receptor.Equals("ALMLIVER") || receptor.Equals("LIVERTIJ") || receptor.Equals("SFERALIV") || receptor.Equals("GLOBALIV") || receptor.Equals("SETRALIV") || receptor.Equals("FACTUMLV"))
                    //        {
                    //            serieinvoice = "TDRL";
                    //        }
                    //        else
                    //        {
                    //            serieinvoice = rowIdent["Seriecpag"].ToString();
                    //        }
                    //        if (folio.Length == 7 && folio.StartsWith("99"))
                    //        {
                    //            folio = folio.Substring(folio.Length - 6, 6);
                    //        }
                    //        else if (folio.Length == 8)
                    //        {
                    //            folio = folio.Substring(folio.Length - 7, 7);
                    //        }



                    //        DataTable datosMaster = facLabControler.getDatosMaster(folio);
                    //        if (datosMaster.Rows.Count > 0)
                    //        {

                    //            foreach (DataRow rowMaster in datosMaster.Rows)
                    //            {
                    //                string invoiceMaster = Regex.Replace(rowMaster[0].ToString(), @"[A-Z]", "");

                    //                var request = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico=" + invoiceMaster + "&serie=" + serieinvoice);
                    //                var response = (HttpWebResponse)request.GetResponse();
                    //                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    //                string[] separadas = responseString.Split(',');
                    //                foreach (string dato in separadas)
                    //                {
                    //                    if (dato.Contains("uuid"))
                    //                    {
                    //                        uid = dato.Replace(dato.Substring(0, 8), "").Replace("\"", "");
                    //                    }
                    //                }

                    //            }
                    //        }
                    //        else
                    //        {
                    //            var request = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico=" + folio + "&serie=" + serieinvoice);
                    //            var response = (HttpWebResponse)request.GetResponse();
                    //            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    //            string[] separadas = responseString.Split(',');
                    //            foreach (string dato in separadas)
                    //            {
                    //                if (dato.Contains("uuid"))
                    //                {
                    //                    uid = dato.Replace(dato.Substring(0, 8), "").Replace("\"", "");
                    //                }
                    //            }
                    //        }
                    //          //todo: UUID DE FACTURAS PAGADAS

                    //        //txtFechaIniOP.Text = txtFechaIniOP.Text + "\r\n" + rowIdent["IdentificadorDelDocumentoPagado"].ToString();
                    //        txtFechaIniOP.Text = txtFechaIniOP.Text + "\r\n" + uid;
                    //    }
                    //}



                    //uid = "";
                    decimal importePagos = 0;
                    int contadorPUE = 0;
                    int contadorPPD = 0;




                    foreach (DataRow rowIdent in detalleIdent.Rows)
                    {
                        folio = Regex.Replace(rowIdent["Foliocpag"].ToString().Replace("TDR", "").Trim(), @"[A-Z]", "");

                        //txtTotal.Text = importePagos.ToString();
                        txtTotal.Text = rowIdent["ImportePagado"].ToString();
                        string receptor = txtIdCliente.Text.ToString().Trim();
                        string serieinvoice = "";
                        if (receptor.Equals("LIVERPOL") || receptor.Equals("LIVERDED") || receptor.Equals("ALMLIVER") || receptor.Equals("LIVERTIJ") || receptor.Equals("SFERALIV") || receptor.Equals("GLOBALIV") || receptor.Equals("SETRALIV") || receptor.Equals("FACTUMLV"))
                        {
                            serieinvoice = "TDRL";
                        }
                        else
                        {
                            serieinvoice = rowIdent["Seriecpag"].ToString();
                        }
                        folio = Regex.Replace(rowIdent["Foliocpag"].ToString().Replace("TDR", "").Trim(), @"[A-Z]", "");
                        if (folio.Length == 7 && folio.StartsWith("99"))
                        {
                            folio = folio.Substring(folio.Length - 6, 6);
                        }
                        else if (folio.Length == 8)
                        {
                            folio = folio.Substring(folio.Length - 7, 7);
                        }
                        folio = folio.Replace("-", "");
                        //validar con la serie el id de sucursal-serie

                        var MetdodoPago = "";
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

                        if (MetdodoPago == "PPD")
                        {

                            identpag = rowIdent["IdentificadorDelPago"].ToString();
                            //txtFechaIniOP.Text = "\r\n" +rowIdent["IdentificadorDelDocumentoPagado"].ToString();
                            seriecpag = rowIdent["Seriecpag"].ToString();
                            foliocpag = rowIdent["Foliocpag"].ToString();
                            monedacpagdoc = rowIdent["Monedacpagdoc"].ToString();
                            tipocambiocpag = rowIdent["TipodeCambiocpagdpc"].ToString();
                            txtMetodoPago.Text = rowIdent["MedotoDePago"].ToString();
                            numerodeparcialidad = rowIdent["NumeroDeParcialidad"].ToString();
                            importeSaldoAnterior = rowIdent["ImporteSaldoAnterior"].ToString();
                            importepago = rowIdent["ImportePagado"].ToString();
                            importesaldoinsoluto = rowIdent["ImporteSaldoInsoluto"].ToString();
                            //FolioUUIDTxt.Text += identpag;
                            try
                            {
                                importePagos = importePagos + Convert.ToDecimal(importepago);
                                txtTotal.Text = importePagos.ToString();
                            }
                            catch (Exception ex)
                            {
                                string errors = ex.Message;
                            }

                            //txtFechaIniOP.Text = txtFechaIniOP.Text + "\r\n" + rowIdent["IdentificadorDelDocumentoPagado"].ToString();
                            txtFechaIniOP.Text = txtFechaIniOP.Text + "\r\n" + uid;
                            FolioUUIDTxt.Text = FolioUUIDTxt.Text + "\r\n" + "Serie:" + serieinvoice + " Folio:" + folio + " UUID:" + uid;



                            if (monedacpagdoc.Trim() == "USD")
                            {
                                cpagdoc = cpagdoc + ("CPAGDOC"                           //1-Tipo De Registro
                                  + "|" + identpag                                       //2-IdentificadorDelPago
                                                                                         //+ "|" + rowIdent["IdentificadorDelDocumentoPagado"].ToString()                            //3-IdentificadorDelDocumentoPagado                                              
                                  + "|" + uid                                            //3-IdentificadorDelDocumentoPagado                                              
                                  + "|" + serieinvoice                                   //4-Seriecpag
                                  + "|" + foliocpag                                      //5-Foliocpag
                                  + "|" + monedacpagdoc                                  //6-Monedacpag
                                  + "|" + ""                                             //7-TipoCambiocpagdpc
                                  + "|" + txtMetodoPago.Text                             //8-MetodoDePago
                                  + "|" + numerodeparcialidad                            //9-NumeroDeParcialidad
                                  + "|" + importepago                                    //10-ImporteSaldoAnterior
                                  + "|" + importepago                                    //11-ImportePagado                                                  
                                  + "|" + "0"                                            //12 ImporteSaldoInsoluto
                                  + "| \r\n");
                            }
                            else
                            {
                                //----------------------------------------Seccion CPAG20PAGO -------------------------------------------------------------------

                                //CPAG20PAGO (1:N)
                                //escritor.WriteLine(
                                //"CPAG20PAGO"                        //1-Tipo De Registro
                                //+ "|" + identpag                    //2-IdentificadorDelPago
                                //+ "|" + fechapago                   //3-FechaPago                                              
                                //+ "|"  + formadepagocpag            //4-Forma de pago
                                //+ "|" + moneda                      //5-Moneda
                                //+ "|"                               //6-TipoDeCambiocpag
                                //+ "|" + monto                       //7-Monto
                                //+ "|"                               //8-NumeroOperacion
                                //+ "|"                               //9-RFCEmisorCuentaOrdenante
                                //+ "|"                               //10-Nombre del Banco
                                //+ "|"                               //11-Número de Cuenta Ordenante
                                //+ "|"                               //12-RFC Emisor Cuenta Beneficiario
                                //+ "|"                               //13-Número de Cuenta Beneficiario
                                //+ "|"                               //14-Tipo Cadena Pago
                                //+ "|"                               //15-Certificado Pago
                                //+ "|"                               //16-Cadena Pago
                                //+ "|"                               //17-Sello de Pago                                                                                                 
                                //+ "|"                               //Fin Del Registro
                                //);

                                //escrituraFactura += "CPAG20PAGO"    //1-Tipo De Registro
                                //+ "|" + identpag                    //2-IdentificadorDelPago
                                //+ "|" + fechapago                   //3-FechaPago                                              
                                //+ "|"  + formadepagocpag            //4-Forma de pago
                                //+ "|" + moneda                      //5-Moneda
                                //+ "|"                               //6-TipoDeCambiocpag
                                //+ "|" + monto                       //7-Monto
                                //+ "|"                               //8-NumeroOperacion
                                //+ "|"                               //9-RFCEmisorCuentaOrdenante
                                //+ "|"                               //10-Nombre del Banco
                                //+ "|"                               //11-Número de Cuenta Ordenante
                                //+ "|"                               //12-RFC Emisor Cuenta Beneficiario
                                //+ "|"                               //13-Número de Cuenta Beneficiario
                                //+ "|"                               //14-Tipo Cadena Pago
                                //+ "|"                               //15-Certificado Pago
                                //+ "|"                               //16-Cadena Pago
                                //+ "|"                               //17-Sello de Pago                                                                                                 
                                //+ "|";                               //Fin Del Registro
                                // -------------------------- CPAG20DOC ------------------------------------------
                                //cpagdoc = cpagdoc + ("CPAG20DOC"                       //1-Tipo De Registro
                                //+ "|" + identpag                                       //2-IdentificadorDelPago
                                //+ "|" + rowIdent["IdentificadorDelDocumentoPagado"].ToString()                            //3-IdentificadorDelDocumentoPagado                                              
                                //+ "|" + uid                            //3-IdentificadorDelDocumentoPagado                                              
                                //+ "|" + serieinvoice                                      //4-Seriecpag
                                //+ "|" + foliocpag                                      //5-Foliocpag
                                //+ "|" + monedacpagdoc                                  //6-Monedacpag
                                //+ "|" + tipocambiocpag                                 //7-TipoCambiocpagdpc Equivalencia                          
                                //+ "|" + numerodeparcialidad                            //9-NumeroDeParcialidad
                                //+ "|" + importeSaldoAnterior                           //10-ImporteSaldoAnterior
                                //+ "|" + importepago                                    //11-ImportePagado                                                  
                                //+ "|" + importesaldoinsoluto                           //12 ImporteSaldoInsoluto
                                //+ "| \r\n");


                                cpagdoc = cpagdoc + ("CPAGDOC"                                              //1-Tipo De Registro
                                  + "|" + identpag                                       //2-IdentificadorDelPago
                                                                                         //+ "|" + rowIdent["IdentificadorDelDocumentoPagado"].ToString()                            //3-IdentificadorDelDocumentoPagado                                              
                                  + "|" + uid                            //3-IdentificadorDelDocumentoPagado                                              
                                  + "|" + serieinvoice                                      //4-Seriecpag
                                  + "|" + foliocpag                                      //5-Foliocpag
                                  + "|" + monedacpagdoc                                  //6-Monedacpag
                                  + "|" + tipocambiocpag                                 //7-TipoCambiocpagdpc
                                  + "|" + txtMetodoPago.Text                             //8-MetodoDePago
                                  + "|" + numerodeparcialidad                            //9-NumeroDeParcialidad
                                  + "|" + importeSaldoAnterior                           //10-ImporteSaldoAnterior
                                  + "|" + importepago                                    //11-ImportePagado                                                  
                                  + "|" + importesaldoinsoluto                           //12 ImporteSaldoInsoluto
                                  + "| \r\n");
                            }
                        }

                    }

                    if (contadorPPD == 0 && contadorPUE > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('La factura es PUE!! y es libre de todo PPD', 'success');", true);
                        //PopupMsg.Message1 = "La factura es PUE!! y es libre de todo PPD";
                        //PopupMsg.ShowPopUp(0);
                    }

                    //OTROS-------------------------------------------------------------------------------------------------------------------------

                    // creamos el FolioUUID
                    DataTable detalleIdentt = facLabControler.getDatosCPAGDOCTRL(identificaciondpago, folioscpag);

                    foreach (DataRow item in detalleIdentt.Rows)
                    {
                        foliopagado = item["K3"].ToString();
                        var request23 = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico=" + foliopagado);
                        var response23 = (HttpWebResponse)request23.GetResponse();
                        var responseString23 = new StreamReader(response23.GetResponseStream()).ReadToEnd();

                        List<ModelFact> separados23 = JsonConvert.DeserializeObject<List<ModelFact>>(responseString23);
                        foreach (var item2 in separados23)
                        {
                            if (RFC == item2.rfc & foliopagado == item2.folio)
                            {
                                string UUID = item2.xmlDownload;

                                XmlDocument xDoc = new XmlDocument();
                                xDoc.Load("https://canal1.xsa.com.mx:9050" + UUID);
                                var xmlTexto = xDoc.InnerXml.ToString();
                                DataSet dataSet1 = new DataSet();
                                XmlTextReader xtr = new XmlTextReader(xDoc.OuterXml, XmlNodeType.Element, null);
                                dataSet1.ReadXml(xtr);
                                foreach (DataRow rowsrcts in (InternalDataCollectionBase)dataSet1.Tables["Pagos"].Rows)
                                {
                                    foreach (DataRow rowsrctp in (InternalDataCollectionBase)dataSet1.Tables["Pago"].Rows)
                                    {
                                        string Pctabeneficiario = rowsrctp["CtaBeneficiario"].ToString();
                                        string Prfcemisorctaben = rowsrctp["RfcEmisorCtaBen"].ToString();
                                        //string Pctaordenante = rowsrctp["CtaOrdenante"].ToString();
                                        //string Pnombancoordext = rowsrctp["NomBancoOrdExt"].ToString();
                                        //string Prfcemisorctaord = rowsrctp["RfcEmisorCtaOrd"].ToString();
                                        Total = rowsrctp["Monto"].ToString();
                                        monedacpag = rowsrctp["MonedaP"].ToString();
                                        formadepagocpag = rowsrctp["FormaDePagoP"].ToString();
                                        fechapago = rowsrctp["FechaPago"].ToString();

                                        f03 += "CPAG20PAGO"
                                            + "|" + idcomprobante.Trim()
                                            + "|" + fechapago.Trim()
                                            + "|" + formadepagocpag.Trim()
                                            + "|" + monedacpag.Trim()
                                            + "|"
                                            + "|" + Total.Trim()
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
                                            + "|";


                                        DataSet dataSet2 = new DataSet();
                                        int x = 0;
                                        foreach (DataRow rowsrctpr in (InternalDataCollectionBase)dataSet1.Tables["DoctoRelacionado"].Rows)
                                        {

                                            k3 = rowsrctpr["Folio"].ToString();
                                            string Dserie = rowsrctpr["Serie"].ToString();
                                            isaldoinsoluto = rowsrctpr["ImpSaldoInsoluto"].ToString();
                                            ipagado = rowsrctpr["ImpPagado"].ToString();
                                            interiorsaldoanterior = rowsrctpr["ImpSaldoAnt"].ToString();
                                            nparcialidades = rowsrctpr["NumParcialidad"].ToString();
                                            metodopago33 = rowsrctpr["MetodoDePagoDR"].ToString();
                                            monedascpadgoc = rowsrctpr["MonedaDR"].ToString();
                                            IdentificadorDelDocumentoPagado = rowsrctpr["IdDocumento"].ToString();

                                            //int totalr = (int)rowsrctpr["Count"] + 1;
                                            //int totalr = (rowsrctpr["Count"] as int?) ?? 0;
                                            int totalr = dataSet1.Tables["DoctoRelacionado"].Select("Folio is not null").Length;

                                            if (xmlTexto.Contains("Traslado") & xmlTexto.Contains("Retencion"))
                                            {
                                                FolioUUIDTxt.Text += IdentificadorDelDocumentoPagado;
                                                Console.WriteLine("Si tiene traslado y retención");
                                            }













                                        }
                                    }
                                }
                            }
                        }
                    }





                    txtFechaHasta.Text = "Complemento Pago";


                    txtFechaDesde.Text = "Complemento Pago";


                    txtTipoCobro.Text = "Complemento Pago";

                }
            }
            // esta parte fue de prueba

               //var request2 = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico=" + foliot);
               // var response2 = (HttpWebResponse)request2.GetResponse();
               // var responseString2 = new StreamReader(response2.GetResponseStream()).ReadToEnd();

               // List<ModelFact> separados = JsonConvert.DeserializeObject<List<ModelFact>>(responseString2);
               // foreach (var item in separados)
               // {
               //    string serietra = item.serie;
               //     if (serietra != "TDRA")
               //     {
               //         serie = "TDRC";
               //     }
                    
               //     //string msg = "Esta no tiene complemento";
               //     //formularioT.Visible = false;
               //     //Div1.Visible = true;
               //     //ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Complemento pago', 'error');setTimeout(function(){window.location.href ='Listado.aspx'}, 3000)", true);

               // }
               
                    
               //     if (serie == "TDRC")
               //     {
               //         Div1.Visible = false;
               //         var request3 = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico=" + foliot + "&serie=" + serie);
               //         var response3 = (HttpWebResponse)request3.GetResponse();
               //         var responseString3 = new StreamReader(response3.GetResponseStream()).ReadToEnd();

               //         List<ModelFact> separados2 = JsonConvert.DeserializeObject<List<ModelFact>>(responseString3);
               //         foreach (var item in separados2)
               //         {
               //             string UUID = item.xmlDownload;

               //             XmlDocument xDoc = new XmlDocument();
               //             xDoc.Load("https://canal1.xsa.com.mx:9050" + UUID);
               //             var xmlTexto = xDoc.InnerXml.ToString();
               //             DataSet dataSet1 = new DataSet();
               //             XmlTextReader xtr = new XmlTextReader(xDoc.OuterXml, XmlNodeType.Element, null);
               //             dataSet1.ReadXml(xtr);
               //             DataTable tdw2 = facLabControler.detalleFacturas(foliot);
               //             //Obtencion de datos-------------------------------------------------------------------------------------------------------------------------

               //             foreach (DataRow rowa in tdw2.Rows)
               //             {
               //                 IdCliente = rowa["IdReceptor"].ToString();
               //                 if (IdCliente == null)
               //                 {
               //                     IdCliente = "";
               //                 }
               //                 CP = rowa["CódigoPostal"].ToString();
               //                 consecutivoconcepto = rowa["ConsecutivoConcepto"].ToString();



               //             }
               //             foreach (DataRow row in (InternalDataCollectionBase)dataSet1.Tables["Comprobante"].Rows)
               //             {
               //                 lugarexpedicion = row["LugarExpedicion"].ToString();
               //                 tipocomprobante = row["TipoDeComprobante"].ToString();
               //                 total = row["Total"].ToString();
               //                 tmoneda = row["Moneda"].ToString();
               //                 subt = row["SubTotal"].ToString();
               //                 string Ccertificado = row["Certificado"].ToString();
               //                 string Cnocertificado = row["NoCertificado"].ToString();
               //                 string Csello = row["Sello"].ToString();
               //                 fecha = row["Fecha"].ToString();
               //                 idcomprobante = row["Folio"].ToString();
               //                 serie = row["Serie"].ToString();
               //             }
               //             foreach (DataRow rowsr in (InternalDataCollectionBase)dataSet1.Tables["Receptor"].Rows)
               //             {
               //                 Cliente = rowsr["Nombre"].ToString();
               //                 RFC = rowsr["Rfc"].ToString();
               //                 usocfdi = rowsr["UsoCFDI"].ToString();
               //                 if (usocfdi == "P01")
               //                 {
               //                     usocfdi = "CP01";
               //                 }
               //             }
               //             foreach (DataRow rowsr in (InternalDataCollectionBase)dataSet1.Tables["Conceptos"].Rows)
               //             {
               //                 foreach (DataRow rowsrc in (InternalDataCollectionBase)dataSet1.Tables["Concepto"].Rows)
               //                 {
               //                     importe = rowsrc["Importe"].ToString();
               //                     valorunitario = rowsrc["ValorUnitario"].ToString();
               //                     descripcion = rowsrc["Descripcion"].ToString();
               //                     claveunidad = rowsrc["ClaveUnidad"].ToString();
               //                     cantidad = rowsrc["Cantidad"].ToString();
               //                     claveproductoservicio = rowsrc["ClaveProdServ"].ToString();
               //                 }
               //             }
               //     foreach (DataRow rowsr1 in (InternalDataCollectionBase)dataSet1.Tables["Complemento"].Rows)
               //     {
               //         foreach (DataRow rowsrct in (InternalDataCollectionBase)dataSet1.Tables["TimbreFiscalDigital"].Rows)
               //         {
               //             string Trfcprovcertif = rowsrct["RfcProvCertif"].ToString();
               //             string Tsellosat = rowsrct["SelloSAT"].ToString();
               //             string Tsellocfd = rowsrct["SelloCFD"].ToString();
               //             string Tnocertidicadosat = rowsrct["NoCertificadoSAT"].ToString();
               //             string Tuuid = rowsrct["UUID"].ToString();
               //             string Tfechatimbrado = rowsrct["FechaTimbrado"].ToString();
               //         }
               //         foreach (DataRow rowsrcts in (InternalDataCollectionBase)dataSet1.Tables["Pagos"].Rows)
               //         {
               //             foreach (DataRow rowsrctp in (InternalDataCollectionBase)dataSet1.Tables["Pago"].Rows)
               //             {
               //                 string Pctabeneficiario = rowsrctp["CtaBeneficiario"].ToString();
               //                 string Prfcemisorctaben = rowsrctp["RfcEmisorCtaBen"].ToString();
               //                 //string Pctaordenante = rowsrctp["CtaOrdenante"].ToString();
               //                 //string Pnombancoordext = rowsrctp["NomBancoOrdExt"].ToString();
               //                 //string Prfcemisorctaord = rowsrctp["RfcEmisorCtaOrd"].ToString();
               //                 Total = rowsrctp["Monto"].ToString();
               //                 monedacpag = rowsrctp["MonedaP"].ToString();
               //                 formadepagocpag = rowsrctp["FormaDePagoP"].ToString();
               //                 fechapago = rowsrctp["FechaPago"].ToString();

               //                 f03 += "CPAG20PAGO"
               //                     + "|" + idcomprobante.Trim()
               //                     + "|" + fechapago.Trim()
               //                     + "|" + formadepagocpag.Trim()
               //                     + "|" + monedacpag.Trim()
               //                     + "|"
               //                     + "|" + Total.Trim()
               //                     + "|"
               //                     + "|"
               //                     + "|"
               //                     + "|"
               //                     + "|"
               //                     + "|"
               //                     + "|"
               //                     + "|"
               //                     + "|"
               //                     + "|"
               //                     + "|";

               //                 int totalmn = 1;
               //                 DataSet dataSet2 = new DataSet();
               //                 int x = 0;
               //                 foreach (DataRow rowsrctpr in (InternalDataCollectionBase)dataSet1.Tables["DoctoRelacionado"].Rows)
               //                 {

               //                     k3 = rowsrctpr["Folio"].ToString();
               //                     string Dserie = rowsrctpr["Serie"].ToString();
               //                     isaldoinsoluto = rowsrctpr["ImpSaldoInsoluto"].ToString();
               //                     ipagado = rowsrctpr["ImpPagado"].ToString();
               //                     interiorsaldoanterior = rowsrctpr["ImpSaldoAnt"].ToString();
               //                     nparcialidades = rowsrctpr["NumParcialidad"].ToString();
               //                     metodopago33 = rowsrctpr["MetodoDePagoDR"].ToString();
               //                     monedascpadgoc = rowsrctpr["MonedaDR"].ToString();
               //                     IdentificadorDelDocumentoPagado = rowsrctpr["IdDocumento"].ToString();

               //                     //int totalr = (int)rowsrctpr["Count"] + 1;
               //                     //int totalr = (rowsrctpr["Count"] as int?) ?? 0;
               //                     int totalr = dataSet1.Tables["DoctoRelacionado"].Select("Folio is not null").Length;
               //                     if (totalmn == totalr)
               //                     {
               //                         if06 = "CPAG20DOCIMPTRA"
               //                         + "|" + idcomprobante.Trim()
               //                         + "|" + k3.Trim()
               //                         + "|" + IdentificadorDelDocumentoPagado.Trim()
               //                         + "|" + "002"
               //                         + "|" + "Tasa"
               //                         + "|" + "0.160000"
               //                         + "|" + totaliva
               //                         //+ "|" + retencion
               //                         + "|" + ipagado.Trim()
               //                         + "|";

               //                     }
               //                     else
               //                     {
               //                         if06 = "CPAG20DOCIMPTRA"
               //                         + "|" + idcomprobante.Trim()
               //                         + "|" + k3.Trim()
               //                         + "|" + IdentificadorDelDocumentoPagado.Trim()
               //                         + "|" + "002"
               //                         + "|" + "Tasa"
               //                         + "|" + "0.160000"
               //                         + "|" + totaliva
               //                         //+ "|" + retencion
               //                         + "|" + ipagado.Trim()
               //                         + "| \r\n";

               //                     }


               //                     if (metodopago33 == "PPD")
               //                     {
               //                         if (monedascpadgoc.Trim() == "USD")
               //                         {
               //                             usdf04 = "04"                                                   //1-Tipo De Registro
               //                                + "|" + consecutivoconcepto.Trim()                       //2-Id Receptor
               //                                + "|" + claveproductoservicio.Trim()                                //3-RFC
               //                                + "|"                          //4-Nombre
               //                                + "|" + cantidad.Trim()                           //5-Pais
               //                                + "|" + claveunidad.Trim()                            //6-Calle
               //                                + "|"                             //7-Numero Exterior
               //                                + "|" + descripcion.Trim()                            //8-Numero Interior
               //                                + "|" + valorunitario.Trim()                         //9-Colonia
               //                                + "|" + importe.Trim()                        //10-Localidad
               //                                + "|"                        //11-Referencia
               //                                + "|"                         //12-Municio/Delegacion
               //                                + "|" + "01"                          //13-EStado
               //                                + "|";

               //                             f04 += "CPAG20DOC"
               //                                  + "|" + idcomprobante.Trim()
               //                                  + "|" + IdentificadorDelDocumentoPagado.Trim()
               //                                  + "|"
               //                                  + "|"
               //                                  + "|" + monedascpadgoc
               //                                  + "|"
               //                                  + "|" + nparcialidades
               //                                  + "|" + interiorsaldoanterior
               //                                  + "|" + ipagado
               //                                  + "|" + isaldoinsoluto
               //                                  + "|" + "02"
               //                                  + "| \r\n";
               //                             f07 = "";
               //                             f08 = "";



               //                         }
               //                         else
               //                         {
               //                             totalIva = (decimal)(ivaa * Convert.ToDouble(ipagado));
               //                             totaliva = totalIva.ToString("F");
               //                             totalIsr = (decimal)(isrr * Convert.ToDouble(ipagado));
               //                             totalisr = totalIsr.ToString("F");

               //                             if05 = "CPAG20DOCIMPRET"
               //                             + "|" + idcomprobante.Trim()
               //                             + "|" + k3.Trim()
               //                             + "|" + IdentificadorDelDocumentoPagado.Trim()
               //                             + "|" + "001"
               //                             + "|" + "Tasa"
               //                             + "|" + "0.040000"
               //                             + "|" + totalisr
               //                             //+ "|" + iva.Trim()
               //                             + "|" + ipagado.Trim()
               //                             + "| \r\n";
               //                             //if06 = "CPAG20DOCIMPTRA"
               //                             //+ "|" + idcomprobante.Trim()
               //                             //+ "|" + k3.Trim()
               //                             //+ "|" + IdentificadorDelDocumentoPagado.Trim()
               //                             //+ "|" + "002"
               //                             //+ "|" + "Tasa"
               //                             //+ "|" + "0.160000"
               //                             //+ "|" + totaliva
               //                             ////+ "|" + retencion
               //                             //+ "|" + ipagado.Trim()
               //                             //+ "| \r\n";
               //                             try
               //                             {
               //                                 importePagos3 = importePagos3 + Convert.ToDecimal(totalisr);
               //                                 TotaldeRe = importePagos3.ToString();

               //                             }
               //                             catch (Exception ex)
               //                             {
               //                                 string errors = ex.Message;
               //                             }

               //                             f07 = "CPAG20IMPRET"
               //                             + "|" + idcomprobante.Trim()
               //                             + "|" + "001"
               //                             + "|" + TotaldeRe
               //                             + "|";

               //                             try
               //                             {
               //                                 importePagos2 = importePagos2 + Convert.ToDecimal(totaliva);
               //                                 TotaldeIva = importePagos2.ToString();
               //                             }
               //                             catch (Exception ex)
               //                             {
               //                                 string errors = ex.Message;
               //                             }

               //                             f08 = "CPAG20IMPTRA"
               //                             + "|" + idcomprobante.Trim()
               //                             + "|" + "002"
               //                             + "|" + "Tasa"
               //                             + "|" + "0.160000"
               //                             + "|" + TotaldeIva
               //                             + "|" + ipagado.Trim()
               //                             + "|";

               //                             usdf04 = "04"                                                   //1-Tipo De Registro
               //                            + "|" + consecutivoconcepto.Trim()                       //2-Id Receptor
               //                            + "|" + claveproductoservicio.Trim()                                //3-RFC
               //                            + "|"                          //4-Nombre
               //                            + "|" + cantidad.Trim()                           //5-Pais
               //                            + "|" + claveunidad.Trim()                            //6-Calle
               //                            + "|"                             //7-Numero Exterior
               //                            + "|" + descripcion.Trim()                            //8-Numero Interior
               //                            + "|" + valorunitario.Trim()                         //9-Colonia
               //                            + "|" + importe.Trim()                        //10-Localidad
               //                            + "|"                        //11-Referencia
               //                            + "|"                         //12-Municio/Delegacion
               //                            + "|" + "02"                          //13-EStado
               //                            + "|";



               //                             f04 += "CPAG20DOC"
               //                          + "|" + idcomprobante.Trim()
               //                          + "|" + IdentificadorDelDocumentoPagado.Trim()
               //                          + "|"
               //                          + "|"
               //                          + "|" + monedascpadgoc
               //                          + "|"
               //                          + "|" + nparcialidades
               //                          + "|" + interiorsaldoanterior
               //                          + "|" + ipagado
               //                          + "|" + isaldoinsoluto
               //                          + "|" + "02"
               //                          + "| \r\n"
               //                          + if05
               //                          + if06;

               //                         }
               //                         totalmn++;








               //                     }


               //                 }
               //             }
               //         }
               //     }
               // }

               // generarTXTTralix();


               //         //string msg = "¡Esta serie TDRC si tiene complemento!";
               
               //         //ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', '', 'success');", true);
               //         //Div1.Visible = false;
                       
               //      }
               //     else
               //     {
               //     string msg = "Este Folio no tiene complemento";
               //     formularioT.Visible = false;
                    
               //     ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', '', 'error');setTimeout(function(){window.location.href ='Listado.aspx'}, 8000)", true);
               //     }
                 

            

            


            

            //AQUI TERMINA TXT PRODUCCIION

        }

        public void generarTXTTralix()
        {
            try
            { //try TLS 1.3
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)12288
                                                     | (SecurityProtocolType)3072
                                                     | (SecurityProtocolType)768
                                                     | SecurityProtocolType.Tls;
            }
            catch (NotSupportedException)
            {
                try
                { //try TLS 1.2
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072
                                                         | (SecurityProtocolType)768
                                                         | SecurityProtocolType.Tls;
                }
                catch (NotSupportedException)
                {
                    try
                    { //try TLS 1.1
                        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768
                                                             | SecurityProtocolType.Tls;
                    }
                    catch (NotSupportedException)
                    { //TLS 1.0
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                    }
                }
            }
            descripcion = validaCampo(descripcion.Trim());

            string path = System.Web.Configuration.WebConfigurationManager.AppSettings["dir"] + foliot + ".txt";
            using (System.IO.StreamWriter escritor = new System.IO.StreamWriter(path))
            {
                //01 INFORMACION GENERAL DEL CFDI (1:1)
                if (formadepagocpag.Trim() != "02")
                {
                    escritor.WriteLine(
                         "01"
                        + "|" + idcomprobante.Trim()
                        + "|" + serie.Trim()
                        + "|" + idcomprobante.Trim()
                        + "|" + fecha.Trim()
                        + "|" + subt.Trim()
                        + "|"
                        + "|"
                        + "|"
                        + "|" + total.Trim()
                        + "|"
                        + "|" + "03"
                        + "|"
                        + "|" + metodopago33
                        + "|" + tmoneda
                        + "|"
                        + "|" + tipocomprobante
                        + "|" + lugarexpedicion
                        + "|" + usocfdi
                        + "|"
                        + "|" + "FAC"
                        + "|"
                        + "|"
                        + "|"
                    );
                    escrituraFactura += "01"
                        + "|" + idcomprobante.Trim()
                        + "|" + serie.Trim()
                        + "|" + idcomprobante.Trim()
                        + "|" + fecha.Trim()
                        + "|" + subt.Trim()
                        + "|"
                        + "|"
                        + "|"
                        + "|" + total.Trim()
                        + "|"
                        + "|" + "03"
                        + "|"
                        + "|" + metodopago33
                        + "|" + tmoneda
                        + "|"
                        + "|" + tipocomprobante
                        + "|" + lugarexpedicion
                        + "|" + usocfdi
                        + "|"
                        + "|" + "FAC"
                        + "|"
                        + "|"
                        + "|";
                }
                else
                {
                    escritor.WriteLine(
                         "01"
                        + "|" + idcomprobante.Trim()
                        + "|" + serie.Trim()
                        + "|" + idcomprobante.Trim()
                        + "|" + fecha.Trim()
                        + "|" + subt.Trim()
                        + "|"
                        + "|"
                        + "|"
                        + "|" + total.Trim()
                        + "|"
                        + "|" + "03"
                        + "|"
                        + "|" + metodopago33
                        + "|" + tmoneda
                        + "|"
                        + "|" + tipocomprobante
                        + "|" + lugarexpedicion
                        + "|" + usocfdi
                        + "|"
                        + "|" + "FAC"
                        + "|"
                        + "|"
                        + "|"
                    );
                    escrituraFactura += "01"
                        + "|" + idcomprobante.Trim()
                        + "|" + serie.Trim()
                        + "|" + idcomprobante.Trim()
                        + "|" + fecha.Trim()
                        + "|" + subt.Trim()
                        + "|"
                        + "|"
                        + "|"
                        + "|" + total.Trim()
                        + "|"
                        + "|" + "03"
                        + "|"
                        + "|" + metodopago33
                        + "|" + tmoneda
                        + "|"
                        + "|" + tipocomprobante
                        + "|" + lugarexpedicion
                        + "|" + usocfdi
                        + "|"
                        + "|" + "FAC"
                        + "|"
                        + "|"
                        + "|";
                }
                if (monedascpadgoc.Trim() == "USD")
                {
                    escritor.WriteLine(
                    "02"                                                   //1-Tipo De Registro
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
                   + "|"
                   );
                    escrituraFactura +=
                        "02"                                                   //1-Tipo De Registro
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
                   + "|";
                }
                else
                {
                    escritor.WriteLine(
                    "02"                                                   //1-Tipo De Registro
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
                   + "|"
                   );
                    escrituraFactura +=
                        "02"                                                   //1-Tipo De Registro
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
                   + "|";
                }
                escritor.WriteLine(usdf04);
                escrituraFactura += usdf04;
                escritor.WriteLine(
                    "CPAG20"
                    + "|" + "2.0"
                    + "|"
                    );
                escrituraFactura +=
                    "CPAG20"
                    + "|" + "2.0"
                    + "|";
                escritor.WriteLine(
                    "CPAG20TOT"                         //1-Tipo De Registro
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
                    + "|"
                    );
                escrituraFactura +=
                    "CPAG20TOT"                         //1-Tipo De Registro
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
                    + "|";
                escritor.WriteLine(f03);
                escrituraFactura += f03;
                escritor.WriteLine(f04);
                escrituraFactura += f04.TrimEnd();
                escrituraFactura = escrituraFactura.Replace("| \r\n", "|");
                escritor.WriteLine(f07);
                escrituraFactura += f07;
                escritor.WriteLine(f08);
                escrituraFactura += f08;

            }

        }
       
        public void generaTXT()
        {
            try
            { //try TLS 1.3
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)12288
                                                     | (SecurityProtocolType)3072
                                                     | (SecurityProtocolType)768
                                                     | SecurityProtocolType.Tls;
            }
            catch (NotSupportedException)
            {
                try
                { //try TLS 1.2
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072
                                                         | (SecurityProtocolType)768
                                                         | SecurityProtocolType.Tls;
                }
                catch (NotSupportedException)
                {
                    try
                    { //try TLS 1.1
                        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768
                                                             | SecurityProtocolType.Tls;
                    }
                    catch (NotSupportedException)
                    { //TLS 1.0
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                    }
                }
            }
            txtConcepto.Text = validaCampo(txtConcepto.Text.Trim());

            string path = System.Web.Configuration.WebConfigurationManager.AppSettings["dir"] + lblFact.Text + ".txt";
            using (System.IO.StreamWriter escritor = new System.IO.StreamWriter(path))



            {
                string f1 = txtFechaDesde.Text;
                string factura = txtFolio.Text;


                //----------------------------------------Seccion De Datos Generales del CFDI-----------------------------------------------------------------------------------------

                //01 INFORMACION GENERAL DEL CFDI (1:1)
                if (formadepago.Trim() != "02")
                {
                    escritor.WriteLine(
                    "01"                                                //1.-Tipo De Registro
                    + "|" + sfolio                                      //2-ID Comprobante
                    + "|" + seriee                                      //3-Serie
                    + "|" + folioe                                      //4-Foliio 
                    + "|" + txtFechaFactura.Text.Trim()                 //5-Fecha y Hora De Emision
                    + "|" + subt                                        //6-Subtotal
                    + "|" + ivat                                        //7-Total Impuestos Trasladados
                    + "|" + rett                                        //8-Total Impuestos Retenidos
                    + "|"                                               //9-Descuentos
                    + "|" + total                                       //10-Total
                    + "|" + cantidadletra.Trim()                        //11-Total Con Letra
                    + "|"                         //12-Forma De Pago
                    + "|" + cond                                        //13-Condiciones De Pago
                    + "|" + metodopago33                                //14-Metodo de Pago
                    + "|" + txtMoneda.Text.Trim()                       //15-Moneda
                    + "|" + tipoc                                       //16-Tipo De Cambio
                    + "|" + tipocomprobante                             //17-Tipo De Comprobante
                    + "|" + lugarexpedicion                             //18-Lugar De Expedicion                                        
                    + "|" + usocfdi                                     //19-Uso CFDI
                    + "|" + confirmacion                                //20-Confirmacion
                    + "|"
                    );
                    escrituraFactura += "01"                                                //1.-Tipo De Registro
                    + "|" + sfolio                                      //2-ID Comprobante
                    + "|" + seriee                                      //3-Serie
                    + "|" + folioe                                      //4-Foliio 
                    + "|" + txtFechaFactura.Text.Trim()                 //5-Fecha y Hora De Emision
                    + "|" + subt                                        //6-Subtotal
                    + "|" + ivat                                        //7-Total Impuestos Trasladados
                    + "|" + rett                                        //8-Total Impuestos Retenidos
                    + "|"                                               //9-Descuentos
                    + "|" + total                                       //10-Total
                    + "|" + cantidadletra.Trim()                        //11-Total Con Letra
                    + "|"                        //12-Forma De Pago
                    + "|" + cond                                        //13-Condiciones De Pago
                    + "|" + metodopago33                                //14-Metodo de Pago
                    + "|" + txtMoneda.Text.Trim()                       //15-Moneda
                    + "|" + tipoc                                       //16-Tipo De Cambio
                    + "|" + tipocomprobante                             //17-Tipo De Comprobante
                    + "|" + lugarexpedicion                             //18-Lugar De Expedicion                                        
                    + "|" + usocfdi                                     //19-Uso CFDI
                    + "|" + confirmacion                                //20-Confirmacion
                    + "|";
                }
                else
                {
                    escritor.WriteLine(
                    "01"                                                //1.-Tipo De Registro
                    + "|" + sfolio                                      //2-ID Comprobante
                    + "|" + seriee                                      //3-Serie
                    + "|" + folioe                                      //4-Foliio 
                    + "|" + txtFechaFactura.Text.Trim()                 //5-Fecha y Hora De Emision
                    + "|" + subt                                        //6-Subtotal
                    + "|" + ivat                                        //7-Total Impuestos Trasladados
                    + "|" + rett                                        //8-Total Impuestos Retenidos
                    + "|"                                               //9-Descuentos
                    + "|" + total                                       //10-Total
                    + "|" + cantidadletra.Trim()                        //11-Total Con Letra
                    + "|"                      //12-Forma De Pago
                    + "|" + cond                                        //13-Condiciones De Pago
                    + "|" + metodopago33                                //14-Metodo de Pago
                    + "|" + txtMoneda.Text.Trim()                       //15-Moneda
                    + "|" + tipoc                                       //16-Tipo De Cambio
                    + "|" + tipocomprobante                             //17-Tipo De Comprobante
                    + "|" + lugarexpedicion                             //18-Lugar De Expedicion                                        
                    + "|" + usocfdi                                     //19-Uso CFDI
                    + "|" + confirmacion                                //20-Confirmacion
                    + "|"
                    );
                    escrituraFactura += "01"                                                //1.-Tipo De Registro
                   + "|" + sfolio                                      //2-ID Comprobante
                   + "|" + seriee                                      //3-Serie
                   + "|" + folioe                                      //4-Foliio 
                   + "|" + txtFechaFactura.Text.Trim()                 //5-Fecha y Hora De Emision
                   + "|" + subt                                        //6-Subtotal
                   + "|" + ivat                                        //7-Total Impuestos Trasladados
                   + "|" + rett                                        //8-Total Impuestos Retenidos
                   + "|"                                               //9-Descuentos
                   + "|" + total                                       //10-Total
                   + "|" + cantidadletra.Trim()                        //11-Total Con Letra
                   + "|"                          //12-Forma De Pago
                   + "|" + cond                                        //13-Condiciones De Pago
                   + "|" + metodopago33                                //14-Metodo de Pago
                   + "|" + txtMoneda.Text.Trim()                       //15-Moneda
                   + "|" + tipoc                                       //16-Tipo De Cambio
                   + "|" + tipocomprobante                             //17-Tipo De Comprobante
                   + "|" + lugarexpedicion                             //18-Lugar De Expedicion                                        
                   + "|" + usocfdi                                     //19-Uso CFDI
                   + "|" + confirmacion                                //20-Confirmacion
                   + "|";
                }
                //----------------------------------------Seccion de los datos del receptor del CFDI -------------------------------------------------------------------------------------

                //02 INFORMACION DEL RECEPTOR (1:1)
                if (txtMoneda.Text.Trim() == "USD")
                {
                    escritor.WriteLine(
                       "02"                                                   //1-Tipo De Registro
                       + "|" + txtIdCliente.Text.Trim()                       //2-Id Receptor
                       + "|" + "XEXX010101000"                                //3-RFC
                       + "|" + txtCliente.Text.Trim()                         //4-Nombre
                       + "|" + txtPaís.Text.Trim()                            //5-Pais
                       + "|" + txtCalle.Text.Trim()                           //6-Calle
                       + "|" + txtNoExt.Text.Trim()                           //7-Numero Exterior
                       + "|" + txtNoInt.Text.Trim()                           //8-Numero Interior
                       + "|" + txtColonia.Text.Trim()                         //9-Colonia
                       + "|" + txtLocalidad.Text.Trim()                       //10-Localidad
                       + "|" + txtReferencia.Text.Trim()                      //11-Referencia
                       + "|" + txtMunicipio.Text.Trim()                       //12-Municio/Delegacion
                       + "|" + txtEstado.Text.Trim()                          //13-EStado
                       + "|" + txtCP.Text.Trim()                              //14-Codigo Postal
                       + "|" + "USA" // paisresidencia                                 //15-Pais de Residecia Fiscal Cuando La Empresa Sea Extrajera
                       + "|" + txtRFC.Text.Trim()                                  //16-Numero de Registro de ID Tributacion 
                       + "|" + mailenvio                                      //17-Correo de envio                                                    
                       + "|"                                                  //Fin Del Registro 
                       );

                    escrituraFactura += "\\n02"                                                   //1-Tipo De Registro
                       + "|" + txtIdCliente.Text.Trim()                       //2-Id Receptor
                       + "|" + "XEXX010101000"                                //3-RFC
                       + "|" + txtCliente.Text.Trim()                         //4-Nombre
                       + "|" + txtPaís.Text.Trim()                            //5-Pais
                       + "|" + txtCalle.Text.Trim()                           //6-Calle
                       + "|" + txtNoExt.Text.Trim()                           //7-Numero Exterior
                       + "|" + txtNoInt.Text.Trim()                           //8-Numero Interior
                       + "|" + txtColonia.Text.Trim()                         //9-Colonia
                       + "|" + txtLocalidad.Text.Trim()                       //10-Localidad
                       + "|" + txtReferencia.Text.Trim()                      //11-Referencia
                       + "|" + txtMunicipio.Text.Trim()                       //12-Municio/Delegacion
                       + "|" + txtEstado.Text.Trim()                          //13-EStado
                       + "|" + txtCP.Text.Trim()                              //14-Codigo Postal
                       + "|" + "USA" // paisresidencia                                 //15-Pais de Residecia Fiscal Cuando La Empresa Sea Extrajera
                       + "|" + txtRFC.Text.Trim()                                  //16-Numero de Registro de ID Tributacion 
                       + "|" + mailenvio                                      //17-Correo de envio                                                    
                       + "|";

                }
                else
                {

                    escritor.WriteLine(
                    "02"                                                   //1-Tipo De Registro
                    + "|" + txtIdCliente.Text.Trim()                       //2-Id Receptor
                    + "|" + txtRFC.Text.Trim()                             //3-RFC
                    + "|" + txtCliente.Text.Trim()                         //4-Nombre
                    + "|" + txtPaís.Text.Trim()                            //5-Pais
                    + "|" + txtCalle.Text.Trim()                           //6-Calle
                    + "|" + txtNoExt.Text.Trim()                           //7-Numero Exterior
                    + "|" + txtNoInt.Text.Trim()                           //8-Numero Interior
                    + "|" + txtColonia.Text.Trim()                         //9-Colonia
                    + "|" + txtLocalidad.Text.Trim()                       //10-Localidad
                    + "|" + txtReferencia.Text.Trim()                      //11-Referencia
                    + "|" + txtMunicipio.Text.Trim()                       //12-Municio/Delegacion
                    + "|" + txtEstado.Text.Trim()                          //13-EStado
                    + "|" + txtCP.Text.Trim()                              //14-Codigo Postal
                    + "|" + "" // paisresidencia                                 //15-Pais de Residecia Fiscal Cuando La Empresa Sea Extrajera
                    + "|" + numtributacion                                 //16-Numero de Registro de ID Tributacion 
                    + "|" + mailenvio                                      //17-Correo de envio                                                    
                    + "|"                                                  //Fin Del Registro 
                    );

                    escrituraFactura += "\\n02"                                                   //1-Tipo De Registro
                    + "|" + txtIdCliente.Text.Trim()                       //2-Id Receptor
                    + "|" + txtRFC.Text.Trim()                             //3-RFC
                    + "|" + txtCliente.Text.Trim()                         //4-Nombre
                    + "|" + txtPaís.Text.Trim()                            //5-Pais
                    + "|" + txtCalle.Text.Trim()                           //6-Calle
                    + "|" + txtNoExt.Text.Trim()                           //7-Numero Exterior
                    + "|" + txtNoInt.Text.Trim()                           //8-Numero Interior
                    + "|" + txtColonia.Text.Trim()                         //9-Colonia
                    + "|" + txtLocalidad.Text.Trim()                       //10-Localidad
                    + "|" + txtReferencia.Text.Trim()                      //11-Referencia
                    + "|" + txtMunicipio.Text.Trim()                       //12-Municio/Delegacion
                    + "|" + txtEstado.Text.Trim()                          //13-EStado
                    + "|" + txtCP.Text.Trim()                              //14-Codigo Postal
                    + "|" + "" // paisresidencia                                 //15-Pais de Residecia Fiscal Cuando La Empresa Sea Extrajera
                    + "|" + numtributacion                                 //16-Numero de Registro de ID Tributacion 
                    + "|" + mailenvio                                      //17-Correo de envio                                                    
                    + "|";

                }

                //----------------------------------------Seccion de detalles del complemento de pago -------------------------------------------------------------------

                //04 INFORMACION DE LOS CONCEPTOS (1:N)
                escritor.WriteLine(
               "04"                                                   //1-Tipo De Registro
               + "|" + consecutivoconcepto                            //2-Consecutivo Concepto
               + "|" + claveproductoservicio                          //3-Clave Producto o Servicio SAT                                               
               + "|" + numidentificacion                              //4-Numero Identificacion TDR
               + "|" + txtCantidad.Text.Trim()                        //5-Cantidad
               + "|" + claveunidad                                    //6-Clave Unidad SAT
               + "|"                                                  //7-Unidad de Medida
               + "|" + concepto.Trim()                                //8-Descripcion
               + "|" + valorunitario                                  //9-Valor Unitario
               + "|" + importe                                        //10-Importe
               + "|" + descuento                                      //11-Descuento                                                  
                                                                      //12 Importe con iva si el rfc es XAXX010101000 y XEXX010101000 OPCIONAL
               + "|"                                                  //Fin Del Registro
                );

                escrituraFactura += "\\n04"                                                   //1-Tipo De Registro
               + "|" + consecutivoconcepto                            //2-Consecutivo Concepto
               + "|" + claveproductoservicio                          //3-Clave Producto o Servicio SAT                                               
               + "|" + numidentificacion                              //4-Numero Identificacion TDR
               + "|" + txtCantidad.Text.Trim()                        //5-Cantidad
               + "|" + claveunidad                                    //6-Clave Unidad SAT
               + "|"                                                  //7-Unidad de Medida
               + "|" + concepto.Trim()                                //8-Descripcion
               + "|" + valorunitario                                  //9-Valor Unitario
               + "|" + importe                                        //10-Importe
               + "|" + descuento                                      //11-Descuento                                                  
                                                                      //12 Importe con iva si el rfc es XAXX010101000 y XEXX010101000 OPCIONAL
               + "|";

                //----------------------------------------Seccion CPAG20 -------------------------------------------------------------------

                //CPAG20 (1:1)
                //escritor.WriteLine(
                //"CPAG20"                         //1-Tipo De Registro
                //+ "|" + "2.0"                    //2-Version
                //+ "|"                               //Fin Del Registro
                //);

                //escrituraFactura += "CPAG20"    //1-Tipo De Registro
                //+ "|"  + "2.0"                   //2-Version  
                //+ "|";		   

                //----------------------------------------Seccion CPAG20TOT -------------------------------------------------------------------

                //CPAG20TOT (1:1)
                //escritor.WriteLine(
                //"CPAG20TOT"                         //1-Tipo De Registro
                //+ "|"                               //2-TotalRetencionesIVA
                //+ "|"                               //3-TotalRetencionesISR                                              
                //+ "|"                               //4-TotalRetencionesIEPS
                //+ "|"                               //5-TotalTrasladosBaseIVA16
                //+ "|"                               //6-TotalTrasladosImpuestoIVA16
                //+ "|"                               //7-TotalTrasladosBaseIVA8
                //+ "|"                               //8-TotalTrasladosImpuestoIVA8
                //+ "|"                               //9-TotalTrasladosBaseIVA0
                //+ "|"                               //10-TotalTrasladosImpuestoIVA0
                //+ "|"                               //11-TotalTrasladosBaseIVAExento
                // + "|" + monto                       //12-MontoTotalPagos                                                                                                 
                // + "|"                               //Fin Del Registro
                //);

                //escrituraFactura += "CPAG20TOT"    //1-Tipo De Registro
                //+ "|"                               //2-TotalRetencionesIVA
                //+ "|"                               //3-TotalRetencionesISR                                              
                //+ "|"                               //4-TotalRetencionesIEPS
                //+ "|"                               //5-TotalTrasladosBaseIVA16
                //+ "|"                               //6-TotalTrasladosImpuestoIVA16
                //+ "|"                               //7-TotalTrasladosBaseIVA8
                //+ "|"                               //8-TotalTrasladosImpuestoIVA8
                //+ "|"                               //9-TotalTrasladosBaseIVA0
                //+ "|"                               //10-TotalTrasladosImpuestoIVA0
                //+ "|"                               //11-TotalTrasladosBaseIVAExento
                //+ "|" + monto                       //12-MontoTotalPagos  
                //+ "|";		   
                //----------------------------------------Seccion CPAG20PAGO-------------------------------------------------------------------------------------------------

                //CPAG20PAGO COMPLEMENTO DE PAGO (1:N)
                //escritor.WriteLine(
                //"CPAG20PAGO"                                           //1-Tipo De Registro
                //+ "|" + identificador                                  //2-Identificador                                             
                //+ "|" + fechapago                                      //3-Fechapago
                //+ "|" + txtFormaPago.Text                              //4-Formadepagocpag
                //+ "|" + monedacpag                                     //5-Monedacpag
                //+ "|" + tipodecambiocpag                               //6-TipoDecambiocpag
                //+ "|" + txtTotal.Text                                  //8-Monto
                //+ "|" + numerooperacion                                //9-NumeroOperacion
                //+ "|" + txtRFCbancoEmisor.Text                         //10-RFCEmisorCuentaBeneficiario
                //+ "|" + txtBancoEmisor.Text                            //11-NombreDelBanco                                                                                            
                //+ "|" + txtCuentaPago.Text                             //12-NumeroCuentaOrdenante
                //+ "|" + rfcemisorcuentaben                             //13-RFCEmisorCuentaBeneficiario
                //+ "|" + numcuentaben                                   //14-NumCuentaBeneficiario
                //+ "|" + tipocadenapago                                 //15-TipoCadenaPago                                               
                //+ "|" + certpago                                       //16-CertificadoPago
                //+ "|" + cadenadelpago                                  //17-CadenaDePago
                //+ "|" + sellodelpago                                   //Fin Del Registro
                //+ "|"
                //);

                //escrituraFactura += "CPAG20PAGO"                      //1-Tipo De Registro
                //+ "|" + identificador                                  //2-Identificador                                             
                //+ "|" + fechapago                                      //3-Fechapago
                //+ "|" + txtFormaPago.Text                              //4-Formadepagocpag
                //+ "|" + monedacpag                                     //5-Monedacpag
                //+ "|" + tipodecambiocpag                               //6-TipoDecambiocpag
                //+ "|" + txtTotal.Text                                  //8-Monto
                //+ "|" + numerooperacion                                //9-NumeroOperacion
                //+ "|" + txtRFCbancoEmisor.Text                         //10-RFCEmisorCuentaBeneficiario
                //+ "|" + txtBancoEmisor.Text                            //11-NombreDelBanco                                                                                            
                //+ "|" + txtCuentaPago.Text                             //12-NumeroCuentaOrdenante
                //+ "|" + rfcemisorcuentaben                             //13-RFCEmisorCuentaBeneficiario
                //+ "|" + numcuentaben                                   //14-NumCuentaBeneficiario
                //+ "|" + tipocadenapago                                 //15-TipoCadenaPago                                               
                //+ "|" + certpago                                       //16-CertificadoPago
                //+ "|" + cadenadelpago                                  //17-CadenaDePago
                //+ "|" + sellodelpago                                   //Fin Del Registro
                //+ "|";


                //----------------------------------------Seccion CPAG-------------------------------------------------------------------------------------------------

                //CPAG COMPLEMENTO DE PAGO (1:N)
                escritor.WriteLine(
               "CPAG"                                                 //1-Tipo De Registro
               + "|" + identificador                                  //2-Identificador
               + "|" + version                                        //3-Version                                             
               + "|" + fechapago                                      //4-Fechapago
               + "|" + txtFormaPago.Text                              //5-Formadepagocpag
               + "|" + monedacpag                                     //6-Monedacpag
               + "|" + tipodecambiocpag                               //7-TipoDecambiocpag
               + "|" + txtTotal.Text                                  //8-Monto
               + "|" + numerooperacion                                //9-NumeroOperacion
               + "|" + txtRFCbancoEmisor.Text                         //10-RFCEmisorCuentaBeneficiario
               + "|" + txtBancoEmisor.Text                            //11-NombreDelBanco                                                                                            
               + "|" + txtCuentaPago.Text                             //12-NumeroCuentaOrdenante
               + "|" + rfcemisorcuentaben                             //13-RFCEmisorCuentaBeneficiario
               + "|" + numcuentaben                                   //14-NumCuentaBeneficiario
               + "|" + tipocadenapago                                 //15-TipoCadenaPago                                               
               + "|" + certpago                                       //16-CertificadoPago
               + "|" + cadenadelpago                                  //17-CadenaDePago
               + "|" + sellodelpago                                   //Fin Del Registro
               + "|"
                );

                escrituraFactura += "CPAG"                                                 //1-Tipo De Registro
               + "|" + identificador                                  //2-Identificador
               + "|" + version                                        //3-Version                                             
               + "|" + fechapago                                      //4-Fechapago
               + "|" + txtFormaPago.Text                              //5-Formadepagocpag
               + "|" + monedacpag                                     //6-Monedacpag
               + "|" + tipodecambiocpag                               //7-TipoDecambiocpag
               + "|" + txtTotal.Text                                  //8-Monto
               + "|" + numerooperacion                                //9-NumeroOperacion
               + "|" + txtRFCbancoEmisor.Text                         //10-RFCEmisorCuentaBeneficiario
               + "|" + txtBancoEmisor.Text                            //11-NombreDelBanco                                                                                            
               + "|" + txtCuentaPago.Text                             //12-NumeroCuentaOrdenante
               + "|" + rfcemisorcuentaben                             //13-RFCEmisorCuentaBeneficiario
               + "|" + numcuentaben                                   //14-NumCuentaBeneficiario
               + "|" + tipocadenapago                                 //15-TipoCadenaPago                                               
               + "|" + certpago                                       //16-CertificadoPago
               + "|" + cadenadelpago                                  //17-CadenaDePago
               + "|" + sellodelpago                                   //Fin Del Registro
               + "|";

                //----------------------------------------Seccion CPAGDOC------------------------------------------------------------------------------------------------

                //CPAG COMPLEMENTO DE PAGO (1:N)
                escritor.WriteLine(cpagdoc
                //"CPAGDOC"                                              //1-Tipo De Registro
                //+ "|" + identpag                                       //2-IdentificadorDelPago
                //+ "|" + txtFechaIniOP.Text                             //3-IdentificadorDelDocumentoPagado                                              
                //+ "|" + seriecpag                                      //4-Seriecpag
                //+ "|" + foliocpag                                      //5-Foliocpag
                //+ "|" + monedacpagdoc                                  //6-Monedacpag
                //+ "|" + tipocambiocpag                                 //7-TipoCambiocpagdpc
                //+ "|" + txtMetodoPago.Text                             //8-MetodoDePago
                //+ "|" + numerodeparcialidad                            //9-NumeroDeParcialidad
                //+ "|" + importeSaldoAnterior                           //10-ImporteSaldoAnterior
                //+ "|" + importepago                                    //11-ImportePagado                                                  
                //+ "|" + importesaldoinsoluto                           //12 ImporteSaldoInsoluto
                //+ "|"                                                  //Fin Del Registro
                );


                escrituraFactura += cpagdoc;
                //escrituraFactura = escrituraFactura.Replace("||02|", "||\\n02|");
                //escrituraFactura = escrituraFactura.Replace("||04|", "||\\n04|");
                escrituraFactura = escrituraFactura.Replace("| \r\n", "|");
                escrituraFactura = escrituraFactura.Replace("|CPAG", "|\\nCPAG");

            }
        }

        //Valida que el campo no contenga caracteres como el char 13,10,9
        public string validaCampo(string campo)
        {
            char salto = (char)10;
            char tab = (char)9;
            char carrier = (char)13;
            char espacio = (char)32;
            campo = campo.Replace(salto, espacio);
            campo = campo.Replace(tab, espacio);
            campo = campo.Replace(carrier, espacio);
            return campo;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            { //try TLS 1.3
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)12288
                                                     | (SecurityProtocolType)3072
                                                     | (SecurityProtocolType)768
                                                     | SecurityProtocolType.Tls;
            }
            catch (NotSupportedException)
            {
                try
                { //try TLS 1.2
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072
                                                         | (SecurityProtocolType)768
                                                         | SecurityProtocolType.Tls;
                }
                catch (NotSupportedException)
                {
                    try
                    { //try TLS 1.1
                        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768
                                                             | SecurityProtocolType.Tls;
                    }
                    catch (NotSupportedException)
                    { //TLS 1.0
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                    }
                }
            }
            bool readOnly = false;
            string stilo = "editTextBox";
            string textoBoton = "Editar";
            bool visible = false;
            if (btnEdit.Text.Equals("Editar"))
            {
                readOnly = false;
                visible = true;
                stilo = "editTextBox";
                textoBoton = "Guardar";
            }
            else
            {
                string fecha1 = "", fecha2 = "";

                bool vacio = false;

                System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
                dateInfo.ShortDatePattern = "dd/MM/yyyy";

                if (!txtFechaDesde.Text.Equals("")) { fecha1 = txtFechaDesde.Text; imgFDesde.Visible = false; }
                else { error = true; imgFDesde.Visible = true; imgFDesde.ToolTip = "La fecha no puede estar vacía"; vacio = true; }

                if (!txtFechaHasta.Text.Equals("")) { fecha2 = txtFechaHasta.Text; imgFHasta.Visible = false; }
                else { error = true; imgFHasta.Visible = true; imgFHasta.ToolTip = "La feche no puede estar vacía"; vacio = true; }

                if (fecha1.CompareTo(fecha2) == 1 && vacio == false)
                {
                    error = true;
                    imgFDesde.Visible = true;
                    imgFDesde.ToolTip = "La Fecha inicial es mayor que la final";
                }
                if (!error)
                {
                    readOnly = true;
                    visible = false;
                    stilo = "readOnlyTextBox";
                    textoBoton = "Editar";
                }
                else
                {
                    readOnly = false;
                    visible = true;
                    stilo = "editTextBox";
                    textoBoton = "Guardar";
                }
            }
            //Se habilita el campo
            txtFechaDesde.ReadOnly = readOnly;
            txtFechaHasta.ReadOnly = readOnly;
            txtConcepto.ReadOnly = readOnly;
            txtTipoCobro.ReadOnly = readOnly;
            txtFormaPago.ReadOnly = readOnly;


            //Se elimina el estilo
            txtFechaDesde.CssClass = stilo;
            txtFechaHasta.CssClass = stilo;
            txtFechaDesde.CssClass = stilo;
            txtConcepto.CssClass = stilo;
            txtTipoCobro.CssClass = stilo;

            btnEdit.Text = textoBoton;

        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            { //try TLS 1.3
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)12288
                                                     | (SecurityProtocolType)3072
                                                     | (SecurityProtocolType)768
                                                     | SecurityProtocolType.Tls;
            }
            catch (NotSupportedException)
            {
                try
                { //try TLS 1.2
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072
                                                         | (SecurityProtocolType)768
                                                         | SecurityProtocolType.Tls;
                }
                catch (NotSupportedException)
                {
                    try
                    { //try TLS 1.1
                        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768
                                                             | SecurityProtocolType.Tls;
                    }
                    catch (NotSupportedException)
                    { //TLS 1.0
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                    }
                }
            }
            bool campoIncorrecto = false;
            if (txtCliente.Text.Equals(""))
            {
                imgCliente.Visible = true;
                imgCliente.ToolTip = "El cliente no está capturado";
                campoIncorrecto = true;
            }

            if (txtCalle.Text.Equals(""))
            {
                imgCalle.Visible = true;
                imgCalle.ToolTip = "La calle no está capturada";
                campoIncorrecto = true;
            }

            if (txtNoExt.Equals(""))
            {
                imgNoExt.Visible = true;
                imgNoExt.ToolTip = "El No. Ext. no está capturado";
                campoIncorrecto = true;
            }

            // if (noInt.Equals (""))
            //{
            //    imgDir.Visible = true;
            //    imgDir.ToolTip = "El No. Int. no esta capturado";
            //    campoIncorrecto = true;
            //}


            if (txtColonia.Text.Equals(""))
            {
                imgColonia.Visible = true;
                imgColonia.ToolTip = "La colonia no está capturada";
                campoIncorrecto = true;
            }

            if (txtMunicipio.Text.Equals(""))
            {
                imgMunicipio.Visible = true;
                imgMunicipio.ToolTip = "El municipio no está capturado";
                campoIncorrecto = true;
            }

            if (txtEstado.Text.Equals(""))
            {
                imgEstado.Visible = true;
                imgEstado.ToolTip = "El Estado. no está capturado";
                campoIncorrecto = true;
            }

            if (txtPaís.Text.Equals(""))
            {
                imgPais.Visible = true;
                imgPais.ToolTip = "El País no está capturado";
                campoIncorrecto = true;
            }

            if (txtCP.Text.Equals(""))
            {
                imgCP.Visible = true;
                imgCP.ToolTip = "El CP. no está capturado";
                campoIncorrecto = true;
            }


            if (!campoIncorrecto)
            {
                //Se elimina el estilo
                txtFechaDesde.CssClass = "readOnlyTextBox";
                txtFechaHasta.CssClass = "readOnlyTextBox";
                txtFechaDesde.CssClass = "readOnlyTextBox";
                txtConcepto.CssClass = "readOnlyTextBox";
                txtTipoCobro.CssClass = "readOnlyTextBox";

                generaTXT();


                //Traer variable generada por txt
                //Traer vairable idSucursal y idTipoFact
                //Traer lbltex´+.txt
                //Etiqueta archivo fuente
                //Crear el JSON
                //Insertar código petición 
                var request_ = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/tiposCfds");
                var response_ = (HttpWebResponse)request_.GetResponse();
                var responseString_ = new StreamReader(response_.GetResponseStream()).ReadToEnd();

                string[] separadas_ = responseString_.Split('}');



                foreach (string dato in separadas_)
                {
                    if (dato.Contains("TDRC"))
                    {
                        string[] separadasSucursal_ = dato.Split(',');
                        foreach (string datoSuc in separadasSucursal_)
                        {
                            if (datoSuc.Contains("idSucursal"))
                            {
                                idSucursal = datoSuc.Replace(dato.Substring(0, 8), "").Replace("\"", "").Split(':')[1];
                            }
                            if (datoSuc.Contains("id") && !datoSuc.Contains("idSucursal"))
                            {
                                idTipoFactura = datoSuc.Replace(dato.Substring(0, 8), "").Replace("\"", "").Split(':')[1];

                            }
                        }
                    }
                }

                jsonFactura = "{\r\n\r\n  \"idTipoCfd\":" + "\"" + idTipoFactura + "\"";
                jsonFactura += ",\r\n\r\n  \"nombre\":" + "\"" + lblFact.Text + ".txt" + "\"";
                jsonFactura += ",\r\n\r\n  \"idSucursal\":" + "\"" + idSucursal + "\"";
                jsonFactura += ", \r\n\r\n  \"archivoFuente\":" + "\"" + escrituraFactura + "\"" + "\r\n\r\n}";

                string folioFactura = "", serieFactura = "", uuidFactura = "", pdf_xml_descargaFactura = "", pdf_descargaFactura = "", xlm_descargaFactura = "", cancelFactura = "", error = "";
                string salida = "";

                try
                {
                    var client = new RestClient("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis");
                    var request = new RestRequest(Method.PUT);

                    request.AddHeader("cache-control", "no-cache");

                    request.AddHeader("content-length", "834");
                    request.AddHeader("accept-encoding", "gzip, deflate");
                    request.AddHeader("Host", "canal1.xsa.com.mx:9050");
                    request.AddHeader("Postman-Token", "b6b7d8eb-29f2-420f-8d70-7775701ec765,a4b60b83-429b-4188-98d4-7983acc6742e");
                    request.AddHeader("Cache-Control", "no-cache");
                    request.AddHeader("Accept", "*/*");
                    request.AddHeader("User-Agent", "PostmanRuntime/7.13.0");

                    request.AddParameter("application/json", jsonFactura, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                    string[] separadaFactura = response.Content.ToString().Split(',');
                    foreach (string factura in separadaFactura)
                    {
                        if (factura.Contains("errors"))
                        {
                            error += factura.Replace(factura.Substring(0, 6), "").Replace("\"", "").Split('[')[1] + "\n";
                            error = error.Replace("\\n", "").Replace("]}", "");
                            salida = "FALLA AL SUBIR";
                        }
                        else
                        {
                            if (factura.Contains("folio"))
                            {
                                folioFactura = factura.Replace(factura.Substring(0, 5), "").Replace("\"", "").Split(':')[1];
                            }

                            if (factura.Contains("serie"))
                            {
                                serieFactura = factura.Replace(factura.Substring(0, 5), "").Replace("\"", "").Split(':')[1];
                            }

                            if (factura.Contains("uuid"))
                            {
                                uuidFactura = factura.Replace(factura.Substring(0, 4), "").Replace("\"", "").Split(':')[1];
                            }

                            if (factura.Contains("pdfAndXmlDownload"))
                            {
                                pdf_xml_descargaFactura = factura.Replace(factura.Substring(0, 17), "").Replace("\"", "").Split(':')[1];
                            }

                            if (factura.Contains("pdfDownload"))
                            {
                                pdf_descargaFactura = factura.Replace(factura.Substring(0, 11), "").Replace("\"", "").Split(':')[1];
                            }

                            if (factura.Contains("xmlDownload") && !factura.Contains("pdfAndXmlDownload"))
                            {
                                xlm_descargaFactura = factura.Replace(factura.Substring(0, 11), "").Replace("\"", "").Split(':')[1];
                            }

                            if (factura.Contains("cancellCfdi"))
                            {
                                cancelFactura = factura.Replace(factura.Substring(0, 11), "").Replace("\"", "").Split(':')[1];
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    string errors = ex.Message;
                }


                string path = System.Web.Configuration.WebConfigurationManager.AppSettings["dir"] + lblFact.Text + ".txt";

                //UploadFile file = new UploadFile();
                string ftp = System.Web.Configuration.WebConfigurationManager.AppSettings["ftp"];
                if (ftp.Equals("Si"))
                {
                    //File.prubeftp(lblFact.Text + ".txt", path, serie);
                    
                }
                if (salida != "FALLA AL SUBIR")
                {
                    string activa = System.Web.Configuration.WebConfigurationManager.AppSettings["activa"];
                    if (activa.Equals("Si"))
                    {

                        facLabControler.insertaFactura(txtFolio.Text, txtFechaFactura.Text);
                    }

                    string msg = "¡Se ha generado correctamente el CFDi!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Carga exitosa', 'success');", true);
                }
                else
                {
                    string msg = "¡Error al conectar al servicio XSA!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Error', 'error');", true);
                    
                }
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            


                string msg = "¡Se ha generado correctamente el CFDi!";
                ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Carga exitosa', 'success');", true);

            



        }

    }
}
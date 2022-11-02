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
using System.Threading.Tasks;
using System.Web.UI;
using System.Xml;

namespace CARGAR_EXCEL
{
    public partial class DetallesComplemento : System.Web.UI.Page
    {
        public facLabController facLabControler = new facLabController();
        //public FacCpController facLabControler = new FacCpController();

        public string fDesde, fHasta, concepto, tipoCobro, tipocomprobante, lugarexpedicion, metodopago33, formadepago, usocfdi, confirmacion, paisresidencia, numtributacion
        , mailenvio, numidentificacion, claveunidad, tipofactoriva, tipofactorret, coditrans, tipofactor, tasatras, codirete, tasarete, relacion, montosoloiva, montoivarete
        , ivadeiva, ivaderet, retderet, conceptoretencion, consecutivoconcepto, claveproductoservicio, valorunitario, importe, descuento, cantidadletra, uuidrel
        , identificador, version, fechapago, monedacpag, tipodecambiocpag, monto, numerooperacion, rfcemisorcuenta, nombrebanco, numerocuentaord, rfcemisorcuentaben, numcuentaben
        , tipocadenapago, certpago, cadenadelpago, sellodelpago, identpag, identdocpago, seriecpag, foliocpag, monedacpagdoc, tipocambiocpag, metododepago, numerodeparcialidad
        , importeSaldoAnterior, importepago, importesaldoinsoluto, total, subt, ivat, rett, cond, tipoc, seriee, folioe, sfolio, Foliosrelacionados, serier, folior, uuidpagadas, IdentificadorDelDocumentoPagado, ipagado, nparcialidades, folio, MetdodoPago, Dserie, monedascpadgoc, interiorsaldoanterior, isaldoinsoluto, identificaciondpago, folioscpag, k1, k3, norden, tmoneda, idcomprobante, cantidad, descripcion, Tuuid, iddelpago, iipagado, basecalculado, basecalculado2, basecalculado3, impSaldoAnterior, impSaldoInsoluto, fechap, fechaemision, f03, totaliva, totalisr, if05, f08, if06, TotaldeRe, TotaldeIva, f07, importePagosTotal, subtotalfinal,totalfinaldeiva, regimenfiscal, tipodecambiocpagd, totaenpesos;

        public bool error = false;

        public string serie;
        public decimal importePagos = 0;
        public decimal importePagos2 = 0;
        public decimal importePagos3 = 0;
        public decimal importePagos4 = 0;
        public decimal importePagos5 = 0;
        public decimal importePagos7 = 0;
        public decimal importePagos22 = 0;
        public decimal importePagos23 = 0;
        public decimal importePagos24 = 0;
        public decimal importePagos25 = 0;
        public decimal importePagos26 = 0;
        public decimal valorunitarios = 0;
        public decimal importePagos77 = 0;
        public decimal importePagos78 = 0;
        public decimal importePagos57 = 0;
        public decimal importePagos58 = 0;
        public decimal importePagos67 = 0;
        public decimal importePagos68 = 0;
        public decimal totaldedolares = 0;

        public decimal importePagos37 = 0;
        public decimal importePagos38 = 0;
        public decimal importePagos88 = 0;
        public decimal importePagos97 = 0;
        public decimal importePagos99 = 0;
        public decimal importePagos98 = 0;
        public decimal importePagosTotal2 = 0;
        public decimal usdmoneda = 0;
        public decimal rtiva = 0;
        public decimal rtisr = 0;
        public decimal srtiva = 0;
        public decimal srtisr = 0;
        public decimal subtotalf = 0;
        public decimal totalfinaliva1 = 0;
        public decimal totalfinaliva2= 0;
        public decimal totalfinaliva3 = 0;
        public decimal totalfinaliva4 = 0;
        public decimal totalfinaliva5 = 0;
        public decimal totalfinaliva6 = 0;
        public bool nodeToFind;
        public bool nodeToFind2;



        public double ivaretencion = 1.12;
        public double ivasolo = 1.16;
        public double ivaa = 0.16;
        public double isrr = 0.04;
        public decimal totalIva = 0;
        public decimal totalIsr = 0;
        public int serietsrl = 0;
        public string ejecutar = "Si";
        public decimal basecalculo = 0;
        public decimal basecalculo2 = 0;
        public decimal basecalculo3 = 0;

        public string uid = "";



        public int contadorPUE = 0;
        public int contadorPPD = 0;
        public int contadortralix = 0;


        string cpagdoc = "";
        public string escrituraFactura = "", idSucursal = "", idTipoFactura = "", jsonFactura = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            imgFDesde.Visible = false;
            imgFHasta.Visible = false;
            lblFact.Text = Request.QueryString["factura"];
            //lblFact.Text = "40979";
            //foliot = Request.QueryString["factura"];
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
                string msg = "¡Error, ponte en contacto con TI";
                ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Error con los folios relacionados ', 'error');setTimeout(function(){window.location.href ='Listado.aspx'}, 10000)", true);
            }
        }

        public async Task iniciaDatos()
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
                    iddelpago = row["Folio"].ToString();
                    txtFolio.Text = row["SFolio"].ToString();
                    DateTime dt = DateTime.Parse(row["FechaHoraEmision"].ToString());
                    txtFechaFactura.Text = dt.ToString("yyyy'/'MM'/'dd HH:mm:ss");

                    // 01 - CAMPOS DE LA NUEVA VERSION

                    idcomprobante = row["IdComprobante"].ToString();
                    seriee = row["Serie"].ToString();
                    folioe = row["Folio"].ToString();
                    DateTime dt2 = DateTime.Parse(row["FechaHoraEmision"].ToString());
                    fechaemision = dt2.ToString("yyyy'/'MM'/'dd HH:mm:ss");
                    subt = row["Subtotal"].ToString();
                    total = row["Total"].ToString();
                    //Metodopago33 lo toma de cpagdoc
                    txtMoneda.Text = row["Moneda"].ToString();
                    tipocomprobante = row["TipodeComprobante"].ToString();
                    lugarexpedicion = row["LugardeExpedición"].ToString();
                    usocfdi = row["UsoCFDI"].ToString();
                    if (usocfdi == "P01")
                    {
                        usocfdi = "CP01";
                    }
                    //Etiqueta tipo documento = FAC


                    // 01 - FIN ----------------------------

                    // 02 - CAMPOS DE LA NUEVA VERSION
                    txtIdCliente.Text = row["IdReceptor"].ToString();
                    txtRFC.Text = row["RFC"].ToString();
                    txtCliente.Text = row["Nombre"].ToString();
                    txtCP.Text = row["CódigoPostal"].ToString();
                    descripcion = row["Descripcion"].ToString();
                    //REGIMEN FISCAL 601

                    // 02 - FIN CAMPOS DE LA NUEVA VERSION


                    sfolio = row["SFolio"].ToString();






                    ivat = row["TotalImpuestosTrasladados"].ToString();
                    rett = row["TotalImpuestosRetenidos"].ToString();

                    cantidadletra = row["Totalconletra"].ToString();
                    //formadepago = row["FormaDePago"].ToString();
                    cond = row["CondicionesdePago"].ToString();
                    metodopago33 = row["MetodoPago"].ToString();

                    tipoc = row["Tipodecambio"].ToString();


                    confirmacion = row["Confirmación"].ToString();

                    //02-------------------------------------------------------------------------------------------------------------------------

                    txtIdCliente.Text = row["IdReceptor"].ToString();

                    txtRFC.Text = row["RFC"].ToString();
                    //RFC = row["RFC"].ToString();
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
                    cantidad = row["Cantidad"].ToString();
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
                    DataTable ctipocambio = facLabControler.getTipoCambio(fechapago);
                    foreach (DataRow tcambio in ctipocambio.Rows)
                    {
                        tipodecambiocpagd = tcambio["XCHGRATE"].ToString();
                    }
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



                    // AQUI VOY-------------------------
                    if (txtRFC.Text != "")
                    {
                        DataTable detalleIdent2 = facLabControler.getDatosCPAGDOC(row["IdentificadorDelPago"].ToString());
                        if (detalleIdent2.Rows.Count > 0)
                        {
                            //CPADOC DESDE GP ----------------------

                            int totalmn = 1;
                            DataSet dataSet2 = new DataSet();
                            int totalr = detalleIdent2.Rows.Count;
                            int x = 0;
                            foreach (DataRow rowIdent2 in detalleIdent2.Rows)
                            {
                                identificaciondpago = rowIdent2["IdentificadorDelPago"].ToString();
                                folioscpag = Regex.Replace(rowIdent2["Foliocpag"].ToString().Replace("SM-", "").Trim(), @"[A-Z]", "");
                                folioscpag = Regex.Replace(rowIdent2["Foliocpag"].ToString().Replace("A", "").Trim(), @"[A-Z]", "");
                                folioscpag = Regex.Replace(rowIdent2["Foliocpag"].ToString().Replace("B", "").Trim(), @"[A-Z]", "");
                                folioscpag = Regex.Replace(rowIdent2["Foliocpag"].ToString().Replace("C", "").Trim(), @"[A-Z]", "");
                                folioscpag = Regex.Replace(rowIdent2["Foliocpag"].ToString().Replace("D", "").Trim(), @"[A-Z]", "");
                                folioscpag = Regex.Replace(rowIdent2["Foliocpag"].ToString().Replace("ND", "").Trim(), @"[A-Z]", "");
                                folioscpag = Regex.Replace(rowIdent2["Foliocpag"].ToString().Replace(".", "").Trim(), @"[A-Z]", "");
                                folioscpag = Regex.Replace(rowIdent2["Foliocpag"].ToString().Replace("-", "").Trim(), @"[A-Z]", "");
                                folioscpag = Regex.Replace(rowIdent2["Foliocpag"].ToString().Replace("NS", "").Trim(), @"[A-Z]", "");
                                importepago = rowIdent2["ImportePagado"].ToString();
                                importeSaldoAnterior = rowIdent2["ImporteSaldoAnterior"].ToString();
                                if (importeSaldoAnterior == "") { importeSaldoAnterior = "0.00"; }
                                else { importeSaldoAnterior = rowIdent2["ImporteSaldoAnterior"].ToString(); }
                                importesaldoinsoluto = rowIdent2["ImporteSaldoInsoluto"].ToString();
                                if (importesaldoinsoluto == "") { importesaldoinsoluto = "0.00"; }
                                else { importesaldoinsoluto = rowIdent2["ImporteSaldoInsoluto"].ToString(); }
                                numerodeparcialidad = rowIdent2["NumeroDeParcialidad"].ToString();
                                tipocambiocpag = rowIdent2["TipodeCambiocpagdpc"].ToString();
                                metodopago33 = rowIdent2["MedotoDePago"].ToString();
                                DataTable detalleIdentt = facLabControler.getDatosCPAGDOCTRL(identificaciondpago, folioscpag);
                                if (detalleIdentt.Rows.Count > 0)
                                {
                                    foreach (DataRow rowIdentt in detalleIdentt.Rows)
                                    {
                                        iipagado = rowIdentt["ActualApplyToAmount"].ToString();
                                        basecalculo = Convert.ToDecimal(iipagado);
                                        basecalculado = basecalculo.ToString("F");

                                        impSaldoAnterior = rowIdentt["ORTRXAMT"].ToString();
                                        if (impSaldoAnterior == "") { impSaldoAnterior = "0.00"; }
                                        else { impSaldoAnterior = rowIdentt["ORTRXAMT"].ToString(); }
                                        basecalculo2 = Convert.ToDecimal(impSaldoAnterior);
                                        basecalculado2 = basecalculo2.ToString("F");

                                        impSaldoInsoluto = rowIdentt["CURTRXAM"].ToString();
                                        if (impSaldoInsoluto == "") { impSaldoInsoluto = "0.00"; }
                                        else { impSaldoInsoluto = rowIdentt["CURTRXAM"].ToString(); }
                                        basecalculo3 = Convert.ToDecimal(impSaldoInsoluto);
                                        basecalculado3 = basecalculo3.ToString("F");


                                        folio = Regex.Replace(rowIdentt["K3"].ToString().Replace("TDR", "").Trim(), @"[A-Z]", "");

                                        //txtTotal.Text = importePagos.ToString();
                                        //txtTotal.Text = rowIdent["ImportePagado"].ToString();
                                        string receptor = txtIdCliente.Text.ToString().Trim();
                                        string serieinvoice = "";
                                        if (receptor.Equals("LIVERPOL") || receptor.Equals("LIVERDED") || receptor.Equals("ALMLIVER") || receptor.Equals("LIVERTIJ") || receptor.Equals("SFERALIV") || receptor.Equals("GLOBALIV") || receptor.Equals("SETRALIV") || receptor.Equals("FACTUMLV"))
                                        {
                                            serieinvoice = "TDRL";
                                        }
                                        else
                                        {
                                            serieinvoice = rowIdent2["Seriecpag"].ToString();
                                        }
                                        folio = Regex.Replace(rowIdentt["K3"].ToString().Replace("TDR", "").Trim(), @"[A-Z]", "");
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

                                        MetdodoPago = "";

                                        // FILTRO DE LA MASTER APROBADA
                                        DataTable datosMaster = facLabControler.getDatosMaster(folio);
                                        if (datosMaster.Rows.Count > 0)
                                        {

                                            foreach (DataRow rowMaster in datosMaster.Rows)
                                            {
                                                string invoiceMaster = Regex.Replace(rowMaster[0].ToString(), @"[A-Z]", "");
                                                folio = invoiceMaster;
                                                int nm = Int32.Parse(invoiceMaster);
                                                var request27 = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico="+nm+"&serie="+serieinvoice);
                                                var response27 = (HttpWebResponse)request27.GetResponse();
                                                var responseString27 = new StreamReader(response27.GetResponseStream()).ReadToEndAsync();

                                                List<ModelFact> separados7 = JsonConvert.DeserializeObject<List<ModelFact>>(await responseString27);
                                                if (separados7 != null)
                                                {
                                                    foreach (var item in separados7)
                                                    {
                                                        uid = item.uuid;
                                                        serier = item.serie;
                                                        folior = item.folio;
                                                        fechap = item.fecha;
                                                        uuidpagadas += uid + "\r\n";

                                                        Foliosrelacionados += "Serie: " + serier + " " + "Folio: " + folior + " " + "UUID: " + uid + "\r\n";



                                                        string UUID = item.xmlDownload;

                                                        XmlDocument xDoc = new XmlDocument();
                                                        xDoc.Load("https://canal1.xsa.com.mx:9050" + UUID);
                                                        var xmlTexto = xDoc.InnerXml.ToString();
                                                        DataSet dataSet1 = new DataSet();
                                                        XmlTextReader xtr = new XmlTextReader(xDoc.OuterXml, XmlNodeType.Element, null);
                                                        dataSet1.ReadXml(xtr);
                                                        if (xmlTexto.Contains("MetodoPago=\"PPD\""))
                                                        {
                                                            MetdodoPago = "PPD";
                                                            contadorPPD++;
                                                        }
                                                        else if (xmlTexto.Contains("MetodoPago=\"PUE\""))
                                                        {
                                                            txtMetodoPago.Text = "PUE";
                                                            MetdodoPago = "PUE";
                                                            contadorPUE++;
                                                        }
                                                        if (MetdodoPago == "PPD")
                                                        {
                                                            foreach (DataRow rowm in (InternalDataCollectionBase)dataSet1.Tables["Emisor"].Rows)
                                                            {
                                                                regimenfiscal = rowm["RegimenFiscal"].ToString();
                                                            }
                                                            foreach (DataRow rowsr in (InternalDataCollectionBase)dataSet1.Tables["Conceptos"].Rows)
                                                            {
                                                                foreach (DataRow rowsrc in (InternalDataCollectionBase)dataSet1.Tables["Concepto"].Rows)
                                                                {
                                                                    importe = rowsrc["Importe"].ToString();
                                                                    valorunitario = rowsrc["ValorUnitario"].ToString();
                                                                    try
                                                                    {
                                                                        importePagos = importePagos + Convert.ToDecimal(importe);
                                                                        importe = importePagos.ToString("F");

                                                                        valorunitarios = valorunitarios + Convert.ToDecimal(valorunitario);
                                                                        valorunitario = valorunitarios.ToString("F");
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        string errors = ex.Message;
                                                                    }
                                                                    //importe = rowsrc["Importe"].ToString();
                                                                    //valorunitario = rowsrc["ValorUnitario"].ToString();
                                                                    //descripcion = rowsrc["Descripcion"].ToString();
                                                                    //claveunidad = rowsrc["ClaveUnidad"].ToString();
                                                                    //cantidad = rowsrc["Cantidad"].ToString();
                                                                    //claveproductoservicio = rowsrc["ClaveProdServ"].ToString();
                                                                }
                                                            }
                                                            foreach (DataRow rowCC in (InternalDataCollectionBase)dataSet1.Tables["Comprobante"].Rows)
                                                            {
                                                                lugarexpedicion = rowCC["LugarExpedicion"].ToString();
                                                                //tipocomprobante = rowCC["TipoDeComprobante"].ToString();
                                                                //total = rowCC["Total"].ToString();
                                                                monedascpadgoc = rowCC["Moneda"].ToString();
                                                                formadepago = rowCC["FormaPago"].ToString();
                                                                if (formadepago == null || formadepago == "99") { formadepago = row["Formadepagocpag"].ToString(); }
                                                                else { formadepago = row["Formadepagocpag"].ToString(); }
                                                                //string Ccertificado = rowCC["Certificado"].ToString();
                                                                //string Cnocertificado = rowCC["NoCertificado"].ToString();
                                                                //string Csello = rowCC["Sello"].ToString();
                                                                tipodecambiocpag = rowCC["TipoCambio"].ToString();
                                                                idcomprobante = rowCC["Folio"].ToString();

                                                                serie = rowCC["Serie"].ToString();
                                                            }
                                                            foreach (DataRow rowsr1 in (InternalDataCollectionBase)dataSet1.Tables["Complemento"].Rows)
                                                            {
                                                                foreach (DataRow rowsrct in (InternalDataCollectionBase)dataSet1.Tables["TimbreFiscalDigital"].Rows)
                                                                {
                                                                    string Trfcprovcertif = rowsrct["RfcProvCertif"].ToString();
                                                                    string Tsellosat = rowsrct["SelloSAT"].ToString();
                                                                    string Tsellocfd = rowsrct["SelloCFD"].ToString();
                                                                    string Tnocertidicadosat = rowsrct["NoCertificadoSAT"].ToString();
                                                                    Tuuid = rowsrct["UUID"].ToString();
                                                                    string Tfechatimbrado = rowsrct["FechaTimbrado"].ToString();



                                                                }
                                                            }
                                                            //FolioUUIDTxt.Text += identpag;
                                                            try
                                                            {
                                                                importePagos2 = importePagos2 + Convert.ToDecimal(basecalculado);
                                                                
                                                                txtTotal.Text = importePagos2.ToString("F");
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                string errors = ex.Message;
                                                            }


                                                            nodeToFind = xmlTexto.Contains("Traslado");
                                                            nodeToFind2 = xmlTexto.Contains("Retencion");
                                                            if (nodeToFind != false && nodeToFind2 != false)
                                                            {
                                                                //AQUI VA EL CODIGO PARA FORMAR EL TXT DE TRASLADO Y RETENCION, TOTALES DEL TRASLADO Y RETENCIIONES
                                                                try
                                                                {
                                                                    subtotalf = (decimal)(Convert.ToDouble(basecalculado) / ivaretencion);
                                                                    subtotalfinal = subtotalf.ToString("F");


                                                                    
                                                                    totalfinaliva1 = totalfinaliva1 + Convert.ToDecimal(subtotalfinal);
                                                                    totalfinaldeiva = totalfinaliva1.ToString("F");
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    string errors = ex.Message;
                                                                }
                                                                totalIva = (decimal)(ivaa * Convert.ToDouble(subtotalfinal));
                                                                totaliva = totalIva.ToString("F");
                                                                totalIsr = (decimal)(isrr * Convert.ToDouble(subtotalfinal));
                                                                totalisr = totalIsr.ToString("F");
                                                                if (totalmn == totalr)
                                                                {
                                                                    if06 = "CPAG20DOCIMPTRA"
                                                                + "|" + iddelpago.Trim()
                                                                
                                                                + "|" + Tuuid.Trim()
                                                                + "|" + "002"
                                                                + "|" + "Tasa"
                                                                + "|" + "0.160000"
                                                                + "|" + totaliva
                                                                //+ "|" + retencion
                                                                + "|" + subtotalfinal.Trim()
                                                                + "|";
                                                                }
                                                                else
                                                                {
                                                                    if06 = "CPAG20DOCIMPTRA"
                                                                + "|" + iddelpago.Trim()
                                                               
                                                                + "|" + Tuuid.Trim()
                                                                + "|" + "002"
                                                                + "|" + "Tasa"
                                                                + "|" + "0.160000"
                                                                + "|" + totaliva
                                                                //+ "|" + retencion
                                                                + "|" + subtotalfinal.Trim()
                                                                + "| \r\n";
                                                                }



                                                                if05 = "CPAG20DOCIMPRET"
                                                                + "|" + iddelpago.Trim()
                                                               
                                                                + "|" + Tuuid.Trim()
                                                                + "|" + "001"
                                                                + "|" + "Tasa"
                                                                + "|" + "0.040000"
                                                                + "|" + totalisr
                                                                //+ "|" + iva.Trim()
                                                                + "|" + subtotalfinal.Trim()
                                                                + "| \r\n";




                                                                try
                                                                {
                                                                    importePagos57 = importePagos57 + Convert.ToDecimal(totalisr);
                                                                    rtisr = 1;
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    string errors = ex.Message;
                                                                }



                                                                try
                                                                {
                                                                    importePagos58 = importePagos58 + Convert.ToDecimal(totaliva);
                                                                    rtiva = 1;
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    string errors = ex.Message;
                                                                }






                                                                //AQUI TERMINA

                                                            }
                                                            if (nodeToFind == false && nodeToFind2 == true)
                                                            {
                                                                //AQUI VA EL CODIGO PARA FORMAR EL TXT DE TRASLADO Y RETENCION, TOTALES DEL TRASLADO Y RETENCIIONES
                                                                totalIva = (decimal)(ivaa * Convert.ToDouble(basecalculado));
                                                                totaliva = totalIva.ToString("F");
                                                                totalIsr = (decimal)(isrr * Convert.ToDouble(basecalculado));
                                                                totalisr = totalIsr.ToString("F");
                                                                if (totalmn == totalr)
                                                                {
                                                                    if05 = "CPAG20DOCIMPRET"
                                                                 + "|" + iddelpago.Trim()
                                                                
                                                                 + "|" + Tuuid.Trim()
                                                                 + "|" + "001"
                                                                 + "|" + "Tasa"
                                                                 + "|" + "0.040000"
                                                                 + "|" + totalisr
                                                                 //+ "|" + iva.Trim()
                                                                 + "|" + basecalculado.Trim()
                                                                 + "|";
                                                                }
                                                                else
                                                                {
                                                                    if05 = "CPAG20DOCIMPRET"
                                                                + "|" + iddelpago.Trim()
                                                                
                                                                + "|" + Tuuid.Trim()
                                                                + "|" + "001"
                                                                + "|" + "Tasa"
                                                                + "|" + "0.040000"
                                                                + "|" + totalisr
                                                                //+ "|" + iva.Trim()
                                                                + "|" + basecalculado.Trim()
                                                                + "| \r\n";

                                                                }
                                                                if06 = "";


                                                                try
                                                                {
                                                                    importePagos67 = importePagos67 + Convert.ToDecimal(totalisr);


                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    string errors = ex.Message;
                                                                }

                                                                //f07 = "CPAG20IMPRET"
                                                                //+ "|" + iddelpago.Trim()
                                                                //+ "|" + "001"
                                                                //+ "|" + TotaldeRe
                                                                //+ "|";
                                                                //f08 = "";


                                                                //AQUI TERMINA
                                                            }
                                                            if (nodeToFind == true && nodeToFind2 == false)
                                                            {
                                                                //AQUI VERIFICA LA MONEDA SI SON DOLARES
                                                                
                                                                
                                                                    try
                                                                    {
                                                                        subtotalf = (decimal)(Convert.ToDouble(basecalculado) / ivasolo);
                                                                        subtotalfinal = subtotalf.ToString("F");
                                                                        totalfinaliva4 = totalfinaliva4 + Convert.ToDecimal(subtotalfinal);
                                                                        totalfinaldeiva = totalfinaliva4.ToString("F");
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        string errors = ex.Message;
                                                                    }
                                                                    totalIva = (decimal)(ivaa * Convert.ToDouble(subtotalfinal));
                                                                    totaliva = totalIva.ToString("F");

                                                                    if (totalmn == totalr)
                                                                    {
                                                                        if06 = "CPAG20DOCIMPTRA"
                                                                    + "|" + iddelpago.Trim()

                                                                    + "|" + Tuuid.Trim()
                                                                    + "|" + "002"
                                                                    + "|" + "Tasa"
                                                                    + "|" + "0.160000"
                                                                    + "|" + totaliva
                                                                    //+ "|" + retencion
                                                                    + "|" + subtotalfinal.Trim()
                                                                    + "|";
                                                                    }
                                                                    else
                                                                    {
                                                                        if06 = "CPAG20DOCIMPTRA"
                                                                    + "|" + iddelpago.Trim()

                                                                    + "|" + Tuuid.Trim()
                                                                    + "|" + "002"
                                                                    + "|" + "Tasa"
                                                                    + "|" + "0.160000"
                                                                    + "|" + totaliva
                                                                    //+ "|" + retencion
                                                                    + "|" + subtotalfinal.Trim()
                                                                    + "| \r\n";
                                                                    }



                                                                    if05 = "";





                                                                    try
                                                                    {
                                                                        importePagos88 = importePagos88 + Convert.ToDecimal(totaliva);
                                                                        srtiva = 3;
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        string errors = ex.Message;
                                                                    }
                                                                
                                                                // FIN DOLARES





                                                            }

                                                            if (monedascpadgoc.Trim() == "USD")
                                                            {


                                                                cpagdoc = cpagdoc + (
                                                                           "CPAG20DOC"                           //1-Tipo De Registro
                                                                     + "|" + iddelpago.Trim()                    //2-IdentificadorDelPago  
                                                                     + "|" + Tuuid.Trim()                        //3-IdentificadorDelDocumentoPagado                                              
                                                                     + "|" + serieinvoice.Trim()                 //4-Seriecpag
                                                                     + "|" + idcomprobante.Trim()                //5-Foliocpag
                                                                     + "|" + monedascpadgoc.Trim()               //6-Monedacpag
                                                                     + "|" + "1"                                      //7-Equivalencia
                                                                     + "|" + numerodeparcialidad.Trim()          //8-NumeroDeParcialidad
                                                                     + "|" + basecalculado.Trim()               //9-ImporteSaldoAnterior
                                                                     + "|" + basecalculado.Trim()                //10-ImportePagado                                                    
                                                                           + "|" + "0"                                            //12 ImporteSaldoInsoluto
                                                                           + "|" + "01"
                                                                           + "| \r\n");

                                                                usdmoneda = 1;
                                                            }
                                                            else
                                                            {
                                                                if (nodeToFind != false && nodeToFind2 != false)
                                                                {
                                                                    if (totalmn == totalr)
                                                                    {
                                                                        cpagdoc = cpagdoc + (
                                                                           "CPAG20DOC"                           //1-Tipo De Registro
                                                                     + "|" + iddelpago.Trim()                    //2-IdentificadorDelPago  
                                                                     + "|" + Tuuid.Trim()                        //3-IdentificadorDelDocumentoPagado                                              
                                                                     + "|" + serieinvoice.Trim()                 //4-Seriecpag
                                                                     + "|" + idcomprobante.Trim()                //5-Foliocpag
                                                                     + "|" + monedascpadgoc.Trim()               //6-Monedacpag
                                                                     + "|" + "1"                                     //7-Equivalencia
                                                                     + "|" + numerodeparcialidad.Trim()          //8-NumeroDeParcialidad
                                                                     + "|" + basecalculado2.Trim()               //9-ImporteSaldoAnterior
                                                                     + "|" + basecalculado.Trim()                //10-ImportePagado                                                  
                                                                     + "|" + basecalculado3.Trim()               //11-ImporteSaldoInsoluto
                                                                     + "|" + "02"                                //12-ObjetoDeImpuesto
                                                                     + "| \r\n")
                                                                     + if05
                                                                     + if06;
                                                                    }
                                                                    else
                                                                    {
                                                                        cpagdoc = cpagdoc + (
                                                                           "CPAG20DOC"                           //1-Tipo De Registro
                                                                     + "|" + iddelpago.Trim()                    //2-IdentificadorDelPago  
                                                                     + "|" + Tuuid.Trim()                        //3-IdentificadorDelDocumentoPagado                                              
                                                                     + "|" + serieinvoice.Trim()                 //4-Seriecpag
                                                                     + "|" + idcomprobante.Trim()                //5-Foliocpag
                                                                     + "|" + monedascpadgoc.Trim()               //6-Monedacpag
                                                                     + "|" + "1"                                     //7-Equivalencia
                                                                     + "|" + numerodeparcialidad.Trim()          //8-NumeroDeParcialidad
                                                                     + "|" + basecalculado2.Trim()               //9-ImporteSaldoAnterior
                                                                     + "|" + basecalculado.Trim()                //10-ImportePagado                                                  
                                                                     + "|" + basecalculado3.Trim()               //11-ImporteSaldoInsoluto
                                                                     + "|" + "02"                                //12-ObjetoDeImpuesto
                                                                     + "| \r\n")
                                                                     + if05
                                                                     + if06;
                                                                    }

                                                                }
                                                                if (nodeToFind == false && nodeToFind2 == true)
                                                                {
                                                                    cpagdoc = cpagdoc + (
                                                                           "CPAG20DOC"                           //1-Tipo De Registro
                                                                     + "|" + iddelpago.Trim()                    //2-IdentificadorDelPago  
                                                                     + "|" + Tuuid.Trim()                        //3-IdentificadorDelDocumentoPagado                                              
                                                                     + "|" + serieinvoice.Trim()                 //4-Seriecpag
                                                                     + "|" + idcomprobante.Trim()                //5-Foliocpag
                                                                     + "|" + monedascpadgoc.Trim()               //6-Monedacpag
                                                                     + "|" + "1"                                     //7-Equivalencia
                                                                     + "|" + numerodeparcialidad.Trim()          //8-NumeroDeParcialidad
                                                                     + "|" + basecalculado2.Trim()               //9-ImporteSaldoAnterior
                                                                     + "|" + basecalculado.Trim()                //10-ImportePagado                                                  
                                                                     + "|" + basecalculado3.Trim()               //11-ImporteSaldoInsoluto
                                                                     + "|" + "02"                                //12-ObjetoDeImpuesto
                                                                     + "| \r\n")
                                                                     + if05;

                                                                }
                                                                if (nodeToFind == true && nodeToFind2 == false)
                                                                {
                                                                    cpagdoc = cpagdoc + (
                                                                          "CPAG20DOC"                           //1-Tipo De Registro
                                                                    + "|" + iddelpago.Trim()                    //2-IdentificadorDelPago  
                                                                    + "|" + Tuuid.Trim()                        //3-IdentificadorDelDocumentoPagado                                              
                                                                    + "|" + serieinvoice.Trim()                 //4-Seriecpag
                                                                    + "|" + idcomprobante.Trim()                //5-Foliocpag
                                                                    + "|" + monedascpadgoc.Trim()               //6-Monedacpag
                                                                    + "|" + "1"                                     //7-Equivalencia
                                                                    + "|" + numerodeparcialidad.Trim()          //8-NumeroDeParcialidad
                                                                    + "|" + basecalculado2.Trim()               //9-ImporteSaldoAnterior
                                                                    + "|" + basecalculado.Trim()                //10-ImportePagado                                                  
                                                                    + "|" + basecalculado3.Trim()               //11-ImporteSaldoInsoluto
                                                                    + "|" + "02"                                //12-ObjetoDeImpuesto
                                                                    + "| \r\n")
                                                                    + if06;
                                                                }
                                                            }

                                                        }
                                                        else
                                                        {
                                                            string msg = "Error: Los folios relacionados no existen en el canal de Tralix";
                                                            formularioT.Visible = false;
                                                            Div1.Visible = true;
                                                            ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Error con los folios relacionados ', 'error');setTimeout(function(){window.location.href ='Listado.aspx'}, 10000)", true);

                                                        }


                                                    }
                                                }


                                            }

                                        }

                                        else
                                        {

                                            //3 FILTRO APROBADO
                                            //AQUI TERMINA EL IF
                                            k1 = rowIdentt["K1"].ToString();

                                            k3 = Regex.Replace(rowIdentt["K3"].ToString().Replace("TDRM", "").Trim(), @"[A-Z]", "");
                                            iipagado = rowIdentt["ActualApplyToAmount"].ToString();
                                            basecalculo = Convert.ToDecimal(iipagado);
                                            basecalculado = basecalculo.ToString("F");
                                            int kk3 = Int32.Parse(k3);
                                            var request281 = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico="+kk3+"&rfc="+txtRFC.Text);
                                            var response281 = (HttpWebResponse)request281.GetResponse();
                                            var responseString281 = new StreamReader(response281.GetResponseStream()).ReadToEndAsync();

                                            List<ModelFact> separados81 = JsonConvert.DeserializeObject<List<ModelFact>>(await responseString281);

                                            if (separados81 != null)
                                            {
                                                contadortralix = 1;

                                                foreach (var item in separados81)
                                                {
                                                    uid = item.uuid;
                                                    serier = item.serie;
                                                    folior = item.folio;
                                                    uuidpagadas += uid + "\r\n";

                                                    Foliosrelacionados += "Serie: " + serier + " " + "Folio: " + folior + " " + "UUID: " + uid + "\r\n";



                                                    string UUID = item.xmlDownload;

                                                    XmlDocument xDoc = new XmlDocument();
                                                    xDoc.Load("https://canal1.xsa.com.mx:9050" + UUID);
                                                    var xmlTexto = xDoc.InnerXml.ToString();
                                                    XmlElement root = xDoc.DocumentElement;
                                                    DataSet dataSet1 = new DataSet();
                                                    XmlTextReader xtr = new XmlTextReader(xDoc.OuterXml, XmlNodeType.Element, null);
                                                    dataSet1.ReadXml(xtr);



                                                    if (xmlTexto.Contains("MetodoPago=\"PPD\""))
                                                    {
                                                        MetdodoPago = "PPD";
                                                        contadorPPD++;
                                                    }
                                                    else if (xmlTexto.Contains("MetodoPago=\"PUE\""))
                                                    {
                                                        txtMetodoPago.Text = "PUE";
                                                        MetdodoPago = "PUE";
                                                        contadorPUE++;
                                                    }
                                                    if (MetdodoPago == "PPD")
                                                    {

                                                        foreach (DataRow rowm in (InternalDataCollectionBase)dataSet1.Tables["Emisor"].Rows)
                                                        {
                                                            regimenfiscal = rowm["RegimenFiscal"].ToString();
                                                        }

                                                        foreach (DataRow rowsr in (InternalDataCollectionBase)dataSet1.Tables["Conceptos"].Rows)
                                                        {
                                                            foreach (DataRow rowsrc in (InternalDataCollectionBase)dataSet1.Tables["Concepto"].Rows)
                                                            {
                                                                importe = rowsrc["Importe"].ToString();
                                                                valorunitario = rowsrc["ValorUnitario"].ToString();
                                                                try
                                                                {
                                                                    importePagos = importePagos + Convert.ToDecimal(importe);
                                                                    importe = importePagos.ToString("F");

                                                                    valorunitarios = valorunitarios + Convert.ToDecimal(valorunitario);
                                                                    valorunitario = valorunitarios.ToString("F");
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    string errors = ex.Message;
                                                                }
                                                                //descripcion = rowsrc["Descripcion"].ToString();
                                                                //claveunidad = rowsrc["ClaveUnidad"].ToString();
                                                                //cantidad = rowsrc["Cantidad"].ToString();
                                                                //claveproductoservicio = rowsrc["ClaveProdServ"].ToString();
                                                            }
                                                        }
                                                        foreach (DataRow rowCC in (InternalDataCollectionBase)dataSet1.Tables["Comprobante"].Rows)
                                                        {
                                                            lugarexpedicion = rowCC["LugarExpedicion"].ToString();
                                                            //tipocomprobante = rowCC["TipoDeComprobante"].ToString();
                                                            //total = rowCC["Total"].ToString();
                                                            monedascpadgoc = rowCC["Moneda"].ToString();
                                                            formadepago = rowCC["FormaPago"].ToString();
                                                            if (formadepago == null || formadepago == "99") { formadepago = row["Formadepagocpag"].ToString(); }
                                                            else { formadepago = row["Formadepagocpag"].ToString(); }
                                                            //string Ccertificado = rowCC["Certificado"].ToString();
                                                            //string Cnocertificado = rowCC["NoCertificado"].ToString();
                                                            //string Csello = rowCC["Sello"].ToString();
                                                            tipodecambiocpag = rowCC["TipoCambio"].ToString();

                                                            idcomprobante = rowCC["Folio"].ToString();
                                                            serie = rowCC["Serie"].ToString();
                                                        }
                                                        foreach (DataRow rowsr1 in (InternalDataCollectionBase)dataSet1.Tables["Complemento"].Rows)
                                                        {
                                                            foreach (DataRow rowsrct in (InternalDataCollectionBase)dataSet1.Tables["TimbreFiscalDigital"].Rows)
                                                            {
                                                                string Trfcprovcertif = rowsrct["RfcProvCertif"].ToString();
                                                                string Tsellosat = rowsrct["SelloSAT"].ToString();
                                                                string Tsellocfd = rowsrct["SelloCFD"].ToString();
                                                                string Tnocertidicadosat = rowsrct["NoCertificadoSAT"].ToString();
                                                                Tuuid = rowsrct["UUID"].ToString();
                                                                string Tfechatimbrado = rowsrct["FechaTimbrado"].ToString();



                                                            }
                                                        }
                                                        //FolioUUIDTxt.Text += identpag;
                                                        try
                                                        {
                                                            importePagos7 = importePagos7 + Convert.ToDecimal(basecalculado);
                                                            txtTotal.Text = importePagos7.ToString("F");
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            string errors = ex.Message;
                                                        }

                                                        nodeToFind = xmlTexto.Contains("Traslado");
                                                        nodeToFind2 = xmlTexto.Contains("Retencion"); ;
                                                        if (nodeToFind != false && nodeToFind2 != false)
                                                        {
                                                            //AQUI VA EL CODIGO PARA FORMAR EL TXT DE TRASLADO Y RETENCION, TOTALES DEL TRASLADO Y RETENCIIONES
                                                            try
                                                            {
                                                                subtotalf = (decimal)(Convert.ToDouble(basecalculado) / ivaretencion);
                                                                subtotalfinal = subtotalf.ToString("F");
                                                                totalfinaliva3 = totalfinaliva3 + Convert.ToDecimal(subtotalfinal);
                                                                totalfinaldeiva = totalfinaliva3.ToString("F");
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                string errors = ex.Message;
                                                            }
                                                            totalIva = (decimal)(ivaa * Convert.ToDouble(subtotalfinal));
                                                            totaliva = totalIva.ToString("F");
                                                            //AQUI VA EL CODIGO PARA FORMAR EL TXT DE TRASLADO Y RETENCION, TOTALES DEL TRASLADO Y RETENCIIONES

                                                            totalIsr = (decimal)(isrr * Convert.ToDouble(subtotalfinal));
                                                            totalisr = totalIsr.ToString("F");
                                                            if (totalmn == totalr)
                                                            {
                                                                if06 = "CPAG20DOCIMPTRA"
                                                            + "|" + iddelpago.Trim()
                                                            
                                                            + "|" + Tuuid.Trim()
                                                            + "|" + "002"
                                                            + "|" + "Tasa"
                                                            + "|" + "0.160000"
                                                            + "|" + totaliva
                                                            //+ "|" + retencion
                                                            + "|" + subtotalfinal.Trim()
                                                            + "|";
                                                            }
                                                            else
                                                            {
                                                                if06 = "CPAG20DOCIMPTRA"
                                                            + "|" + iddelpago.Trim()
                                                            
                                                            + "|" + Tuuid.Trim()
                                                            + "|" + "002"
                                                            + "|" + "Tasa"
                                                            + "|" + "0.160000"
                                                            + "|" + totaliva
                                                            //+ "|" + retencion
                                                            + "|" + subtotalfinal.Trim()
                                                            + "| \r\n";
                                                            }



                                                            if05 = "CPAG20DOCIMPRET"
                                                            + "|" + iddelpago.Trim()
                                                           
                                                            + "|" + Tuuid.Trim()
                                                            + "|" + "001"
                                                            + "|" + "Tasa"
                                                            + "|" + "0.040000"
                                                            + "|" + totalisr
                                                            //+ "|" + iva.Trim()
                                                            + "|" + subtotalfinal.Trim()
                                                            + "| \r\n";




                                                            try
                                                            {
                                                                importePagos77 = importePagos77 + Convert.ToDecimal(totalisr);
                                                                rtisr = 1;

                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                string errors = ex.Message;
                                                            }



                                                            try
                                                            {
                                                                importePagos78 = importePagos78 + Convert.ToDecimal(totaliva);
                                                                rtiva = 1;
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                string errors = ex.Message;
                                                            }





                                                            //AQUI TERMINA

                                                        }
                                                        if (nodeToFind == false && nodeToFind2 == true)
                                                        {
                                                            //AQUI VA EL CODIGO PARA FORMAR EL TXT DE TRASLADO Y RETENCION, TOTALES DEL TRASLADO Y RETENCIIONES
                                                            totalIva = (decimal)(ivaa * Convert.ToDouble(basecalculado));
                                                            totaliva = totalIva.ToString("F");
                                                            totalIsr = (decimal)(isrr * Convert.ToDouble(basecalculado));
                                                            totalisr = totalIsr.ToString("F");
                                                            if (totalmn == totalr)
                                                            {
                                                                if05 = "CPAG20DOCIMPRET"
                                                             + "|" + iddelpago.Trim()
                                                            
                                                             + "|" + Tuuid.Trim()
                                                             + "|" + "001"
                                                             + "|" + "Tasa"
                                                             + "|" + "0.040000"
                                                             + "|" + totalisr
                                                             //+ "|" + iva.Trim()
                                                             + "|" + basecalculado.Trim()
                                                             + "|";
                                                            }
                                                            else
                                                            {
                                                                if05 = "CPAG20DOCIMPRET"
                                                            + "|" + iddelpago.Trim()
                                                           
                                                            + "|" + Tuuid.Trim()
                                                            + "|" + "001"
                                                            + "|" + "Tasa"
                                                            + "|" + "0.040000"
                                                            + "|" + totalisr
                                                            //+ "|" + iva.Trim()
                                                            + "|" + basecalculado.Trim()
                                                            + "| \r\n";

                                                            }
                                                            if06 = "";


                                                            try
                                                            {
                                                                importePagos97 = importePagos97 + Convert.ToDecimal(totalisr);
                                                                srtisr = 2;

                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                string errors = ex.Message;
                                                            }

                                                            //f07 = "CPAG20IMPRET"
                                                            //+ "|" + iddelpago.Trim()
                                                            //+ "|" + "001"
                                                            //+ "|" + TotaldeRe
                                                            //+ "|";
                                                            //f08 = "";


                                                            //AQUI TERMINA
                                                        }
                                                        if (nodeToFind == true && nodeToFind2 == false)
                                                        {
                                                            //AQUI VERIFICA LA MONEDA SI SON DOLARES
                                                            
                                                            
                                                                try
                                                                {
                                                                    subtotalf = (decimal)(Convert.ToDouble(basecalculado) / ivasolo);
                                                                    subtotalfinal = subtotalf.ToString("F");
                                                                    totalfinaliva4 = totalfinaliva4 + Convert.ToDecimal(subtotalfinal);
                                                                    totalfinaldeiva = totalfinaliva4.ToString("F");
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    string errors = ex.Message;
                                                                }
                                                                totalIva = (decimal)(ivaa * Convert.ToDouble(subtotalfinal));
                                                                totaliva = totalIva.ToString("F");

                                                                if (totalmn == totalr)
                                                                {
                                                                    if06 = "CPAG20DOCIMPTRA"
                                                                + "|" + iddelpago.Trim()

                                                                + "|" + Tuuid.Trim()
                                                                + "|" + "002"
                                                                + "|" + "Tasa"
                                                                + "|" + "0.160000"
                                                                + "|" + totaliva
                                                                //+ "|" + retencion
                                                                + "|" + subtotalfinal.Trim()
                                                                + "|";
                                                                }
                                                                else
                                                                {
                                                                    if06 = "CPAG20DOCIMPTRA"
                                                                + "|" + iddelpago.Trim()

                                                                + "|" + Tuuid.Trim()
                                                                + "|" + "002"
                                                                + "|" + "Tasa"
                                                                + "|" + "0.160000"
                                                                + "|" + totaliva
                                                                //+ "|" + retencion
                                                                + "|" + subtotalfinal.Trim()
                                                                + "| \r\n";
                                                                }



                                                                if05 = "";





                                                                try
                                                                {
                                                                    importePagos88 = importePagos88 + Convert.ToDecimal(totaliva);
                                                                    srtiva = 3;
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    string errors = ex.Message;
                                                                }
                                                            
                                                            // FIN DOLARES
                                                            




                                                        }

                                                        if (monedascpadgoc.Trim() == "USD")
                                                        {


                                                            cpagdoc = cpagdoc + (
                                                                           "CPAG20DOC"                           //1-Tipo De Registro
                                                                     + "|" + iddelpago.Trim()                    //2-IdentificadorDelPago  
                                                                     + "|" + Tuuid.Trim()                        //3-IdentificadorDelDocumentoPagado                                              
                                                                     + "|" + serieinvoice.Trim()                 //4-Seriecpag
                                                                     + "|" + idcomprobante.Trim()                //5-Foliocpag
                                                                     + "|" + monedascpadgoc.Trim()               //6-Monedacpag
                                                                     + "|" + "1"                                      //7-Equivalencia
                                                                     + "|" + numerodeparcialidad.Trim()          //8-NumeroDeParcialidad
                                                                     + "|" + basecalculado.Trim()               //9-ImporteSaldoAnterior
                                                                     + "|" + basecalculado.Trim()                //10-ImportePagado                                                    
                                                                           + "|" + "0"                                            //12 ImporteSaldoInsoluto
                                                                           + "|" + "01"
                                                                           + "| \r\n");
                                                           
                                                            usdmoneda = 1;
                                                        }
                                                        else
                                                        {
                                                            if (nodeToFind != false && nodeToFind2 != false)
                                                            {
                                                                if (totalmn == totalr)
                                                                {
                                                                    cpagdoc = cpagdoc + (
                                                                           "CPAG20DOC"                           //1-Tipo De Registro
                                                                     + "|" + iddelpago.Trim()                    //2-IdentificadorDelPago  
                                                                     + "|" + Tuuid.Trim()                        //3-IdentificadorDelDocumentoPagado                                              
                                                                     + "|" + serieinvoice.Trim()                 //4-Seriecpag
                                                                     + "|" + idcomprobante.Trim()                //5-Foliocpag
                                                                     + "|" + monedascpadgoc.Trim()               //6-Monedacpag
                                                                     + "|" + "1"                                      //7-Equivalencia
                                                                     + "|" + numerodeparcialidad.Trim()          //8-NumeroDeParcialidad
                                                                     + "|" + basecalculado2.Trim()               //9-ImporteSaldoAnterior
                                                                     + "|" + basecalculado.Trim()                //10-ImportePagado                                                  
                                                                     + "|" + basecalculado3.Trim()               //11-ImporteSaldoInsoluto
                                                                     + "|" + "02"                                //12-ObjetoDeImpuesto
                                                                     + "| \r\n")
                                                                     + if05
                                                                     + if06;
                                                                }
                                                                else
                                                                {
                                                                    cpagdoc = cpagdoc + (
                                                                           "CPAG20DOC"                           //1-Tipo De Registro
                                                                     + "|" + iddelpago.Trim()                    //2-IdentificadorDelPago  
                                                                     + "|" + Tuuid.Trim()                        //3-IdentificadorDelDocumentoPagado                                              
                                                                     + "|" + serieinvoice.Trim()                 //4-Seriecpag
                                                                     + "|" + idcomprobante.Trim()                //5-Foliocpag
                                                                     + "|" + monedascpadgoc.Trim()               //6-Monedacpag
                                                                     + "|" + "1"                                      //7-Equivalencia
                                                                     + "|" + numerodeparcialidad.Trim()          //8-NumeroDeParcialidad
                                                                     + "|" + basecalculado2.Trim()               //9-ImporteSaldoAnterior
                                                                     + "|" + basecalculado.Trim()                //10-ImportePagado                                                  
                                                                     + "|" + basecalculado3.Trim()               //11-ImporteSaldoInsoluto
                                                                     + "|" + "02"                                //12-ObjetoDeImpuesto
                                                                     + "| \r\n")
                                                                     + if05
                                                                     + if06;
                                                                }

                                                            }
                                                            if (nodeToFind == false && nodeToFind2 == true)
                                                            {
                                                                cpagdoc = cpagdoc + (
                                                                          "CPAG20DOC"                           //1-Tipo De Registro
                                                                    + "|" + iddelpago.Trim()                    //2-IdentificadorDelPago  
                                                                    + "|" + Tuuid.Trim()                        //3-IdentificadorDelDocumentoPagado                                              
                                                                    + "|" + serieinvoice.Trim()                 //4-Seriecpag
                                                                    + "|" + idcomprobante.Trim()                //5-Foliocpag
                                                                    + "|" + monedascpadgoc.Trim()               //6-Monedacpag
                                                                    + "|" + "1"                                      //7-Equivalencia
                                                                    + "|" + numerodeparcialidad.Trim()          //8-NumeroDeParcialidad
                                                                    + "|" + basecalculado2.Trim()               //9-ImporteSaldoAnterior
                                                                    + "|" + basecalculado.Trim()                //10-ImportePagado                                                  
                                                                    + "|" + basecalculado3.Trim()               //11-ImporteSaldoInsoluto
                                                                    + "|" + "02"                                //12-ObjetoDeImpuesto
                                                                    + "| \r\n")
                                                                    + if05;

                                                            }
                                                            if (nodeToFind == true && nodeToFind2 == false)
                                                            {
                                                                cpagdoc = cpagdoc + (
                                                                          "CPAG20DOC"                           //1-Tipo De Registro
                                                                    + "|" + iddelpago.Trim()                    //2-IdentificadorDelPago  
                                                                    + "|" + Tuuid.Trim()                        //3-IdentificadorDelDocumentoPagado                                              
                                                                    + "|" + serieinvoice.Trim()                 //4-Seriecpag
                                                                    + "|" + idcomprobante.Trim()                //5-Foliocpag
                                                                    + "|" + monedascpadgoc.Trim()               //6-Monedacpag
                                                                    + "|" + "1"                                      //7-Equivalencia
                                                                    + "|" + numerodeparcialidad.Trim()          //8-NumeroDeParcialidad
                                                                    + "|" + basecalculado2.Trim()               //9-ImporteSaldoAnterior
                                                                    + "|" + basecalculado.Trim()                //10-ImportePagado                                                  
                                                                    + "|" + basecalculado3.Trim()               //11-ImporteSaldoInsoluto
                                                                    + "|" + "02"                                //12-ObjetoDeImpuesto
                                                                    + "| \r\n")
                                                                    + if06;
                                                            }
                                                        }
                                                    }
                                                    //else
                                                    //{
                                                    //    string msg = "Error: Los folios relacionados no existen en el canal de Tralix";
                                                    //    formularioT.Visible = false;
                                                    //    Div1.Visible = true;
                                                    //    ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Error con los folios relacionados ', 'error');setTimeout(function(){window.location.href ='Listado.aspx'}, 10000)", true);

                                                    //}
                                                    //AQUI FALTA AGREGAR LO QUE TIENE EL XML Y FORMAR EL TXT


                                                }

                                            }
                                            else
                                            {

                                                DataTable invoiceresult = facLabControler.getDatosInvoice(k3);
                                                if (invoiceresult.Rows.Count > 0)
                                                {
                                                    foreach (DataRow rowInvoice in invoiceresult.Rows)
                                                    {

                                                        norden = rowInvoice["ord_hdrnumber"].ToString();
                                                        DataTable segmentoresult = facLabControler.getDatosSegmentos(norden);

                                                        foreach (DataRow seg in segmentoresult.Rows)
                                                        {
                                                            string Segmento = seg["Segmento"].ToString();
                                                            int s3 = Int32.Parse(Segmento);
                                                            var request2819 = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico="+s3+"&rfc="+txtRFC.Text);
                                                            var response2819 = (HttpWebResponse)request2819.GetResponse();
                                                            var responseString2819 = new StreamReader(response2819.GetResponseStream()).ReadToEndAsync();

                                                            List<ModelFact> separados819 = JsonConvert.DeserializeObject<List<ModelFact>>(await responseString2819);

                                                            if (separados819 != null)
                                                            {
                                                                //int totalmn = 1;
                                                                //DataSet dataSet2 = new DataSet();
                                                                //int totalr = separados819.Count;
                                                                //int x = 0;
                                                                foreach (var item in separados819)
                                                                {
                                                                    string uuid = item.uuid;
                                                                    string xmld = item.xmlDownload;
                                                                    serieinvoice = item.serie;

                                                                    uid = item.uuid;
                                                                    serier = item.serie;
                                                                    folior = item.folio;
                                                                    uuidpagadas += uid + "\r\n";

                                                                    Foliosrelacionados += "Serie: " + serier + " " + "Folio: " + folior + " " + "UUID: " + uid + "\r\n";

                                                                    XmlDocument xDoc = new XmlDocument();
                                                                    xDoc.Load("https://canal1.xsa.com.mx:9050" + xmld);
                                                                    var xmlTexto = xDoc.InnerXml.ToString();
                                                                    DataSet dataSet1 = new DataSet();
                                                                    XmlTextReader xtr = new XmlTextReader(xDoc.OuterXml, XmlNodeType.Element, null);
                                                                    dataSet1.ReadXml(xtr);

                                                                    if (xmlTexto.Contains("MetodoPago=\"PPD\""))
                                                                    {
                                                                        MetdodoPago = "PPD";
                                                                        contadorPPD++;
                                                                    }
                                                                    else if (xmlTexto.Contains("MetodoPago=\"PUE\""))
                                                                    {
                                                                        txtMetodoPago.Text = "PUE";
                                                                        MetdodoPago = "PUE";
                                                                        contadorPUE++;
                                                                    }
                                                                    if (MetdodoPago == "PPD")
                                                                    {
                                                                        foreach (DataRow rowm in (InternalDataCollectionBase)dataSet1.Tables["Emisor"].Rows)
                                                                        {
                                                                            regimenfiscal = rowm["RegimenFiscal"].ToString();
                                                                        }
                                                                        foreach (DataRow rowsr in (InternalDataCollectionBase)dataSet1.Tables["Conceptos"].Rows)
                                                                        {
                                                                            foreach (DataRow rowsrc in (InternalDataCollectionBase)dataSet1.Tables["Concepto"].Rows)
                                                                            {
                                                                                importe = rowsrc["Importe"].ToString();
                                                                                valorunitario = rowsrc["ValorUnitario"].ToString();
                                                                                try
                                                                                {
                                                                                    //importePagos77 = importePagos77 + Convert.ToDecimal(importe);
                                                                                    //importe = importePagos77.ToString("F");

                                                                                    //valorunitarios = valorunitarios + Convert.ToDecimal(valorunitario);
                                                                                    //valorunitario = valorunitarios.ToString("F");
                                                                                }
                                                                                catch (Exception ex)
                                                                                {
                                                                                    string errors = ex.Message;
                                                                                }

                                                                                //descripcion = rowsrc["Descripcion"].ToString();
                                                                                //claveunidad = rowsrc["ClaveUnidad"].ToString();
                                                                                //cantidad = rowsrc["Cantidad"].ToString();
                                                                                //claveproductoservicio = rowsrc["ClaveProdServ"].ToString();
                                                                            }
                                                                        }
                                                                        foreach (DataRow rowCC in (InternalDataCollectionBase)dataSet1.Tables["Comprobante"].Rows)
                                                                        {
                                                                            lugarexpedicion = rowCC["LugarExpedicion"].ToString();
                                                                            //tipocomprobante = rowCC["TipoDeComprobante"].ToString();
                                                                            //total = rowCC["Total"].ToString();
                                                                            monedascpadgoc = rowCC["Moneda"].ToString();
                                                                            formadepago = rowCC["FormaPago"].ToString();
                                                                            if (formadepago == null || formadepago == "99") { formadepago = row["Formadepagocpag"].ToString(); }
                                                                            else { formadepago = row["Formadepagocpag"].ToString(); }
                                                                            //string Ccertificado = rowCC["Certificado"].ToString();
                                                                            //string Cnocertificado = rowCC["NoCertificado"].ToString();
                                                                            //string Csello = rowCC["Sello"].ToString();
                                                                            tipodecambiocpag = rowCC["TipoCambio"].ToString();
                                                                            idcomprobante = rowCC["Folio"].ToString();
                                                                            serie = rowCC["Serie"].ToString();
                                                                        }
                                                                        foreach (DataRow rowsr1 in (InternalDataCollectionBase)dataSet1.Tables["Complemento"].Rows)
                                                                        {
                                                                            foreach (DataRow rowsrct in (InternalDataCollectionBase)dataSet1.Tables["TimbreFiscalDigital"].Rows)
                                                                            {
                                                                                string Trfcprovcertif = rowsrct["RfcProvCertif"].ToString();
                                                                                string Tsellosat = rowsrct["SelloSAT"].ToString();
                                                                                string Tsellocfd = rowsrct["SelloCFD"].ToString();
                                                                                string Tnocertidicadosat = rowsrct["NoCertificadoSAT"].ToString();
                                                                                Tuuid = rowsrct["UUID"].ToString();
                                                                                string Tfechatimbrado = rowsrct["FechaTimbrado"].ToString();



                                                                            }
                                                                        }
                                                                        //FolioUUIDTxt.Text += identpag;
                                                                        try
                                                                        {
                                                                            importePagos4 = importePagos4 + Convert.ToDecimal(basecalculado);
                                                                            txtTotal.Text = importePagos4.ToString("F");
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                            string errors = ex.Message;
                                                                        }

                                                                        nodeToFind = xmlTexto.Contains("Traslado");
                                                                        nodeToFind2 = xmlTexto.Contains("Retencion"); ;
                                                                        if (nodeToFind != false && nodeToFind2 != false)
                                                                        {
                                                                            try
                                                                            {
                                                                                subtotalf = (decimal)(Convert.ToDouble(basecalculado) / ivaretencion);
                                                                                subtotalfinal = subtotalf.ToString("F");
                                                                                totalfinaliva5 = totalfinaliva5 + Convert.ToDecimal(subtotalfinal);
                                                                                totalfinaldeiva = totalfinaliva5.ToString("F");
                                                                            }
                                                                            catch (Exception ex)
                                                                            {
                                                                                string errors = ex.Message;
                                                                            }
                                                                            totalIva = (decimal)(ivaa * Convert.ToDouble(subtotalfinal));
                                                                            totaliva = totalIva.ToString("F");
                                                                            //AQUI VA EL CODIGO PARA FORMAR EL TXT DE TRASLADO Y RETENCION, TOTALES DEL TRASLADO Y RETENCIIONES

                                                                            totalIsr = (decimal)(isrr * Convert.ToDouble(subtotalfinal));
                                                                            totalisr = totalIsr.ToString("F");
                                                                            if (totalmn == totalr)
                                                                            {
                                                                                if06 = "CPAG20DOCIMPTRA"
                                                                            + "|" + iddelpago.Trim()
                                                                           
                                                                            + "|" + Tuuid.Trim()
                                                                            + "|" + "002"
                                                                            + "|" + "Tasa"
                                                                            + "|" + "0.160000"
                                                                            + "|" + totaliva
                                                                            //+ "|" + retencion
                                                                            + "|" + subtotalfinal.Trim()
                                                                            + "|";
                                                                            }
                                                                            else
                                                                            {
                                                                                if06 = "CPAG20DOCIMPTRA"
                                                                            + "|" + iddelpago.Trim()
                                                                            
                                                                            + "|" + Tuuid.Trim()
                                                                            + "|" + "002"
                                                                            + "|" + "Tasa"
                                                                            + "|" + "0.160000"
                                                                            + "|" + totaliva
                                                                            //+ "|" + retencion
                                                                            + "|" + subtotalfinal.Trim()
                                                                            + "| \r\n";
                                                                            }



                                                                            if05 = "CPAG20DOCIMPRET"
                                                                            + "|" + iddelpago.Trim()
                                                                           
                                                                            + "|" + Tuuid.Trim()
                                                                            + "|" + "001"
                                                                            + "|" + "Tasa"
                                                                            + "|" + "0.040000"
                                                                            + "|" + totalisr
                                                                            //+ "|" + iva.Trim()
                                                                            + "|" + subtotalfinal.Trim()
                                                                            + "| \r\n";


                                                                            try
                                                                            {
                                                                                importePagos37 = importePagos37 + Convert.ToDecimal(totalisr);
                                                                                rtisr = 1;

                                                                            }
                                                                            catch (Exception ex)
                                                                            {
                                                                                string errors = ex.Message;
                                                                            }



                                                                            try
                                                                            {
                                                                                importePagos38 = importePagos38 + Convert.ToDecimal(totaliva);
                                                                                rtiva = 1;
                                                                            }
                                                                            catch (Exception ex)
                                                                            {
                                                                                string errors = ex.Message;
                                                                            }






                                                                            //AQUI TERMINA

                                                                        }
                                                                        if (nodeToFind == false && nodeToFind2 == true)
                                                                        {
                                                                            //AQUI VA EL CODIGO PARA FORMAR EL TXT DE TRASLADO Y RETENCION, TOTALES DEL TRASLADO Y RETENCIIONES
                                                                            totalIva = (decimal)(ivaa * Convert.ToDouble(basecalculado));
                                                                            totaliva = totalIva.ToString("F");
                                                                            totalIsr = (decimal)(isrr * Convert.ToDouble(basecalculado));
                                                                            totalisr = totalIsr.ToString("F");
                                                                            if (totalmn == totalr)
                                                                            {
                                                                                if05 = "CPAG20DOCIMPRET"
                                                                             + "|" + iddelpago.Trim()
                                                                             
                                                                             + "|" + Tuuid.Trim()
                                                                             + "|" + "001"
                                                                             + "|" + "Tasa"
                                                                             + "|" + "0.040000"
                                                                             + "|" + totalisr
                                                                             //+ "|" + iva.Trim()
                                                                             + "|" + basecalculado.Trim()
                                                                             + "|";
                                                                            }
                                                                            else
                                                                            {
                                                                                if05 = "CPAG20DOCIMPRET"
                                                                            + "|" + iddelpago.Trim()
                                                                            
                                                                            + "|" + Tuuid.Trim()
                                                                            + "|" + "001"
                                                                            + "|" + "Tasa"
                                                                            + "|" + "0.040000"
                                                                            + "|" + totalisr
                                                                            //+ "|" + iva.Trim()
                                                                            + "|" + basecalculado.Trim()
                                                                            + "| \r\n";

                                                                            }
                                                                            if06 = "";


                                                                            try
                                                                            {
                                                                                importePagos99 = importePagos99 + Convert.ToDecimal(totalisr);
                                                                                srtisr = 2;

                                                                            }
                                                                            catch (Exception ex)
                                                                            {
                                                                                string errors = ex.Message;
                                                                            }

                                                                            //f07 = "CPAG20IMPRET"
                                                                            //+ "|" + iddelpago.Trim()
                                                                            //+ "|" + "001"
                                                                            //+ "|" + TotaldeRe
                                                                            //+ "|";
                                                                            //f08 = "";


                                                                            //AQUI TERMINA
                                                                        }
                                                                        if (nodeToFind == true && nodeToFind2 == false)
                                                                        {
                                                                            //AQUI VERIFICA LA MONEDA SI SON DOLARES
                                                                            
                                                                            
                                                                                try
                                                                                {
                                                                                    subtotalf = (decimal)(Convert.ToDouble(basecalculado) / ivasolo);
                                                                                    subtotalfinal = subtotalf.ToString("F");
                                                                                    totalfinaliva4 = totalfinaliva4 + Convert.ToDecimal(subtotalfinal);
                                                                                    totalfinaldeiva = totalfinaliva4.ToString("F");
                                                                                }
                                                                                catch (Exception ex)
                                                                                {
                                                                                    string errors = ex.Message;
                                                                                }
                                                                                totalIva = (decimal)(ivaa * Convert.ToDouble(subtotalfinal));
                                                                                totaliva = totalIva.ToString("F");

                                                                                if (totalmn == totalr)
                                                                                {
                                                                                    if06 = "CPAG20DOCIMPTRA"
                                                                                + "|" + iddelpago.Trim()

                                                                                + "|" + Tuuid.Trim()
                                                                                + "|" + "002"
                                                                                + "|" + "Tasa"
                                                                                + "|" + "0.160000"
                                                                                + "|" + totaliva
                                                                                //+ "|" + retencion
                                                                                + "|" + subtotalfinal.Trim()
                                                                                + "|";
                                                                                }
                                                                                else
                                                                                {
                                                                                    if06 = "CPAG20DOCIMPTRA"
                                                                                + "|" + iddelpago.Trim()

                                                                                + "|" + Tuuid.Trim()
                                                                                + "|" + "002"
                                                                                + "|" + "Tasa"
                                                                                + "|" + "0.160000"
                                                                                + "|" + totaliva
                                                                                //+ "|" + retencion
                                                                                + "|" + subtotalfinal.Trim()
                                                                                + "| \r\n";
                                                                                }



                                                                                if05 = "";





                                                                                try
                                                                                {
                                                                                    importePagos88 = importePagos88 + Convert.ToDecimal(totaliva);
                                                                                    srtiva = 3;
                                                                                }
                                                                                catch (Exception ex)
                                                                                {
                                                                                    string errors = ex.Message;
                                                                                }
                                                                            
                                                                            // FIN DOLARES





                                                                        }

                                                                        if (monedascpadgoc.Trim() == "USD")
                                                                        {

                                                                            cpagdoc = cpagdoc + (
                                                                                                                                                                                                                                       "CPAG20DOC"                           //1-Tipo De Registro
                                                                                                                                                 + "|" + iddelpago.Trim()                    //2-IdentificadorDelPago  
                                                                                                                                                 + "|" + Tuuid.Trim()                        //3-IdentificadorDelDocumentoPagado                                              
                                                                                                                                                 + "|" + serieinvoice.Trim()                 //4-Seriecpag
                                                                                                                                                 + "|" + idcomprobante.Trim()                //5-Foliocpag
                                                                                                                                                 + "|" + monedascpadgoc.Trim()               //6-Monedacpag
                                                                                                                                                 + "|" + "1"                                      //7-Equivalencia
                                                                                                                                                 + "|" + numerodeparcialidad.Trim()          //8-NumeroDeParcialidad
                                                                                                                                                 + "|" + basecalculado.Trim()               //9-ImporteSaldoAnterior
                                                                                                                                                 + "|" + basecalculado.Trim()                //10-ImportePagado                                                    
                                                                                                                                                       + "|" + "0"                                            //12 ImporteSaldoInsoluto
                                                                                                                                                       + "|" + "01"
                                                                                                                                                       + "| \r\n");



                                                                            usdmoneda = 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (nodeToFind != false && nodeToFind2 != false)
                                                                            {
                                                                                if (totalmn == totalr)
                                                                                {
                                                                                    cpagdoc = cpagdoc + (
                                                                          "CPAG20DOC"                           //1-Tipo De Registro
                                                                    + "|" + iddelpago.Trim()                    //2-IdentificadorDelPago  
                                                                    + "|" + Tuuid.Trim()                        //3-IdentificadorDelDocumentoPagado                                              
                                                                    + "|" + serieinvoice.Trim()                 //4-Seriecpag
                                                                    + "|" + idcomprobante.Trim()                //5-Foliocpag
                                                                    + "|" + monedascpadgoc.Trim()               //6-Monedacpag
                                                                    + "|" + "1"                                    //7-Equivalencia
                                                                    + "|" + numerodeparcialidad.Trim()          //8-NumeroDeParcialidad
                                                                    + "|" + basecalculado2.Trim()               //9-ImporteSaldoAnterior
                                                                    + "|" + basecalculado.Trim()                //10-ImportePagado                                                  
                                                                    + "|" + basecalculado3.Trim()               //11-ImporteSaldoInsoluto
                                                                    + "|" + "02"                                //12-ObjetoDeImpuesto
                                                                    + "| \r\n")
                                                                    + if05
                                                                    + if06;
                                                                                }
                                                                                else
                                                                                {
                                                                                    cpagdoc = cpagdoc + (
                                                                          "CPAG20DOC"                           //1-Tipo De Registro
                                                                    + "|" + iddelpago.Trim()                    //2-IdentificadorDelPago  
                                                                    + "|" + Tuuid.Trim()                        //3-IdentificadorDelDocumentoPagado                                              
                                                                    + "|" + serieinvoice.Trim()                 //4-Seriecpag
                                                                    + "|" + idcomprobante.Trim()                //5-Foliocpag
                                                                    + "|" + monedascpadgoc.Trim()               //6-Monedacpag
                                                                    + "|" + "1"                                    //7-Equivalencia
                                                                    + "|" + numerodeparcialidad.Trim()          //8-NumeroDeParcialidad
                                                                    + "|" + basecalculado2.Trim()               //9-ImporteSaldoAnterior
                                                                    + "|" + basecalculado.Trim()                //10-ImportePagado                                                  
                                                                    + "|" + basecalculado3.Trim()               //11-ImporteSaldoInsoluto
                                                                    + "|" + "02"                                //12-ObjetoDeImpuesto
                                                                    + "| \r\n")
                                                                    + if05
                                                                    + if06;
                                                                                }
                                                                            }
                                                                            if (nodeToFind == false && nodeToFind2 == true)
                                                                            {
                                                                                cpagdoc = cpagdoc + (
                                                                          "CPAG20DOC"                           //1-Tipo De Registro
                                                                    + "|" + iddelpago.Trim()                    //2-IdentificadorDelPago  
                                                                    + "|" + Tuuid.Trim()                        //3-IdentificadorDelDocumentoPagado                                              
                                                                    + "|" + serieinvoice.Trim()                 //4-Seriecpag
                                                                    + "|" + idcomprobante.Trim()                //5-Foliocpag
                                                                    + "|" + monedascpadgoc.Trim()               //6-Monedacpag
                                                                    + "|" + "1"                                     //7-Equivalencia
                                                                    + "|" + numerodeparcialidad.Trim()          //8-NumeroDeParcialidad
                                                                    + "|" + basecalculado2.Trim()               //9-ImporteSaldoAnterior
                                                                    + "|" + basecalculado.Trim()                //10-ImportePagado                                                  
                                                                    + "|" + basecalculado3.Trim()               //11-ImporteSaldoInsoluto
                                                                    + "|" + "02"                                //12-ObjetoDeImpuesto
                                                                    + "| \r\n")
                                                                    + if05;

                                                                            }
                                                                            if (nodeToFind == true && nodeToFind2 == false)
                                                                            {
                                                                                cpagdoc = cpagdoc + (
                                                                          "CPAG20DOC"                           //1-Tipo De Registro
                                                                    + "|" + iddelpago.Trim()                    //2-IdentificadorDelPago  
                                                                    + "|" + Tuuid.Trim()                        //3-IdentificadorDelDocumentoPagado                                              
                                                                    + "|" + serieinvoice.Trim()                 //4-Seriecpag
                                                                    + "|" + idcomprobante.Trim()                //5-Foliocpag
                                                                    + "|" + monedascpadgoc.Trim()               //6-Monedacpag
                                                                    + "|" + "1"                                     //7-Equivalencia
                                                                    + "|" + numerodeparcialidad.Trim()          //8-NumeroDeParcialidad
                                                                    + "|" + basecalculado2.Trim()               //9-ImporteSaldoAnterior
                                                                    + "|" + basecalculado.Trim()                //10-ImportePagado                                                  
                                                                    + "|" + basecalculado3.Trim()               //11-ImporteSaldoInsoluto
                                                                    + "|" + "02"                                //12-ObjetoDeImpuesto
                                                                    + "| \r\n")
                                                                    + if06;
                                                                            }

                                                                        }

                                                                    }

                                                                }
                                                            }

                                                        }
                                                    }

                                                }

                                            }
                                            if (uid == "" && serieinvoice == "TDRA")
                                            {
                                                int fl = Int32.Parse(folio);
                                                var request23 = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico="+fl+"&serie="+"SAEM");
                                                var response23 = (HttpWebResponse)request23.GetResponse();
                                                var responseString23 = new StreamReader(response23.GetResponseStream()).ReadToEndAsync();

                                                List<ModelFact> separados23 = JsonConvert.DeserializeObject<List<ModelFact>>(await responseString23);

                                                foreach (var item23 in separados23)
                                                {
                                                    uid = item23.uuid;


                                                    string UUID = item23.xmlDownload;

                                                    XmlDocument xDoc = new XmlDocument();
                                                    xDoc.Load("https://canal1.xsa.com.mx:9050" + UUID);
                                                    var xmlTexto = xDoc.InnerXml.ToString();
                                                    DataSet dataSet1 = new DataSet();
                                                    XmlTextReader xtr = new XmlTextReader(xDoc.OuterXml, XmlNodeType.Element, null);
                                                    dataSet1.ReadXml(xtr);
                                                    if (xmlTexto.Contains("MetodoPago=\"PPD\""))
                                                    {
                                                        MetdodoPago = "PPD";
                                                        contadorPPD++;
                                                    }
                                                    else if (xmlTexto.Contains("MetodoPago=\"PUE\""))
                                                    {
                                                        txtMetodoPago.Text = "PUE";
                                                        MetdodoPago = "PUE";
                                                        contadorPUE++;
                                                    }

                                                }
                                            }
                                            //ME FALTA ESTA DE PROBAR Y GENERAR TXT


                                        }






                                    }
                                }
                                totalmn++;

                            }
                            decimal totald = importePagos2 + importePagos22 + importePagos7 + importePagos4 + importePagos23 + importePagos24;
                            
                            txtTotal.Text = totald.ToString("F");

                            totaldedolares = (decimal)(Convert.ToDouble(tipodecambiocpagd) * Convert.ToDouble(txtTotal.Text));
                            totaenpesos = totaldedolares.ToString("F");


                            decimal sumatotaliva = totalfinaliva1 + totalfinaliva2 + totalfinaliva3 + totalfinaliva4 + totalfinaliva5 + totalfinaliva6;
                            decimal.Round(sumatotaliva);
                            totalfinaldeiva = sumatotaliva.ToString("F");




                            try
                            {
                                decimal totalRet = importePagos37 + importePagos77 + importePagos97 + importePagos99 + importePagos57 + importePagos67;
                                TotaldeRe = totalRet.ToString();

                                decimal totalTrasl = importePagos38 + importePagos78 + importePagos88 + importePagos98 + importePagos58 + importePagos68;
                                TotaldeIva = totalTrasl.ToString();

                            }
                            catch (Exception ex)
                            {
                                string errors = ex.Message;
                            }

                            if (nodeToFind == true && nodeToFind2 == true)
                            {

                                f07 = "CPAG20IMPRET"
                                 + "|" + iddelpago.Trim()
                                 + "|" + "001"
                                 + "|" + TotaldeRe
                                 + "|";



                                f08 = "CPAG20IMPTRA"
                                + "|" + iddelpago.Trim()
                                + "|" + "002"
                                + "|" + "Tasa"
                                + "|" + "0.160000"
                                + "|" + TotaldeIva
                                + "|" + totalfinaldeiva.Trim()
                                + "|";

                                
                                decimal subtotalpre = (decimal)(Convert.ToDouble(txtTotal.Text) / ivaretencion);
                                decimal totalredondo = decimal.Round(subtotalpre,2);
                                subtotalfinal = totalredondo.ToString("F");
                            }
                            if (nodeToFind == true && nodeToFind2 == false)
                            {
                                //Aqui va el IF si tienen IVA e ISR los folios anteriores
                                //Se agrega totalIVA Y totalISR
                                if (rtiva == 1 && rtisr == 1)
                                {
                                    f07 = "CPAG20IMPRET"
                                    + "|" + iddelpago.Trim()
                                    + "|" + "001"
                                    + "|" + TotaldeRe
                                    + "|";
                                    f08 = "CPAG20IMPTRA"
                                    + "|" + iddelpago.Trim()
                                    + "|" + "002"
                                    + "|" + "Tasa"
                                    + "|" + "0.160000"
                                    + "|" + TotaldeIva
                                    + "|" + totalfinaldeiva.Trim()
                                    + "|";
                                    decimal subtotalpre = (decimal)(Convert.ToDouble(txtTotal.Text) / ivaretencion);
                                    decimal totalredondo = decimal.Round(subtotalpre,2);
                                    subtotalfinal = totalredondo.ToString("F");
                                }
                                else
                                {
                                    if (usdmoneda == 1)
                                    {
                                        f07 = "";
                                        f08 = "";
                                        
                                    }
                                    else
                                    {
                                        f08 = "CPAG20IMPTRA"
                                        + "|" + iddelpago.Trim()
                                        + "|" + "002"
                                        + "|" + "Tasa"
                                        + "|" + "0.160000"
                                        + "|" + TotaldeIva
                                        + "|" + totalfinaldeiva.Trim()
                                        + "|";
                                    }
                                    if (srtiva == 3)
                                    {
                                        TotaldeRe = "0.00";
                                        decimal subtotalpre = (decimal)(Convert.ToDouble(txtTotal.Text) / ivasolo);
                                        decimal totalredondo = decimal.Round(subtotalpre,2);
                                        subtotalfinal = totalredondo.ToString("F");
                                        
                                    }
                                }
                                
                               


                            }
                            if (nodeToFind == false && nodeToFind2 == true)
                            {
                                //Aqui va el IF si tienen IVA e ISR los folios anteriores
                                //Se agrega totalIVA Y totalISR
                                if (rtiva == 1 && rtisr == 1)
                                {
                                    f07 = "CPAG20IMPRET"
                                    + "|" + iddelpago.Trim()
                                    + "|" + "001"
                                    + "|" + TotaldeRe
                                    + "|";
                                    f08 = "CPAG20IMPTRA"
                                    + "|" + iddelpago.Trim()
                                    + "|" + "002"
                                    + "|" + "Tasa"
                                    + "|" + "0.160000"
                                    + "|" + TotaldeIva
                                    + "|" + totalfinaldeiva.Trim()
                                    + "|";
                                }
                                if (usdmoneda == 1)
                                {
                                    f07 = "";
                                    f08 = "";
                                }
                                else
                                {
                                    f07 = "CPAG20IMPRET"
                                + "|" + iddelpago.Trim()
                                + "|" + "001"
                                + "|" + TotaldeRe
                                + "|";
                                }

                            }



                            
                        }
                        //else
                        //{
                        //    //PRIMER FILTRO - PENDIENTE DE PRUEBA
                        //    var request28 = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico=" + row["IdentificadorDelPago"].ToString() + "&rfc=" + txtRFC.Text);
                        //    var response28 = (HttpWebResponse)request28.GetResponse();
                        //    var responseString28 = new StreamReader(response28.GetResponseStream()).ReadToEndAsync();


                        //    List<ModelFact> separados8 = JsonConvert.DeserializeObject<List<ModelFact>>(await responseString28);

                        //    if (separados8 != null)
                        //    {
                        //        foreach (var item in separados8)
                        //        {


                        //            string UUID = item.xmlDownload;

                        //            XmlDocument xDoc = new XmlDocument();
                        //            xDoc.Load("https://canal1.xsa.com.mx:9050" + UUID);
                        //            var xmlTexto = xDoc.InnerXml.ToString();
                        //            DataSet dataSet1 = new DataSet();
                        //            XmlTextReader xtr = new XmlTextReader(xDoc.OuterXml, XmlNodeType.Element, null);
                        //            dataSet1.ReadXml(xtr);
                        //            foreach (DataRow rowCC in (InternalDataCollectionBase)dataSet1.Tables["Comprobante"].Rows)
                        //            {
                        //                lugarexpedicion = rowCC["LugarExpedicion"].ToString();
                        //                //tipocomprobante = rowCC["TipoDeComprobante"].ToString();
                        //                //tipodecambiocpag = rowCC["TipoCambio"].ToString();
                        //                //total = rowCC["Total"].ToString();
                        //                monedascpadgoc = rowCC["Moneda"].ToString();
                        //                //formadepago = rowCC["FormaPago"].ToString();
                        //                if (formadepago == null || formadepago == "99") { formadepago = row["Formadepagocpag"].ToString(); }
                        //                else { formadepago = row["Formadepagocpag"].ToString(); }
                        //                //string Ccertificado = rowCC["Certificado"].ToString();
                        //                //string Cnocertificado = rowCC["NoCertificado"].ToString();
                        //                //string Csello = rowCC["Sello"].ToString();

                        //                idcomprobante = rowCC["Folio"].ToString();
                        //                serie = rowCC["Serie"].ToString();
                        //            }
                        //            foreach (DataRow rowsr in (InternalDataCollectionBase)dataSet1.Tables["Conceptos"].Rows)
                        //            {
                        //                foreach (DataRow rowsrc in (InternalDataCollectionBase)dataSet1.Tables["Concepto"].Rows)
                        //                {
                        //                    importe = rowsrc["Importe"].ToString();
                        //                    valorunitario = rowsrc["ValorUnitario"].ToString();
                        //                    //descripcion = rowsrc["Descripcion"].ToString();
                        //                    //claveunidad = rowsrc["ClaveUnidad"].ToString();
                        //                    //cantidad = rowsrc["Cantidad"].ToString();
                        //                    //claveproductoservicio = rowsrc["ClaveProdServ"].ToString();
                        //                }
                        //            }
                        //            foreach (DataRow rowsr1 in (InternalDataCollectionBase)dataSet1.Tables["Complemento"].Rows)
                        //            {
                        //                foreach (DataRow rowsrcts in dataSet1.Tables["Pagos"].Rows)
                        //                {
                        //                    foreach (DataRow rowsrctp in dataSet1.Tables["Pago"].Rows)
                        //                    {
                        //                        formadepago = rowsrctp["FormaDePagoP"].ToString();
                        //                        if (formadepago == null || formadepago == "99") { formadepago = row["Formadepagocpag"].ToString(); }
                        //                        else { formadepago = row["Formadepagocpag"].ToString(); }
                        //                        foreach (DataRow rowsrctpr in dataSet1.Tables["DoctoRelacionado"].Rows)
                        //                        {
                        //                            folio = rowsrctpr["Folio"].ToString();
                        //                            Dserie = rowsrctpr["Serie"].ToString();
                        //                            isaldoinsoluto = rowsrctpr["ImpSaldoInsoluto"].ToString();
                        //                            if (isaldoinsoluto == "") { isaldoinsoluto = "0.00"; }
                        //                            else { isaldoinsoluto = rowsrctpr["ImpSaldoInsoluto"].ToString(); }
                        //                            ipagado = rowsrctpr["ImpPagado"].ToString();
                        //                            interiorsaldoanterior = rowsrctpr["ImpSaldoAnt"].ToString();
                        //                            nparcialidades = rowsrctpr["NumParcialidad"].ToString();
                        //                            MetdodoPago = rowsrctpr["MetodoDePagoDR"].ToString();
                        //                            monedascpadgoc = rowsrctpr["MonedaDR"].ToString();

                        //                            IdentificadorDelDocumentoPagado = rowsrctpr["IdDocumento"].ToString();


                        //                            uuidpagadas += IdentificadorDelDocumentoPagado + "\r\n";
                        //                            Foliosrelacionados += "Serie: " + Dserie + " " + "Folio: " + folio + " " + "UUID: " + IdentificadorDelDocumentoPagado + "\r\n";

                        //                            string receptor = txtIdCliente.Text.ToString().Trim();
                        //                            string serieinvoice = "";
                        //                            if (receptor.Equals("LIVERPOL") || receptor.Equals("LIVERDED") || receptor.Equals("ALMLIVER") || receptor.Equals("LIVERTIJ") || receptor.Equals("SFERALIV") || receptor.Equals("GLOBALIV") || receptor.Equals("SETRALIV") || receptor.Equals("FACTUMLV"))
                        //                            {
                        //                                serieinvoice = "TDRL";
                        //                            }
                        //                            else
                        //                            {
                        //                                serieinvoice = row["Seriecpag"].ToString();
                        //                            }

                        //                            if (folio.Length == 7 && folio.StartsWith("99"))
                        //                            {
                        //                                folio = folio.Substring(folio.Length - 6, 6);
                        //                            }
                        //                            else if (folio.Length == 8)
                        //                            {
                        //                                folio = folio.Substring(folio.Length - 7, 7);
                        //                            }
                        //                            folio = folio.Replace("-", "");
                        //                            //validar con la serie el id de sucursal-serie



                        //                            if (MetdodoPago == "PPD")
                        //                            {

                        //                                identpag = row["IdentificadorDelPago"].ToString();
                        //                                //txtFechaIniOP.Text = "\r\n" +rowIdent["IdentificadorDelDocumentoPagado"].ToString();

                        //                                //FolioUUIDTxt.Text += identpag;
                        //                                try
                        //                                {
                        //                                    importePagos = importePagos + Convert.ToDecimal(ipagado);
                        //                                    txtTotal.Text = importePagos.ToString();
                        //                                }
                        //                                catch (Exception ex)
                        //                                {
                        //                                    string errors = ex.Message;
                        //                                }
                        //                                if (monedascpadgoc.Trim() == "USD")
                        //                                {



                        //                                    cpagdoc = cpagdoc + ("CPAG20DOC"                           //1-Tipo De Registro
                        //                                      + "|" + identpag                                       //2-IdentificadorDelPago
                        //                                                                                             //+ "|" + rowIdent["IdentificadorDelDocumentoPagado"].ToString()                            //3-IdentificadorDelDocumentoPagado                                              
                        //                                      + "|" + IdentificadorDelDocumentoPagado                                            //3-IdentificadorDelDocumentoPagado                                              
                        //                                      + "|" + serieinvoice                                   //4-Seriecpag
                        //                                      + "|" + folio                                      //5-Foliocpag
                        //                                      + "|" + monedascpadgoc                                  //6-Monedacpag
                        //                                      + "|"                                              //7-TipoCambiocpagdpc
                        //                                      + "|" + MetdodoPago                             //8-MetodoDePago
                        //                                      + "|" + nparcialidades                            //9-NumeroDeParcialidad
                        //                                      + "|" + ipagado                                    //10-ImporteSaldoAnterior
                        //                                      + "|" + ipagado                                    //11-ImportePagado                                                  
                        //                                      + "|" + "0"                                            //12 ImporteSaldoInsoluto
                        //                                      + "|" + "01"
                        //                                      + "| \r\n");
                        //                                }
                        //                                else
                        //                                {
                        //                                    //----------------------------------------Seccion CPAG20PAGO -------------------------------------------------------------------

                        //                                    //CPAG20PAGO (1:N)
                        //                                    //escritor.WriteLine(
                        //                                    //"CPAG20PAGO"                        //1-Tipo De Registro
                        //                                    //+ "|" + identpag                    //2-IdentificadorDelPago
                        //                                    //+ "|" + fechapago                   //3-FechaPago                                              
                        //                                    //+ "|"  + formadepagocpag            //4-Forma de pago
                        //                                    //+ "|" + moneda                      //5-Moneda
                        //                                    //+ "|"                               //6-TipoDeCambiocpag
                        //                                    //+ "|" + monto                       //7-Monto
                        //                                    //+ "|"                               //8-NumeroOperacion
                        //                                    //+ "|"                               //9-RFCEmisorCuentaOrdenante
                        //                                    //+ "|"                               //10-Nombre del Banco
                        //                                    //+ "|"                               //11-Número de Cuenta Ordenante
                        //                                    //+ "|"                               //12-RFC Emisor Cuenta Beneficiario
                        //                                    //+ "|"                               //13-Número de Cuenta Beneficiario
                        //                                    //+ "|"                               //14-Tipo Cadena Pago
                        //                                    //+ "|"                               //15-Certificado Pago
                        //                                    //+ "|"                               //16-Cadena Pago
                        //                                    //+ "|"                               //17-Sello de Pago                                                                                                 
                        //                                    //+ "|"                               //Fin Del Registro
                        //                                    //);

                        //                                    //escrituraFactura += "CPAG20PAGO"    //1-Tipo De Registro
                        //                                    //+ "|" + identpag                    //2-IdentificadorDelPago
                        //                                    //+ "|" + fechapago                   //3-FechaPago                                              
                        //                                    //+ "|"  + formadepagocpag            //4-Forma de pago
                        //                                    //+ "|" + moneda                      //5-Moneda
                        //                                    //+ "|"                               //6-TipoDeCambiocpag
                        //                                    //+ "|" + monto                       //7-Monto
                        //                                    //+ "|"                               //8-NumeroOperacion
                        //                                    //+ "|"                               //9-RFCEmisorCuentaOrdenante
                        //                                    //+ "|"                               //10-Nombre del Banco
                        //                                    //+ "|"                               //11-Número de Cuenta Ordenante
                        //                                    //+ "|"                               //12-RFC Emisor Cuenta Beneficiario
                        //                                    //+ "|"                               //13-Número de Cuenta Beneficiario
                        //                                    //+ "|"                               //14-Tipo Cadena Pago
                        //                                    //+ "|"                               //15-Certificado Pago
                        //                                    //+ "|"                               //16-Cadena Pago
                        //                                    //+ "|"                               //17-Sello de Pago                                                                                                 
                        //                                    //+ "|";                               //Fin Del Registro
                        //                                    // -------------------------- CPAG20DOC ------------------------------------------
                        //                                    //cpagdoc = cpagdoc + ("CPAG20DOC"                       //1-Tipo De Registro
                        //                                    //+ "|" + identpag                                       //2-IdentificadorDelPago
                        //                                    //+ "|" + rowIdent["IdentificadorDelDocumentoPagado"].ToString()                            //3-IdentificadorDelDocumentoPagado                                              
                        //                                    //+ "|" + uid                            //3-IdentificadorDelDocumentoPagado                                              
                        //                                    //+ "|" + serieinvoice                                      //4-Seriecpag
                        //                                    //+ "|" + foliocpag                                      //5-Foliocpag
                        //                                    //+ "|" + monedacpagdoc                                  //6-Monedacpag
                        //                                    //+ "|" + tipocambiocpag                                 //7-TipoCambiocpagdpc Equivalencia                          
                        //                                    //+ "|" + numerodeparcialidad                            //9-NumeroDeParcialidad
                        //                                    //+ "|" + importeSaldoAnterior                           //10-ImporteSaldoAnterior
                        //                                    //+ "|" + importepago                                    //11-ImportePagado                                                  
                        //                                    //+ "|" + importesaldoinsoluto                           //12 ImporteSaldoInsoluto
                        //                                    //+ "| \r\n");


                        //                                    cpagdoc = cpagdoc + ("CPAG20DOC"                                              //1-Tipo De Registro
                        //                                      + "|" + identpag                                       //2-IdentificadorDelPago
                        //                                                                                             //+ "|" + rowIdent["IdentificadorDelDocumentoPagado"].ToString()                            //3-IdentificadorDelDocumentoPagado                                              
                        //                                      + "|" + IdentificadorDelDocumentoPagado                            //3-IdentificadorDelDocumentoPagado                                              
                        //                                      + "|" + serieinvoice                                      //4-Seriecpag
                        //                                      + "|" + folio                                      //5-Foliocpag
                        //                                      + "|" + monedascpadgoc                                  //6-Monedacpag
                        //                                      + "|" + tipodecambiocpag                                 //7-TipoCambiocpagdpc
                        //                                      + "|" + MetdodoPago                             //8-MetodoDePago
                        //                                      + "|" + nparcialidades                            //9-NumeroDeParcialidad
                        //                                      + "|" + interiorsaldoanterior                           //10-ImporteSaldoAnterior
                        //                                      + "|" + ipagado                                    //11-ImportePagado                                                  
                        //                                      + "|" + isaldoinsoluto                           //12 ImporteSaldoInsoluto
                        //                                      + "|" + "02"
                        //                                      + "| \r\n");
                        //                                }




                        //                                //txtFechaIniOP.Text = txtFechaIniOP.Text + "\r\n" + rowIdent["IdentificadorDelDocumentoPagado"].ToString();
                        //                                //txtFechaIniOP.Text = txtFechaIniOP.Text + "\r\n" + uid;
                        //                                //FolioUUIDTxt.Text = FolioUUIDTxt.Text + "\r\n" + "Serie:" + serieinvoice + " Folio:" + folio + " UUID:" + uid;




                        //                            }
                        //                            else
                        //                            {
                        //                                string msg = "Error: Los folios relacionados no existen en el canal de Tralix";
                        //                                formularioT.Visible = false;
                        //                                Div1.Visible = true;
                        //                                ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Error con los folios relacionados ', 'error');setTimeout(function(){window.location.href ='Listado.aspx'}, 10000)", true);

                        //                            }

                        //                        }
                        //                    }
                        //                }
                        //            }
                        //        }

                        //    }// FIN DEL IF SEPARADOS 8
                        //    else
                        //    {
                        //        //CPADOC DESDE GP ----------------------
                        //        DataTable detalleIdent = facLabControler.getDatosCPAGDOC(row["IdentificadorDelPago"].ToString());

                        //        foreach (DataRow rowIdent in detalleIdent.Rows)
                        //        {
                        //            identificaciondpago = rowIdent["IdentificadorDelPago"].ToString();
                        //            folioscpag = Regex.Replace(rowIdent["Foliocpag"].ToString().Replace("SM-", "").Trim(), @"[A-Z]", "");
                        //            importepago = rowIdent["ImportePagado"].ToString();
                        //            importeSaldoAnterior = rowIdent["ImporteSaldoAnterior"].ToString();
                        //            if (importeSaldoAnterior == "") { importeSaldoAnterior = "0.00"; }
                        //            else { importeSaldoAnterior = rowIdent["ImporteSaldoAnterior"].ToString(); }
                        //            importesaldoinsoluto = rowIdent["ImporteSaldoInsoluto"].ToString();
                        //            if (importesaldoinsoluto == "") { importesaldoinsoluto = "0.00"; }
                        //            else { importesaldoinsoluto = rowIdent["ImporteSaldoInsoluto"].ToString(); }
                        //            numerodeparcialidad = rowIdent["NumeroDeParcialidad"].ToString();
                        //            tipocambiocpag = rowIdent["TipodeCambiocpagdpc"].ToString();
                        //            DataTable detalleIdentt = facLabControler.getDatosCPAGDOCTRL(identificaciondpago, folioscpag);
                        //            if (detalleIdentt.Rows.Count > 0)
                        //            {
                        //                foreach (DataRow rowIdentt in detalleIdentt.Rows)
                        //                {
                        //                    iipagado = rowIdentt["ActualApplyToAmount"].ToString();
                        //                    basecalculo = Convert.ToDecimal(iipagado);
                        //                    basecalculado = basecalculo.ToString("F");
                        //                    folio = Regex.Replace(rowIdentt["K3"].ToString().Replace("TDR", "").Trim(), @"[A-Z]", "");

                        //                    impSaldoAnterior = rowIdentt["ORTRXAMT"].ToString();
                        //                    if (impSaldoAnterior == "") { impSaldoAnterior = "0.00"; }
                        //                    else { impSaldoAnterior = rowIdentt["ORTRXAMT"].ToString(); }
                        //                    basecalculo2 = Convert.ToDecimal(impSaldoAnterior);
                        //                    basecalculado2 = basecalculo2.ToString("F");

                        //                    impSaldoInsoluto = rowIdentt["CURTRXAM"].ToString();
                        //                    if (impSaldoInsoluto == "") { impSaldoInsoluto = "0.00"; }
                        //                    else { impSaldoInsoluto = rowIdentt["CURTRXAM"].ToString(); }
                        //                    basecalculo3 = Convert.ToDecimal(impSaldoInsoluto);
                        //                    basecalculado3 = basecalculo3.ToString("F");

                        //                    //txtTotal.Text = importePagos.ToString();
                        //                    //txtTotal.Text = rowIdent["ImportePagado"].ToString();
                        //                    string receptor = txtIdCliente.Text.ToString().Trim();
                        //                    string serieinvoice = "";
                        //                    if (receptor.Equals("LIVERPOL") || receptor.Equals("LIVERDED") || receptor.Equals("ALMLIVER") || receptor.Equals("LIVERTIJ") || receptor.Equals("SFERALIV") || receptor.Equals("GLOBALIV") || receptor.Equals("SETRALIV") || receptor.Equals("FACTUMLV"))
                        //                    {
                        //                        serieinvoice = "TDRL";
                        //                    }
                        //                    else
                        //                    {
                        //                        serieinvoice = rowIdent["Seriecpag"].ToString();
                        //                    }
                        //                    folio = Regex.Replace(rowIdentt["K3"].ToString().Replace("TDR", "").Trim(), @"[A-Z]", "");
                        //                    if (folio.Length == 7 && folio.StartsWith("99"))
                        //                    {
                        //                        folio = folio.Substring(folio.Length - 6, 6);
                        //                    }
                        //                    else if (folio.Length == 8)
                        //                    {
                        //                        folio = folio.Substring(folio.Length - 7, 7);
                        //                    }
                        //                    folio = folio.Replace("-", "");
                        //                    //validar con la serie el id de sucursal-serie

                        //                    MetdodoPago = "";

                        //                    // FILTRO DE LA MASTER APROBADA
                        //                    DataTable datosMaster = facLabControler.getDatosMaster(folio);
                        //                    if (datosMaster.Rows.Count > 0)
                        //                    {

                        //                        foreach (DataRow rowMaster in datosMaster.Rows)
                        //                        {
                        //                            string invoiceMaster = Regex.Replace(rowMaster[0].ToString(), @"[A-Z]", "");
                        //                            folio = invoiceMaster;

                        //                            var request27 = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico=" + invoiceMaster + "&serie=" + serieinvoice);
                        //                            var response27 = (HttpWebResponse)request27.GetResponse();
                        //                            var responseString27 = new StreamReader(response27.GetResponseStream()).ReadToEndAsync();

                        //                            List<ModelFact> separados7 = JsonConvert.DeserializeObject<List<ModelFact>>(await responseString27);
                        //                            if (separados7 != null)
                        //                            {
                        //                                foreach (var item in separados7)
                        //                                {
                        //                                    uid = item.uuid;
                        //                                    serier = item.serie;
                        //                                    folior = item.folio;
                        //                                    uuidpagadas += uid + "\r\n";

                        //                                    Foliosrelacionados += "Serie: " + serier + " " + "Folio: " + folior + " " + "UUID: " + uid + "\r\n";



                        //                                    string UUID = item.xmlDownload;

                        //                                    XmlDocument xDoc = new XmlDocument();
                        //                                    xDoc.Load("https://canal1.xsa.com.mx:9050" + UUID);
                        //                                    var xmlTexto = xDoc.InnerXml.ToString();
                        //                                    DataSet dataSet1 = new DataSet();
                        //                                    XmlTextReader xtr = new XmlTextReader(xDoc.OuterXml, XmlNodeType.Element, null);
                        //                                    dataSet1.ReadXml(xtr);
                        //                                    if (xmlTexto.Contains("MetodoPago=\"PPD\""))
                        //                                    {
                        //                                        MetdodoPago = "PPD";
                        //                                        contadorPPD++;
                        //                                    }
                        //                                    else if (xmlTexto.Contains("MetodoPago=\"PUE\""))
                        //                                    {
                        //                                        txtMetodoPago.Text = "PUE";
                        //                                        MetdodoPago = "PUE";
                        //                                        contadorPUE++;
                        //                                    }
                        //                                    if (MetdodoPago == "PPD")
                        //                                    {
                        //                                        foreach (DataRow rowsr in (InternalDataCollectionBase)dataSet1.Tables["Conceptos"].Rows)
                        //                                        {
                        //                                            foreach (DataRow rowsrc in (InternalDataCollectionBase)dataSet1.Tables["Concepto"].Rows)
                        //                                            {
                        //                                                importe = rowsrc["Importe"].ToString();
                        //                                                valorunitario = rowsrc["ValorUnitario"].ToString();
                        //                                                try
                        //                                                {
                        //                                                    importePagos = importePagos + Convert.ToDecimal(importe);
                        //                                                    importe = importePagos.ToString("F");

                        //                                                    valorunitarios = valorunitarios + Convert.ToDecimal(valorunitario);
                        //                                                    valorunitario = valorunitarios.ToString("F");
                        //                                                }
                        //                                                catch (Exception ex)
                        //                                                {
                        //                                                    string errors = ex.Message;
                        //                                                }
                        //                                                //importe = rowsrc["Importe"].ToString();
                        //                                                //valorunitario = rowsrc["ValorUnitario"].ToString();
                        //                                                //descripcion = rowsrc["Descripcion"].ToString();
                        //                                                //claveunidad = rowsrc["ClaveUnidad"].ToString();
                        //                                                //cantidad = rowsrc["Cantidad"].ToString();
                        //                                                //claveproductoservicio = rowsrc["ClaveProdServ"].ToString();
                        //                                            }
                        //                                        }
                        //                                        foreach (DataRow rowCC in (InternalDataCollectionBase)dataSet1.Tables["Comprobante"].Rows)
                        //                                        {
                        //                                            lugarexpedicion = rowCC["LugarExpedicion"].ToString();
                        //                                            //tipocomprobante = rowCC["TipoDeComprobante"].ToString();
                        //                                            //total = rowCC["Total"].ToString();
                        //                                            monedascpadgoc = rowCC["Moneda"].ToString();
                        //                                            formadepago = rowCC["FormaPago"].ToString();
                        //                                            if (formadepago == null || formadepago == "99") { formadepago = row["Formadepagocpag"].ToString(); }
                        //                                            else { formadepago = row["Formadepagocpag"].ToString(); }
                        //                                            //string Ccertificado = rowCC["Certificado"].ToString();
                        //                                            //string Cnocertificado = rowCC["NoCertificado"].ToString();
                        //                                            //string Csello = rowCC["Sello"].ToString();
                        //                                            //tipocambiocpag = rowCC["TipoCambio"].ToString();
                        //                                            idcomprobante = rowCC["Folio"].ToString();
                        //                                            serie = rowCC["Serie"].ToString();
                        //                                        }
                        //                                        foreach (DataRow rowsr1 in (InternalDataCollectionBase)dataSet1.Tables["Complemento"].Rows)
                        //                                        {
                        //                                            foreach (DataRow rowsrct in (InternalDataCollectionBase)dataSet1.Tables["TimbreFiscalDigital"].Rows)
                        //                                            {
                        //                                                string Trfcprovcertif = rowsrct["RfcProvCertif"].ToString();
                        //                                                string Tsellosat = rowsrct["SelloSAT"].ToString();
                        //                                                string Tsellocfd = rowsrct["SelloCFD"].ToString();
                        //                                                string Tnocertidicadosat = rowsrct["NoCertificadoSAT"].ToString();
                        //                                                Tuuid = rowsrct["UUID"].ToString();
                        //                                                string Tfechatimbrado = rowsrct["FechaTimbrado"].ToString();



                        //                                            }
                        //                                        }
                        //                                        //FolioUUIDTxt.Text += identpag;
                        //                                        try
                        //                                        {
                        //                                            importePagos2 = importePagos2 + Convert.ToDecimal(total);
                        //                                            txtTotal.Text = importePagos2.ToString();
                        //                                        }
                        //                                        catch (Exception ex)
                        //                                        {
                        //                                            string errors = ex.Message;
                        //                                        }

                        //                                        if (monedascpadgoc.Trim() == "USD")
                        //                                        {
                        //                                            try
                        //                                            {
                        //                                                importePagos22 = importePagos22 + Convert.ToDecimal(basecalculado);
                        //                                                txtTotal.Text = importePagos22.ToString();
                        //                                            }
                        //                                            catch (Exception ex)
                        //                                            {
                        //                                                string errors = ex.Message;
                        //                                            }

                        //                                            cpagdoc = cpagdoc + ("CPAG20DOC"                           //1-Tipo De Registro
                        //                                               + "|" + iddelpago                                       //2-IdentificadorDelPago
                        //                                                                                                       //+ "|" + rowIdent["IdentificadorDelDocumentoPagado"].ToString()                            //3-IdentificadorDelDocumentoPagado                                              
                        //                                               + "|" + Tuuid                                            //3-IdentificadorDelDocumentoPagado                                              
                        //                                               + "|" + serieinvoice                                   //4-Seriecpag
                        //                                               + "|" + idcomprobante                                      //5-Foliocpag
                        //                                               + "|" + monedascpadgoc                                  //6-Monedacpag
                        //                                               + "|"  //+ tipocambiocpag                                       //7-TipoCambiocpagdpc

                        //                                               + "|" + numerodeparcialidad                            //9-NumeroDeParcialidad
                        //                                               + "|" + basecalculado.Trim()                                  //10-ImporteSaldoAnterior
                        //                                               + "|" + basecalculado.Trim()                                  //11-ImportePagado                                                  
                        //                                               + "|" + "0"                                            //12 ImporteSaldoInsoluto
                        //                                               + "|" + "01"
                        //                                               + "| \r\n");
                        //                                        }
                        //                                        else
                        //                                        {
                        //                                            cpagdoc = cpagdoc + ("CPAG20DOC"                           //1-Tipo De Registro
                        //                                              + "|" + iddelpago                                       //2-IdentificadorDelPago
                        //                                                                                                      //+ "|" + rowIdent["IdentificadorDelDocumentoPagado"].ToString()                            //3-IdentificadorDelDocumentoPagado                                              
                        //                                              + "|" + Tuuid                                            //3-IdentificadorDelDocumentoPagado                                              
                        //                                              + "|" + serieinvoice                                   //4-Seriecpag
                        //                                              + "|" + idcomprobante                                      //5-Foliocpag
                        //                                              + "|" + monedascpadgoc                                  //6-Monedacpag
                        //                                              + "|" + tipocambiocpag                                             //7-TipoCambiocpagdpc
                        //                                              + "|" + MetdodoPago                             //8-MetodoDePago
                        //                                              + "|" + numerodeparcialidad                            //9-NumeroDeParcialidad
                        //                                              + "|" + basecalculado2                                    //10-ImporteSaldoAnterior
                        //                                              + "|" + basecalculado                                   //11-ImportePagado                                                  
                        //                                              + "|" + basecalculado3                                            //12 ImporteSaldoInsoluto
                        //                                              + "|" + "02"
                        //                                              + "| \r\n");
                        //                                        }
                        //                                    }
                        //                                    else
                        //                                    {
                        //                                        string msg = "Error: Los folios relacionados no existen en el canal de Tralix";
                        //                                        formularioT.Visible = false;
                        //                                        Div1.Visible = true;
                        //                                        ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Error con los folios relacionados ', 'error');setTimeout(function(){window.location.href ='Listado.aspx'}, 10000)", true);

                        //                                    }


                        //                                }
                        //                            }


                        //                        }

                        //                    }

                        //                    else
                        //                    {

                        //                        //3 FILTRO APROBADO
                        //                        //AQUI TERMINA EL IF
                        //                        k1 = rowIdentt["K1"].ToString();

                        //                        k3 = Regex.Replace(rowIdentt["K3"].ToString().Replace("TDRM", "").Trim(), @"[A-Z]", "");
                        //                        iipagado = rowIdentt["ActualApplyToAmount"].ToString();
                        //                        basecalculo = Convert.ToDecimal(iipagado);
                        //                        basecalculado = basecalculo.ToString("F");

                        //                        var request281 = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico=" + k3 + "&rfc=" + txtRFC.Text);
                        //                        var response281 = (HttpWebResponse)request281.GetResponse();
                        //                        var responseString281 = new StreamReader(response281.GetResponseStream()).ReadToEndAsync();

                        //                        List<ModelFact> separados81 = JsonConvert.DeserializeObject<List<ModelFact>>(await responseString281);

                        //                        if (separados81 != null)
                        //                        {
                        //                            contadortralix = 1;
                        //                            foreach (var item in separados81)
                        //                            {
                        //                                uid = item.uuid;
                        //                                serier = item.serie;
                        //                                folior = item.folio;
                        //                                uuidpagadas += uid + "\r\n";

                        //                                Foliosrelacionados += "Serie: " + serier + " " + "Folio: " + folior + " " + "UUID: " + uid + "\r\n";



                        //                                string UUID = item.xmlDownload;

                        //                                XmlDocument xDoc = new XmlDocument();
                        //                                xDoc.Load("https://canal1.xsa.com.mx:9050" + UUID);
                        //                                var xmlTexto = xDoc.InnerXml.ToString();
                        //                                DataSet dataSet1 = new DataSet();
                        //                                XmlTextReader xtr = new XmlTextReader(xDoc.OuterXml, XmlNodeType.Element, null);
                        //                                dataSet1.ReadXml(xtr);
                        //                                if (xmlTexto.Contains("MetodoPago=\"PPD\""))
                        //                                {
                        //                                    MetdodoPago = "PPD";
                        //                                    contadorPPD++;
                        //                                }
                        //                                else if (xmlTexto.Contains("MetodoPago=\"PUE\""))
                        //                                {
                        //                                    txtMetodoPago.Text = "PUE";
                        //                                    MetdodoPago = "PUE";
                        //                                    contadorPUE++;
                        //                                }
                        //                                if (MetdodoPago == "PPD")
                        //                                {
                        //                                    foreach (DataRow rowsr in (InternalDataCollectionBase)dataSet1.Tables["Conceptos"].Rows)
                        //                                    {
                        //                                        foreach (DataRow rowsrc in (InternalDataCollectionBase)dataSet1.Tables["Concepto"].Rows)
                        //                                        {
                        //                                            importe = rowsrc["Importe"].ToString();
                        //                                            valorunitario = rowsrc["ValorUnitario"].ToString();
                        //                                            try
                        //                                            {
                        //                                                importePagos = importePagos + Convert.ToDecimal(importe);
                        //                                                importe = importePagos.ToString("F");

                        //                                                valorunitarios = valorunitarios + Convert.ToDecimal(valorunitario);
                        //                                                valorunitario = valorunitarios.ToString("F");
                        //                                            }
                        //                                            catch (Exception ex)
                        //                                            {
                        //                                                string errors = ex.Message;
                        //                                            }
                        //                                            //descripcion = rowsrc["Descripcion"].ToString();
                        //                                            //claveunidad = rowsrc["ClaveUnidad"].ToString();
                        //                                            //cantidad = rowsrc["Cantidad"].ToString();
                        //                                            //claveproductoservicio = rowsrc["ClaveProdServ"].ToString();
                        //                                        }
                        //                                    }
                        //                                    foreach (DataRow rowCC in (InternalDataCollectionBase)dataSet1.Tables["Comprobante"].Rows)
                        //                                    {
                        //                                        lugarexpedicion = rowCC["LugarExpedicion"].ToString();
                        //                                        //tipocomprobante = rowCC["TipoDeComprobante"].ToString();
                        //                                        //total = rowCC["Total"].ToString();
                        //                                        monedascpadgoc = rowCC["Moneda"].ToString();
                        //                                        formadepago = rowCC["FormaPago"].ToString();
                        //                                        if (formadepago == null || formadepago == "99") { formadepago = row["Formadepagocpag"].ToString(); }
                        //                                        else { formadepago = row["Formadepagocpag"].ToString(); }
                        //                                        //string Ccertificado = rowCC["Certificado"].ToString();
                        //                                        //string Cnocertificado = rowCC["NoCertificado"].ToString();
                        //                                        //string Csello = rowCC["Sello"].ToString();
                        //                                        //tipocambiocpag = rowCC["TipoCambio"].ToString();

                        //                                        idcomprobante = rowCC["Folio"].ToString();
                        //                                        serie = rowCC["Serie"].ToString();
                        //                                    }
                        //                                    foreach (DataRow rowsr1 in (InternalDataCollectionBase)dataSet1.Tables["Complemento"].Rows)
                        //                                    {
                        //                                        foreach (DataRow rowsrct in (InternalDataCollectionBase)dataSet1.Tables["TimbreFiscalDigital"].Rows)
                        //                                        {
                        //                                            string Trfcprovcertif = rowsrct["RfcProvCertif"].ToString();
                        //                                            string Tsellosat = rowsrct["SelloSAT"].ToString();
                        //                                            string Tsellocfd = rowsrct["SelloCFD"].ToString();
                        //                                            string Tnocertidicadosat = rowsrct["NoCertificadoSAT"].ToString();
                        //                                            Tuuid = rowsrct["UUID"].ToString();
                        //                                            string Tfechatimbrado = rowsrct["FechaTimbrado"].ToString();



                        //                                        }
                        //                                    }
                        //                                    //FolioUUIDTxt.Text += identpag;
                        //                                    try
                        //                                    {
                        //                                        importePagos7 = importePagos7 + Convert.ToDecimal(basecalculado);
                        //                                        txtTotal.Text = importePagos7.ToString("F");
                        //                                    }
                        //                                    catch (Exception ex)
                        //                                    {
                        //                                        string errors = ex.Message;
                        //                                    }
                        //                                    if (monedascpadgoc.Trim() == "USD")
                        //                                    {
                        //                                        try
                        //                                        {
                        //                                            importePagos22 = importePagos22 + Convert.ToDecimal(basecalculado);
                        //                                            txtTotal.Text = importePagos22.ToString();
                        //                                        }
                        //                                        catch (Exception ex)
                        //                                        {
                        //                                            string errors = ex.Message;
                        //                                        }

                        //                                        cpagdoc = cpagdoc + ("CPAG20DOC"                           //1-Tipo De Registro
                        //                                              + "|" + iddelpago                                       //2-IdentificadorDelPago
                        //                                                                                                      //+ "|" + rowIdent["IdentificadorDelDocumentoPagado"].ToString()                            //3-IdentificadorDelDocumentoPagado                                              
                        //                                              + "|" + Tuuid                                            //3-IdentificadorDelDocumentoPagado                                              
                        //                                              + "|" + serieinvoice                                   //4-Seriecpag
                        //                                              + "|" + idcomprobante                                      //5-Foliocpag
                        //                                              + "|" + monedascpadgoc                                  //6-Monedacpag
                        //                                              + "|"  //+ tipocambiocpag                                       //7-TipoCambiocpagdpc

                        //                                              + "|" + numerodeparcialidad                            //9-NumeroDeParcialidad
                        //                                              + "|" + basecalculado.Trim()                                  //10-ImporteSaldoAnterior
                        //                                              + "|" + basecalculado.Trim()                                  //11-ImportePagado                                                  
                        //                                              + "|" + "0"                                            //12 ImporteSaldoInsoluto
                        //                                              + "|" + "01"
                        //                                              + "| \r\n");
                        //                                    }
                        //                                    else
                        //                                    {
                        //                                        cpagdoc = cpagdoc + ("CPAG20DOC"                           //1-Tipo De Registro
                        //                                          + "|" + iddelpago.Trim()                                      //2-IdentificadorDelPago
                        //                                                                                                        //+ "|" + rowIdent["IdentificadorDelDocumentoPagado"].ToString()                            //3-IdentificadorDelDocumentoPagado                                              
                        //                                          + "|" + Tuuid.Trim()                                          //3-IdentificadorDelDocumentoPagado                                              
                        //                                          + "|" + serieinvoice.Trim()                                  //4-Seriecpag
                        //                                          + "|" + idcomprobante.Trim()                                     //5-Foliocpag
                        //                                          + "|" + monedascpadgoc.Trim()                                //6-Monedacpag
                        //                                          + "|" + tipocambiocpag                                             //7-TipoCambiocpagdpc
                        //                                          + "|" + MetdodoPago.Trim()                            //8-MetodoDePago
                        //                                          + "|" + numerodeparcialidad.Trim()                            //9-NumeroDeParcialidad
                        //                                          + "|" + basecalculado2.Trim()                                  //10-ImporteSaldoAnterior
                        //                                          + "|" + basecalculado.Trim()                                //11-ImportePagado                                                  
                        //                                          + "|" + basecalculado3.Trim()                                            //12 ImporteSaldoInsoluto
                        //                                          + "|" + "02"
                        //                                          + "| \r\n");
                        //                                    }


                        //                                }
                        //                                //else
                        //                                //{
                        //                                //    string msg = "Error: Los folios relacionados no existen en el canal de Tralix";
                        //                                //    formularioT.Visible = false;
                        //                                //    Div1.Visible = true;
                        //                                //    ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Error con los folios relacionados ', 'error');setTimeout(function(){window.location.href ='Listado.aspx'}, 10000)", true);

                        //                                //}
                        //                                //AQUI FALTA AGREGAR LO QUE TIENE EL XML Y FORMAR EL TXT

                        //                            }

                        //                        }

                        //                        //ME FALTA ESTA DE PROBAR Y GENERAR TXT
                        //                        else
                        //                        {
                        //                            DataTable invoiceresult = facLabControler.getDatosInvoice(k3);
                        //                            if (invoiceresult.Rows.Count > 0)
                        //                            {
                        //                                foreach (DataRow rowInvoice in invoiceresult.Rows)
                        //                                {

                        //                                    norden = rowInvoice["ord_hdrnumber"].ToString();
                        //                                    DataTable segmentoresult = facLabControler.getDatosSegmentos(norden);
                        //                                    foreach (DataRow seg in segmentoresult.Rows)
                        //                                    {
                        //                                        string Segmento = seg["Segmento"].ToString();

                        //                                        var request2819 = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico=" + Segmento + "&rfc=" + txtRFC.Text);
                        //                                        var response2819 = (HttpWebResponse)request2819.GetResponse();
                        //                                        var responseString2819 = new StreamReader(response2819.GetResponseStream()).ReadToEndAsync();

                        //                                        List<ModelFact> separados819 = JsonConvert.DeserializeObject<List<ModelFact>>(await responseString2819);

                        //                                        if (separados819 != null)
                        //                                        {
                        //                                            foreach (var item in separados819)
                        //                                            {
                        //                                                string uuid = item.uuid;
                        //                                                string xmld = item.xmlDownload;
                        //                                                serieinvoice = item.serie;

                        //                                                uid = item.uuid;
                        //                                                serier = item.serie;
                        //                                                folior = item.folio;
                        //                                                uuidpagadas += uid + "\r\n";

                        //                                                Foliosrelacionados += "Serie: " + serier + " " + "Folio: " + folior + " " + "UUID: " + uid + "\r\n";

                        //                                                XmlDocument xDoc = new XmlDocument();
                        //                                                xDoc.Load("https://canal1.xsa.com.mx:9050" + xmld);
                        //                                                var xmlTexto = xDoc.InnerXml.ToString();
                        //                                                DataSet dataSet1 = new DataSet();
                        //                                                XmlTextReader xtr = new XmlTextReader(xDoc.OuterXml, XmlNodeType.Element, null);
                        //                                                dataSet1.ReadXml(xtr);

                        //                                                if (xmlTexto.Contains("MetodoPago=\"PPD\""))
                        //                                                {
                        //                                                    MetdodoPago = "PPD";
                        //                                                    contadorPPD++;
                        //                                                }
                        //                                                else if (xmlTexto.Contains("MetodoPago=\"PUE\""))
                        //                                                {
                        //                                                    txtMetodoPago.Text = "PUE";
                        //                                                    MetdodoPago = "PUE";
                        //                                                    contadorPUE++;
                        //                                                }
                        //                                                if (MetdodoPago == "PPD")
                        //                                                {

                        //                                                    foreach (DataRow rowsr in (InternalDataCollectionBase)dataSet1.Tables["Conceptos"].Rows)
                        //                                                    {
                        //                                                        foreach (DataRow rowsrc in (InternalDataCollectionBase)dataSet1.Tables["Concepto"].Rows)
                        //                                                        {
                        //                                                            importe = rowsrc["Importe"].ToString();
                        //                                                            valorunitario = rowsrc["ValorUnitario"].ToString();
                        //                                                            try
                        //                                                            {
                        //                                                                importePagos3 = importePagos3 + Convert.ToDecimal(importe);
                        //                                                                importe = importePagos3.ToString("F");

                        //                                                                valorunitarios = valorunitarios + Convert.ToDecimal(valorunitario);
                        //                                                                valorunitario = valorunitarios.ToString("F");
                        //                                                            }
                        //                                                            catch (Exception ex)
                        //                                                            {
                        //                                                                string errors = ex.Message;
                        //                                                            }

                        //                                                            //descripcion = rowsrc["Descripcion"].ToString();
                        //                                                            //claveunidad = rowsrc["ClaveUnidad"].ToString();
                        //                                                            //cantidad = rowsrc["Cantidad"].ToString();
                        //                                                            //claveproductoservicio = rowsrc["ClaveProdServ"].ToString();
                        //                                                        }
                        //                                                    }
                        //                                                    foreach (DataRow rowCC in (InternalDataCollectionBase)dataSet1.Tables["Comprobante"].Rows)
                        //                                                    {
                        //                                                        lugarexpedicion = rowCC["LugarExpedicion"].ToString();
                        //                                                        //tipocomprobante = rowCC["TipoDeComprobante"].ToString();
                        //                                                        total = rowCC["Total"].ToString();
                        //                                                        monedascpadgoc = rowCC["Moneda"].ToString();
                        //                                                        formadepago = rowCC["FormaPago"].ToString();
                        //                                                        if (formadepago == null || formadepago == "99") { formadepago = row["Formadepagocpag"].ToString(); }
                        //                                                        else { formadepago = row["Formadepagocpag"].ToString(); }
                        //                                                        //string Ccertificado = rowCC["Certificado"].ToString();
                        //                                                        //string Cnocertificado = rowCC["NoCertificado"].ToString();
                        //                                                        //string Csello = rowCC["Sello"].ToString();
                        //                                                        //tipocambiocpag = rowCC["TipoCambio"].ToString();
                        //                                                        idcomprobante = rowCC["Folio"].ToString();
                        //                                                        serie = rowCC["Serie"].ToString();
                        //                                                    }
                        //                                                    foreach (DataRow rowsr1 in (InternalDataCollectionBase)dataSet1.Tables["Complemento"].Rows)
                        //                                                    {
                        //                                                        foreach (DataRow rowsrct in (InternalDataCollectionBase)dataSet1.Tables["TimbreFiscalDigital"].Rows)
                        //                                                        {
                        //                                                            string Trfcprovcertif = rowsrct["RfcProvCertif"].ToString();
                        //                                                            string Tsellosat = rowsrct["SelloSAT"].ToString();
                        //                                                            string Tsellocfd = rowsrct["SelloCFD"].ToString();
                        //                                                            string Tnocertidicadosat = rowsrct["NoCertificadoSAT"].ToString();
                        //                                                            Tuuid = rowsrct["UUID"].ToString();
                        //                                                            string Tfechatimbrado = rowsrct["FechaTimbrado"].ToString();



                        //                                                        }
                        //                                                    }
                        //                                                    //FolioUUIDTxt.Text += identpag;
                        //                                                    try
                        //                                                    {
                        //                                                        importePagos4 = importePagos4 + Convert.ToDecimal(basecalculado);
                        //                                                        txtTotal.Text = importePagos4.ToString();
                        //                                                    }
                        //                                                    catch (Exception ex)
                        //                                                    {
                        //                                                        string errors = ex.Message;
                        //                                                    }
                        //                                                    if (monedascpadgoc.Trim() == "USD")
                        //                                                    {
                        //                                                        try
                        //                                                        {
                        //                                                            importePagos22 = importePagos22 + Convert.ToDecimal(basecalculado);
                        //                                                            txtTotal.Text = importePagos22.ToString();
                        //                                                        }
                        //                                                        catch (Exception ex)
                        //                                                        {
                        //                                                            string errors = ex.Message;
                        //                                                        }

                        //                                                        cpagdoc = cpagdoc + ("CPAG20DOC"                           //1-Tipo De Registro
                        //                                                          + "|" + iddelpago                                       //2-IdentificadorDelPago
                        //                                                                                                                  //+ "|" + rowIdent["IdentificadorDelDocumentoPagado"].ToString()                            //3-IdentificadorDelDocumentoPagado                                              
                        //                                                          + "|" + Tuuid                                            //3-IdentificadorDelDocumentoPagado                                              
                        //                                                          + "|" + serieinvoice                                   //4-Seriecpag
                        //                                                          + "|" + idcomprobante                                      //5-Foliocpag
                        //                                                          + "|" + monedascpadgoc                                  //6-Monedacpag
                        //                                                          + "|"  //+ tipocambiocpag                                       //7-TipoCambiocpagdpc

                        //                                                          + "|" + numerodeparcialidad                            //9-NumeroDeParcialidad
                        //                                                          + "|" + basecalculado.Trim()                                  //10-ImporteSaldoAnterior
                        //                                                          + "|" + basecalculado.Trim()                                  //11-ImportePagado                                                  
                        //                                                          + "|" + "0"                                            //12 ImporteSaldoInsoluto
                        //                                                          + "|" + "01"
                        //                                                          + "| \r\n");
                        //                                                    }
                        //                                                    else
                        //                                                    {
                        //                                                        cpagdoc = cpagdoc + ("CPAG20DOC"                           //1-Tipo De Registro
                        //                                                          + "|" + iddelpago.Trim()                                    //2-IdentificadorDelPago
                        //                                                                                                                      //+ "|" + rowIdent["IdentificadorDelDocumentoPagado"].ToString()                            //3-IdentificadorDelDocumentoPagado                                              
                        //                                                          + "|" + Tuuid.Trim()                                         //3-IdentificadorDelDocumentoPagado                                              
                        //                                                          + "|" + serieinvoice.Trim()                                 //4-Seriecpag
                        //                                                          + "|" + idcomprobante.Trim()                                    //5-Foliocpag
                        //                                                          + "|" + monedascpadgoc.Trim()                                 //6-Monedacpag
                        //                                                          + "|" + tipocambiocpag                                             //7-TipoCambiocpagdpc
                        //                                                          + "|" + MetdodoPago.Trim()                            //8-MetodoDePago
                        //                                                          + "|" + numerodeparcialidad.Trim()                          //9-NumeroDeParcialidad
                        //                                                          + "|" + basecalculado.Trim()                                   //10-ImporteSaldoAnterior
                        //                                                          + "|" + basecalculado.Trim()                                  //11-ImportePagado                                                  
                        //                                                          + "|" + basecalculado.Trim()                                            //12 ImporteSaldoInsoluto
                        //                                                          + "|" + "02"
                        //                                                          + "| \r\n");
                        //                                                    }

                        //                                                }
                        //                                            }
                        //                                        }
                        //                                    }
                        //                                }

                        //                            }

                        //                        }
                        //                        if (uid == "" && serieinvoice == "TDRA")
                        //                        {
                        //                            var request23 = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico=" + folio + "&serie=" + "SAEM");
                        //                            var response23 = (HttpWebResponse)request23.GetResponse();
                        //                            var responseString23 = new StreamReader(response23.GetResponseStream()).ReadToEndAsync();

                        //                            List<ModelFact> separados23 = JsonConvert.DeserializeObject<List<ModelFact>>(await responseString23);

                        //                            foreach (var item23 in separados23)
                        //                            {
                        //                                uid = item23.uuid;


                        //                                string UUID = item23.xmlDownload;

                        //                                XmlDocument xDoc = new XmlDocument();
                        //                                xDoc.Load("https://canal1.xsa.com.mx:9050" + UUID);
                        //                                var xmlTexto = xDoc.InnerXml.ToString();
                        //                                DataSet dataSet1 = new DataSet();
                        //                                XmlTextReader xtr = new XmlTextReader(xDoc.OuterXml, XmlNodeType.Element, null);
                        //                                dataSet1.ReadXml(xtr);
                        //                                if (xmlTexto.Contains("MetodoPago=\"PPD\""))
                        //                                {
                        //                                    MetdodoPago = "PPD";
                        //                                    contadorPPD++;
                        //                                }
                        //                                else if (xmlTexto.Contains("MetodoPago=\"PUE\""))
                        //                                {
                        //                                    txtMetodoPago.Text = "PUE";
                        //                                    MetdodoPago = "PUE";
                        //                                    contadorPUE++;
                        //                                }

                        //                            }
                        //                        }

                        //                    }






                        //                }
                        //            }
                        //        }

                        //        decimal totalds = importePagos2 + importePagos7 + importePagos4;
                        //        txtTotal.Text = totalds.ToString();

                        //        //AQUI TERMINA GP ---------------------




                        //        //CPAGDOC-----------------------------------------------------------------------------------------------------------------------
                        //        //DataTable detalleIdent = facLabControler.getDatosCPAGDOC(row["IdentificadorDelPago"].ToString());

                        //        //foreach (DataRow rowIdent in detalleIdent.Rows)
                        //        //{
                        //        //    folio = Regex.Replace(rowIdent["Foliocpag"].ToString().Replace("TDR", "").Trim(), @"[A-Z]", "");

                        //        //    //txtTotal.Text = importePagos.ToString();
                        //        //    //txtTotal.Text = rowIdent["ImportePagado"].ToString();
                        //        //    string receptor = txtIdCliente.Text.ToString().Trim();
                        //        //    string serieinvoice = "";
                        //        //    if (receptor.Equals("LIVERPOL") || receptor.Equals("LIVERDED") || receptor.Equals("ALMLIVER") || receptor.Equals("LIVERTIJ") || receptor.Equals("SFERALIV") || receptor.Equals("GLOBALIV") || receptor.Equals("SETRALIV") || receptor.Equals("FACTUMLV"))
                        //        //    {
                        //        //        serieinvoice = "TDRL";
                        //        //    }
                        //        //    else
                        //        //    {
                        //        //        serieinvoice = rowIdent["Seriecpag"].ToString();
                        //        //    }
                        //        //    folio = Regex.Replace(rowIdent["Foliocpag"].ToString().Replace("TDR", "").Trim(), @"[A-Z]", "");
                        //        //    if (folio.Length == 7 && folio.StartsWith("99"))
                        //        //    {
                        //        //        folio = folio.Substring(folio.Length - 6, 6);
                        //        //    }
                        //        //    else if (folio.Length == 8)
                        //        //    {
                        //        //        folio = folio.Substring(folio.Length - 7, 7);
                        //        //    }
                        //        //    folio = folio.Replace("-", "");
                        //        //    //validar con la serie el id de sucursal-serie

                        //        //    MetdodoPago = "";





                        //        //    DataTable datosMaster = facLabControler.getDatosMaster(folio);
                        //        //    if (datosMaster.Rows.Count > 0)
                        //        //    {

                        //        //        foreach (DataRow rowMaster in datosMaster.Rows)
                        //        //        {
                        //        //            string invoiceMaster = Regex.Replace(rowMaster[0].ToString(), @"[A-Z]", "");
                        //        //            folio = invoiceMaster;

                        //        //            var request27 = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico=" + invoiceMaster + "&serie=" + serieinvoice);
                        //        //            var response27 = (HttpWebResponse)request27.GetResponse();
                        //        //            var responseString27 = new StreamReader(response27.GetResponseStream()).ReadToEnd();

                        //        //            List<ModelFact> separados7 = JsonConvert.DeserializeObject<List<ModelFact>>(responseString27);
                        //        //            foreach (var item in separados7)
                        //        //            {



                        //        //                uid = item.uuid;
                        //        //                serier = item.serie;
                        //        //                folior = item.folio;
                        //        //                uuidpagadas += uid + "\r\n";

                        //        //                Foliosrelacionados += "Serie: " + serier + " " + "Folio: " + folior + " " + "UUID: " + uid + "\r\n";

                        //        //                if (serieinvoice != "TDRL")
                        //        //                {
                        //        //                    string UUID = item.xmlDownload;

                        //        //                    XmlDocument xDoc = new XmlDocument();
                        //        //                    xDoc.Load("https://canal1.xsa.com.mx:9050" + UUID);
                        //        //                    var xmlTexto = xDoc.InnerXml.ToString();
                        //        //                    DataSet dataSet1 = new DataSet();
                        //        //                    XmlTextReader xtr = new XmlTextReader(xDoc.OuterXml, XmlNodeType.Element, null);
                        //        //                    dataSet1.ReadXml(xtr);
                        //        //                    if (xmlTexto.Contains("MetodoPago=\"PPD\""))
                        //        //                    {
                        //        //                        MetdodoPago = "PPD";
                        //        //                        contadorPPD++;
                        //        //                    }
                        //        //                    else if (xmlTexto.Contains("MetodoPago=\"PUE\""))
                        //        //                    {
                        //        //                        txtMetodoPago.Text = "PUE";
                        //        //                        MetdodoPago = "PUE";
                        //        //                        contadorPUE++;
                        //        //                    }
                        //        //                }
                        //        //            }

                        //        //        }
                        //        //    }
                        //        //    else
                        //        //    {
                        //        //        //INICIO DE CODIGO

                        //        //        var request2 = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico=" + folio + "&serie=" + serieinvoice);
                        //        //        var response2 = (HttpWebResponse)request2.GetResponse();
                        //        //        var responseString2 = new StreamReader(response2.GetResponseStream()).ReadToEnd();

                        //        //        List<ModelFact> separados = JsonConvert.DeserializeObject<List<ModelFact>>(responseString2);
                        //        //        //PONER UNA CONDICION POR SI SEPADOS ES NULL
                        //        //        if (separados != null)
                        //        //        {
                        //        //            foreach (var item in separados)
                        //        //            {



                        //        //                uid = item.uuid;
                        //        //                serier = item.serie;
                        //        //                folior = item.folio;
                        //        //                uuidpagadas += uid + "\r\n";
                        //        //                Foliosrelacionados += "Serie: " + serier + " " + "Folio: " + folior + " " + "UUID: " + uid + "\r\n";
                        //        //                if (serieinvoice != "TDRL")
                        //        //                {
                        //        //                    string UUID = item.xmlDownload;

                        //        //                    XmlDocument xDoc = new XmlDocument();
                        //        //                    xDoc.Load("https://canal1.xsa.com.mx:9050" + UUID);
                        //        //                    var xmlTexto = xDoc.InnerXml.ToString();
                        //        //                    DataSet dataSet1 = new DataSet();
                        //        //                    XmlTextReader xtr = new XmlTextReader(xDoc.OuterXml, XmlNodeType.Element, null);
                        //        //                    dataSet1.ReadXml(xtr);
                        //        //                    if (xmlTexto.Contains("MetodoPago=\"PPD\""))
                        //        //                    {
                        //        //                        MetdodoPago = "PPD";
                        //        //                        contadorPPD++;
                        //        //                    }
                        //        //                    else if (xmlTexto.Contains("MetodoPago=\"PUE\""))
                        //        //                    {
                        //        //                        txtMetodoPago.Text = "PUE";
                        //        //                        MetdodoPago = "PUE";
                        //        //                        contadorPUE++;
                        //        //                    }
                        //        //                }
                        //        //            }

                        //        //        }





                        //        //        // FIN DE MI CODIGO 

                        //        //        if (uid == "" && serieinvoice == "TDRA")
                        //        //        {
                        //        //            var request23 = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico=" + folio + "&serie=" + "SAEM");
                        //        //            var response23 = (HttpWebResponse)request23.GetResponse();
                        //        //            var responseString23 = new StreamReader(response23.GetResponseStream()).ReadToEnd();

                        //        //            List<ModelFact> separados23 = JsonConvert.DeserializeObject<List<ModelFact>>(responseString2);

                        //        //            foreach (var item23 in separados23)
                        //        //            {
                        //        //                uid = item23.uuid;
                        //        //                if (serieinvoice != "TDRL")
                        //        //                {
                        //        //                    string UUID = item23.xmlDownload;

                        //        //                    XmlDocument xDoc = new XmlDocument();
                        //        //                    xDoc.Load("https://canal1.xsa.com.mx:9050" + UUID);
                        //        //                    var xmlTexto = xDoc.InnerXml.ToString();
                        //        //                    DataSet dataSet1 = new DataSet();
                        //        //                    XmlTextReader xtr = new XmlTextReader(xDoc.OuterXml, XmlNodeType.Element, null);
                        //        //                    dataSet1.ReadXml(xtr);
                        //        //                    if (xmlTexto.Contains("MetodoPago=\"PPD\""))
                        //        //                    {
                        //        //                        MetdodoPago = "PPD";
                        //        //                        contadorPPD++;
                        //        //                    }
                        //        //                    else if (xmlTexto.Contains("MetodoPago=\"PUE\""))
                        //        //                    {
                        //        //                        txtMetodoPago.Text = "PUE";
                        //        //                        MetdodoPago = "PUE";
                        //        //                        contadorPUE++;
                        //        //                    }
                        //        //                }
                        //        //            }
                        //        //        }

                        //        //    }

                        //        //    if (MetdodoPago == "PPD")
                        //        //    {

                        //        //        identpag = rowIdent["IdentificadorDelPago"].ToString();
                        //        //        //txtFechaIniOP.Text = "\r\n" +rowIdent["IdentificadorDelDocumentoPagado"].ToString();
                        //        //        seriecpag = rowIdent["Seriecpag"].ToString();
                        //        //        foliocpag = rowIdent["Foliocpag"].ToString();
                        //        //        monedacpagdoc = rowIdent["Monedacpagdoc"].ToString();
                        //        //        tipocambiocpag = rowIdent["TipodeCambiocpagdpc"].ToString();
                        //        //        txtMetodoPago.Text = rowIdent["MedotoDePago"].ToString();
                        //        //        numerodeparcialidad = rowIdent["NumeroDeParcialidad"].ToString();
                        //        //        importeSaldoAnterior = rowIdent["ImporteSaldoAnterior"].ToString();
                        //        //        importepago = rowIdent["ImportePagado"].ToString();
                        //        //        importesaldoinsoluto = rowIdent["ImporteSaldoInsoluto"].ToString();
                        //        //        //FolioUUIDTxt.Text += identpag;
                        //        //        try
                        //        //        {
                        //        //            importePagos = importePagos + Convert.ToDecimal(importepago);
                        //        //            txtTotal.Text = importePagos.ToString();
                        //        //        }
                        //        //        catch (Exception ex)
                        //        //        {
                        //        //            string errors = ex.Message;
                        //        //        }

                        //        //        //txtFechaIniOP.Text = txtFechaIniOP.Text + "\r\n" + rowIdent["IdentificadorDelDocumentoPagado"].ToString();
                        //        //        //txtFechaIniOP.Text = txtFechaIniOP.Text + "\r\n" + uid;
                        //        //        //FolioUUIDTxt.Text = FolioUUIDTxt.Text + "\r\n" + "Serie:" + serieinvoice + " Folio:" + folio + " UUID:" + uid;



                        //        //        if (monedacpagdoc.Trim() == "USD")
                        //        //        {
                        //        //            cpagdoc = cpagdoc + ("CPAGDOC"                           //1-Tipo De Registro
                        //        //              + "|" + identpag                                       //2-IdentificadorDelPago
                        //        //                                                                     //+ "|" + rowIdent["IdentificadorDelDocumentoPagado"].ToString()                            //3-IdentificadorDelDocumentoPagado                                              
                        //        //              + "|" + uid                                            //3-IdentificadorDelDocumentoPagado                                              
                        //        //              + "|" + serieinvoice                                   //4-Seriecpag
                        //        //              + "|" + foliocpag                                      //5-Foliocpag
                        //        //              + "|" + monedacpagdoc                                  //6-Monedacpag
                        //        //              + "|" + ""                                             //7-TipoCambiocpagdpc
                        //        //              + "|" + txtMetodoPago.Text                             //8-MetodoDePago
                        //        //              + "|" + numerodeparcialidad                            //9-NumeroDeParcialidad
                        //        //              + "|" + importepago                                    //10-ImporteSaldoAnterior
                        //        //              + "|" + importepago                                    //11-ImportePagado                                                  
                        //        //              + "|" + "0"                                            //12 ImporteSaldoInsoluto
                        //        //              + "| \r\n");
                        //        //        }
                        //        //        else
                        //        //        {
                        //        //            //----------------------------------------Seccion CPAG20PAGO -------------------------------------------------------------------

                        //        //            //CPAG20PAGO (1:N)
                        //        //            //escritor.WriteLine(
                        //        //            //"CPAG20PAGO"                        //1-Tipo De Registro
                        //        //            //+ "|" + identpag                    //2-IdentificadorDelPago
                        //        //            //+ "|" + fechapago                   //3-FechaPago                                              
                        //        //            //+ "|"  + formadepagocpag            //4-Forma de pago
                        //        //            //+ "|" + moneda                      //5-Moneda
                        //        //            //+ "|"                               //6-TipoDeCambiocpag
                        //        //            //+ "|" + monto                       //7-Monto
                        //        //            //+ "|"                               //8-NumeroOperacion
                        //        //            //+ "|"                               //9-RFCEmisorCuentaOrdenante
                        //        //            //+ "|"                               //10-Nombre del Banco
                        //        //            //+ "|"                               //11-Número de Cuenta Ordenante
                        //        //            //+ "|"                               //12-RFC Emisor Cuenta Beneficiario
                        //        //            //+ "|"                               //13-Número de Cuenta Beneficiario
                        //        //            //+ "|"                               //14-Tipo Cadena Pago
                        //        //            //+ "|"                               //15-Certificado Pago
                        //        //            //+ "|"                               //16-Cadena Pago
                        //        //            //+ "|"                               //17-Sello de Pago                                                                                                 
                        //        //            //+ "|"                               //Fin Del Registro
                        //        //            //);

                        //        //            //escrituraFactura += "CPAG20PAGO"    //1-Tipo De Registro
                        //        //            //+ "|" + identpag                    //2-IdentificadorDelPago
                        //        //            //+ "|" + fechapago                   //3-FechaPago                                              
                        //        //            //+ "|"  + formadepagocpag            //4-Forma de pago
                        //        //            //+ "|" + moneda                      //5-Moneda
                        //        //            //+ "|"                               //6-TipoDeCambiocpag
                        //        //            //+ "|" + monto                       //7-Monto
                        //        //            //+ "|"                               //8-NumeroOperacion
                        //        //            //+ "|"                               //9-RFCEmisorCuentaOrdenante
                        //        //            //+ "|"                               //10-Nombre del Banco
                        //        //            //+ "|"                               //11-Número de Cuenta Ordenante
                        //        //            //+ "|"                               //12-RFC Emisor Cuenta Beneficiario
                        //        //            //+ "|"                               //13-Número de Cuenta Beneficiario
                        //        //            //+ "|"                               //14-Tipo Cadena Pago
                        //        //            //+ "|"                               //15-Certificado Pago
                        //        //            //+ "|"                               //16-Cadena Pago
                        //        //            //+ "|"                               //17-Sello de Pago                                                                                                 
                        //        //            //+ "|";                               //Fin Del Registro
                        //        //            // -------------------------- CPAG20DOC ------------------------------------------
                        //        //            //cpagdoc = cpagdoc + ("CPAG20DOC"                       //1-Tipo De Registro
                        //        //            //+ "|" + identpag                                       //2-IdentificadorDelPago
                        //        //            //+ "|" + rowIdent["IdentificadorDelDocumentoPagado"].ToString()                            //3-IdentificadorDelDocumentoPagado                                              
                        //        //            //+ "|" + uid                            //3-IdentificadorDelDocumentoPagado                                              
                        //        //            //+ "|" + serieinvoice                                      //4-Seriecpag
                        //        //            //+ "|" + foliocpag                                      //5-Foliocpag
                        //        //            //+ "|" + monedacpagdoc                                  //6-Monedacpag
                        //        //            //+ "|" + tipocambiocpag                                 //7-TipoCambiocpagdpc Equivalencia                          
                        //        //            //+ "|" + numerodeparcialidad                            //9-NumeroDeParcialidad
                        //        //            //+ "|" + importeSaldoAnterior                           //10-ImporteSaldoAnterior
                        //        //            //+ "|" + importepago                                    //11-ImportePagado                                                  
                        //        //            //+ "|" + importesaldoinsoluto                           //12 ImporteSaldoInsoluto
                        //        //            //+ "| \r\n");


                        //        //            cpagdoc = cpagdoc + ("CPAGDOC"                                              //1-Tipo De Registro
                        //        //              + "|" + identpag                                       //2-IdentificadorDelPago
                        //        //                                                                     //+ "|" + rowIdent["IdentificadorDelDocumentoPagado"].ToString()                            //3-IdentificadorDelDocumentoPagado                                              
                        //        //              + "|" + uid                            //3-IdentificadorDelDocumentoPagado                                              
                        //        //              + "|" + serieinvoice                                      //4-Seriecpag
                        //        //              + "|" + foliocpag                                      //5-Foliocpag
                        //        //              + "|" + monedacpagdoc                                  //6-Monedacpag
                        //        //              + "|" + tipocambiocpag                                 //7-TipoCambiocpagdpc
                        //        //              + "|" + txtMetodoPago.Text                             //8-MetodoDePago
                        //        //              + "|" + numerodeparcialidad                            //9-NumeroDeParcialidad
                        //        //              + "|" + importeSaldoAnterior                           //10-ImporteSaldoAnterior
                        //        //              + "|" + importepago                                    //11-ImportePagado                                                  
                        //        //              + "|" + importesaldoinsoluto                           //12 ImporteSaldoInsoluto
                        //        //              + "| \r\n");
                        //        //        }
                        //        //    }
                        //        //    //else
                        //        //    //{
                        //        //    //    string msg = "Error: Los folios relacionados no existen en el canal de Tralix";
                        //        //    //    formularioT.Visible = false;
                        //        //    //    Div1.Visible = true;
                        //        //    //    ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Error con los folios relacionados ', 'error');setTimeout(function(){window.location.href ='Listado.aspx'}, 10000)", true);

                        //        //    //}

                        //        //}


                        //    }
                        //}


                    }



                    //AQUI VOY -------------------------------


                    if (contadorPPD == 0 && contadorPUE > 0)
                    {
                        string msg = "¡La factura es PUE!, es libre de todo PPD";

                        Div1.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'La factura es PUE ', 'success');setTimeout(function(){window.location.href ='Listado.aspx'}, 10000)", true);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('La factura es PUE!! y es libre de todo PPD', 'success');", true);
                        //PopupMsg.Message1 = "La factura es PUE!! y es libre de todo PPD";
                        //PopupMsg.ShowPopUp(0);
                    }
                    else
                    {
                        var uuidpagadas2 = uuidpagadas;

                        if (uuidpagadas2 == null)
                        {
                            string msg = "Error: El o los folios relacionados no existen en el canal de Tralix";
                            formularioT.Visible = false;
                            Div1.Visible = true;
                            ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Error con los folios relacionados ', 'error');setTimeout(function(){window.location.href ='Listado.aspx'}, 10000)", true);
                        }
                        else
                        {


                            txtFechaIniOP.Text = uuidpagadas;
                            FolioUUIDTxt.Text = Foliosrelacionados;

                            txtFechaHasta.Text = "Complemento Pago";


                            txtFechaDesde.Text = "Complemento Pago";


                            txtTipoCobro.Text = "Complemento Pago";
                            
                        }
                    }


                    //OTROS-------------------------------------------------------------------------------------------------------------------------

                    // creamos el FolioUUID



                }
            }


            //AQUI TERMINA TXT PRODUCCIION

        }

        public void generaTXT2()
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

            string path = System.Web.Configuration.WebConfigurationManager.AppSettings["dir2"] + lblFact.Text + ".txt";

            using (System.IO.StreamWriter escritor = new System.IO.StreamWriter(path))




            {
                string f1 = txtFechaDesde.Text;
                string factura = txtFolio.Text;


                //----------------------------------------Seccion De Datos Generales del CFDI-----------------------------------------------------------------------------------------
                //string f01 = "01"   // LISTO

                //01 INFORMACION GENERAL DEL CFDI (1:1)
                if (formadepago.Trim() != "02")
                {
                    escritor.WriteLine(
                    "01"                           //1.-Tipo De Registro
                    + "|" + folioe.Trim()         //2.- IdComprobante
                    + "|" + seriee.Trim()         //3.- Serie
                    + "|" + folioe.Trim()         //4.- Folio  
                    + "|" + fechaemision.Trim()  //5.- FechayHoraEmision 
                    + "|" + subt.Trim()          //6.- Subtotal
                    + "|"                        //7.- TotalImupuestosTrasladados
                    + "|"                        //8.- TotalImpuestosRetenidos
                    + "|"                        //9.- Descuentos
                    + "|" + total.Trim()         //10.- Total
                    + "|"                        //11.- TotalconLetra
                    + "|"                        //12.- FormadePago
                    + "|"                        //13.- CondicionesdePago
                    + "|"                          //14.-  MetododePago
                    + "|" + txtMoneda.Text.Trim() //15.- Moneda
                    + "|"     //16.- Tipodecambio + "|" + tipodecambiocpag.Trim()   
                    + "|" + tipocomprobante.Trim() //17.- TipodeComprobante
                    + "|" + lugarexpedicion.Trim() //18.- LugardeExpedicion
                    + "|" + usocfdi.Trim()         //19.- UsoCfdi
                    + "|"                          //20.- Confirmacion
                    + "|"                          //21.- Referencia
                    + "|"                           //22.- Nota1
                    + "|"                           //23.- Nota2
                    + "|"                           //24.- Nota 3
                    + "|"                     //25.- Exportacion 
                    + "|" + "01"                          //26.- FacAtrAquiriente
                    + "|"
                    );
                    escrituraFactura += "01"                                                //1.-Tipo De Registro
                       + "|" + folioe.Trim()         //2.- IdComprobante
                    + "|" + seriee.Trim()         //3.- Serie
                    + "|" + folioe.Trim()         //4.- Folio  
                    + "|" + fechaemision.Trim()  //5.- FechayHoraEmision 
                    + "|" + subt.Trim()          //6.- Subtotal
                    + "|"                        //7.- TotalImupuestosTrasladados
                    + "|"                        //8.- TotalImpuestosRetenidos
                    + "|"                        //9.- Descuentos
                    + "|" + total.Trim()         //10.- Total
                    + "|"                        //11.- TotalconLetra
                    + "|"                        //12.- FormadePago
                    + "|"                        //13.- CondicionesdePago
                    + "|"                          //14.-  MetododePago
                    + "|" + txtMoneda.Text.Trim() //15.- Moneda
                    + "|"     //16.- Tipodecambio + "|" + tipodecambiocpag.Trim()   
                    + "|" + tipocomprobante.Trim() //17.- TipodeComprobante
                    + "|" + lugarexpedicion.Trim() //18.- LugardeExpedicion
                    + "|" + usocfdi.Trim()         //19.- UsoCfdi
                    + "|"                          //20.- Confirmacion
                    + "|"                          //21.- Referencia
                    + "|"                           //22.- Nota1
                    + "|"                           //23.- Nota2
                    + "|"                           //24.- Nota 3
                    + "|"                     //25.- Exportacion 
                    + "|" + "01"                          //26.- FacAtrAquiriente
                    + "|";
                }
                else
                {
                    escritor.WriteLine(
                   "01"                                                //1.-Tipo De Registro
                     + "|" + folioe.Trim()         //2.- IdComprobante
                    + "|" + seriee.Trim()         //3.- Serie
                    + "|" + folioe.Trim()         //4.- Folio  
                    + "|" + fechaemision.Trim()  //5.- FechayHoraEmision 
                    + "|" + subt.Trim()          //6.- Subtotal
                    + "|"                        //7.- TotalImupuestosTrasladados
                    + "|"                        //8.- TotalImpuestosRetenidos
                    + "|"                        //9.- Descuentos
                    + "|" + total.Trim()         //10.- Total
                    + "|"                        //11.- TotalconLetra
                    + "|"                        //12.- FormadePago
                    + "|"                        //13.- CondicionesdePago
                    + "|"                          //14.-  MetododePago
                    + "|" + txtMoneda.Text.Trim() //15.- Moneda
                    + "|"     //16.- Tipodecambio + "|" + tipodecambiocpag.Trim()   
                    + "|" + tipocomprobante.Trim() //17.- TipodeComprobante
                    + "|" + lugarexpedicion.Trim() //18.- LugardeExpedicion
                    + "|" + usocfdi.Trim()         //19.- UsoCfdi
                    + "|"                          //20.- Confirmacion
                    + "|"                          //21.- Referencia
                    + "|"                           //22.- Nota1
                    + "|"                           //23.- Nota2
                    + "|"                           //24.- Nota 3
                    + "|"                     //25.- Exportacion 
                    + "|" + "01"                        //26.- FacAtrAquiriente
                    + "|"
                   );
                    escrituraFactura += "01"                                                //1.-Tipo De Registro
                       + "|" + folioe.Trim()         //2.- IdComprobante
                    + "|" + seriee.Trim()         //3.- Serie
                    + "|" + folioe.Trim()         //4.- Folio  
                    + "|" + fechaemision.Trim()  //5.- FechayHoraEmision 
                    + "|" + subt.Trim()          //6.- Subtotal
                    + "|"                        //7.- TotalImupuestosTrasladados
                    + "|"                        //8.- TotalImpuestosRetenidos
                    + "|"                        //9.- Descuentos
                    + "|" + total.Trim()         //10.- Total
                    + "|"                        //11.- TotalconLetra
                    + "|"                        //12.- FormadePago
                    + "|"                        //13.- CondicionesdePago
                    + "|"                          //14.-  MetododePago
                    + "|" + txtMoneda.Text.Trim() //15.- Moneda
                    + "|"     //16.- Tipodecambio + "|" + tipodecambiocpag.Trim()   
                    + "|" + tipocomprobante.Trim() //17.- TipodeComprobante
                    + "|" + lugarexpedicion.Trim() //18.- LugardeExpedicion
                    + "|" + usocfdi.Trim()         //19.- UsoCfdi
                    + "|"                          //20.- Confirmacion
                    + "|"                          //21.- Referencia
                    + "|"                           //22.- Nota1
                    + "|"                           //23.- Nota2
                    + "|"                           //24.- Nota 3
                    + "|"                     //25.- Exportacion 
                    + "|" + "01"                         //26.- FacAtrAquiriente
                    + "|";
                }
                //----------------------------------------Seccion de los datos del receptor del CFDI -------------------------------------------------------------------------------------

                //02 INFORMACION DEL RECEPTOR (1:1)
                if (monedascpadgoc.Trim() == "USD")
                {
                    escritor.WriteLine(
                       "02"                                                   //1-Tipo De Registro
                       + "|" + txtIdCliente.Text.Trim()                       //2-Id Receptor
                       + "|" + txtRFC.Text.Trim()                                //3-RFC
                       + "|" + txtCliente.Text.Trim()                         //4-Nombre
                       + "|"                             //5-Pais
                       + "|"                           //6-Calle
                       + "|"                           //7-Numero Exterior
                       + "|"                          //8-Numero Interior
                       + "|"                         //9-Colonia
                       + "|"                       //10-Localidad
                       + "|"                      //11-Referencia
                       + "|"                       //12-Municio/Delegacion
                       + "|"                           //13-EStado
                       + "|" + txtCP.Text.Trim()                              //14-Codigo Postal
                       + "|"                                                // paisresidencia                                 //15-Pais de Residecia Fiscal Cuando La Empresa Sea Extrajera
                       + "|"                                   //16-Numero de Registro de ID Tributacion 
                       + "|"                                     //17-Correo de envio                                                    
                       + "|" + regimenfiscal.Trim()
                       + "|"      //Fin Del Registro 
                       );

                    escrituraFactura += "\\n02"                                                   //1-Tipo De Registro
                      + "|" + txtIdCliente.Text.Trim()                       //2-Id Receptor
                       + "|" + txtRFC.Text.Trim()                                //3-RFC
                       + "|" + txtCliente.Text.Trim()                         //4-Nombre
                       + "|"                             //5-Pais
                       + "|"                           //6-Calle
                       + "|"                           //7-Numero Exterior
                       + "|"                          //8-Numero Interior
                       + "|"                         //9-Colonia
                       + "|"                       //10-Localidad
                       + "|"                      //11-Referencia
                       + "|"                       //12-Municio/Delegacion
                       + "|"                           //13-EStado
                       + "|" + txtCP.Text.Trim()                              //14-Codigo Postal
                       + "|"                                                // paisresidencia                                 //15-Pais de Residecia Fiscal Cuando La Empresa Sea Extrajera
                       + "|"                                   //16-Numero de Registro de ID Tributacion 
                       + "|"                                     //17-Correo de envio                                                    
                       + "|" + regimenfiscal.Trim()
                       + "|";
                    escritor.WriteLine(
                       "04"                                                   //1-Tipo De Registro
                      + "|" + consecutivoconcepto.Trim()                       //2-Id Receptor
                      + "|" + claveproductoservicio.Trim()                                //3-RFC
                      + "|"                          //4-Nombre
                      + "|" + cantidad.Trim()                           //5-Pais
                      + "|" + claveunidad.Trim()                            //6-Calle
                      + "|"                             //7-Numero Exterior
                      + "|" + descripcion.Trim()                            //8-Numero Interior
                      + "|" + "0"                         //9-Colonia
                      + "|" + "0"                        //10-Localidad
                      + "|"                        //11-Referencia
                      + "|"                         //12-Municio/Delegacion
                      + "|" + "01"                          //13-EStado
                      + "|"
                      );


                    escrituraFactura += "\\n04"                                                   //1-Tipo De Registro
                        + "|" + consecutivoconcepto.Trim()                       //2-Id Receptor
                      + "|" + claveproductoservicio.Trim()                                //3-RFC
                      + "|"                          //4-Nombre
                      + "|" + cantidad.Trim()                           //5-Pais
                      + "|" + claveunidad.Trim()                            //6-Calle
                      + "|"                             //7-Numero Exterior
                      + "|" + descripcion.Trim()                            //8-Numero Interior
                      + "|" + "0"                         //9-Colonia
                      + "|" + "0"                        //10-Localidad
                      + "|"                        //11-Referencia
                      + "|"                         //12-Municio/Delegacion
                      + "|" + "01"                          //13-EStado
                      + "|";

                    escritor.WriteLine(
                    "CPAG20"                         //1-Tipo De Registro
                    + "|" + "2.0"                    //2-Version
                    + "|"                               //Fin Del Registro
                    );

                    escrituraFactura += "CPAG20"    //1-Tipo De Registro
                    + "|" + "2.0"                   //2-Version  
                    + "|";

                    escritor.WriteLine(
                    "CPAG20TOT"                         //1-Tipo De Registro
                    + "|"                               //2-TotalRetencionesIVA
                    + "|"             //3-TotalRetencionesISR                                              
                    + "|"                               //4-TotalRetencionesIEPS
                    + "|"            //5-TotalTrasladosBaseIVA16
                    + "|"             //6-TotalTrasladosImpuestoIVA16
                    + "|"                               //7-TotalTrasladosBaseIVA8
                    + "|"                               //8-TotalTrasladosImpuestoIVA8
                    + "|"                               //9-TotalTrasladosBaseIVA0
                    + "|"                              //10-TotalTrasladosImpuestoIVA0
                    + "|"                              //11-TotalTrasladosBaseIVAExento
                     + "|" + totaenpesos.Trim()                       //12-MontoTotalPagos                                                                                                 
                     + "|"                               //Fin Del Registro
                    );

                    escrituraFactura += "CPAG20TOT"    //1-Tipo De Registro
                                    + "|"                               //2-TotalRetencionesIVA
                    + "|"             //3-TotalRetencionesISR                                              
                    + "|"                               //4-TotalRetencionesIEPS
                    + "|"            //5-TotalTrasladosBaseIVA16
                    + "|"             //6-TotalTrasladosImpuestoIVA16
                    + "|"                               //7-TotalTrasladosBaseIVA8
                    + "|"                               //8-TotalTrasladosImpuestoIVA8
                    + "|"                               //9-TotalTrasladosBaseIVA0
                    + "|"                              //10-TotalTrasladosImpuestoIVA0
                    + "|"                              //11-TotalTrasladosBaseIVAExento
                     + "|" + totaenpesos.Trim()                       //12-MontoTotalPagos                                                                                                 
                     + "|";
                    escritor.WriteLine(
                "CPAG20PAGO"                                           //1-Tipo De Registro
                + "|" + folioe.Trim()                                  //2-Identificador                                             
                + "|" + fechapago.Trim()                                      //3-Fechapago
                + "|" + formadepago.Trim()                              //4-Formadepagocpag
                + "|" + monedacpag.Trim()                                     //5-Monedacpag
                + "|" + tipodecambiocpagd.Trim()                               //6-TipoDecambiocpag
                + "|" + txtTotal.Text.Trim()                                  //8-Monto
                + "|" + numerooperacion.Trim()                                //9-NumeroOperacion
                + "|" + txtRFCbancoEmisor.Text.Trim()                         //10-RFCEmisorCuentaBeneficiario
                + "|" + txtBancoEmisor.Text.Trim()                            //11-NombreDelBanco                                                                                            
                + "|" + txtCuentaPago.Text.Trim()                             //12-NumeroCuentaOrdenante
                + "|" + rfcemisorcuentaben.Trim()                             //13-RFCEmisorCuentaBeneficiario
                + "|" + numcuentaben.Trim()                                   //14-NumCuentaBeneficiario
                + "|" + tipocadenapago.Trim()                                 //15-TipoCadenaPago                                               
                + "|" + certpago.Trim()                                       //16-CertificadoPago
                + "|" + cadenadelpago.Trim()                                  //17-CadenaDePago
                + "|" + sellodelpago.Trim()                                   //Fin Del Registro
                + "|"
                );

                    escrituraFactura += "CPAG20PAGO"                      //1-Tipo De Registro
                    + "|" + folioe.Trim()                                  //2-Identificador                                             
                    + "|" + fechapago.Trim()                                      //3-Fechapago
                    + "|" + formadepago.Trim()                              //4-Formadepagocpag
                    + "|" + monedacpag.Trim()                                     //5-Monedacpag
                    + "|" + tipodecambiocpagd.Trim()                               //6-TipoDecambiocpag
                    + "|" + txtTotal.Text.Trim()                                  //8-Monto
                    + "|" + numerooperacion.Trim()                                //9-NumeroOperacion
                    + "|" + txtRFCbancoEmisor.Text.Trim()                         //10-RFCEmisorCuentaBeneficiario
                    + "|" + txtBancoEmisor.Text.Trim()                            //11-NombreDelBanco                                                                                            
                    + "|" + txtCuentaPago.Text.Trim()                             //12-NumeroCuentaOrdenante
                    + "|" + rfcemisorcuentaben.Trim()                             //13-RFCEmisorCuentaBeneficiario
                    + "|" + numcuentaben.Trim()                                   //14-NumCuentaBeneficiario
                    + "|" + tipocadenapago.Trim()                                 //15-TipoCadenaPago                                               
                    + "|" + certpago.Trim()                                       //16-CertificadoPago
                    + "|" + cadenadelpago.Trim()                                  //17-CadenaDePago
                    + "|" + sellodelpago.Trim()                                   //Fin Del Registro
                    + "|";

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

                }
                else
                {

                    escritor.WriteLine(
                        "02"                                                   //1-Tipo De Registro
                        + "|" + txtIdCliente.Text.Trim()                       //2-Id Receptor
                        + "|" + txtRFC.Text.Trim()                                //3-RFC
                        + "|" + txtCliente.Text.Trim()                         //4-Nombre
                        + "|"                             //5-Pais
                        + "|"                           //6-Calle
                        + "|"                           //7-Numero Exterior
                        + "|"                          //8-Numero Interior
                        + "|"                         //9-Colonia
                        + "|"                       //10-Localidad
                        + "|"                      //11-Referencia
                        + "|"                       //12-Municio/Delegacion
                        + "|"                           //13-EStado
                        + "|" + txtCP.Text.Trim()                              //14-Codigo Postal
                        + "|"                                                // paisresidencia                                 //15-Pais de Residecia Fiscal Cuando La Empresa Sea Extrajera
                        + "|"                                   //16-Numero de Registro de ID Tributacion 
                        + "|"                                     //17-Correo de envio                                                    
                        + "|" + regimenfiscal.Trim()
                        + "|"      //Fin Del Registro 
                        );

                    escrituraFactura += "\\n02"                                                   //1-Tipo De Registro
                      + "|" + txtIdCliente.Text.Trim()                       //2-Id Receptor
                       + "|" + txtRFC.Text.Trim()                                //3-RFC
                       + "|" + txtCliente.Text.Trim()                         //4-Nombre
                       + "|"                             //5-Pais
                       + "|"                           //6-Calle
                       + "|"                           //7-Numero Exterior
                       + "|"                          //8-Numero Interior
                       + "|"                         //9-Colonia
                       + "|"                       //10-Localidad
                       + "|"                      //11-Referencia
                       + "|"                       //12-Municio/Delegacion
                       + "|"                           //13-EStado
                       + "|" + txtCP.Text.Trim()                              //14-Codigo Postal
                       + "|"                                                // paisresidencia                                 //15-Pais de Residecia Fiscal Cuando La Empresa Sea Extrajera
                       + "|"                                   //16-Numero de Registro de ID Tributacion 
                       + "|"                                     //17-Correo de envio                                                    
                       + "|" + regimenfiscal.Trim()
                       + "|";
                    escritor.WriteLine(
                       "04"                                                   //1-Tipo De Registro
                      + "|" + consecutivoconcepto.Trim()                       //2-Id Receptor
                      + "|" + claveproductoservicio.Trim()                                //3-RFC
                      + "|"                          //4-Nombre
                      + "|" + cantidad.Trim()                           //5-Pais
                      + "|" + claveunidad.Trim()                            //6-Calle
                      + "|"                             //7-Numero Exterior
                      + "|" + descripcion.Trim()                            //8-Numero Interior
                      + "|" + "0"                         //9-Colonia
                      + "|" + "0"                        //10-Localidad
                      + "|"                        //11-Referencia
                      + "|"                         //12-Municio/Delegacion
                      + "|" + "01"                          //13-EStado
                      + "|"
                      );


                    escrituraFactura += "\\n04"                                                   //1-Tipo De Registro
                    + "|" + consecutivoconcepto.Trim()                       //2-Id Receptor
                  + "|" + claveproductoservicio.Trim()                                //3-RFC
                  + "|"                          //4-Nombre
                  + "|" + cantidad.Trim()                           //5-Pais
                  + "|" + claveunidad.Trim()                            //6-Calle
                  + "|"                             //7-Numero Exterior
                  + "|" + descripcion.Trim()                            //8-Numero Interior
                  + "|" + "0"                         //9-Colonia
                  + "|" + "0"                        //10-Localidad
                  + "|"                        //11-Referencia
                  + "|"                         //12-Municio/Delegacion
                  + "|" + "01"                          //13-EStado
                  + "|";

                    //CPAG20 (1:1)
                    escritor.WriteLine(
                    "CPAG20"                         //1-Tipo De Registro
                    + "|" + "2.0"                    //2-Version
                    + "|"                               //Fin Del Registro
                    );

                    escrituraFactura += "CPAG20"    //1-Tipo De Registro
                    + "|" + "2.0"                   //2-Version  
                    + "|";

                    escritor.WriteLine(
                    "CPAG20TOT"                         //1-Tipo De Registro
                    + "|"                               //2-TotalRetencionesIVA
                    + "|" + TotaldeRe.Trim()            //3-TotalRetencionesISR                                              
                    + "|"                               //4-TotalRetencionesIEPS
                    + "|" + totalfinaldeiva.Trim()           //5-TotalTrasladosBaseIVA16
                    + "|" + TotaldeIva.Trim()            //6-TotalTrasladosImpuestoIVA16
                    + "|"                               //7-TotalTrasladosBaseIVA8
                    + "|"                               //8-TotalTrasladosImpuestoIVA8
                    + "|"                               //9-TotalTrasladosBaseIVA0
                    + "|"                               //10-TotalTrasladosImpuestoIVA0
                    + "|"                               //11-TotalTrasladosBaseIVAExento
                     + "|" + txtTotal.Text.Trim()                       //12-MontoTotalPagos                                                                                                 
                     + "|"                               //Fin Del Registro
                    );

                    escrituraFactura += "CPAG20TOT"    //1-Tipo De Registro
                                   + "|"                               //2-TotalRetencionesIVA
                    + "|" + TotaldeRe.Trim()            //3-TotalRetencionesISR                                              
                    + "|"                               //4-TotalRetencionesIEPS
                    + "|" + totalfinaldeiva.Trim()           //5-TotalTrasladosBaseIVA16
                    + "|" + TotaldeIva.Trim()            //6-TotalTrasladosImpuestoIVA16
                    + "|"                               //7-TotalTrasladosBaseIVA8
                    + "|"                               //8-TotalTrasladosImpuestoIVA8
                    + "|"                               //9-TotalTrasladosBaseIVA0
                    + "|"                               //10-TotalTrasladosImpuestoIVA0
                    + "|"                               //11-TotalTrasladosBaseIVAExento
                     + "|" + txtTotal.Text.Trim()                       //12-MontoTotalPagos                                                                                                 
                     + "|";

                    //CPAG20PAGO COMPLEMENTO DE PAGO (1:N)

                    escritor.WriteLine(
                    "CPAG20PAGO"                                           //1-Tipo De Registro
                    + "|" + folioe.Trim()                                  //2-Identificador                                             
                    + "|" + fechapago.Trim()                                      //3-Fechapago
                    + "|" + formadepago.Trim()                              //4-Formadepagocpag
                    + "|" + monedacpag.Trim()                                     //5-Monedacpag
                    + "|" + tipodecambiocpag.Trim()                               //6-TipoDecambiocpag
                    + "|" + txtTotal.Text.Trim()                                  //8-Monto
                    + "|" + numerooperacion.Trim()                                //9-NumeroOperacion
                    + "|" + txtRFCbancoEmisor.Text.Trim()                         //10-RFCEmisorCuentaBeneficiario
                    + "|" + txtBancoEmisor.Text.Trim()                            //11-NombreDelBanco                                                                                            
                    + "|" + txtCuentaPago.Text.Trim()                             //12-NumeroCuentaOrdenante
                    + "|" + rfcemisorcuentaben.Trim()                             //13-RFCEmisorCuentaBeneficiario
                    + "|" + numcuentaben.Trim()                                   //14-NumCuentaBeneficiario
                    + "|" + tipocadenapago.Trim()                                 //15-TipoCadenaPago                                               
                    + "|" + certpago.Trim()                                       //16-CertificadoPago
                    + "|" + cadenadelpago.Trim()                                  //17-CadenaDePago
                    + "|" + sellodelpago.Trim()                                   //Fin Del Registro
                    + "|"
                    );

                    escrituraFactura += "CPAG20PAGO"                      //1-Tipo De Registro
                    + "|" + folioe.Trim()                                  //2-Identificador                                             
                    + "|" + fechapago.Trim()                                      //3-Fechapago
                    + "|" + formadepago.Trim()                              //4-Formadepagocpag
                    + "|" + monedacpag.Trim()                                     //5-Monedacpag
                    + "|" + tipodecambiocpag.Trim()                               //6-TipoDecambiocpag
                    + "|" + txtTotal.Text.Trim()                                  //8-Monto
                    + "|" + numerooperacion.Trim()                                //9-NumeroOperacion
                    + "|" + txtRFCbancoEmisor.Text.Trim()                         //10-RFCEmisorCuentaBeneficiario
                    + "|" + txtBancoEmisor.Text.Trim()                            //11-NombreDelBanco                                                                                            
                    + "|" + txtCuentaPago.Text.Trim()                             //12-NumeroCuentaOrdenante
                    + "|" + rfcemisorcuentaben.Trim()                             //13-RFCEmisorCuentaBeneficiario
                    + "|" + numcuentaben.Trim()                                   //14-NumCuentaBeneficiario
                    + "|" + tipocadenapago.Trim()                                 //15-TipoCadenaPago                                               
                    + "|" + certpago.Trim()                                       //16-CertificadoPago
                    + "|" + cadenadelpago.Trim()                                  //17-CadenaDePago
                    + "|" + sellodelpago.Trim()                                   //Fin Del Registro
                    + "|";

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
                    //escrituraFactura = escrituraFactura.Replace("| \r\n", "|");
                    //escrituraFactura = escrituraFactura.Replace("|CPAG20DOC", "|\\nCPAG20DOC");


                }


                if (nodeToFind == true && nodeToFind2 == true)
                {
                    escrituraFactura = escrituraFactura.Replace(Environment.NewLine, "");
                    escritor.WriteLine(f07);
                    escrituraFactura += f07;
                    escritor.WriteLine(f08);
                    escrituraFactura += f08;
                }
                if (nodeToFind == true && nodeToFind2 == false)
                {
                    
                    //if (srtiva == 3)
                    //{
                    //    escritor.WriteLine(f08);
                    //    escrituraFactura += f08;
                    //}
                    if (usdmoneda == 1)
                    {
                        f07 = "";
                        f08 = "";
                        escritor.WriteLine(f07);
                        escrituraFactura += f07;
                        escritor.WriteLine(f08);
                        escrituraFactura += f08;
                    } else
                    {
                        if (rtiva == 1 && rtisr == 1)
                        {
                            escrituraFactura = escrituraFactura.Replace(Environment.NewLine, "");
                            escritor.WriteLine(f07);
                            escrituraFactura += f07;
                            escritor.WriteLine(f08);
                            escrituraFactura += f08;
                        }
                        else
                        {
                            escritor.WriteLine(f08);
                            escrituraFactura += f08;
                        }
                    }
                    //else
                    //{
                    //    escrituraFactura = escrituraFactura.Replace("| \r\n", "");
                    //    escritor.WriteLine(f08);
                    //    escrituraFactura += f08;
                    //}

                }
                if (nodeToFind == false && nodeToFind2 == true)
                {
                    if (rtiva == 1 && rtisr == 1)
                    {
                        escrituraFactura = escrituraFactura.Replace("| \r\n", "");
                        escritor.WriteLine(f07);
                        escrituraFactura += f07;
                        escritor.WriteLine(f08);
                        escrituraFactura += f08;
                    }
                    else
                    {
                        escritor.WriteLine(f07);
                        escrituraFactura += f07;
                    }
                    //if (srtisr == 2)
                    //{
                    //    escritor.WriteLine(f07);
                    //    escrituraFactura += f07;
                    //}
                    if (usdmoneda == 1)
                    {
                        f07 = "";
                        f08 = "";
                        escritor.WriteLine(f07);
                        escrituraFactura += f07;
                        escritor.WriteLine(f08);
                        escrituraFactura += f08;
                    }
                    else
                    {
                        escritor.WriteLine(f07);
                        escrituraFactura += f07;
                    }

                }






                //----------------------------------------Seccion de detalles del complemento de pago -------------------------------------------------------------------


            }
            string[] strAllLines = File.ReadAllLines(path);
            File.WriteAllLines(path, strAllLines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray());
        }
        public void generaTXTCP()
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
                //string f01 = "01"   // LISTO

                //01 INFORMACION GENERAL DEL CFDI (1:1)
                if (formadepago.Trim() != "02")
                {
                    escritor.WriteLine(
                    "01"                           //1.-Tipo De Registro
                    + "|" + folioe.Trim()         //2.- IdComprobante
                    + "|" + seriee.Trim()         //3.- Serie
                    + "|" + folioe.Trim()         //4.- Folio  
                    + "|" + fechaemision.Trim()  //5.- FechayHoraEmision 
                    + "|" + subt.Trim()          //6.- Subtotal
                    + "|"                        //7.- TotalImupuestosTrasladados
                    + "|"                        //8.- TotalImpuestosRetenidos
                    + "|"                        //9.- Descuentos
                    + "|" + total.Trim()         //10.- Total
                    + "|"                        //11.- TotalconLetra
                    + "|"                        //12.- FormadePago
                    + "|"                        //13.- CondicionesdePago
                    + "|"                          //14.-  MetododePago
                    + "|" + txtMoneda.Text.Trim() //15.- Moneda
                    + "|"     //16.- Tipodecambio + "|" + tipodecambiocpag.Trim()   
                    + "|" + tipocomprobante.Trim() //17.- TipodeComprobante
                    + "|" + lugarexpedicion.Trim() //18.- LugardeExpedicion
                    + "|" + usocfdi.Trim()         //19.- UsoCfdi
                    + "|"                          //20.- Confirmacion
                    + "|"                          //21.- Referencia
                    + "|"                           //22.- Nota1
                    + "|"                           //23.- Nota2
                    + "|"                           //24.- Nota 3
                    + "|"                     //25.- Exportacion 
                    + "|" + "01"                          //26.- FacAtrAquiriente
                    + "|"
                    );
                    escrituraFactura += "01"                                                //1.-Tipo De Registro
                       + "|" + folioe.Trim()         //2.- IdComprobante
                    + "|" + seriee.Trim()         //3.- Serie
                    + "|" + folioe.Trim()         //4.- Folio  
                    + "|" + fechaemision.Trim()  //5.- FechayHoraEmision 
                    + "|" + subt.Trim()          //6.- Subtotal
                    + "|"                        //7.- TotalImupuestosTrasladados
                    + "|"                        //8.- TotalImpuestosRetenidos
                    + "|"                        //9.- Descuentos
                    + "|" + total.Trim()         //10.- Total
                    + "|"                        //11.- TotalconLetra
                    + "|"                        //12.- FormadePago
                    + "|"                        //13.- CondicionesdePago
                    + "|"                          //14.-  MetododePago
                    + "|" + txtMoneda.Text.Trim() //15.- Moneda
                    + "|"     //16.- Tipodecambio + "|" + tipodecambiocpag.Trim()   
                    + "|" + tipocomprobante.Trim() //17.- TipodeComprobante
                    + "|" + lugarexpedicion.Trim() //18.- LugardeExpedicion
                    + "|" + usocfdi.Trim()         //19.- UsoCfdi
                    + "|"                          //20.- Confirmacion
                    + "|"                          //21.- Referencia
                    + "|"                           //22.- Nota1
                    + "|"                           //23.- Nota2
                    + "|"                           //24.- Nota 3
                    + "|"                     //25.- Exportacion 
                    + "|" + "01"                          //26.- FacAtrAquiriente
                    + "|";
                }
                else
                {
                    escritor.WriteLine(
                   "01"                                                //1.-Tipo De Registro
                     + "|" + folioe.Trim()         //2.- IdComprobante
                    + "|" + seriee.Trim()         //3.- Serie
                    + "|" + folioe.Trim()         //4.- Folio  
                    + "|" + fechaemision.Trim()  //5.- FechayHoraEmision 
                    + "|" + subt.Trim()          //6.- Subtotal
                    + "|"                        //7.- TotalImupuestosTrasladados
                    + "|"                        //8.- TotalImpuestosRetenidos
                    + "|"                        //9.- Descuentos
                    + "|" + total.Trim()         //10.- Total
                    + "|"                        //11.- TotalconLetra
                    + "|"                        //12.- FormadePago
                    + "|"                        //13.- CondicionesdePago
                    + "|"                          //14.-  MetododePago
                    + "|" + txtMoneda.Text.Trim() //15.- Moneda
                    + "|"     //16.- Tipodecambio + "|" + tipodecambiocpag.Trim()   
                    + "|" + tipocomprobante.Trim() //17.- TipodeComprobante
                    + "|" + lugarexpedicion.Trim() //18.- LugardeExpedicion
                    + "|" + usocfdi.Trim()         //19.- UsoCfdi
                    + "|"                          //20.- Confirmacion
                    + "|"                          //21.- Referencia
                    + "|"                           //22.- Nota1
                    + "|"                           //23.- Nota2
                    + "|"                           //24.- Nota 3
                    + "|"                     //25.- Exportacion 
                    + "|" + "01"                        //26.- FacAtrAquiriente
                    + "|"
                   );
                    escrituraFactura += "01"                                                //1.-Tipo De Registro
                       + "|" + folioe.Trim()         //2.- IdComprobante
                    + "|" + seriee.Trim()         //3.- Serie
                    + "|" + folioe.Trim()         //4.- Folio  
                    + "|" + fechaemision.Trim()  //5.- FechayHoraEmision 
                    + "|" + subt.Trim()          //6.- Subtotal
                    + "|"                        //7.- TotalImupuestosTrasladados
                    + "|"                        //8.- TotalImpuestosRetenidos
                    + "|"                        //9.- Descuentos
                    + "|" + total.Trim()         //10.- Total
                    + "|"                        //11.- TotalconLetra
                    + "|"                        //12.- FormadePago
                    + "|"                        //13.- CondicionesdePago
                    + "|"                          //14.-  MetododePago
                    + "|" + txtMoneda.Text.Trim() //15.- Moneda
                    + "|"     //16.- Tipodecambio + "|" + tipodecambiocpag.Trim()   
                    + "|" + tipocomprobante.Trim() //17.- TipodeComprobante
                    + "|" + lugarexpedicion.Trim() //18.- LugardeExpedicion
                    + "|" + usocfdi.Trim()         //19.- UsoCfdi
                    + "|"                          //20.- Confirmacion
                    + "|"                          //21.- Referencia
                    + "|"                           //22.- Nota1
                    + "|"                           //23.- Nota2
                    + "|"                           //24.- Nota 3
                    + "|"                     //25.- Exportacion 
                    + "|" + "01"                         //26.- FacAtrAquiriente
                    + "|";
                }
                //----------------------------------------Seccion de los datos del receptor del CFDI -------------------------------------------------------------------------------------

                //02 INFORMACION DEL RECEPTOR (1:1)
                if (monedascpadgoc.Trim() == "USD")
                {
                    escritor.WriteLine(
                       "02"                                                   //1-Tipo De Registro
                       + "|" + txtIdCliente.Text.Trim()                       //2-Id Receptor
                       + "|" + txtRFC.Text.Trim()                                //3-RFC
                       + "|" + txtCliente.Text.Trim()                         //4-Nombre
                       + "|"                             //5-Pais
                       + "|"                           //6-Calle
                       + "|"                           //7-Numero Exterior
                       + "|"                          //8-Numero Interior
                       + "|"                         //9-Colonia
                       + "|"                       //10-Localidad
                       + "|"                      //11-Referencia
                       + "|"                       //12-Municio/Delegacion
                       + "|"                           //13-EStado
                       + "|" + txtCP.Text.Trim()                              //14-Codigo Postal
                       + "|"                                                // paisresidencia                                 //15-Pais de Residecia Fiscal Cuando La Empresa Sea Extrajera
                       + "|"                                   //16-Numero de Registro de ID Tributacion 
                       + "|"                                     //17-Correo de envio                                                    
                       + "|" + regimenfiscal.Trim()
                       + "|"      //Fin Del Registro 
                       );

                    escrituraFactura += "\\n02"                                                   //1-Tipo De Registro
                      + "|" + txtIdCliente.Text.Trim()                       //2-Id Receptor
                       + "|" + txtRFC.Text.Trim()                                //3-RFC
                       + "|" + txtCliente.Text.Trim()                         //4-Nombre
                       + "|"                             //5-Pais
                       + "|"                           //6-Calle
                       + "|"                           //7-Numero Exterior
                       + "|"                          //8-Numero Interior
                       + "|"                         //9-Colonia
                       + "|"                       //10-Localidad
                       + "|"                      //11-Referencia
                       + "|"                       //12-Municio/Delegacion
                       + "|"                           //13-EStado
                       + "|" + txtCP.Text.Trim()                              //14-Codigo Postal
                       + "|"                                                // paisresidencia                                 //15-Pais de Residecia Fiscal Cuando La Empresa Sea Extrajera
                       + "|"                                   //16-Numero de Registro de ID Tributacion 
                       + "|"                                     //17-Correo de envio                                                    
                       + "|" + regimenfiscal.Trim()
                       + "|";
                    escritor.WriteLine(
                       "04"                                                   //1-Tipo De Registro
                      + "|" + consecutivoconcepto.Trim()                       //2-Id Receptor
                      + "|" + claveproductoservicio.Trim()                                //3-RFC
                      + "|"                          //4-Nombre
                      + "|" + cantidad.Trim()                           //5-Pais
                      + "|" + claveunidad.Trim()                            //6-Calle
                      + "|"                             //7-Numero Exterior
                      + "|" + descripcion.Trim()                            //8-Numero Interior
                      + "|" + "0"                         //9-Colonia
                      + "|" + "0"                        //10-Localidad
                      + "|"                        //11-Referencia
                      + "|"                         //12-Municio/Delegacion
                      + "|" + "01"                          //13-EStado
                      + "|"
                      );


                    escrituraFactura += "\\n04"                                                   //1-Tipo De Registro
                        + "|" + consecutivoconcepto.Trim()                       //2-Id Receptor
                      + "|" + claveproductoservicio.Trim()                                //3-RFC
                      + "|"                          //4-Nombre
                      + "|" + cantidad.Trim()                           //5-Pais
                      + "|" + claveunidad.Trim()                            //6-Calle
                      + "|"                             //7-Numero Exterior
                      + "|" + descripcion.Trim()                            //8-Numero Interior
                      + "|" + "0"                         //9-Colonia
                      + "|" + "0"                        //10-Localidad
                      + "|"                        //11-Referencia
                      + "|"                         //12-Municio/Delegacion
                      + "|" + "01"                          //13-EStado
                      + "|";

                    escritor.WriteLine(
                    "CPAG20"                         //1-Tipo De Registro
                    + "|" + "2.0"                    //2-Version
                    + "|"                               //Fin Del Registro
                    );

                    escrituraFactura += "CPAG20"    //1-Tipo De Registro
                    + "|" + "2.0"                   //2-Version  
                    + "|";

                    escritor.WriteLine(
                    "CPAG20TOT"                         //1-Tipo De Registro
                    + "|"                               //2-TotalRetencionesIVA
                    + "|"             //3-TotalRetencionesISR                                              
                    + "|"                               //4-TotalRetencionesIEPS
                    + "|"            //5-TotalTrasladosBaseIVA16
                    + "|"             //6-TotalTrasladosImpuestoIVA16
                    + "|"                               //7-TotalTrasladosBaseIVA8
                    + "|"                               //8-TotalTrasladosImpuestoIVA8
                    + "|"                               //9-TotalTrasladosBaseIVA0
                    + "|"                              //10-TotalTrasladosImpuestoIVA0
                    + "|"                              //11-TotalTrasladosBaseIVAExento
                     + "|" + totaenpesos.Trim()                       //12-MontoTotalPagos                                                                                                 
                     + "|"                               //Fin Del Registro
                    );

                    escrituraFactura += "CPAG20TOT"    //1-Tipo De Registro
                                    + "|"                               //2-TotalRetencionesIVA
                    + "|"             //3-TotalRetencionesISR                                              
                    + "|"                               //4-TotalRetencionesIEPS
                    + "|"            //5-TotalTrasladosBaseIVA16
                    + "|"             //6-TotalTrasladosImpuestoIVA16
                    + "|"                               //7-TotalTrasladosBaseIVA8
                    + "|"                               //8-TotalTrasladosImpuestoIVA8
                    + "|"                               //9-TotalTrasladosBaseIVA0
                    + "|"                              //10-TotalTrasladosImpuestoIVA0
                    + "|"                              //11-TotalTrasladosBaseIVAExento
                     + "|" + totaenpesos.Trim()                       //12-MontoTotalPagos                                                                                                 
                     + "|";
                    escritor.WriteLine(
                "CPAG20PAGO"                                           //1-Tipo De Registro
                + "|" + folioe.Trim()                                  //2-Identificador                                             
                + "|" + fechapago.Trim()                                      //3-Fechapago
                + "|" + formadepago.Trim()                              //4-Formadepagocpag
                + "|" + monedacpag.Trim()                                     //5-Monedacpag
                + "|" + tipodecambiocpagd.Trim()                               //6-TipoDecambiocpag
                + "|" + txtTotal.Text.Trim()                                  //8-Monto
                + "|" + numerooperacion.Trim()                                //9-NumeroOperacion
                + "|" + txtRFCbancoEmisor.Text.Trim()                         //10-RFCEmisorCuentaBeneficiario
                + "|" + txtBancoEmisor.Text.Trim()                            //11-NombreDelBanco                                                                                            
                + "|" + txtCuentaPago.Text.Trim()                             //12-NumeroCuentaOrdenante
                + "|" + rfcemisorcuentaben.Trim()                             //13-RFCEmisorCuentaBeneficiario
                + "|" + numcuentaben.Trim()                                   //14-NumCuentaBeneficiario
                + "|" + tipocadenapago.Trim()                                 //15-TipoCadenaPago                                               
                + "|" + certpago.Trim()                                       //16-CertificadoPago
                + "|" + cadenadelpago.Trim()                                  //17-CadenaDePago
                + "|" + sellodelpago.Trim()                                   //Fin Del Registro
                + "|"
                );

                    escrituraFactura += "CPAG20PAGO"                      //1-Tipo De Registro
                    + "|" + folioe.Trim()                                  //2-Identificador                                             
                    + "|" + fechapago.Trim()                                      //3-Fechapago
                    + "|" + formadepago.Trim()                              //4-Formadepagocpag
                    + "|" + monedacpag.Trim()                                     //5-Monedacpag
                    + "|" + tipodecambiocpagd.Trim()                               //6-TipoDecambiocpag
                    + "|" + txtTotal.Text.Trim()                                  //8-Monto
                    + "|" + numerooperacion.Trim()                                //9-NumeroOperacion
                    + "|" + txtRFCbancoEmisor.Text.Trim()                         //10-RFCEmisorCuentaBeneficiario
                    + "|" + txtBancoEmisor.Text.Trim()                            //11-NombreDelBanco                                                                                            
                    + "|" + txtCuentaPago.Text.Trim()                             //12-NumeroCuentaOrdenante
                    + "|" + rfcemisorcuentaben.Trim()                             //13-RFCEmisorCuentaBeneficiario
                    + "|" + numcuentaben.Trim()                                   //14-NumCuentaBeneficiario
                    + "|" + tipocadenapago.Trim()                                 //15-TipoCadenaPago                                               
                    + "|" + certpago.Trim()                                       //16-CertificadoPago
                    + "|" + cadenadelpago.Trim()                                  //17-CadenaDePago
                    + "|" + sellodelpago.Trim()                                   //Fin Del Registro
                    + "|";

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

                }
                else
                {

                    escritor.WriteLine(
                        "02"                                                   //1-Tipo De Registro
                        + "|" + txtIdCliente.Text.Trim()                       //2-Id Receptor
                        + "|" + txtRFC.Text.Trim()                                //3-RFC
                        + "|" + txtCliente.Text.Trim()                         //4-Nombre
                        + "|"                             //5-Pais
                        + "|"                           //6-Calle
                        + "|"                           //7-Numero Exterior
                        + "|"                          //8-Numero Interior
                        + "|"                         //9-Colonia
                        + "|"                       //10-Localidad
                        + "|"                      //11-Referencia
                        + "|"                       //12-Municio/Delegacion
                        + "|"                           //13-EStado
                        + "|" + txtCP.Text.Trim()                              //14-Codigo Postal
                        + "|"                                                // paisresidencia                                 //15-Pais de Residecia Fiscal Cuando La Empresa Sea Extrajera
                        + "|"                                   //16-Numero de Registro de ID Tributacion 
                        + "|"                                     //17-Correo de envio                                                    
                        + "|" + regimenfiscal.Trim()
                        + "|"      //Fin Del Registro 
                        );

                    escrituraFactura += "\\n02"                                                   //1-Tipo De Registro
                      + "|" + txtIdCliente.Text.Trim()                       //2-Id Receptor
                       + "|" + txtRFC.Text.Trim()                                //3-RFC
                       + "|" + txtCliente.Text.Trim()                         //4-Nombre
                       + "|"                             //5-Pais
                       + "|"                           //6-Calle
                       + "|"                           //7-Numero Exterior
                       + "|"                          //8-Numero Interior
                       + "|"                         //9-Colonia
                       + "|"                       //10-Localidad
                       + "|"                      //11-Referencia
                       + "|"                       //12-Municio/Delegacion
                       + "|"                           //13-EStado
                       + "|" + txtCP.Text.Trim()                              //14-Codigo Postal
                       + "|"                                                // paisresidencia                                 //15-Pais de Residecia Fiscal Cuando La Empresa Sea Extrajera
                       + "|"                                   //16-Numero de Registro de ID Tributacion 
                       + "|"                                     //17-Correo de envio                                                    
                       + "|" + regimenfiscal.Trim()
                       + "|";
                    escritor.WriteLine(
                       "04"                                                   //1-Tipo De Registro
                      + "|" + consecutivoconcepto.Trim()                       //2-Id Receptor
                      + "|" + claveproductoservicio.Trim()                                //3-RFC
                      + "|"                          //4-Nombre
                      + "|" + cantidad.Trim()                           //5-Pais
                      + "|" + claveunidad.Trim()                            //6-Calle
                      + "|"                             //7-Numero Exterior
                      + "|" + descripcion.Trim()                            //8-Numero Interior
                      + "|" + "0"                         //9-Colonia
                      + "|" + "0"                        //10-Localidad
                      + "|"                        //11-Referencia
                      + "|"                         //12-Municio/Delegacion
                      + "|" + "01"                          //13-EStado
                      + "|"
                      );


                    escrituraFactura += "\\n04"                                                   //1-Tipo De Registro
                    + "|" + consecutivoconcepto.Trim()                       //2-Id Receptor
                  + "|" + claveproductoservicio.Trim()                                //3-RFC
                  + "|"                          //4-Nombre
                  + "|" + cantidad.Trim()                           //5-Pais
                  + "|" + claveunidad.Trim()                            //6-Calle
                  + "|"                             //7-Numero Exterior
                  + "|" + descripcion.Trim()                            //8-Numero Interior
                  + "|" + "0"                         //9-Colonia
                  + "|" + "0"                        //10-Localidad
                  + "|"                        //11-Referencia
                  + "|"                         //12-Municio/Delegacion
                  + "|" + "01"                          //13-EStado
                  + "|";

                    //CPAG20 (1:1)
                    escritor.WriteLine(
                    "CPAG20"                         //1-Tipo De Registro
                    + "|" + "2.0"                    //2-Version
                    + "|"                               //Fin Del Registro
                    );

                    escrituraFactura += "CPAG20"    //1-Tipo De Registro
                    + "|" + "2.0"                   //2-Version  
                    + "|";

                    escritor.WriteLine(
                    "CPAG20TOT"                         //1-Tipo De Registro
                    + "|"                               //2-TotalRetencionesIVA
                    + "|" + TotaldeRe.Trim()            //3-TotalRetencionesISR                                              
                    + "|"                               //4-TotalRetencionesIEPS
                    + "|" + totalfinaldeiva.Trim()           //5-TotalTrasladosBaseIVA16
                    + "|" + TotaldeIva.Trim()            //6-TotalTrasladosImpuestoIVA16
                    + "|"                               //7-TotalTrasladosBaseIVA8
                    + "|"                               //8-TotalTrasladosImpuestoIVA8
                    + "|"                               //9-TotalTrasladosBaseIVA0
                    + "|"                               //10-TotalTrasladosImpuestoIVA0
                    + "|"                               //11-TotalTrasladosBaseIVAExento
                     + "|" + txtTotal.Text.Trim()                       //12-MontoTotalPagos                                                                                                 
                     + "|"                               //Fin Del Registro
                    );

                    escrituraFactura += "CPAG20TOT"    //1-Tipo De Registro
                                   + "|"                               //2-TotalRetencionesIVA
                    + "|" + TotaldeRe.Trim()            //3-TotalRetencionesISR                                              
                    + "|"                               //4-TotalRetencionesIEPS
                    + "|" + totalfinaldeiva.Trim()           //5-TotalTrasladosBaseIVA16
                    + "|" + TotaldeIva.Trim()            //6-TotalTrasladosImpuestoIVA16
                    + "|"                               //7-TotalTrasladosBaseIVA8
                    + "|"                               //8-TotalTrasladosImpuestoIVA8
                    + "|"                               //9-TotalTrasladosBaseIVA0
                    + "|"                               //10-TotalTrasladosImpuestoIVA0
                    + "|"                               //11-TotalTrasladosBaseIVAExento
                     + "|" + txtTotal.Text.Trim()                       //12-MontoTotalPagos                                                                                                 
                     + "|";

                    //CPAG20PAGO COMPLEMENTO DE PAGO (1:N)

                    escritor.WriteLine(
                    "CPAG20PAGO"                                           //1-Tipo De Registro
                    + "|" + folioe.Trim()                                  //2-Identificador                                             
                    + "|" + fechapago.Trim()                                      //3-Fechapago
                    + "|" + formadepago.Trim()                              //4-Formadepagocpag
                    + "|" + monedacpag.Trim()                                     //5-Monedacpag
                    + "|" + tipodecambiocpag.Trim()                               //6-TipoDecambiocpag
                    + "|" + txtTotal.Text.Trim()                                  //8-Monto
                    + "|" + numerooperacion.Trim()                                //9-NumeroOperacion
                    + "|" + txtRFCbancoEmisor.Text.Trim()                         //10-RFCEmisorCuentaBeneficiario
                    + "|" + txtBancoEmisor.Text.Trim()                            //11-NombreDelBanco                                                                                            
                    + "|" + txtCuentaPago.Text.Trim()                             //12-NumeroCuentaOrdenante
                    + "|" + rfcemisorcuentaben.Trim()                             //13-RFCEmisorCuentaBeneficiario
                    + "|" + numcuentaben.Trim()                                   //14-NumCuentaBeneficiario
                    + "|" + tipocadenapago.Trim()                                 //15-TipoCadenaPago                                               
                    + "|" + certpago.Trim()                                       //16-CertificadoPago
                    + "|" + cadenadelpago.Trim()                                  //17-CadenaDePago
                    + "|" + sellodelpago.Trim()                                   //Fin Del Registro
                    + "|"
                    );

                    escrituraFactura += "CPAG20PAGO"                      //1-Tipo De Registro
                    + "|" + folioe.Trim()                                  //2-Identificador                                             
                    + "|" + fechapago.Trim()                                      //3-Fechapago
                    + "|" + formadepago.Trim()                              //4-Formadepagocpag
                    + "|" + monedacpag.Trim()                                     //5-Monedacpag
                    + "|" + tipodecambiocpag.Trim()                               //6-TipoDecambiocpag
                    + "|" + txtTotal.Text.Trim()                                  //8-Monto
                    + "|" + numerooperacion.Trim()                                //9-NumeroOperacion
                    + "|" + txtRFCbancoEmisor.Text.Trim()                         //10-RFCEmisorCuentaBeneficiario
                    + "|" + txtBancoEmisor.Text.Trim()                            //11-NombreDelBanco                                                                                            
                    + "|" + txtCuentaPago.Text.Trim()                             //12-NumeroCuentaOrdenante
                    + "|" + rfcemisorcuentaben.Trim()                             //13-RFCEmisorCuentaBeneficiario
                    + "|" + numcuentaben.Trim()                                   //14-NumCuentaBeneficiario
                    + "|" + tipocadenapago.Trim()                                 //15-TipoCadenaPago                                               
                    + "|" + certpago.Trim()                                       //16-CertificadoPago
                    + "|" + cadenadelpago.Trim()                                  //17-CadenaDePago
                    + "|" + sellodelpago.Trim()                                   //Fin Del Registro
                    + "|";

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
                    //escrituraFactura = escrituraFactura.Replace("| \r\n", "|");
                    //escrituraFactura = escrituraFactura.Replace("|CPAG20DOC", "|\\nCPAG20DOC");


                }


                if (nodeToFind == true && nodeToFind2 == true)
                {
                    escrituraFactura = escrituraFactura.Replace(Environment.NewLine, "");
                    escritor.WriteLine(f07);
                    escrituraFactura += f07;
                    escritor.WriteLine(f08);
                    escrituraFactura += f08;
                }
                if (nodeToFind == true && nodeToFind2 == false)
                {

                    //if (srtiva == 3)
                    //{
                    //    escritor.WriteLine(f08);
                    //    escrituraFactura += f08;
                    //}
                    if (usdmoneda == 1)
                    {
                        f07 = "";
                        f08 = "";
                        escritor.WriteLine(f07);
                        escrituraFactura += f07;
                        escritor.WriteLine(f08);
                        escrituraFactura += f08;
                    }
                    else
                    {
                        if (rtiva == 1 && rtisr == 1)
                        {
                            escrituraFactura = escrituraFactura.Replace(Environment.NewLine, "");
                            escritor.WriteLine(f07);
                            escrituraFactura += f07;
                            escritor.WriteLine(f08);
                            escrituraFactura += f08;
                        }
                        else
                        {
                            escritor.WriteLine(f08);
                            escrituraFactura += f08;
                        }
                    }
                    //else
                    //{
                    //    escrituraFactura = escrituraFactura.Replace("| \r\n", "");
                    //    escritor.WriteLine(f08);
                    //    escrituraFactura += f08;
                    //}

                }
                if (nodeToFind == false && nodeToFind2 == true)
                {
                    if (rtiva == 1 && rtisr == 1)
                    {
                        escrituraFactura = escrituraFactura.Replace("| \r\n", "");
                        escritor.WriteLine(f07);
                        escrituraFactura += f07;
                        escritor.WriteLine(f08);
                        escrituraFactura += f08;
                    }
                    else
                    {
                        escritor.WriteLine(f07);
                        escrituraFactura += f07;
                    }
                    //if (srtisr == 2)
                    //{
                    //    escritor.WriteLine(f07);
                    //    escrituraFactura += f07;
                    //}
                    if (usdmoneda == 1)
                    {
                        f07 = "";
                        f08 = "";
                        escritor.WriteLine(f07);
                        escrituraFactura += f07;
                        escritor.WriteLine(f08);
                        escrituraFactura += f08;
                    }
                    else
                    {
                        escritor.WriteLine(f07);
                        escrituraFactura += f07;
                    }

                }






                //----------------------------------------Seccion de detalles del complemento de pago -------------------------------------------------------------------


            }
            string[] strAllLines = File.ReadAllLines(path);
            File.WriteAllLines(path, strAllLines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray());
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
                //string f01 = "01"   // LISTO

                //01 INFORMACION GENERAL DEL CFDI (1:1)
                if (formadepago.Trim() != "02")
                {
                    escritor.WriteLine(
                    "01"                                                //1.-Tipo De Registro
                    + "|" + idcomprobante.Trim()  //2.- IdComprobante
                    + "|" + seriee.Trim()         //3. Serie
                    + "|" + folioe.Trim()         //4.  
                    + "|" + fechaemision.Trim()
                    + "|" + subt.Trim()
                    + "|"
                    + "|"
                    + "|"
                    + "|" + total.Trim()
                    + "|"
                    + "|" + "03"
                    + "|"
                    + "|" + metodopago33 // este viene de CPADOC
                    + "|" + txtMoneda.Text
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
                    escrituraFactura += "01"                                                //1.-Tipo De Registro
                    + "|" + idcomprobante.Trim()  //2.- IdComprobante
                    + "|" + seriee.Trim()         //3. Serie
                    + "|" + folioe.Trim()         //4.  
                    + "|" + fechaemision.Trim()
                    + "|" + subt.Trim()
                    + "|"
                    + "|"
                    + "|"
                    + "|" + total.Trim()
                    + "|"
                    + "|" + "03"
                    + "|"
                    + "|" + metodopago33 // este viene de CPADOC
                    + "|" + txtMoneda.Text
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
                   "01"                                                //1.-Tipo De Registro
                   + "|" + idcomprobante.Trim()  //2.- IdComprobante
                   + "|" + seriee.Trim()         //3. Serie
                   + "|" + folioe.Trim()         //4.  
                   + "|" + fechaemision.Trim()
                   + "|" + subt.Trim()
                   + "|"
                   + "|"
                   + "|"
                   + "|" + total.Trim()
                   + "|"
                   + "|" + "03"
                   + "|"
                   + "|" + metodopago33 // este viene de CPADOC
                   + "|" + txtMoneda.Text
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
                    escrituraFactura += "01"                                                //1.-Tipo De Registro
                    + "|" + idcomprobante.Trim()  //2.- IdComprobante
                    + "|" + seriee.Trim()         //3. Serie
                    + "|" + folioe.Trim()         //4.  
                    + "|" + fechaemision.Trim()
                    + "|" + subt.Trim()
                    + "|"
                    + "|"
                    + "|"
                    + "|" + total.Trim()
                    + "|"
                    + "|" + "03"
                    + "|"
                    + "|" + metodopago33 // este viene de CPADOC
                    + "|" + txtMoneda.Text
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
                //----------------------------------------Seccion de los datos del receptor del CFDI -------------------------------------------------------------------------------------

                //02 INFORMACION DEL RECEPTOR (1:1)
                if (monedascpadgoc.Trim() == "USD")
                {
                    escritor.WriteLine(
                       "02"                                                   //1-Tipo De Registro
                       + "|" + txtIdCliente.Text.Trim()                       //2-Id Receptor
                       + "|" + txtRFC.Text.Trim()                                //3-RFC
                       + "|" + txtCliente.Text.Trim()                         //4-Nombre
                       + "|"                             //5-Pais
                       + "|"                           //6-Calle
                       + "|"                           //7-Numero Exterior
                       + "|"                          //8-Numero Interior
                       + "|"                         //9-Colonia
                       + "|"                       //10-Localidad
                       + "|"                      //11-Referencia
                       + "|"                       //12-Municio/Delegacion
                       + "|"                           //13-EStado
                       + "|" + txtCP.Text.Trim()                              //14-Codigo Postal
                       + "|"                                                // paisresidencia                                 //15-Pais de Residecia Fiscal Cuando La Empresa Sea Extrajera
                       + "|"                                   //16-Numero de Registro de ID Tributacion 
                       + "|"                                     //17-Correo de envio                                                    
                       + "|" + "601"
                       + "|"      //Fin Del Registro 
                       );

                    escrituraFactura += "\\n02"                                                   //1-Tipo De Registro
                      + "|" + txtIdCliente.Text.Trim()                       //2-Id Receptor
                       + "|" + txtRFC.Text.Trim()                                //3-RFC
                       + "|" + txtCliente.Text.Trim()                         //4-Nombre
                       + "|"                             //5-Pais
                       + "|"                           //6-Calle
                       + "|"                           //7-Numero Exterior
                       + "|"                          //8-Numero Interior
                       + "|"                         //9-Colonia
                       + "|"                       //10-Localidad
                       + "|"                      //11-Referencia
                       + "|"                       //12-Municio/Delegacion
                       + "|"                           //13-EStado
                       + "|" + txtCP.Text.Trim()                              //14-Codigo Postal
                       + "|"                                                // paisresidencia                                 //15-Pais de Residecia Fiscal Cuando La Empresa Sea Extrajera
                       + "|"                                   //16-Numero de Registro de ID Tributacion 
                       + "|"                                     //17-Correo de envio                                                    
                       + "|" + "601"
                       + "|";

                }
                else
                {

                    escritor.WriteLine(
                        "02"                                                   //1-Tipo De Registro
                        + "|" + txtIdCliente.Text.Trim()                       //2-Id Receptor
                        + "|" + txtRFC.Text.Trim()                                //3-RFC
                        + "|" + txtCliente.Text.Trim()                         //4-Nombre
                        + "|"                             //5-Pais
                        + "|"                           //6-Calle
                        + "|"                           //7-Numero Exterior
                        + "|"                          //8-Numero Interior
                        + "|"                         //9-Colonia
                        + "|"                       //10-Localidad
                        + "|"                      //11-Referencia
                        + "|"                       //12-Municio/Delegacion
                        + "|"                           //13-EStado
                        + "|" + txtCP.Text.Trim()                              //14-Codigo Postal
                        + "|"                                                // paisresidencia                                 //15-Pais de Residecia Fiscal Cuando La Empresa Sea Extrajera
                        + "|"                                   //16-Numero de Registro de ID Tributacion 
                        + "|"                                     //17-Correo de envio                                                    
                        + "|" + "601"
                        + "|"      //Fin Del Registro 
                        );

                    escrituraFactura += "\\n02"                                                   //1-Tipo De Registro
                      + "|" + txtIdCliente.Text.Trim()                       //2-Id Receptor
                       + "|" + txtRFC.Text.Trim()                                //3-RFC
                       + "|" + txtCliente.Text.Trim()                         //4-Nombre
                       + "|"                             //5-Pais
                       + "|"                           //6-Calle
                       + "|"                           //7-Numero Exterior
                       + "|"                          //8-Numero Interior
                       + "|"                         //9-Colonia
                       + "|"                       //10-Localidad
                       + "|"                      //11-Referencia
                       + "|"                       //12-Municio/Delegacion
                       + "|"                           //13-EStado
                       + "|" + txtCP.Text.Trim()                              //14-Codigo Postal
                       + "|"                                                // paisresidencia                                 //15-Pais de Residecia Fiscal Cuando La Empresa Sea Extrajera
                       + "|"                                   //16-Numero de Registro de ID Tributacion 
                       + "|"                                     //17-Correo de envio                                                    
                       + "|" + "601"
                       + "|";

                }

                //----------------------------------------Seccion de detalles del complemento de pago -------------------------------------------------------------------

                //04 INFORMACION DE LOS CONCEPTOS (1:N)
                escritor.WriteLine(
               "04"                                                   //1-Tipo De Registro
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
              + "|"
              );


                escrituraFactura += "\\n04"                                                   //1-Tipo De Registro
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
               + "|" + identificador.Trim()                                  //2-Identificador
               + "|" + version.Trim()                                        //3-Version                                             
               + "|" + fechapago.Trim()                                      //4-Fechapago
               + "|" + formadepago.Trim()                              //5-Formadepagocpag
               + "|" + monedascpadgoc.Trim()                                     //6-Monedacpag
               + "|" + tipodecambiocpag.Trim()                               //7-TipoDecambiocpag AQUI LO VOY A TOMAR DE OTRA CONSULTA
               + "|" + txtTotal.Text.Trim()                                 //8-Monto
               + "|" + numerooperacion.Trim()                               //9-NumeroOperacion
               + "|" + txtRFCbancoEmisor.Text.Trim()                         //10-RFCEmisorCuentaBeneficiario
               + "|" + txtBancoEmisor.Text.Trim()                            //11-NombreDelBanco                                                                                            
               + "|" + txtCuentaPago.Text.Trim()                            //12-NumeroCuentaOrdenante
               + "|" + rfcemisorcuentaben.Trim()                           //13-RFCEmisorCuentaBeneficiario
               + "|" + numcuentaben.Trim()                                //14-NumCuentaBeneficiario
               + "|" + tipocadenapago.Trim()                                //15-TipoCadenaPago                                               
               + "|" + certpago.Trim()                                    //16-CertificadoPago
               + "|" + cadenadelpago.Trim()                                //17-CadenaDePago
               + "|" + sellodelpago.Trim()                                  //Fin Del Registro
               + "|"
                );

                escrituraFactura += "CPAG"                                                 //1-Tipo De Registro
               + "|" + identificador.Trim()                                 //2-Identificador
               + "|" + version.Trim()                                   //3-Version                                             
               + "|" + fechapago.Trim()                                     //4-Fechapago
               + "|" + formadepago.Trim()                              //5-Formadepagocpag
               + "|" + monedascpadgoc.Trim()                                    //6-Monedacpag
               + "|" + tipodecambiocpag.Trim()                              //7-TipoDecambiocpag
               + "|" + txtTotal.Text.Trim()                                 //8-Monto
               + "|" + numerooperacion.Trim()                               //9-NumeroOperacion
               + "|" + txtRFCbancoEmisor.Text.Trim()                        //10-RFCEmisorCuentaBeneficiario
               + "|" + txtBancoEmisor.Text.Trim()                          //11-NombreDelBanco                                                                                            
               + "|" + txtCuentaPago.Text.Trim()                           //12-NumeroCuentaOrdenante
               + "|" + rfcemisorcuentaben.Trim()                            //13-RFCEmisorCuentaBeneficiario
               + "|" + numcuentaben.Trim()                                //14-NumCuentaBeneficiario
               + "|" + tipocadenapago.Trim()                                //15-TipoCadenaPago                                               
               + "|" + certpago.Trim()                                  //16-CertificadoPago
               + "|" + cadenadelpago.Trim()                                 //17-CadenaDePago
               + "|" + sellodelpago.Trim()                                 //Fin Del Registro
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
        public void generadorTXT()
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

            string path = System.Web.Configuration.WebConfigurationManager.AppSettings["dir2"] + lblFact.Text + ".txt";
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
                    + "|" + "0"                                       //10-Total
                    + "|" + cantidadletra.Trim()                        //11-Total Con Letra
                    + "|"                         //12-Forma De Pago
                    + "|" + cond                                        //13-Condiciones De Pago
                    + "|"                                 //14-Metodo de Pago
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
                    + "|" + "0"                                       //10-Total
                    + "|" + cantidadletra.Trim()                        //11-Total Con Letra
                    + "|"                        //12-Forma De Pago
                    + "|" + cond                                        //13-Condiciones De Pago
                    + "|"                                 //14-Metodo de Pago
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
                    + "|" + "0"                                       //10-Total
                    + "|" + cantidadletra.Trim()                        //11-Total Con Letra
                    + "|"                      //12-Forma De Pago
                    + "|" + cond                                        //13-Condiciones De Pago
                    + "|"                                 //14-Metodo de Pago
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
                   + "|" + "0"                                       //10-Total
                   + "|" + cantidadletra.Trim()                        //11-Total Con Letra
                   + "|"                          //12-Forma De Pago
                   + "|" + cond                                        //13-Condiciones De Pago
                   + "|"                                 //14-Metodo de Pago
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
                if (monedascpadgoc.Trim() == "USD")
                {
                    escritor.WriteLine(
                       "02"                                                   //1-Tipo De Registro
                       + "|" + txtIdCliente.Text.Trim()                       //2-Id Receptor
                       + "|" + txtRFC.Text.Trim()                                //3-RFC
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
                       + "|"                                                // paisresidencia                                 //15-Pais de Residecia Fiscal Cuando La Empresa Sea Extrajera
                       + "|"                                   //16-Numero de Registro de ID Tributacion 
                       + "|" + mailenvio                                      //17-Correo de envio                                                    
                       + "|"                                                  //Fin Del Registro 
                       );

                    escrituraFactura += "\\n02"                                                   //1-Tipo De Registro
                       + "|" + txtIdCliente.Text.Trim()                       //2-Id Receptor
                       + "|" + txtRFC.Text.Trim()                                //3-RFC
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
                       + "|"                                          // paisresidencia                                 //15-Pais de Residecia Fiscal Cuando La Empresa Sea Extrajera
                       + "|"                                   //16-Numero de Registro de ID Tributacion 
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
               + "|" + consecutivoconcepto.Trim()                            //2-Consecutivo Concepto
               + "|" + claveproductoservicio.Trim()                          //3-Clave Producto o Servicio SAT                                               
               + "|" + numidentificacion.Trim()                              //4-Numero Identificacion TDR
               + "|" + txtCantidad.Text.Trim()                        //5-Cantidad
               + "|" + claveunidad.Trim()                                    //6-Clave Unidad SAT
               + "|"                                                  //7-Unidad de Medida
               + "|" + txtConcepto.Text.Trim()                                //8-Descripcion
               + "|" + "0"                                  //9-Valor Unitario
               + "|" + "0"                                        //10-Importe
               + "|" + descuento.Trim()                                      //11-Descuento                                                  
                                                                             //12 Importe con iva si el rfc es XAXX010101000 y XEXX010101000 OPCIONAL
               + "|"                                                  //Fin Del Registro
                );

                escrituraFactura += "\\n04"                                                   //1-Tipo De Registro
               + "|" + consecutivoconcepto.Trim()                            //2-Consecutivo Concepto
               + "|" + claveproductoservicio.Trim()                          //3-Clave Producto o Servicio SAT                                               
               + "|" + numidentificacion.Trim()                             //4-Numero Identificacion TDR
               + "|" + txtCantidad.Text.Trim()                        //5-Cantidad
               + "|" + claveunidad.Trim()                                   //6-Clave Unidad SAT
               + "|"                                                  //7-Unidad de Medida
               + "|" + txtConcepto.Text.Trim()                                //8-Descripcion
               + "|" + "0"                                  //9-Valor Unitario
               + "|" + "0"                                        //10-Importe
               + "|" + descuento.Trim()                                      //11-Descuento                                                  
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
               + "|" + identificador.Trim()                                  //2-Identificador
               + "|" + version.Trim()                                        //3-Version                                             
               + "|" + fechapago.Trim()                                      //4-Fechapago
               + "|" + formadepago.Trim()                              //5-Formadepagocpag
               + "|" + monedascpadgoc.Trim()                                     //6-Monedacpag
               + "|" + tipodecambiocpag.Trim()                               //7-TipoDecambiocpag AQUI LO VOY A TOMAR DE OTRA CONSULTA
               + "|" + txtTotal.Text.Trim()                                 //8-Monto
               + "|" + numerooperacion.Trim()                               //9-NumeroOperacion
               + "|" + txtRFCbancoEmisor.Text.Trim()                         //10-RFCEmisorCuentaBeneficiario
               + "|" + txtBancoEmisor.Text.Trim()                            //11-NombreDelBanco                                                                                            
               + "|" + txtCuentaPago.Text.Trim()                            //12-NumeroCuentaOrdenante
               + "|" + rfcemisorcuentaben.Trim()                           //13-RFCEmisorCuentaBeneficiario
               + "|" + numcuentaben.Trim()                                //14-NumCuentaBeneficiario
               + "|" + tipocadenapago.Trim()                                //15-TipoCadenaPago                                               
               + "|" + certpago.Trim()                                    //16-CertificadoPago
               + "|" + cadenadelpago.Trim()                                //17-CadenaDePago
               + "|" + sellodelpago.Trim()                                  //Fin Del Registro
               + "|"
                );

                escrituraFactura += "CPAG"                                                 //1-Tipo De Registro
               + "|" + identificador.Trim()                                 //2-Identificador
               + "|" + version.Trim()                                   //3-Version                                             
               + "|" + fechapago.Trim()                                     //4-Fechapago
               + "|" + formadepago.Trim()                              //5-Formadepagocpag
               + "|" + monedascpadgoc.Trim()                                    //6-Monedacpag
               + "|" + tipodecambiocpag.Trim()                              //7-TipoDecambiocpag
               + "|" + txtTotal.Text.Trim()                                 //8-Monto
               + "|" + numerooperacion.Trim()                               //9-NumeroOperacion
               + "|" + txtRFCbancoEmisor.Text.Trim()                        //10-RFCEmisorCuentaBeneficiario
               + "|" + txtBancoEmisor.Text.Trim()                          //11-NombreDelBanco                                                                                            
               + "|" + txtCuentaPago.Text.Trim()                           //12-NumeroCuentaOrdenante
               + "|" + rfcemisorcuentaben.Trim()                            //13-RFCEmisorCuentaBeneficiario
               + "|" + numcuentaben.Trim()                                //14-NumCuentaBeneficiario
               + "|" + tipocadenapago.Trim()                                //15-TipoCadenaPago                                               
               + "|" + certpago.Trim()                                  //16-CertificadoPago
               + "|" + cadenadelpago.Trim()                                 //17-CadenaDePago
               + "|" + sellodelpago.Trim()                                 //Fin Del Registro
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

                generaTXTCP();


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
                    string msg = "¡Error, ponte en contacto con TI";
                    ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Error con los folios relacionados ', 'error');setTimeout(function(){window.location.href ='Listado.aspx'}, 10000)", true);
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
                    facLabControler.Elist(txtFolio.Text);
                    string msg = "¡Se ha generado correctamente el CFDi!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Carga exitosa', 'success');setTimeout(function(){window.location.href ='Listado.aspx'}, 10000)", true);
                }
                else
                {
                    string msg = "¡Error al conectar al servicio XSA!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Error', 'error');setTimeout(function(){window.location.href ='Listado.aspx'}, 10000)", true);

                }
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {



            string msg = "¡Se ha generado correctamente el CFDi!";
            ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Carga exitosa', 'success');", true);





        }

        protected void btnGenerarTxt_Click(object sender, EventArgs e)
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
            btnEdit.Visible = false;
            btnGuardar.Visible = false;
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
                generaTXT2();
                //generadorTXT();
                //facLabControler.Elist(txtFolio.Text);
                string msg = "¡Se genero correctamente el TXT!";
                ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Success', 'success');setTimeout(function(){window.location.href ='DownloadTxt.aspx'}, 10000)", true);


            }
        }
    }
}
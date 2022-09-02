using CARGAR_EXCEL.Controllers;
using CARGAR_EXCEL.Models;
using Newtonsoft.Json;
using RestSharp;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace CARGAR_EXCEL
{
    public partial class CartaPorte : System.Web.UI.Page
    {
        public facLabController facLabControler = new facLabController();
        //public FacCpController facLabControler = new FacCpController();

        public string fDesde, fHasta, concepto, tipoCobro, tipocomprobante, lugarexpedicion, metodopago33, formadepago, usocfdi, confirmacion, paisresidencia, numtributacion
        , mailenvio, numidentificacion, claveunidad, tipofactoriva, tipofactorret, coditrans, tipofactor, tasatras, codirete, tasarete, relacion, montosoloiva, montoivarete
        , ivadeiva, ivaderet, retderet, conceptoretencion, consecutivoconcepto, claveproductoservicio, valorunitario, importe, descuento, cantidadletra, uuidrel
        , identificador, version, fechapago, monedacpag, tipodecambiocpag, monto, numerooperacion, rfcemisorcuenta, nombrebanco, numerocuentaord, rfcemisorcuentaben, numcuentaben
        , tipocadenapago, certpago, cadenadelpago, sellodelpago, identpag, identdocpago, seriecpag, foliocpag, monedacpagdoc, tipocambiocpag, metododepago, numerodeparcialidad
        , importeSaldoAnterior, importepago, importesaldoinsoluto, total, subt, ivat, rett, cond, tipoc, seriee, folioe, sfolio, Foliosrelacionados, serier, folior, uuidpagadas, IdentificadorDelDocumentoPagado, ipagado, nparcialidades, folio, MetdodoPago, Dserie, monedascpadgoc, interiorsaldoanterior, isaldoinsoluto, identificaciondpago, folioscpag, k1, k3, norden, tmoneda, idcomprobante, cantidad, descripcion, Tuuid, iddelpago, iipagado, basecalculado, basecalculado2, basecalculado3, impSaldoAnterior, impSaldoInsoluto, fechap, fechaemision, f03, totaliva, totalisr, if05, f08, if06, TotaldeRe, TotaldeIva,f07, importePagosTotal;

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

        public decimal importePagos37 = 0;
        public decimal importePagos38 = 0;
        public decimal importePagos88 = 0;
        public decimal importePagos97 = 0;
        public decimal importePagos99 = 0;
        public decimal importePagos98 = 0;
        public decimal importePagosTotal2 = 0;
        public bool nodeToFind;
        public bool nodeToFind2;




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
            //imgFDesde.Visible = false;
            //imgFHasta.Visible = false;
            lblFact.Text = Request.QueryString["factura"];
            //lblFact.Text = "40750";
            //foliot = Request.QueryString["factura"];
            if (IsPostBack)
            {
                //fDesde = txtFechaDesde.Text;
                //fHasta = txtFechaHasta.Text;
                //concepto = txtConcepto.Text;
                //tipoCobro = txtTipoCobro.Text;
                //formadepago = txtFormaPago.Text;
                lblFact.Text = "";
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
            
            DataTable td = facLabControler.getCartasPorte(lblFact.Text);
            Div1.Visible = false;
            //Obtencion de datos------------------------------------------------------------------------------------------------------------------------ -

            foreach (DataRow row in td.Rows)
            {
                     lblFact.Text = row["Folio"].ToString();
                  
                    txtDesc.NavigateUrl = row["Pdf_descargaFactura"].ToString();
            }

        }
    }
}
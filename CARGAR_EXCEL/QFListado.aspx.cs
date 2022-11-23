using CARGAR_EXCEL.Controllers;
using CARGAR_EXCEL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CARGAR_EXCEL
{
    public partial class QFListado : System.Web.UI.Page
    {
        public facLabController facLabControler = new facLabController();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string numero = Folio.Text.Trim();
            string billto = Billto.Text.Trim();
            string folio = billto + numero;
            //string merror = "<br> <br>";
            //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert("+merror+");", true);
            //string msg = "pariatur";
            //ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Factura timbrada ', 'success');setTimeout(function(){window.location.href ='Listado.aspx'}, 10000)", true);
            //pop(numero);

            valida(folio);
        }

        public void valida(string folio)
        {
            //TextBox1.Value = folio;
            facLabControler.Elist(folio);
            string msg = "Se agrego el: " + folio;
            ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Registro exitoso ', 'success');setTimeout(function(){window.location.href ='Listado.aspx'}, 10000)", true);
            //string msg = "Folio agregado:" + folio;
            //ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert()", true);
        }


    }
}
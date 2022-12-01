using CARGAR_EXCEL.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CARGAR_EXCEL
{
    public partial class DeleteBillto : System.Web.UI.Page
    {
        public facLabController facLabControler = new facLabController();
        protected void Page_Load(object sender, EventArgs e)
        {
            string id_num = Request.QueryString["idnum"];
            valida(id_num);
            
        }
        public void valida(string id_num)
        {
            //TextBox1.Value = folio;
            facLabControler.IFpagoDelete(id_num);
            string msg = "Se elimino el billto";
            ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Eliminación exitosa ', 'success');setTimeout(function(){window.location.href ='FDPago.aspx'}, 10000)", true);
        }
    }
}
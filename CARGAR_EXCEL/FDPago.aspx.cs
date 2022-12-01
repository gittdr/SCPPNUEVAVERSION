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
    public partial class FDPago : System.Web.UI.Page
    {
        public facLabController facLabControler = new facLabController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //cargaFacturas();
                okTralix();


            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //string numero = Folio.Text.Trim();
            string billto = Billto.Text.Trim().ToUpper();
            
            //string merror = "<br> <br>";
            //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert("+merror+");", true);
            //string msg = "pariatur";
            //ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Factura timbrada ', 'success');setTimeout(function(){window.location.href ='Listado.aspx'}, 10000)", true);
            //pop(numero);

            valida(billto);
        }

        public void valida(string billto)
        {
            //TextBox1.Value = folio;
            facLabControler.IFpago(billto);
            string msg = "Se agrego el billto: " + billto;
            ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Registro exitoso ', 'success');setTimeout(function(){window.location.href ='FDPago.aspx'}, 5000)", true);
            //string msg = "Folio agregado:" + folio;
            //ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert()", true);
        }
        private async Task okTralix()
        {

            DataTable cargaStops = facLabControler.billtoFp();
            int numCells = 2;
            int rownum = 0;
            foreach (DataRow item in cargaStops.Rows)
            {
                string billtor = item["billto"].ToString();
                        TableRow r = new TableRow();
                        for (int i = 0; i < numCells; i++)
                        {
                            if (i == 0)
                            {

                                HyperLink hp1 = new HyperLink();
                                hp1.ID = "hpIndex" + rownum.ToString();
                                hp1.Text = "<i class='fa fa-minus-square btn btn-danger' aria-hidden='true'></i>";
                                hp1.NavigateUrl = "DeleteBilltoFp.aspx?idnum=" + item[i].ToString()+"&billto="+billtor;
                                TableCell c = new TableCell();
                                c.Controls.Add(hp1);
                                r.Cells.Add(c);

                            }
                            else
                            {
                                TableCell c = new TableCell();
                                c.Controls.Add(new LiteralControl("row "
                                    + rownum.ToString() + ", cell " + i.ToString()));
                                c.Text = item[i].ToString();
                                r.Cells.Add(c);
                            }
                        }


                        tablaStops.Rows.Add(r);
                        rownum++;
                    

                
            }
        }


    }
}
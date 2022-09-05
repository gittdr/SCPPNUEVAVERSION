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
    public partial class Listado : System.Web.UI.Page
    {
        public FacCpController facLabControler = new FacCpController();
        //public GridViewControl gridControl = new GridViewControl();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                //cargaFacturas();
                okTralix();


            }

            if (hdFiltrar2.Value == "entra")
            {
                cargaFacturasFiltradas();
                hdFiltrar2.Value = "";
            }
        }
        //public void spinner()
        //{
        //    var t = Task.Run(async delegate
        //    {
        //        await Task.Delay(110000);
        //        return divLoading.Visible = false;
        //    });
        //    t.Wait();

        //}
        //var request28 = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico=" + row["IdentificadorDelPago"].ToString() + "&rfc=" + txtRFC.Text);
        //var response28 = (HttpWebResponse)request28.GetResponse();
        //var responseString28 = new StreamReader(response28.GetResponseStream()).ReadToEnd();

        //List<ModelFact> separados8 = JsonConvert.DeserializeObject<List<ModelFact>>(responseString28);

        //                if (separados8 != null)
        //                {
        //                    foreach (var item in separados8)
        //                    {

        private async Task okTralix()
        {
            DataTable cargaStops = facLabControler.facturas();
            int numCells = 3;
            int rownum = 0;
            foreach (DataRow item in cargaStops.Rows)
            {
                string folio = item["Folio"].ToString();
                var request28 = (HttpWebRequest)WebRequest.Create("https://canal1.xsa.com.mx:9050/bf2e1036-ba47-49a0-8cd9-e04b36d5afd4/cfdis?folioEspecifico=" + folio);
                var response28 = (HttpWebResponse)request28.GetResponse();
                var responseString28 = new StreamReader(response28.GetResponseStream()).ReadToEndAsync();

                List<ModelFact> separados8 = JsonConvert.DeserializeObject<List<ModelFact>>(await responseString28);
                if (separados8 != null)
                {
                    DataTable sae_ar = facLabControler.Elist2(folio);
                    if (sae_ar.Rows.Count == 0)
                    {
                        TableRow r = new TableRow();
                        for (int i = 0; i < numCells; i++)
                        {
                            if (i == 0)
                            {
                                HyperLink hp1 = new HyperLink();
                                hp1.ID = "hpIndex" + rownum.ToString();
                                hp1.Text = "<button type='button' class='btn btn-primary'>" + item[i].ToString() + "</button>";
                                hp1.NavigateUrl = "DetallesComplemento.aspx?factura=" + item[i].ToString();
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
        private void cargaFacturas()
        {
            
            DataTable cargaStops = facLabControler.facturas();
            //cargaStops.AsDataView().RowFilter("");
            int numCells = 3;
            int rownum = 0;
            //cargaStops = cargaStops.Orde
            foreach (DataRow row in cargaStops.Rows)
            {
                TableRow r = new TableRow();
                for (int i = 0; i < numCells; i++)
                {
                    if (i == 0)
                    {
                        HyperLink hp1 = new HyperLink();
                        hp1.ID = "hpIndex" + rownum.ToString();
                        hp1.Text = "<button type='button' class='btn btn-primary'>" + row[i].ToString() + "</button>";
                        hp1.NavigateUrl = "DetallesComplemento.aspx?factura=" + row[i].ToString();
                        TableCell c = new TableCell();
                        c.Controls.Add(hp1);
                        r.Cells.Add(c);
                        
                    }
                    else
                    {
                        TableCell c = new TableCell();
                        c.Controls.Add(new LiteralControl("row "
                            + rownum.ToString() + ", cell " + i.ToString()));
                        c.Text = row[i].ToString();
                        r.Cells.Add(c);
                    }
                }


                tablaStops.Rows.Add(r);
                rownum++;
                
            }
            
        }
        private void cargaFacturasFiltradas()
        {
            tablaStops.Rows.Clear();
            DataTable cargaStops = facLabControler.facturas();
            DataView dv = new DataView(cargaStops);
            dv.RowFilter = "Convert([Folio], System.String) like '%" + txtFiltro.Text + "%' or Cliente like '%" + txtFiltro.Text + "%' or Convert([Fecha], System.String) like '%" + txtFiltro.Text + "%'"; // query example = "id = 10"

            //encabezado


            int numCells = 3;
            int rownum = 0;
            //cargaStops = cargaStops.Orde
            foreach (DataRow row in dv.ToTable().Rows)
            {
                TableRow r = new TableRow();
                for (int i = 0; i < numCells; i++)
                {
                    if (i == 0)
                    {
                        HyperLink hp1 = new HyperLink();
                        hp1.ID = "hpIndex" + rownum.ToString();
                        hp1.Text = "<button type='button' class='btn btn-primary'>" + row[i].ToString() + "</button>";
                        hp1.NavigateUrl = "DetallesComplemento.aspx?factura=" + row[i].ToString();
                        TableCell c = new TableCell();
                        c.Controls.Add(hp1);
                        r.Cells.Add(c);
                    }
                    else
                    {
                        TableCell c = new TableCell();
                        c.Controls.Add(new LiteralControl("row "
                            + rownum.ToString() + ", cell " + i.ToString()));
                        c.Text = row[i].ToString();
                        r.Cells.Add(c);
                    }
                }


                tablaStops.Rows.Add(r);
                rownum++;
            }
        }


    }
}
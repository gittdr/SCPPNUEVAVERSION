using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Data;
using System.Data.SqlClient;

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Globalization;

namespace CARGAR_EXCEL
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usr_userid"]==null)
            {
                Response.Redirect("Login.aspx");
            }
            lblUserDetails.Text = "Usuario: " + Session["usr_userid"];
            
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }
        

            protected void Button1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                
                //string ruta_carpeta = HttpContext.Current.Server.MapPath("~/Temporal");

                //if (!Directory.Exists(ruta_carpeta))
                //{
                //    Directory.CreateDirectory(ruta_carpeta);
                //}

                ////GUARDAMOS EL ARCHIVO EN LOCAL
                //var ruta_guardado = Path.Combine(ruta_carpeta, FileUpload1.FileName);
                //FileUpload1.SaveAs(ruta_guardado);


                //IWorkbook MiExcel = null;
                //FileStream fs = new FileStream(ruta_guardado, FileMode.Open, FileAccess.Read);

                //if (Path.GetExtension(ruta_guardado) == ".xlsx")
                //    MiExcel = new XSSFWorkbook(fs);
                //else
                //    MiExcel = new HSSFWorkbook(fs);


                //ISheet hoja = MiExcel.GetSheetAt(0);

                //DataTable table = new DataTable();

                //table.Columns.Add("Ai_orden", typeof(int));
                //table.Columns.Add("Av_cmd_code", typeof(string));
                //table.Columns.Add("Av_cmd_description", typeof(string));
                //table.Columns.Add("Af_weight", typeof(float));
                //table.Columns.Add("Av_weightunit", typeof(string));
                //table.Columns.Add("Af_count", typeof(float));
                //table.Columns.Add("Av_countunit", typeof(string));

                //if (hoja != null) {

                //    int cantidadfilas = hoja.LastRowNum;

                //    for (int i = 1; i <= cantidadfilas; i++) {
                //        IRow fila = hoja.GetRow(i);


                //        if(fila != null)
                //            table.Rows.Add(
                //                fila.GetCell(0, MissingCellPolicy.RETURN_NULL_AND_BLANK) != null ? fila.GetCell(0, MissingCellPolicy.RETURN_NULL_AND_BLANK).NumericCellValue.ToString() : "",
                //                fila.GetCell(1, MissingCellPolicy.RETURN_NULL_AND_BLANK) != null ? fila.GetCell(1, MissingCellPolicy.RETURN_NULL_AND_BLANK).ToString() : "",
                //                fila.GetCell(2, MissingCellPolicy.RETURN_NULL_AND_BLANK) != null ? fila.GetCell(1, MissingCellPolicy.RETURN_NULL_AND_BLANK).ToString()+"-"+fila.GetCell(2, MissingCellPolicy.RETURN_NULL_AND_BLANK).ToString() : "",
                //                fila.GetCell(3, MissingCellPolicy.RETURN_NULL_AND_BLANK) != null  ? fila.GetCell(3, MissingCellPolicy.RETURN_NULL_AND_BLANK).NumericCellValue.ToString() : "",
                //                 fila.GetCell(4, MissingCellPolicy.RETURN_NULL_AND_BLANK) != null ? fila.GetCell(4, MissingCellPolicy.RETURN_NULL_AND_BLANK).ToString() : "",
                //                 fila.GetCell(5, MissingCellPolicy.RETURN_NULL_AND_BLANK) != null ? fila.GetCell(5, MissingCellPolicy.RETURN_NULL_AND_BLANK).NumericCellValue.ToString() : "",
                //                 fila.GetCell(6, MissingCellPolicy.RETURN_NULL_AND_BLANK) != null ? fila.GetCell(6, MissingCellPolicy.RETURN_NULL_AND_BLANK).ToString() : ""
                //                );
                //    }
                //}

                //GridView1.DataSource = table;
                //GridView1.DataBind();
                ////string nombre = txtName.Text;
                ////txtName.Text = "";
                ////ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "hola" + nombre + "');", true);
                //int resultado = cargarEnSQL(table);

                //if (resultado == 1) {
                //    GridView1.DataSource = table;
                //    GridView1.DataBind();
                //}
                string csvPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);

                FileUpload1.SaveAs(csvPath);

                //Create a DataTable.
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[9] {
                new DataColumn("Ai_orden", typeof(int)),
                new DataColumn("Av_cmd_code", typeof(string)),
                new DataColumn("Av_cmd_description", typeof(string)),
                new DataColumn("Af_count", typeof(string)),
                new DataColumn("Av_countunit",typeof(string)),
                new DataColumn("Av_description_parts", typeof(string)),
                new DataColumn("Af_weight", typeof(float)),
                new DataColumn("Av_weightunit",typeof(string)),
                new DataColumn("Av_description_units", typeof(string))
            });

                //Read the contents of CSV file.
                string csvData = File.ReadAllText(csvPath);

                //Execute a loop over the rows.
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        dt.Rows.Add();
                        int i = 0;

                        //Execute a loop over the columns.
                        foreach (string cell in row.Split('\t'))
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell;
                            i++;
                        }
                    }
                }

                //Bind the DataTable.
                //GridView1.DataSource = dt;
                //GridView1.DataBind();
                //GridView1.DataSource = table;
                //GridView1.DataBind();
                ////string nombre = txtName.Text;
                ////txtName.Text = "";
                ////ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "hola" + nombre + "');", true);
                int resultado = cargarEnSQL(dt);

                if (resultado == 1)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    string msg = "¡Carga Masiva Exitosa!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Carga exitosa', 'success');", true);
                }
                else
                {
                    string msg = "La orden ya fue procesada";
                    ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Intenten cargar otra orden', 'error');", true);
                }
            }
            
            

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                
                    string msg = "¡Carga Masiva Exitosa!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Carga exitosa', 'success');", true);
                
            }



        }




        public int cargarEnSQL(DataTable tabla)
        {
            
            int resultado = 0;
            try
            {
                //NOS CONECTAMOS CON LA BASE DE DATOS
                string cadena = @"Data source=DESKTOP-CV57FOU\SQLEXPRESS; Initial Catalog=DBCARGA; User ID=jdev; Password=tdr123;Trusted_Connection=false;MultipleActiveResultSets=true";
                using (SqlConnection cn = new SqlConnection(cadena))
                {
                    SqlCommand cmd = new SqlCommand("usp_cargarxorden", cn);
                    //cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.Add("EstructuraCargaxOrdenMe", SqlDbType.Structured).Value = tabla;
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                   
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                }
            }
            catch (Exception ex)
            {

                string mensaje = ex.Message.ToString();
                resultado = 0;
            }

            return resultado;
        }


    }
}
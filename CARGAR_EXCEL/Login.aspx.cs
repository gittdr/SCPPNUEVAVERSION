using NPOI.POIFS.Crypt;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CARGAR_EXCEL
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string cadena = @"Data source=172.24.16.112; Initial Catalog=TMWSuite; User ID=sa; Password=tdr9312;Trusted_Connection=false;MultipleActiveResultSets=true";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM tlbUserAccess WHERE usr_mail = @usr_mail AND usr_password = @usr_password", cn);
                cmd.Parameters.AddWithValue("@usr_mail", txtUserName.Text.Trim());
                cmd.Parameters.AddWithValue("@usr_password", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(txtPassword.Text.Trim())));
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 1)
                {
                    Session["usr_userid"] = txtUserName.Text.Trim();
                    Response.Redirect("WebForm1.aspx");
                }
                else { lblErrorMessage.Visible = true; }
               
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CARGAR_EXCEL.Models
{
    public class ModelFact
    {
        public string uuid { get; set; }
        public string motivo { get; set; }
        public string status { get; set; }
        public string xmlDownload { get; set; }
        public string folio { get; set; }
        public string fecha { get; set; }
        public string serie { get; set; }
        public string rfc { get; set; }
        public string ord_hdrnumber { get; set; }
        public string tcfix { get; set; }



        public class User
        {
            public string identificadorDelPago { get; set; }

            public string fechapago { get; set; }
            public string formadepagocpag { get; set; }
            public string monedacpag { get; set; }
            public string mmonto { get; set; }
            public int folio { get; set; }
            public DateTime? fecha { get; set; }
            public string ord_billto { get; set; }
            public string serie { get; set; }
            public string tcfix { get; set; }
            


        }
        public DataTable getDatosFacturas(string fact)
        {
            DataTable dataTable = new DataTable();
            string cadena = @"Data source=172.24.16.113; Initial Catalog=TDR; User ID=sa; Password=tdr9312;Trusted_Connection=false;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand selectCommand = new SqlCommand("select * from vista_fe_copago where folio = @factura and medotodepago = 'PPD' union select * from vista_fe_copago_Enviados where folio = @factura and medotodepago = 'PPD' ", connection))
                {
                    selectCommand.CommandType = CommandType.Text;
                    selectCommand.CommandTimeout = 200;
                    selectCommand.Parameters.AddWithValue("@factura", (object)fact);
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                    {
                        try
                        {
                            selectCommand.Connection.Open();
                            sqlDataAdapter.Fill(dataTable);
                        }
                        catch (SqlException ex)
                        {
                            string message = ex.Message;
                        }
                    }
                }
            }
            return dataTable;
        }
        public DataTable getDatosCPAGDOC(string identificador)
        {
            DataTable dataTable2 = new DataTable();
            string cadena = @"Data source=172.24.16.113; Initial Catalog=TDR; User ID=sa; Password=tdr9312;Trusted_Connection=false;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand selectCommand = new SqlCommand("select * from vista_fe_copago_cpagdoc where identificadordelPago = @identificador", connection))
                {
                    selectCommand.CommandType = CommandType.Text;
                    selectCommand.CommandTimeout = 200;
                    selectCommand.Parameters.AddWithValue("@identificador", identificador);
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                    {
                        try
                        {
                            selectCommand.Connection.Open();
                            sqlDataAdapter.Fill(dataTable2);
                        }
                        catch (SqlException ex)
                        {
                            string message = ex.Message;
                        }
                    }
                }
            }
            return dataTable2;
        }
        public DataTable getDatosCPAGDOCTR(string identificador)
        {
            DataTable dataTable2 = new DataTable();
            string cadena = @"Data source=172.24.16.113; Initial Catalog=TDR; User ID=sa; Password=tdr9312;Trusted_Connection=false;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand selectCommand = new SqlCommand("select * from vista_fe_copago_cpagdoc where identificadordelPago = @identificador", connection))
                {
                    selectCommand.CommandType = CommandType.Text;
                    selectCommand.CommandTimeout = 200;
                    selectCommand.Parameters.AddWithValue("@identificador", identificador);
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                    {
                        try
                        {
                            selectCommand.Connection.Open();
                            sqlDataAdapter.Fill(dataTable2);
                        }
                        catch (SqlException ex)
                        {
                            string message = ex.Message;
                        }
                    }
                }
            }
            return dataTable2;
        }

        public DataTable getDatosCPAGDOCTRL(string identificador, string foliocpag)
        {


            DataTable dataTable3 = new DataTable();
            //NOS CONECTAMOS CON LA BASE DE DATOS
            string cadena = @"Data source=172.24.16.113; Initial Catalog=DYNAMICS; User ID=sa; Password=tdr9312;Trusted_Connection=false;MultipleActiveResultSets=true";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_ccpp", cn))
                    {
                        //Le indico que es del itpo procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        //Esta linea define un parametro
                        cmd.Parameters.AddWithValue("@identificador", identificador);
                        cmd.Parameters.AddWithValue("@foliocpag", foliocpag);
                        //Ejecutamos el procedimiento
                        cmd.ExecuteNonQuery();
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd))
                        {
                            try
                            {

                                sqlDataAdapter.Fill(dataTable3);
                                cn.Close();
                            }
                            catch (SqlException ex)
                            {
                                cn.Close();
                                string message = ex.Message;

                            }

                        }

                    }
                }
                catch (SqlException ex)
                {

                    cn.Close();
                    string message = ex.Message;

                }
            }

            return dataTable3;

        }
        public DataTable getDatosMaster(string identificador)
        {
            DataTable dataTable = new DataTable();
            string cadena = @"Data source=172.24.16.113; Initial Catalog=DYNAMICS; User ID=sa; Password=tdr9312;Trusted_Connection=false;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand selectCommand = new SqlCommand("select invoice as folio from [172.24.16.112].[TMWSuite].[dbo].VISTA_Fe_generadas where nmaster = @identificador", connection))
                {
                    selectCommand.CommandType = CommandType.Text;
                    selectCommand.Parameters.AddWithValue("@identificador", (object)identificador);
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                    {
                        try
                        {
                            selectCommand.Connection.Open();
                            sqlDataAdapter.Fill(dataTable);
                        }
                        catch (SqlException ex)
                        {
                            string message = ex.Message;
                        }
                    }
                }
            }
            return dataTable;
        }
        public DataTable getFacturasEnviadas()
        {
            DataTable dataTable = new DataTable();
            string cadena = @"Data source=172.24.16.113; Initial Catalog=TDR; User ID=sa; Password=tdr9312;Trusted_Connection=false;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand selectCommand = new SqlCommand("select CONVERT(INT, folio) as Folio, FechaHoraEmision as Fecha, Nombre as Cliente from vista_fe_copago_Enviados order by CONVERT(INT, folio) DESC", connection))
                {
                    selectCommand.CommandType = CommandType.Text;
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                    {
                        try
                        {
                            selectCommand.Connection.Open();
                            sqlDataAdapter.Fill(dataTable);
                        }
                        catch (SqlException ex)
                        {
                            string message = ex.Message;
                        }
                    }
                }
            }
            return dataTable;
        }
        public DataTable getFacturas()
        {
            DataTable dataTablee = new DataTable();
            string cadena = @"Data source=172.24.16.113; Initial Catalog=TDR; User ID=sa; Password=tdr9312;Trusted_Connection=false;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand selectCommand = new SqlCommand("select DISTINCT TOP 100 TRY_CONVERT(INT, REPLACE(Folio, '-','')) as Folio, FechaPago as Fecha, Nombre as Cliente,idreceptor, RFC from vista_fe_copago WHERE RFC != '' order by Folio DESC", connection))
                {
                    selectCommand.CommandType = CommandType.Text;
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                    {
                        try
                        {
                            selectCommand.Connection.Open();
                            sqlDataAdapter.Fill(dataTablee);
                        }
                        catch (SqlException ex)
                        {
                            string message = ex.Message;
                        }
                    }
                }
            }
            return dataTablee;
        }
        public DataTable getFacturasSinRfc()
        {
            DataTable dataTablee = new DataTable();
            string cadena = @"Data source=172.24.16.113; Initial Catalog=TDR; User ID=sa; Password=tdr9312;Trusted_Connection=false;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand selectCommand = new SqlCommand("select DISTINCT TOP 100 TRY_CONVERT(INT, REPLACE(Folio, '-','')) as Folio, FechaPago as Fecha, Nombre as Cliente,idreceptor, RFC from vista_fe_copago WHERE RFC = '' order by Folio DESC", connection))
                {
                    selectCommand.CommandType = CommandType.Text;
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                    {
                        try
                        {
                            selectCommand.Connection.Open();
                            sqlDataAdapter.Fill(dataTablee);
                        }
                        catch (SqlException ex)
                        {
                            string message = ex.Message;
                        }
                    }
                }
            }
            return dataTablee;
        }
        public DataTable getDatosInvoice(string identificador)
        {
            DataTable dataTable = new DataTable();
            string cadena = @"Data source=172.24.16.112; Initial Catalog=TMWSuite; User ID=sa; Password=tdr9312;Trusted_Connection=false;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand selectCommand = new SqlCommand("select ord_hdrnumber from invoiceheader where ivh_hdrnumber = @identificador", connection))
                {
                    selectCommand.CommandType = CommandType.Text;
                    selectCommand.Parameters.AddWithValue("@identificador", (object)identificador);
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                    {
                        try
                        {
                            selectCommand.Connection.Open();
                            sqlDataAdapter.Fill(dataTable);
                        }
                        catch (SqlException ex)
                        {
                            string message = ex.Message;
                        }
                    }
                }
            }
            return dataTable;
        }
        public DataTable getCartasPorte(string factura)
        {
            DataTable dataTable = new DataTable();
            string cadena = @"Data source=172.24.16.112; Initial Catalog=TMWSuite; User ID=sa; Password=tdr9312;Trusted_Connection=false;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand selectCommand = new SqlCommand("SELECT * FROM VISTA_Carta_Porte where Folio = @factura", connection))
                {
                    selectCommand.CommandType = CommandType.Text;
                    selectCommand.Parameters.AddWithValue("@factura", (object)factura);
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                    {
                        try
                        {
                            selectCommand.Connection.Open();
                            sqlDataAdapter.Fill(dataTable);
                        }
                        catch (SqlException ex)
                        {
                            string message = ex.Message;
                        }
                    }
                }
            }
            return dataTable;
        }
        public DataTable getDatosSegmentos(string orden)
        {


            DataTable dataTable3 = new DataTable();
            //NOS CONECTAMOS CON LA BASE DE DATOS
            string cadena = @"Data source=172.24.16.112; Initial Catalog=TMWSuite; User ID=sa; Password=tdr9312;Trusted_Connection=false;MultipleActiveResultSets=true";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_obtener_segmento_JC", cn))
                    {
                        //Le indico que es del itpo procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        //Esta linea define un parametro
                        cmd.Parameters.AddWithValue("@orden", orden);

                        //Ejecutamos el procedimiento
                        cmd.ExecuteNonQuery();
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd))
                        {
                            try
                            {

                                sqlDataAdapter.Fill(dataTable3);
                                cn.Close();
                            }
                            catch (SqlException ex)
                            {
                                cn.Close();
                                string message = ex.Message;

                            }

                        }

                    }
                }
                catch (SqlException ex)
                {

                    cn.Close();
                    string message = ex.Message;

                }
            }

            return dataTable3;

        }

    }
}
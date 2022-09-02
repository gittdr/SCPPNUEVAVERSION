
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Shapes;

using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;
using System.Net;

namespace CARGAR_EXCEL
{
    public partial class DownloadTxt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                
            }
            try
            {

                okTralix();
               
            }
            catch (Exception)
            {

                throw;
            }
        }
        private async Task okTralix()
        {
            //DirectoryInfo files = new DirectoryInfo(@"\\10.223.208.41\inetpub\wwwroot\ComplementoPago\TxtGenerados\Generados\");
            DirectoryInfo files = new DirectoryInfo(@"C:\CartaPorteCargaMasiva\ComplementoPagoNuevaVersion\CARGAR_EXCEL\TxtGenerados\");
            FileInfo[] di = files.GetFiles("*.txt").OrderByDescending(p => p.CreationTime).ToArray();

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Folio", typeof(string)),new DataColumn("Archivo", typeof(string)), new DataColumn("Descargar", typeof(string)) });
            //dt.Rows.Add("Movie 1", "http://www.aspforums.net");
            //dt.Rows.Add("Movie 2", "http://www.aspsnippets.com");
            //dt.Rows.Add("Movie 3", "http://www.jqueryfaqs.com");

            //Execute a loop over the rows.
            foreach (FileInfo row in di)
            {
                //var ultimo_archivo = (from f in di
                //                      orderby f.LastWriteTime descending
                //                      select f).First();
                string nombreA = row.Name;
               

                string folio =  nombreA.Replace(".txt", "");
                //string rutanueva = @"http://69.20.92.117:8083/TxtGenerados/Generados/";
                string rutanueva = @"http://localhost:56747/TxtGenerados/";
                string rutaA = files.ToString();
                string completo = rutanueva + nombreA;
                string completo2 = rutaA + nombreA;

                //FileInfo sourceFile = new FileInfo(@completo2);
                //FileStream sourceStream = sourceFile.OpenRead();
                //// 2
                //FileStream stream = new FileStream(@"C:\CartaPorteCargaMasiva\TralixComplementoPago\CARGAR_EXCEL\TxtGenerados\ZipGenerados\"+folio+".zip", FileMode.Open);
                //// 3 
                //ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Create);
                //// 4 
                //ZipArchiveEntry entry = archive.CreateEntry(sourceFile.Name);
                //// 5
                //Stream zipStream = entry.Open();
                //// 6
                //sourceStream.CopyTo(zipStream);
                //// 7
                //zipStream.Close();
                //sourceStream.Close();
                //archive.Dispose();
                //stream.Close();
                //Descargar(completo);
                //string completo2 = rutaA + nombreA;
                //compressDirectory(
                //     completo2,
                //     @"C:\CartaPorteCargaMasiva\TralixComplementoPago\CARGAR_EXCEL\TxtGenerados\ZipGenerados\MyOutputFile.zip",
                //     9
                // );
                dt.Rows.Add(folio,nombreA, completo);
                    
            }
             GridView1.DataSource = dt;
             GridView1.DataBind();
            
        }
        //protected void descargar(object sender, EventArgs e)
        //{

        //    //LinkButton lnk = sender as LinkButton;
        //    String Value1 = ((LinkButton)sender).CommandArgument.ToString();
        //    if (Value1 != null)
        //    {
        //        WebClient mywebClient = new WebClient();
        //        mywebClient.DownloadFile("http://localhost:56747/TxtGenerados/" + Value1, @"C:\CartaPorteCargaMasiva\TralixComplementoPago\CARGAR_EXCEL\TxtGenerados\" + Value1);
        //        string msg = "¡Success, Se descargo correctamente";
        //        ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Se descargaron correctamente ', 'success');setTimeout(function(){window.location.href ='Listado.aspx'}, 10000)", true);
        //    }
        //    else
        //    {
        //        string msg = "¡Error, ponte en contacto con TI";
        //        ScriptManager.RegisterStartupScript(this, GetType(), "swal", "swal('" + msg + "', 'Error con los folios relacionados ', 'error');setTimeout(function(){window.location.href ='Listado.aspx'}, 10000)", true);
        //    }
            
        //}

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {

        }

        //public void Descargar(string completo)
        //{
        //    WebClient webClient = new WebClient();
        //    webClient.DownloadFile(completo, @"C:\CartaPorteCargaMasiva\TralixComplementoPago\CARGAR_EXCEL\TxtGenerados\");
        //}

        //private void compressDirectory(string DirectoryPath, string OutputFilePath, int CompressionLevel = 9)
        //{
        //    try
        //    {
        //        // Dependiendo del directorio, esto podría ser muy grande y requeriría más atención.
        //        // en un paquete comercial.
        //        string[] filenames = Directory.GetFiles(DirectoryPath);

        //        // Las declaraciones de 'using' garantizan que la transmisión se cierre correctamente, lo cual es una gran fuente
        //        // de problemas de otra manera. Su excepción también es segura, lo cual es genial.
        //        using (ZipOutputStream OutputStream = new ZipOutputStream(File.Create(OutputFilePath)))
        //        {

        //            // Definir el nivel de compresión
        //            //0 - almacenar solamente, en 9 - significa mejor compresión
        //            OutputStream.SetLevel(CompressionLevel);

        //            byte[] buffer = new byte[4096];

        //            foreach (string file in filenames)
        //            {

        //                // El uso de GetFileName hace que el resultado sea compatible con XP
        //                // ya que la ruta resultante no es absoluta.
        //                ZipEntry entry = new ZipEntry(Path.GetFileName(file));

        //                // Configure los datos de entrada según sea necesario.

        //                // Crc y el tamaño son manejados por la biblioteca para flujos sellables
        //                // Así que no hay necesidad de hacerlos aquí.

        //                // También podría utilizar la hora de la última escritura o similar para el archivo.
        //                entry.DateTime = DateTime.Now;
        //                OutputStream.PutNextEntry(entry);

        //                using (FileStream fs = File.OpenRead(file))
        //                {

        //                    // El uso de un búfer de tamaño fijo aquí no hace una diferencia notable para la salida
        //                    // pero mantiene a raya el uso de la memoria.
        //                    int sourceBytes;

        //                    do
        //                    {
        //                        sourceBytes = fs.Read(buffer, 0, buffer.Length);
        //                        OutputStream.Write(buffer, 0, sourceBytes);
        //                    } while (sourceBytes > 0);
        //                }
        //            }

        //            // Finalizar / Cerrar no son necesarios estrictamente ya que la declaración de uso lo hace automáticamente

        //            // Finalizar es importante para garantizar que se agregue la información final de un archivo Zip. Sin esto
        //            // el archivo creado no sería válido.
        //            OutputStream.Finish();

        //            // Cerrar es importante para terminar y desbloquear el archivo.
        //            OutputStream.Close();

        //            Console.WriteLine("Files successfully compressed");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // No es necesario volver a lanzar la excepción, ya que para nuestros propósitos se maneja.
        //        Console.WriteLine("Exception during processing {0}", ex);
        //    }
        //}






    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BEUCrtPortBelly.Queris
{
    public class ArchivoBLL
    {
        public string confirmacion { get; set; }
        public Exception error { get; set; }

        public void SubirArchivo(string ruta, HttpPostedFileBase file)
        {
            try
            {
                file.SaveAs(ruta);
                this.confirmacion = "Imagen Guardada";
            }
            catch (Exception ex)
            {
                this.error = ex;
                throw;
            }
        }

        public void EliminarArchivo(string ruta)
        {
            try
            {
                File.Delete(ruta);
                this.confirmacion = "Imagen Eliminada";
            }
            catch (Exception ex)
            {
                this.error = ex;
                throw;
            }
        }

        public bool ComprobarRuta(string ruta)
        {
            if (File.Exists(ruta))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SubirArchivo(string ruta, HttpPostedFile file)
        {
            try
            {
                file.SaveAs(ruta);
                this.confirmacion = "Imagen Guardada";
            }
            catch (Exception ex)
            {
                this.error = ex;
            }
        }

        public string SubirImagen(HttpPostedFile postedFile)
        {
            string imageName = "";

            if (postedFile != null && postedFile.ContentLength > 0)
                try
                {

                    //Create custom fileName
                    imageName = new string(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                    imageName = imageName + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(postedFile.FileName);
                    var filePath = HttpContext.Current.Server.MapPath(@"~/portbelly2/Imagenes/" + imageName);
                    if (!this.ComprobarRuta(filePath))
                    {
                        this.SubirArchivo(filePath, postedFile);
                    }
                    else
                    {
                        imageName = "";
                    }
                    //postedFile.SaveAs(filePath);
                }
                catch (Exception)
                {
                    imageName = "";
                }
            return imageName;
        }

        public  void EliminarImagen(string imageName)
        {
            try
            {
                string filePath = HttpContext.Current.Server.MapPath(@"~/portbelly2/Imagenes/" + imageName);
               
                if (this.ComprobarRuta(filePath))
                {
                    this.EliminarArchivo(filePath);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}

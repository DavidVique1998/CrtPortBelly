using BEUCrtPortBelly.Queris;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApiPortBelly.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    
    public class ImagenController : ApiController
    {
        [Authorize(Roles = "Administrador")]
        public IHttpActionResult Post()
        {
            try
            {
                string imageName = null;
                var httpRequest = HttpContext.Current.Request;
                //Upload Image
                HttpPostedFile postedFile = httpRequest.Files["Image"];
                imageName = SubirImagen(postedFile);
                if (imageName != "")
                {
                    return Content(HttpStatusCode.OK, imageName);
                }
                else
                {
                    return Content(HttpStatusCode.Conflict, "Error la imagen entro en conflicto Crear");
                }
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.UnsupportedMediaType, "Error Imagen no soportada");
            }
        }
        [Authorize(Roles = "Administrador,Cliente")]
        public IHttpActionResult GetImage(string name)
        {
            try
            {
                try
                {
                    string filePath = HttpContext.Current.Server.MapPath(@"~/Content/Imagenes/" + name);
                    //Compruebo si la imagen existe
                    if (File.Exists(filePath))
                    {
                        //Optengo la imagen de la carpeta
                        using (Image data = Image.FromFile(filePath))
                        {
                            //transformo en bytes para mandar como request
                            byte[] result = (byte[])new ImageConverter().ConvertTo(data, typeof(byte[]));
                            return Content(HttpStatusCode.OK, result);
                        }
                    }
                    else
                    {
                        //Optengo la imagen de la carpeta
                        using (Image data = Image.FromFile(HttpContext.Current.Server.MapPath(@"~/Content/Imagenes/default.png")))
                        {
                            //transformo en bytes para mandar como request
                            byte[] result = (byte[])new ImageConverter().ConvertTo(data, typeof(byte[]));
                            return Content(HttpStatusCode.OK, result);
                        }
                    }
                }
                catch (UnsupportedMediaTypeException)
                {
                    return Content(HttpStatusCode.UnsupportedMediaType, "Imagen no compatible");

                }
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Error desconocido");

            }
        }
        [Authorize(Roles = "Administrador")]
        public IHttpActionResult Delete(string name)
        {
            try
            {
                EliminarImagen(name);
                return Content(HttpStatusCode.OK, "La imagen se eliminó correctamente " + name);

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private string SubirImagen(HttpPostedFile postedFile )
        {
            string imageName = "";
            
            if (postedFile != null && postedFile.ContentLength > 0)
                try
                {
                    ArchivoBLL archivoBLL = new ArchivoBLL();

                    //Create custom fileName
                    imageName = new string(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                    imageName = imageName + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(postedFile.FileName);
                    var filePath = HttpContext.Current.Server.MapPath(@"~/Content/Imagenes/" + imageName);
                    Console.WriteLine(filePath);
                    if (!archivoBLL.ComprobarRuta(filePath))
                    {
                        archivoBLL.SubirArchivo(filePath, postedFile);
                    }
                    //postedFile.SaveAs(filePath);
                }
                catch (Exception)
                {
                    imageName = "";
                }
            return imageName;
        }
        private void EliminarImagen(string imageName)
        {
            try
            {
                string filePath = HttpContext.Current.Server.MapPath(@"~/Content/Imagenes/" + imageName);
                ArchivoBLL archivoBLL = new ArchivoBLL();
                if (archivoBLL.ComprobarRuta(filePath))
                {
                    archivoBLL.EliminarArchivo(filePath);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}

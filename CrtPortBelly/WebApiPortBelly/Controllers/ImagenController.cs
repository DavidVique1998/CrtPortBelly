using BEUCrtPortBelly.Queris;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApiPortBelly.Controllers
{
    
    //[EnableCorsAttribute("*", "*", "*",SupportsCredentials =true)]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("api/Imagen")]
    public class ImagenController : ApiController
    {
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IHttpActionResult Post()
        {
            try
            {
                string imageName = null;
                var httpRequest = System.Web.HttpContext.Current.Request;
                //Upload Image
                HttpPostedFile postedFile = httpRequest.Files["image"];
                ArchivoBLL archivoBLL = new ArchivoBLL();
                imageName = archivoBLL.SubirImagen(postedFile);
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
                    string filePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/Imagenes/" + name);
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
                        using (Image data = Image.FromFile(System.Web.HttpContext.Current.Server.MapPath(@"~/Content/Imagenes/default.png")))
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
                ArchivoBLL archivoBLL = new ArchivoBLL();
                archivoBLL.EliminarImagen(name);
                return Content(HttpStatusCode.OK, "La imagen se eliminó correctamente " + name);

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}

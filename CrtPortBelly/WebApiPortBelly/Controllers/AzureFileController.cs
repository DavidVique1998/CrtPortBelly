using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web;
using BEUCrtPortBelly.Queris;
using WebApiPortBelly.Models;

namespace WebApiPortBelly.Controllers
{
    [EnableCorsAttribute("*", "*", "*", SupportsCredentials = true)]
    //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("api/AzureFile")]
    public class AzureFileController : ApiController
    {
        // ModeloFileAzure modeloFileAzure;
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
                ArchivoAzureBLL archivoBLL = new ArchivoAzureBLL();
                imageName = archivoBLL.SubirImagen(postedFile);
                if (imageName != "")
                {
                    // Guradamos el contenido del archvio en formato stream
                    
                    using (Stream stream = postedFile.InputStream)
                    {
                        // Lllamamos a nuestro metodo para subir ficheros a Azure de nuestro modelo 
                        // Le mandamos el nombre del archivo y su contenido en dormato stream
                        using (ModeloFileAzure modeloFileAzure= new ModeloFileAzure())
                        {
                            modeloFileAzure.SubirFicheroAzure(imageName, stream);
                        }
                            
                    }
                    //Stream stream = postedFile.InputStream;

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
                    using (ModeloFileAzure modeloFileAzure= new ModeloFileAzure())
                    {
                        if (modeloFileAzure.ComprobarArchivo(name))
                        {
                            byte[] result = (byte[])modeloFileAzure.GetFile(name);
                            return Content(HttpStatusCode.OK, result);
                        }
                        else
                        {
                            byte[] result = (byte[])modeloFileAzure.GetFile("default.png");
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
                using (ModeloFileAzure modeloFileAzure = new ModeloFileAzure())
                {
                    modeloFileAzure.DeleteFile(name);
                }
                return Content(HttpStatusCode.OK, "La imagen se eliminó correctamente " + name);

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

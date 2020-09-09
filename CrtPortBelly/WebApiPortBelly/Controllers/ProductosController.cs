using BEUCrtPortBelly;
using BEUCrtPortBelly.Queris;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.UI.WebControls;
using Image = System.Drawing.Image;

namespace WebApiPortBelly.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    //[EnableCorsAttribute("*", "*", "*", SupportsCredentials = true)]
    [RoutePrefix("api/Productos")]
    public class ProductosController : ApiController
    {
        [Authorize(Roles = "Administrador")]
        public IHttpActionResult Post(Producto producto)
        {
            try
            {
                ProductoBLL.Create(producto);
                return Content(HttpStatusCode.Created, "Producto creado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Administrador,Cliente")]
        //[ResponseType(typeof(List<Producto>))]
        public IHttpActionResult Get()
        {
            try
            {
                List<Producto> todos = ProductoBLL.GetList();
                return Content(HttpStatusCode.OK, todos);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [Authorize(Roles = "Administrador")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                ProductoBLL.Delete(id);
                return Ok("Producto eliminado correctamente");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }
        [Authorize(Roles = "Administrador,Cliente")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Producto producto = ProductoBLL.Get(id);
                return Content(HttpStatusCode.OK, producto);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Administrador")]
        public IHttpActionResult Creates(Producto producto)
        {
            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent())
                return Content(HttpStatusCode.UnsupportedMediaType, "Error al crear Método Post debe ser MultipartContent");
            else
            {
                try
                {
                    var httpRequest = HttpContext.Current.Request;
                    HttpPostedFile fileBase = httpRequest.Files[0];
                    string path = SubirImagen(fileBase);
                    if (path != "")
                    {
                        try
                        {
                            producto.prd_img = path;
                            ProductoBLL.Create(producto);
                            return Content(HttpStatusCode.Created, "Producto creado correctamente");
                        }
                        catch (Exception ex)
                        {
                            return BadRequest(ex.Message);
                        }
                    }
                    else
                    {
                        return Content(HttpStatusCode.UnsupportedMediaType, "Error al obtener imagen");
                    }

                }
                catch (Exception ex)
                {
                    return Content(HttpStatusCode.InternalServerError, $"Internal server error: {ex}");
                }
            }
        }

       

        private string SubirImagen(HttpPostedFile file)
        {
            string nombre = "";
            if (file != null && file.ContentLength > 0)
                try
                {
                    ArchivoBLL modelo = new ArchivoBLL();
                    nombre = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
                    string path = HttpContext.Current.Server.MapPath("~/Content/Imagenes/") + nombre;
                    modelo.SubirArchivo(path, file);
                    return path;
                }
                catch (UnsupportedMediaTypeException)
                {
                    return "";
                }
            return nombre;
        } 

    }
}

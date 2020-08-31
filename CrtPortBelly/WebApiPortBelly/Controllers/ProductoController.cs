using BEUCrtPortBelly;
using BEUCrtPortBelly.Queris;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApiPortBelly.Controllers
{

    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]

    public class ProductoController : ApiController
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

        [Authorize(Roles = "Administrador,Cliente")]
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
                return Content(HttpStatusCode.OK ,"Producto eliminado correctamente");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        [Authorize(Roles = "Administrador")]
        public IHttpActionResult Put(Producto producto)
        {
            try
            {
                bool llave= ProductoBLL.Updates(producto);
                if (llave)
                {
                    return Content(HttpStatusCode.Accepted, "Producto actualizado correctamente");
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest,"No se puede actualizar el producto");
                }
                

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message+producto.ToString());
            }
        }
        //private string EliminarImagen(string nombre)
        //{

        //}
    }
}

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
    public class ProductoEnCarritosController : ApiController
    {
        
        public IHttpActionResult Post(ProductoEnCarrito prdCarrito)
        {
            try
            {
                ProductoEnCarritoBLL.Create(prdCarrito);
                return Content(HttpStatusCode.Created, "Producto añadido correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                ProductoEnCarrito prdCarrito = ProductoEnCarritoBLL.Get(id);
                return Content(HttpStatusCode.OK, prdCarrito);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        public IHttpActionResult Get()
        {
            try
            {
                List<ProductoEnCarrito> todos = ProductoEnCarritoBLL.List();
                return Content(HttpStatusCode.OK, todos);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        public IHttpActionResult Delete(int id)
        {
            try
            {
                ProductoEnCarritoBLL.Delete(id);
                return Ok("Producto en Carrito eliminado correctamente");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }
        public IHttpActionResult Put(ProductoEnCarrito prdCarrito)
        {
            try
            {
                ProductoEnCarritoBLL.Updates(prdCarrito);

                return Content(HttpStatusCode.Accepted, "Producto en Carrito actualizado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + prdCarrito.ToString());
            }
        }
    }
}

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
    [EnableCorsAttribute("*", "*", "*")]
    //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("api/ProductoEnCarritos")]
    public class ProductoEnCarritosController : ApiController
    {
        [Authorize(Roles = "Cliente")]
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
 
        [Authorize(Roles = "Administrador, Cliente")]
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

        [Authorize(Roles = "Administrador, Cliente")]
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
        [Authorize(Roles = "Cliente")]
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

        [Authorize(Roles = "Cliente")]
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
        [HttpGet]
        [Route("MisProductosEnCarritoPen")]
        [Authorize(Roles = "Cliente")]
        public IHttpActionResult MisProductosEnCarritoPen(int id)
        {
            try
            {
                List<ProductoEnCarrito> productoEnCarritos = ProductoEnCarritoBLL.GetProdutsPendInCarByCli(id);
                //202
                return Content(HttpStatusCode.OK, productoEnCarritos);
            }
            catch (Exception ex)
            {
                //400
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("MisProductosEnCarritosPag")]
        [Authorize(Roles = "Cliente")]
        public IHttpActionResult MisProductosEnCarritosPag(int id)
        {
            try
            {
                List<ProductoEnCarrito> productoEnCarritos = ProductoEnCarritoBLL.GetProdutsPagInCarByCli(id);
                //202
                return Content(HttpStatusCode.OK, productoEnCarritos);
            }
            catch (Exception ex)
            {
                //400
                return BadRequest(ex.Message);
            }
        }
    }
}

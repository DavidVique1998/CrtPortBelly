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
    [RoutePrefix("api/Carritos")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class CarritosController : ApiController
    {
        [Authorize(Roles = "Cliente")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Carrito carrito = CarritoBLL.Get(id);
                return Content(HttpStatusCode.OK, carrito);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Cliente")]
        public IHttpActionResult Put(Carrito carrito)
        {
            try
            {
                CarritoBLL.Update(carrito);
                return Content(HttpStatusCode.Accepted, "Carrito actualizado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + carrito.ToString());
            }
        }

        [HttpGet]
        [Route("MiCarritoPendiente")]
        [Authorize(Roles = "Cliente")]
        public IHttpActionResult MiCarritoPendiente(int id)
        {
            try
            {
                Carrito carrito= CarritoBLL.ObtenerCarritoPendiente(id);
                return Content(HttpStatusCode.OK, carrito);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + id.ToString());
            }
        }
        [HttpGet]
        [Route("MisCarritosPagados")]
        [Authorize(Roles = "Cliente")]
        public IHttpActionResult MisCarritosPagados(int id)
        {
            try
            {
                List<Carrito> carritos= CarritoBLL.ObtenerCarritosPagados(id);
                return Content(HttpStatusCode.OK, carritos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + id.ToString());
            }
        }
    }
}

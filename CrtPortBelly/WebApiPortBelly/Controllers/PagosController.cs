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
    //[EnableCorsAttribute("*", "*", "*")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("api/Pagos")]
    public class PagosController : ApiController
    {
        [Authorize(Roles = "Cliente")]
        public IHttpActionResult Post(Pago pago)
        {
            try
            {
                PagoBLL.Create(pago);
                return Content(HttpStatusCode.Created, "Metodo de pago creado correctamente");
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
                Pago pago = PagoBLL.Get(id);
                return Content(HttpStatusCode.OK, pago);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "Administrador")]
        public IHttpActionResult Get()
        {
            try
            {
                List<Pago> todos = PagoBLL.List();
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
                PagoBLL.Delete(id);
                return Ok("Metodo de pago eliminado correctamente");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }
        [Authorize(Roles = "Cliente")]
        public IHttpActionResult Put(Pago pago)
        {
            try
            {
                PagoBLL.Update(pago);

                return Content(HttpStatusCode.Accepted, "Pago actualizado correctamente");



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + pago.ToString());
            }
        }
        [HttpGet]
        [Route("MisMetodosPago")]
        [Authorize(Roles = "Cliente")]
        public IHttpActionResult MisMetodosPago(int id)
        {
            try
            {
                List<Pago> pagos = PagoBLL.List(id);
                //202
                return Content(HttpStatusCode.OK, pagos);
            }
            catch (Exception ex)
            {
                //400
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("MiUnicoMetodoPago")]
        [Authorize(Roles = "Cliente")]
        public IHttpActionResult MiUnicoMetodoPago(int id)
        {
            try
            {
                Pago pagos = PagoBLL.GetUnicoPago(id);
                //202
                return Content(HttpStatusCode.OK, pagos);
            }
            catch (Exception ex)
            {
                //400
                return BadRequest(ex.Message);
            }
        }
    }
}

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
    public class PagosController : ApiController
    {
        public IHttpActionResult Post(Pago pago)
        {
            try
            {
                PagoBLL.Create(pago);
                return Content(HttpStatusCode.Created, "Pagos creado correctamente");
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
                Pago pago = PagoBLL.Get(id);
                return Content(HttpStatusCode.OK, pago);
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
                List<Pago> todos = PagoBLL.List();
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
                PagoBLL.Delete(id);
                return Ok("Pagos eliminado correctamente");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        public IHttpActionResult Put(Pago pago)
        {
            try
            {
                PagoBLL.Update(pago);

                return Content(HttpStatusCode.OK, "Alumno actualizado correctamente");



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + pago.ToString());
            }
        }
    }
}

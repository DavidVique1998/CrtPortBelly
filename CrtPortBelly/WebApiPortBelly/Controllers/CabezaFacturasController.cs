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
    public class CabezaFacturasController : ApiController
    {
        public IHttpActionResult Post(CabezaFactura cabezaFactura)
        {
            try
            {
                CabezaFacturaBLL.Create(cabezaFactura);
                return Content(HttpStatusCode.Created, "CabezaFacturas creado correctamente");
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
                CabezaFactura cabezaFactura = CabezaFacturaBLL.Get(id);
                return Content(HttpStatusCode.OK, cabezaFactura);
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
                List<CabezaFactura> todos = CabezaFacturaBLL.List();
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
                CabezaFacturaBLL.Delete(id);
                return Ok("CabezaFacturas eliminado correctamente");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        public IHttpActionResult Put(CabezaFactura cabezaFactura)
        {
            try
            {
                CabezaFacturaBLL.Update(cabezaFactura);

                return Content(HttpStatusCode.OK, "Alumno actualizado correctamente");



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + cabezaFactura.ToString());
            }
        }
    }
}

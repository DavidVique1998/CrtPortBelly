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
    [RoutePrefix("api/CabezaFacturas")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]

    public class CabezaFacturasController : ApiController
    {
        [Authorize(Roles = "Cliente")]
        public IHttpActionResult Post(CabezaFactura cabezaFactura)
        {
            try
            {
                CabezaFacturaBLL.Create(cabezaFactura);
                return Content(HttpStatusCode.Created, "Factura creado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Cliente")]
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
        [Authorize(Roles = "Cliente")]
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
        [Authorize(Roles = "Cliente")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                CabezaFacturaBLL.Delete(id);
                return Ok("Facturaa eliminada correctamente");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }
        [Authorize(Roles = "Cliente")]
        public IHttpActionResult Put(CabezaFactura cabezaFactura)
        {
            try
            {
                CabezaFacturaBLL.Update(cabezaFactura);

                return Content(HttpStatusCode.OK, "Factura actualizada correctamente");



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + cabezaFactura.ToString());
            }
        }
        [HttpGet]
        [Route("GenerateFacturaByCli")]
        public IHttpActionResult GenerateFacturaByCli(int id) 
        {
            try
            {
                CabezaFactura cabezaFactura = CabezaFacturaBLL.GetCabFactByCli(id);
                if (cabezaFactura == null)
                {
                    return Content(HttpStatusCode.NotFound, "Factura no encontrada");
                }
                else
                {
                    return Content(HttpStatusCode.OK, cabezaFactura);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}

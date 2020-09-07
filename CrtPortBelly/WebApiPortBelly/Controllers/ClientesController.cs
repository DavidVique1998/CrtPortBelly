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
    
    [RoutePrefix("api/Clientes")]
    [EnableCorsAttribute("*", "*", "*")]
    //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [Authorize(Roles = "Cliente")]
    public class ClientesController : ApiController
    {
      
        public IHttpActionResult Get(int id)
        {
            try
            {
                Cliente cliente = ClienteBLL.Get(id);
                //200
                return Content(HttpStatusCode.OK, cliente);
            }
            catch (Exception)
            {
                //404
                return NotFound();
            }
        }

        public IHttpActionResult Put(Cliente cliente)
        {
            try
            {
                ClienteBLL.Update(cliente);

                return Content(HttpStatusCode.OK, "Cliente actualizado correctamente");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + cliente.ToString());
            }
        }

        [HttpGet]
        [Route("GetCliente")]
        public IHttpActionResult GetCliente(int id)
        {
            try
            {
                Cliente cliente= ClienteBLL.ClienteByUsu(id);
                return Content(HttpStatusCode.OK, cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

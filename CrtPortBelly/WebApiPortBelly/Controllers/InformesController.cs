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
    [RoutePrefix("api/Informes")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]

    public class InformesController : ApiController
    {
        [HttpGet]
        [Route("GetCategorias")]
        public IHttpActionResult GetCategorias(string estado)
        {
            try
            {

                List<PestadosProductosPorCategoria_Result> todos = InformeBLL.GetProductosCarritoPorCategorias(estado);
                return Content(HttpStatusCode.OK, todos);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetPromociones")]
        public IHttpActionResult GetPromociones(string estado)
        {
            try
            {

                List<PestadosProductosPorPromocion_Result> todos = InformeBLL.GetProductosCarritoPorPromociones(estado);
                return Content(HttpStatusCode.OK, todos);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}

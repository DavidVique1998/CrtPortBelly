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
    //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [EnableCorsAttribute("*", "*", "*", SupportsCredentials = true)]
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

        [HttpGet]
        [Route("GetProductosPorCategoria")]
        public IHttpActionResult GetProductosPorCategoria()
        {
            try
            {

                List<PproductosExistentesPorCategoria_Result> todos = InformeBLL.GetProductosExistentesPorCategoria();
                return Content(HttpStatusCode.OK, todos);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetProductosVendidosPorMesesPorCategoriaEnAnio")]
        public IHttpActionResult GetProductosVendidosPorMesesPorCategoriaEnAnio(int year)
        {
            try
            {

                List<PventasPorMesesSegunCategoriaEnAnio_Result> todos = InformeBLL.GetProductoVendidosPorMesesPorCategoriaEnAnio(year);
                return Content(HttpStatusCode.OK, todos);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetPrdVenPorMesPorCatEnAnio")]

        public IHttpActionResult GetPrdVenPorMesPorCatEnAnio(int year)
        {
            try
            {

                List<PventPorMesSegCatEnAnio_Result> todos = InformeBLL.GetPrdVenPorMesPorCatEnAnio(year);
                return Content(HttpStatusCode.OK, todos);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}

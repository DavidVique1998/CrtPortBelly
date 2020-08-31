using BEUCrtPortBelly;
using BEUCrtPortBelly.Queris;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiPortBelly.Models;

namespace WebApiPortBelly.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Login")]

    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("Authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            Usuario usuario= new Usuario();
            usuario.uso_cor= login.correo;
            usuario.uso_con = login.password;
            Usuario user = UsuarioBLL.LoginByMail(usuario.uso_cor, usuario.uso_con);
            if (user != null)
            {
                return Ok(new { token = TokenGeneratos.GenerateTokenJwt(user) });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}

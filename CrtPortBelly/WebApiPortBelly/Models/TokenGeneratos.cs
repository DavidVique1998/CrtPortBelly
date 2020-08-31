using BEUCrtPortBelly;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace WebApiPortBelly.Models
{
    internal static class TokenGeneratos
    {
        public static string GenerateTokenJwt(Usuario usuario)
        {
            //Recuperar las variabvles de configuración 
            var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
            var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
            var expireTime= ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];

            //Creamos el header
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var _Header = new JwtHeader(signinCredentials);
            //Cleamos los Claims
            var _Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId,usuario.uso_id.ToString()),
                new Claim("id", usuario.uso_id.ToString()),
                new Claim("rol", usuario.uso_rol),
                new Claim("Nombres", usuario.uso_nom),
                new Claim(JwtRegisteredClaimNames.UniqueName,usuario.uso_usu),
                new Claim(JwtRegisteredClaimNames.Email, usuario.uso_cor),
                new Claim(ClaimTypes.Role, usuario.uso_rol)
            };
            // Creamos el Payload
            var _Payload = new JwtPayload
            (
               issuer: issuerToken,
               audience: audienceToken,
               claims: _Claims,
               notBefore: DateTime.UtcNow,
               // Expira en 10 min
               expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime))
            );
            // Creamos Token
            var _Token = new JwtSecurityToken(
                _Header,
                _Payload);
            return new JwtSecurityTokenHandler().WriteToken(_Token);

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace WebApiPortBelly.Models
{
    public class LoginRequest
    {
        public string correo { get; set; }
        public string password { get; set; }

    }
}
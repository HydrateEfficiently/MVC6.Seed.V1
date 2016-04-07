using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Services.Identity.Models
{
    public class LoginRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}

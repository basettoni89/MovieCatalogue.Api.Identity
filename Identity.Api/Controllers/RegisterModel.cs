using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalogue.Api.Identity.Controllers
{
    public class RegisterModel
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}

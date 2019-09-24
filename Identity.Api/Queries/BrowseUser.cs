using MovieCatalogie.Api.Identity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalogie.Api.Identity.Queries
{
    public class BrowseUser : PagedQuery
    {
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;

    }
}

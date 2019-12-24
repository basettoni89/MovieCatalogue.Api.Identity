using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalogue.Api.Identity.Types
{
    public class PagedQuery
    {
        public int Page { get; set; }
        public int Results { get; set; }
    }
}

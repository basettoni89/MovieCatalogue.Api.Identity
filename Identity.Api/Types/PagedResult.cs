using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalogie.Api.Identity.Types
{
    public class PagedResult<TElem>
    {
        public IEnumerable<TElem> Items { get; set; }

        public int CurrentPage { get; }
        public int ResultsPerPage { get; }
        public int TotalPages { get; }
        public long TotalResults { get; }


        protected PagedResult()
        {
            Items = Enumerable.Empty<TElem>();
        }

        [JsonConstructor]
        protected PagedResult(IEnumerable<TElem> items,
            int currentPage, int resultsPerPage,
            int totalPages, long totalResults)
        {
            Items = items;
        }

        public static PagedResult<TElem> Create(IEnumerable<TElem> items,
            int currentPage, int resultsPerPage,
            int totalPages, long totalResults)
            => new PagedResult<TElem>(items, currentPage, resultsPerPage, totalPages, totalResults);

        public static PagedResult<TElem> Empty => new PagedResult<TElem>();
    }
}

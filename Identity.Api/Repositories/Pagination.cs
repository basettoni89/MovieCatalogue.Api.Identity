using System;
using System.Linq;
using System.Threading.Tasks;
using MovieCatalogue.Api.Identity.Types;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MovieCatalogue.Api.Identity.Repositories
{
    public static class Pagination
    {
        public static PagedResult<T> PaginateAsync<T>(this IQueryable<T> collection, PagedQuery query)
            => collection.PaginateAsync(query.Page, query.Results);

        public static PagedResult<T> PaginateAsync<T>(this IQueryable<T> collection,
            int page = 1, int resultsPerPage = 10)
        {
            if (page <= 0)
            {
                page = 1;
            }
            if (resultsPerPage <= 0)
            {
                resultsPerPage = 10;
            }
            var isEmpty = collection.Any() == false;
            if (isEmpty)
            {
                return PagedResult<T>.Empty;
            }
            var totalResults = collection.Count();
            var totalPages = (int)Math.Ceiling((decimal)totalResults / resultsPerPage);
            var data = collection.Limit(page, resultsPerPage).ToList();

            return PagedResult<T>.Create(data, page, resultsPerPage, totalPages, totalResults);
        }

        public static IQueryable<T> Limit<T>(this IQueryable<T> collection, PagedQuery query)
            => collection.Limit(query.Page, query.Results);

        public static IQueryable<T> Limit<T>(this IQueryable<T> collection,
            int page = 1, int resultsPerPage = 10)
        {
            if (page <= 0)
            {
                page = 1;
            }
            if (resultsPerPage <= 0)
            {
                resultsPerPage = 10;
            }
            var skip = (page - 1) * resultsPerPage;
            var data = collection.Skip(skip)
                .Take(resultsPerPage);

            return data;
        }
    }
}
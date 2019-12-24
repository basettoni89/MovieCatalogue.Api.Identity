using System.Linq;
using System.Threading.Tasks;
using MovieCatalogue.Api.Identity.Queries;
using MovieCatalogue.Api.Identity.Types;
using MongoDB.Driver;
using MovieCatalogue.Api.Identity.Models;

namespace MovieCatalogue.Api.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IQueryable<UserModel> context;

        public UserRepository(IQueryable<UserModel> context)
        {
            this.context = context;
        }

        public PagedResult<UserModel> BrowseUsers(BrowseUser query)
        {
            return this.context.Where(x =>
                x.Username.Contains(query.Username) 
                && x.Name.Contains(query.Name) 
                && x.Surname.Contains(query.Surname))
                .PaginateAsync(query);
        }

        public UserModel GetUserByID(int userId)
        {
            return this.context.Where(x => x.ID == userId).FirstOrDefault();
        }
    }
}

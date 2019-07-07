using DataService.Infrastructure;
using DataService.Models;

namespace DataService.Repositories
{
    public interface IUserRepository : IRepository<User>
    {

    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BUSTAPContext context) : base(context)
        {
        }
    }
}
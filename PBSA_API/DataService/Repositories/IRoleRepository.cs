using DataService.Infrastructure;
using DataService.Models;

namespace DataService.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {

    }

    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(BUSTAPContext context) : base(context)
        {
        }
    }
}
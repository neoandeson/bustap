using DataService.Infrastructure;
using DataService.Models;

namespace DataService.Repositories
{
    public interface IIDPaperRepository : IRepository<Idpaper>
    {

    }

    public class IDPaperRepository : Repository<Idpaper>, IIDPaperRepository
    {
        public IDPaperRepository(BUSTAPContext context) : base(context)
        {
        }
    }
}
using DataService.Infrastructure;
using DataService.Models;

namespace DataService.Repositories
{
    public interface IApplyPriceTypeRepository : IRepository<ApplyPriceType>
    {

    }

    public class ApplyPriceTypeRepository : Repository<ApplyPriceType>, IApplyPriceTypeRepository
    {
        public ApplyPriceTypeRepository(BUSTAPContext context) : base(context)
        {
        }
    }
}
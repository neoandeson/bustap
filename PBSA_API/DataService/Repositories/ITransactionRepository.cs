using DataService.Infrastructure;
using DataService.Models;

namespace DataService.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
    }

    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(BUSTAPContext context) : base(context)
        {
        }
    }
}
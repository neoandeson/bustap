using DataService.Infrastructure;
using DataService.Models;

namespace DataService.Repositories
{
    public interface IWalletRepository : IRepository<Wallet>
    {
    }

    public class WalletRepository : Repository<Wallet>, IWalletRepository
    {
        public WalletRepository(BUSTAPContext context) : base(context)
        {
        }
    }
}
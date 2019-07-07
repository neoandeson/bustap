using DataService.Repositories;
using System.Linq;
using DataService.Infrastructure;
using DataService.ViewModels;
using DataService.Models;
using DataService.Constants;
using System.Collections.Generic;

namespace DataService.Services
{
    public interface IStatisticService
    {
        List<UserTranscationInfo> GetUserMonthSTT(int userId, int month);
        List<UserTranscationInfo> GetCompanyMonthSTT(int month);
    }

    public class StatisticService : IStatisticService
    {
        private readonly ITransactionRepository _transactionRepository;

        public StatisticService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public List<UserTranscationInfo> GetCompanyMonthSTT(int month)
        {
            var trans = _transactionRepository.GetAll().Where(t => t.Type == PaymentConstants.PAYMENT_TYPE_PAY && t.Time.Month == month);
            List<UserTranscationInfo> listUti = new List<UserTranscationInfo>();
            UserTranscationInfo uti = null;
            foreach (var tran in trans)
            {
                uti = new UserTranscationInfo()
                {
                    Amount = tran.Amount,
                    Detail = tran.Detail,
                    State = tran.State,
                    Time = tran.Time,
                    Type = tran.Type
                };
                listUti.Add(uti);
            }

            return listUti;
        }

        public List<UserTranscationInfo> GetUserMonthSTT(int userId, int month)
        {
            var trans = _transactionRepository.GetAll().Where(t => (t.SenderId == userId || t.ReceiverId == userId) && t.Time.Month == month);
            List<UserTranscationInfo> listUti = new List<UserTranscationInfo>();
            UserTranscationInfo uti = null;
            foreach (var tran in trans)
            {
                uti = new UserTranscationInfo()
                {
                    Amount = tran.Amount,
                    Detail = tran.Detail,
                    State = tran.State,
                    Time = tran.Time,
                    Type = tran.Type
                };
                listUti.Add(uti);
            }

            return listUti;
        }
    }
}

using DataService.Models;
using DataService.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DataService.ViewModels;
using DataService.Constants;
using System;
using System.Collections.Generic;

namespace DataService.Services
{
    public interface IWalletService
    {
        UserWalletInfo GetWallet(int userId);
        bool CheckPayable(int userId, decimal amount);
        Transaction PayTransaction(int senderId, int receiverId, decimal amount,
            string transDetail);
        Transaction TopupTransaction(int receiverId, decimal amount,
            string transDetail);
        void RegisterWallet(int userId);
    }

    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;

        public WalletService(IWalletRepository walletRepository, ITransactionRepository transactionRepository, IUserRepository userRepository)
        {
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
        }

        public bool CheckPayable(int userId, decimal amount)
        {
            bool result = false;

            Wallet wallet = _walletRepository.GetAll().Where(w => w.UserId == userId).FirstOrDefault();
            if(wallet != null)
            {
                if(wallet.Balance - (double)amount >= 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public UserWalletInfo GetWallet(int userId)
        {
            UserWalletInfo userWalletInfo = null;

            Wallet wallet = _walletRepository
                .GetAll()
                .Where(w => w.UserId == userId)
                .FirstOrDefault();
            if(wallet != null)
            {
                userWalletInfo = new UserWalletInfo()
                {
                    Id = wallet.Id,
                    Balance = wallet.Balance,
                    UserId = wallet.UserId
                };

                List<Transaction> transactions = _transactionRepository.GetAll().Where(t => t.SenderId == userId || t.ReceiverId == userId).ToList();

                UserTranscationInfo uti = null;
                foreach (var transaction in transactions)
                {
                    uti = new UserTranscationInfo()
                    {
                        Amount = transaction.Amount,
                        Detail = transaction.Detail,
                        State = transaction.State,
                        Time = transaction.Time,
                        Type = transaction.Type
                    };
                    userWalletInfo.Transactions.Add(uti);
                }

                userWalletInfo.Transactions.OrderBy(t => t.Time);
            }

            return userWalletInfo;
        }

        public void RegisterWallet(int userId)
        {
            Wallet wallet = new Wallet()
            {
                UserId = userId,
                Balance = 0,
                IsActive = true
            };

            _walletRepository.Add(wallet);
        }

        public Transaction PayTransaction(int senderId, int receiverId, decimal amount, string transDetail)
        {
            Transaction transaction = new Transaction()
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Amount = amount,
                Detail = transDetail,
                State = PaymentConstants.PAYMENT_STATE_SUCCESS,
                Type = PaymentConstants.PAYMENT_TYPE_PAY,
                Time = DateTime.Now
            };

            Wallet senderWallet = _walletRepository.GetAll().Where(w => w.UserId == senderId).FirstOrDefault();
            Wallet recieverWallet = _walletRepository.GetAll().Where(w => w.UserId == receiverId).FirstOrDefault();

            if (senderWallet != null && recieverWallet != null)
            {
                senderWallet.Balance -= (double)amount;
                recieverWallet.Balance += (double)amount;
                _walletRepository.Update(senderWallet);
                _walletRepository.Update(recieverWallet);

                return _transactionRepository.Add(transaction);
            }
            return null;
        }

        public Transaction TopupTransaction(int receiverId, decimal amount, string transDetail)
        {
            Transaction transaction = new Transaction()
            {
                SenderId = null,
                ReceiverId = receiverId,
                Amount = amount,
                Detail = transDetail,
                State = PaymentConstants.PAYMENT_STATE_SUCCESS,
                Type = PaymentConstants.PAYMENT_TYPE_TOPUP,
                Time = DateTime.Now
            };

            Wallet recieverWallet = _walletRepository.GetAll().Where(w => w.UserId == receiverId).FirstOrDefault();

            if (recieverWallet != null)
            {
                recieverWallet.Balance += (double)amount;
                _walletRepository.Update(recieverWallet);

                return _transactionRepository.Add(transaction);
            }
            return null;
        }
    }
}

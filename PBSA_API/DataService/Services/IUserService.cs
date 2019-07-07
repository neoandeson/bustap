using DataService.Repositories;
using System.Linq;
using DataService.Infrastructure;
using DataService.ViewModels;
using DataService.Models;
using System.Collections.Generic;
using DataService.Constants;

namespace DataService.Services
{
    public interface IUserService
    {
        bool Authenticate(string username, string password, string roleName);
        bool Register(AuthUser authUser);
        List<User> GetAllDrivers();
        List<User> GetAllCustomers();
        bool ChangeState(int userId, bool isActivated);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IWalletService _walletService;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IWalletService walletService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _walletService = walletService;
        }

        public bool Authenticate(string username, string password, string roleName)
        {
            bool result = false;
            HashHelper hashHelper = new HashHelper();

            Role role = _roleRepository.GetAll().Where(r => r.Name == roleName).FirstOrDefault();
            if(role != null)
            {
                var user = _userRepository
               .GetAll()
               .Where(u => u.Username == username && u.RoleId == role.Id && hashHelper.VerifyHashedPassword(u.PasswordHash, password) == true)
               .FirstOrDefault();

                result = true;
                // return false if user not found
                if (user == null) result = false;
            }
            return result;
        }

        public bool ChangeState(int userId, bool isActivated)
        {
            bool result = false;
            User user = _userRepository.GetById(userId);
            if(user != null)
            {
                user.IsActive = isActivated;
                _userRepository.Update(user);
                result = true;
            }

            return result;
        }

        public List<User> GetAllCustomers()
        {
            List<User> drivers = _userRepository.GetAll().Where(u => u.RoleId == RoleConstants.Customer).ToList();
            return drivers;
        }

        public List<User> GetAllDrivers()
        {
            List<User> drivers = _userRepository.GetAll().Where(u => u.RoleId == RoleConstants.Driver).ToList();
            return drivers;
        }

        /// <summary>
        /// Register user distinct by username
        /// </summary>
        /// <param name="authUser"></param>
        /// <returns></returns>
        public bool Register(AuthUser authUser)
        {
            bool result = false;
            int existedUser = _userRepository.GetAll().Where(u => u.Username == authUser.Username).Count();
            if (existedUser > 0)
            {
                return false;
            }

            HashHelper hashHelper = new HashHelper();
            User user = new User()
            {
                Username = authUser.Username,
                Fullname = authUser.FullName,
                IsActive = true,
                PasswordHash = hashHelper.HashPassword(authUser.Password),
                Phone = authUser.PhoneNumber
            };

            Role role = _roleRepository.GetAll().Where(r => r.Name == authUser.Role).FirstOrDefault();
            if (role != null)
            {
                user.RoleId = role.Id;
                user = _userRepository.Add(user);

                _walletService.RegisterWallet(user.Id);
                result = true;
            }

            return result;
        }
    }
}

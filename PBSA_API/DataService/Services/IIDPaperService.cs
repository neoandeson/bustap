using DataService.Repositories;
using System.Linq;
using DataService.Infrastructure;
using DataService.ViewModels;
using DataService.Models;
using DataService.Constants;
using System.Collections.Generic;

namespace DataService.Services
{
    public interface IIDPaperService
    {
        bool RegisterIDPaper(string type, string content, int customerId);
        IDPaperInfo GetUserByIDPaper(string content);
        List<IDPaperInfo> GetByUserId(int userId);
        bool ChangeState(int id, bool isActive);
        IDPaperInfo VerifyIDPaper(string input);
        bool DeleteIDPaper(int id);
    }

    public class IDPaperService : IIDPaperService
    {
        private readonly IIDPaperRepository _iDPaperRepository;
        private readonly IApplyPriceTypeRepository _applyPriceTypeRepository;

        public IDPaperService(IIDPaperRepository iDPaperRepository, IApplyPriceTypeRepository applyPriceTypeRepository)
        {
            _iDPaperRepository = iDPaperRepository;
            _applyPriceTypeRepository = applyPriceTypeRepository;
        }

        public bool ChangeState(int id, bool isActive)
        {
            Idpaper idpaper = _iDPaperRepository.GetById(id);
            if(idpaper == null)
            {
                return false;
            }
            idpaper.IsUsing = isActive;
            _iDPaperRepository.Update(idpaper);
            return true;
        }

        public bool DeleteIDPaper(int id)
        {
            bool result = false;
            Idpaper idpaper = _iDPaperRepository.GetById(id);
            if(idpaper != null)
            {
                _iDPaperRepository.Delete(idpaper);
                result = true;
            }

            return result;
        }

        public List<IDPaperInfo> GetByUserId(int userId)
        {
            List<IDPaperInfo> results = null;
            IDPaperInfo iDPaperInfo = null;
            List<Idpaper> idpapers = _iDPaperRepository.GetAll().Where(ip => ip.CustomerId == userId).ToList();
            if(idpapers != null)
            {
                results = new List<IDPaperInfo>();
                foreach (var idpp in idpapers)
                {
                    iDPaperInfo = new IDPaperInfo()
                    {
                        UserId = idpp.CustomerId,
                        AppliedPrice = idpp.AppliedPrice,
                        Type = idpp.Type
                    };
                    results.Add(iDPaperInfo);
                }
            }

            return results;
        }

        public IDPaperInfo GetUserByIDPaper(string content)
        {
            IDPaperInfo iDPaperInfo = null;
            Idpaper idpaper = _iDPaperRepository.GetAll().Where(ip => ip.Content.ToLower().Contains(content)).FirstOrDefault();
            if(idpaper != null)
            {
                iDPaperInfo = new IDPaperInfo()
                {
                    UserId = idpaper.CustomerId,
                    AppliedPrice = idpaper.AppliedPrice
                };
            }

            return iDPaperInfo;
        }

        public bool RegisterIDPaper(string type, string content, int customerId)
        {
            var result = false;
            //Check if paper is registed?
            Idpaper idpaper  = _iDPaperRepository.GetAll().Where(ip => ip.CustomerId == customerId && ip.Content.Contains(content)).FirstOrDefault();
            if(idpaper == null)
            {//If paper is not registered
                idpaper = new Idpaper()
                {
                    CustomerId = customerId,
                    Content = content,
                    AppliedPrice = GetPriceByType(type),
                    IsUsing = true,
                    IsValid = true,
                    Type = type
                };
                idpaper = _iDPaperRepository.Add(idpaper);
                if(idpaper != null)
                {
                    result = true;
                }
            }

            return result;
        }

        public IDPaperInfo VerifyIDPaper(string input)
        {
            IDPaperInfo result = null;
            string[] inputs = input.Split(" ");
            Idpaper idpaper = _iDPaperRepository.GetAll().Where(ip => inputs.Any(val => ip.Content.Contains(val))).FirstOrDefault();
            if(idpaper != null)
            {
                if (!idpaper.IsValid)
                {
                    result = new IDPaperInfo()
                    {
                        Type = IDPaperConstants.IDPP_INVALID,
                        UserId = idpaper.CustomerId,
                        AppliedPrice = 0
                    };
                } else if (!idpaper.IsUsing)
                {
                    result = new IDPaperInfo()
                    {
                        Type = IDPaperConstants.IDPP_INACTIVE,
                        UserId = idpaper.CustomerId,
                        AppliedPrice = 0
                    };
                } else
                {
                    result = new IDPaperInfo()
                    {
                        Type = idpaper.Type,
                        UserId = idpaper.CustomerId,
                        AppliedPrice = idpaper.AppliedPrice
                    };
                }
            }

            return result;
        }

        private decimal GetPriceByType(string type)
        {
            ApplyPriceType apt = _applyPriceTypeRepository.GetAll().Where(q => q.Type == type).FirstOrDefault();
            if(apt != null)
            {
                return apt.Price;
            }
            return 0;
        }
    }
}

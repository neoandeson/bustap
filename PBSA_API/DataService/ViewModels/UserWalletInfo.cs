using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.ViewModels
{
    public class UserWalletInfo
    {
        public int Id { get; set; }
        public double Balance { get; set; }
        public bool? IsActive { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<UserTranscationInfo> Transactions { get; set; }
    }

    public class ComapnyMonthInfo
    {
        public double Total { get; set; }
        public virtual ICollection<UserTranscationInfo> Transactions { get; set; }
    }
}

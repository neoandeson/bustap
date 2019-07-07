using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Wallet
    {
        public int Id { get; set; }
        public double Balance { get; set; }
        public bool? IsActive { get; set; }
        public int UserId { get; set; }
    }
}

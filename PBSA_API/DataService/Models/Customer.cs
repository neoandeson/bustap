using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public int? DebitCount { get; set; }
        public int UserId { get; set; }
    }
}

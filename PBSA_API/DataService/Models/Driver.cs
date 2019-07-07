using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Driver
    {
        public int Id { get; set; }
        public DateTime? WorkingDate { get; set; }
        public int UserId { get; set; }
    }
}

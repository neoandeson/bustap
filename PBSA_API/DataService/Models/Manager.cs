using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Manager
    {
        public int Id { get; set; }
        public string Department { get; set; }
        public int UserId { get; set; }
    }
}

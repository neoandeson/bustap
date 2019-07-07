using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Idpaper
    {
        public int Id { get; set; }
        public bool IsValid { get; set; }
        public bool IsUsing { get; set; }
        public string Content { get; set; }
        public decimal AppliedPrice { get; set; }
        public string Type { get; set; }
        public int CustomerId { get; set; }
    }
}

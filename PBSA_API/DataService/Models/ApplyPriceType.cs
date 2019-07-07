using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class ApplyPriceType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Type { get; set; }
        public string Detail { get; set; }
        public string State { get; set; }
        public int? SenderId { get; set; }
        public int ReceiverId { get; set; }
        public decimal? Amount { get; set; }
    }
}

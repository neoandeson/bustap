using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.ViewModels
{
    public class UserTranscationInfo
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Type { get; set; }
        public string Detail { get; set; }
        public string State { get; set; }
        public decimal? Amount { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Idnumber { get; set; }
        public bool? IsTraveling { get; set; }
        public DateTime? LastTravelTime { get; set; }
        public string Department { get; set; }

        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}

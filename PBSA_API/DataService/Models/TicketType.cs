using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class TicketType
    {
        public TicketType()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public double Price { get; set; }

        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}

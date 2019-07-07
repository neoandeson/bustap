using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public DateTime? SoldTime { get; set; }
        public int VerhicleId { get; set; }
        public int DriverId { get; set; }
        public double Charge { get; set; }
        public int CustomerId { get; set; }
        public string PaymentMethod { get; set; }
        public int? TicketTypeId { get; set; }

        public virtual TicketType TicketType { get; set; }
        public virtual Vehicle Verhicle { get; set; }
    }
}

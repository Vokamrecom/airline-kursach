using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Airline.DataProvider.Entities
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }

        public int BuyerId { get; set; }
        public Passenger Buyer { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Passport { get; set; }

        public int FlightId { get; set; }

        public virtual Flight Flight { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Airline.DataProvider.Entities
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }

        public DateTime BookingDateTime { get; set; }

        public int FlightId { get; set; }
        public Flight Flight { get; set; }

        public int Amount { get; set; }

        public int Price { get; set; }

        public bool IsActive { get; set; }
    }
}

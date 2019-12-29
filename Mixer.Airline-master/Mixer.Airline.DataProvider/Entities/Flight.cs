using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Airline.DataProvider.Entities
{
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }

        [Required]
        [StringLength(5)]
        public string FlightNumber { get; set; }

        public DateTime DepartureDateTime { get; set; }

        [StringLength(4)]
        public string DepartureAirport { get; set; }

        public Airport DepAirport { get; set; }

        [StringLength(5)]
        public string AircraftCode { get; set; }

        public Aircraft Aircraft { get; set; }

        public int TotalPlaces { get; set; }

        public DateTime ArrivalDateTime { get; set; }

        [StringLength(4)]
        public string ArrivalAirport { get; set; }

        public Airport ArrAirport { get; set; }

        public int Price { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}

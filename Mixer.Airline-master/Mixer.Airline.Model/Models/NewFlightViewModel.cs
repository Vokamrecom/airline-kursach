using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Model.Models
{
    public class NewFlightViewModel 
    {
        [RegularExpression(@"[A-Z]{2}[0-9]{3}", ErrorMessage = "Incorrect flight number")]
        public string FlightNumber { get; set; }

        public DateTime DepartureDate { get; set; }

        public string DepartureTime { get; set; }

        public string DepartureAirport { get; set; }

        public string AircraftCode { get; set; }

        public DateTime ArrivalDate { get; set; }

        public string ArrivalTime { get; set; }

        public string ArrivalAirport { get; set; }

        [Required]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Incorrect price")]
        public int Price { get; set; }

    }
}

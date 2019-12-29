using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Model.Models
{
    public class FlightViewModel
    {
        public int FlightId { get; set; }

        public string FlightNumber { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public string DepartureAirport { get; set; }
        public string DepartureCity { get; set; }
        public string Aircraft { get; set; }

        [Required]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Incorrect free places")]
        public int? FreePlaces { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public string ArrivalAirport { get; set; }
        public string ArrivalCity { get; set; }

        [RegularExpression(@"[0-9]+", ErrorMessage = "Incorrect price")]
        public int Price { get; set; }
    }
}

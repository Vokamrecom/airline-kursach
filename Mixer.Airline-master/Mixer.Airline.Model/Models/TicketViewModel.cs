using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Model.Models
{
    public class TicketViewModel
    {
        public int TicketId { get; set; }
        public int Buyer { get; set; }
        public string PassengerName { get; set; }
        public string PassengerSurname { get; set; }
        public string Passport { get; set; }
        public int FlightId { get; set; }
        public string FlightNumber { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public string DepartureAirport { get; set; }
        public string DepartureCity { get; set; }
        public decimal Price { get; set; }
        public string Aircraft { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public string ArrivalAirport { get; set; }
        public string ArrivalCity { get; set; }
    }
}

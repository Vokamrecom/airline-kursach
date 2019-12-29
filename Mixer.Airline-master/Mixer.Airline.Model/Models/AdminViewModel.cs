using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Model.Models
{
    public class AdminViewModel : NewFlightViewModel
    {
        public IEnumerable<FlightViewModel> FlightsList { get; set; }
        public IEnumerable<BookingViewModel> BookingsList { get; set; }
        public IEnumerable<TicketViewModel> TicketsList { get; set; }
        public IEnumerable<PassengerViewModel> UsersList { get; set; }
        public IEnumerable<AirportViewModel> AirportsList { get; set; }
        public IEnumerable<AircraftViewModel> AircraftsList { get; set; }
    }
}

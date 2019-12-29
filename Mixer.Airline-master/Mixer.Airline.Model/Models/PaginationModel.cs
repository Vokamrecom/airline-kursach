using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Model.Models
{
    public class PagedList<T>
    {
        public int RowsCount { get; set; }
        public int PagesCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public IEnumerable<T> Data { get; set; }
        public string Message { get; set; }
    }

    public class PaginationModel : NewFlightViewModel
    {
        public int? UserId { get; set; }
        public PagedList<BookingViewModel> BookingList { get; set; }
        public PagedList<TicketViewModel> TicketList { get; set; }
        public PagedList<PassengerViewModel> PassengerList { get; set; }
        public PagedList<FlightViewModel> FlightList { get; set; }
        public IEnumerable AirportList { get; set; }
        public IEnumerable AircraftList { get; set; }
        
        public PaginationModel()
        {

        }
    }
}
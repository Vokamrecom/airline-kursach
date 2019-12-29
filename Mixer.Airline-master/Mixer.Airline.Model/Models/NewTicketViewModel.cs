using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Model.Models
{
    public class NewTicketViewModel
    {
        public int FlightId { get; set; }

        public int Amount { get; set; }

        public List<PassengerInfo> Passengers { get; set; }

        public int TotalPrice { get; set; }

        [Required(ErrorMessage = "Card number is not specified")]
        [RegularExpression(@"[0-9]{16}", ErrorMessage = "Incorrect card number")]
        public string CardNumber { get; set; }

        public NewTicketViewModel()
        {
            Passengers = new List<PassengerInfo>();
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Model.Models
{
    public class NewBookingViewModel
    {
        public int FlightId { get; set; }

        [Required]
        [RegularExpression(@"[1-5]", ErrorMessage = "Please choose from 1 to 5 places")]
        public int Amount { get; set; }
        public string Message { get; set; }
    }
}

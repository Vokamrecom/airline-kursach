using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Model.Models
{
    public class SearchFlightViewModel
    {
        [Required(ErrorMessage = "Departure city not specified")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Incorrect departure city")]
        public string From { get; set; }

        [Required(ErrorMessage = "Arrival city not specified")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Incorrect arrival city")]
        public string To { get; set; }

        [Required(ErrorMessage = "Date not specified")]
        public DateTime Date { get; set; }
    }
}

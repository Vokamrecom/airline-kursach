using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Airline.Model.Models
{
    public class FlightDelayModel
    {
        public int FlightId { get; set; }
        [Required]
        [RegularExpression(@"[1-3]", ErrorMessage = "Please choose from 1 to 3 hours")]
        public int DelayTime { get; set; }
    }
}

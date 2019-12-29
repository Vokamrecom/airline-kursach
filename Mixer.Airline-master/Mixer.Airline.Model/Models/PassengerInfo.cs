using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Model.Models
{
    public class PassengerInfo
    {
        [Required(ErrorMessage = "Name is not specified")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Incorrect name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Surname is not specified")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Incorrect surname")]
        public string Surname { get; set; }


        [Required(ErrorMessage = "Passport number is not specified")]
        [RegularExpression(@"[A-Z]{2}[0-9]{7}", ErrorMessage = "Incorrect passport number")]
        public string Passport { get; set; }
    }
}

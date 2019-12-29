using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Model.Models
{
    public class EditUserInfoViewModel
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is not specified")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Incorrect name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Surname is not specified")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Incorrect surname")]
        public string Surname { get; set; }
    }
}

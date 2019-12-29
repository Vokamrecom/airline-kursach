using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Airline.DataProvider.Entities
{
    public class Airport
    {
        [Key]
        [StringLength(4)]
        public string AirportCode { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string City { get; set; }

        public virtual ICollection<Flight> Flights1 { get; set; }

        public virtual ICollection<Flight> Flights2 { get; set; }
    }
}

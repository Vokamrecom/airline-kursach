using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Airline.DataProvider.Entities
{
    public class Aircraft
    {
        [Key]
        [StringLength(5)]
        public string AircraftCode { get; set; }

        [StringLength(20)]
        public string Model { get; set; }

        public int TotalPlaces { get; set; }

        public ICollection<Flight> Flights { get; set; }
    }
}

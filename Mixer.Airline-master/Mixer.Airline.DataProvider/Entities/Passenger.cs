using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Airline.DataProvider.Entities
{
    public class Passenger
    {
        [Key]
        public int PassengerId { get; set; }

        public byte[] ProfileImage { get; set; }

        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        [Required]
        [StringLength(15)]
        public string Surname { get; set; }

        [Required]
        [StringLength(40)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}

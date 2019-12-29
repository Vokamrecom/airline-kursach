using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Airline.DataProvider.Entities
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }

        [Required]
        public string Message { get; set; }


    }
}

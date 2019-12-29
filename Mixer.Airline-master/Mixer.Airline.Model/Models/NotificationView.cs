using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Model.Models
{
    public class NotificationView
    {
        public IEnumerable<NotificationViewModel> List { get; set; }
        public int Count { get; set; }
    }
}

using Airline.Model.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Model.PaginationModels
{
    public class FlightPaginationModel
    {
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Incorrect departure city")]
        public string From { get; set; }

        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Incorrect arrival city")]
        public string To { get; set; }

        public DateTime? Date { get; set; }
        public bool GetNearby { get; set; }
        public bool OrderAscending { get; set; }
        public bool SortByDate { get; set; }
        public int PageNumber { get; set; }

        public PagedList<FlightViewModel> FlightList { get; set; }

        public FlightPaginationModel()
        {

        }
    }
}

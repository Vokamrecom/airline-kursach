using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Airline.Logic.Services;
using Airline.Model.Models;
using Airline.Model.PaginationModels;
using Airline.Website.Models;
using Newtonsoft.Json;

namespace Airline.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFlightService _flightService;

        public HomeController(
            IFlightService flightService)
        {
            _flightService = flightService;
        }

        public const int Amount = 5;

        [HttpGet]
        public ActionResult Index(bool? backToList)
        {
            var model = new FlightPaginationModel();

            var str = HttpContext.Session.GetString("LastSearch");

            if (backToList != null && str != null)
                model = JsonConvert.DeserializeObject<FlightPaginationModel>(str);
            else
            {
                var flightList = new PagedList<FlightViewModel>();
                flightList.PageNumber = 0;
                flightList.PageSize = Amount;
                model.FlightList = flightList;
                model.SortByDate = model.OrderAscending = true;
                model = _flightService.GetNeededFlightsOffset(model);
                HttpContext.Session.SetString("LastSearch", JsonConvert.SerializeObject(model));
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult GetFlightsOffset(FlightPaginationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var flightList = new PagedList<FlightViewModel>();
            flightList.PageNumber = model.PageNumber;
            flightList.PageSize = Amount;
            model.FlightList = flightList;
            model = _flightService.GetNeededFlightsOffset(model);
            if (model.FlightList.RowsCount == 0)
            {
                model.GetNearby = true;
                model = _flightService.GetNeededFlightsOffset(model);
                model.FlightList.Message = "There are no flights between these cities that day";
            }
            HttpContext.Session.SetString("LastSearch", JsonConvert.SerializeObject(model));
            return PartialView("FlightPartialView", model);
        }

        public ActionResult NewBooking(int flightId)
        {
            _flightService.IsExists(flightId);
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("NewBooking", "Booking", new { FlightId = flightId });
            else
            {
                HttpContext.Session.SetString("RedirectUrl", Url.Action("NewBooking", "Booking", new { FlightId = flightId }));
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

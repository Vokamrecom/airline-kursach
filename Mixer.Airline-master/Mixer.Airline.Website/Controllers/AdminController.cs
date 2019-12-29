using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Airline.Logic.Services;
using Airline.Model.Models;
using Airline.Model.PaginationModels;
using Airline.Website.Controllers;
using Airline.Website.Models;
using Newtonsoft.Json;

namespace Airline.Website.Controllers
{
    public class FlightsViewComponent : ViewComponent
    {
        private readonly IFlightService _flightService;
        public FlightsViewComponent(IFlightService flightService)
        {
            _flightService = flightService;
        }
        public IViewComponentResult Invoke()
        {
            var flightList = new PagedList<FlightViewModel>
            {
                PageNumber = 0,
                PageSize = 5
            };
            var m = new PaginationModel
            {
                FlightList = flightList
            };
            var model = _flightService.GetFlightsOffset(m);
            return View("AdminFlights", model);
        }
    }   

    public class PassengersViewComponent : ViewComponent
    {
        private readonly IPassengerService _passengerService;
        public PassengersViewComponent(IPassengerService passengerService)
        {
            _passengerService = passengerService;
        }
        public IViewComponentResult Invoke()
        {
            var passengerList = new PagedList<PassengerViewModel>
            {
                PageNumber = 0,
                PageSize = 5
            };
            var m = new PaginationModel
            {
                PassengerList = passengerList
            };
            var model = _passengerService.GetPassengersOffset(m);
            return View("AdminPassengers", model);
        }
    }

    public class BookingsViewComponent : ViewComponent
    {
        private readonly IBookingService _bookingService;
        public BookingsViewComponent(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        public IViewComponentResult Invoke()
        {
            var bookingList = new PagedList<BookingViewModel>
            {
                PageNumber = 0,
                PageSize = 5
            };
            var m = new PaginationModel
            {
                BookingList = bookingList
            };
            var model = _bookingService.GetBookingsOffset(m);
            return View("AdminBookings", model);
        }
    }

    public class TicketsViewComponent : ViewComponent
    {
        private readonly ITicketService _ticketService;
        public TicketsViewComponent(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public IViewComponentResult Invoke()
        {
            var ticketList = new PagedList<TicketViewModel>
            {
                PageNumber = 0,
                PageSize = 5
            };
            var m = new PaginationModel
            {
                TicketList = ticketList
            };
            var model = _ticketService.GetTicketsOffset(m);
            return View("AdminTickets", model);
        }
    }


    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IPassengerService _passengerService;
        private readonly IFlightService _flightService;
        private readonly ITicketService _ticketService;
        private readonly IBookingService _bookingService;
        private readonly IAircraftService _aircraftService;
        private readonly IAirportService _airportService;
        private readonly INotificationService _notificationService;

        public const int Amount = 3;

        public AdminController(
            IPassengerService passengerService,
            IFlightService flightService,
            IBookingService bookingService,
            ITicketService ticketService,
            IAircraftService aircraftService,
            IAirportService airportService,
            INotificationService notificationService)
        {
            _passengerService = passengerService;
            _ticketService = ticketService;
            _bookingService = bookingService;
            _flightService = flightService;
            _aircraftService = aircraftService;
            _airportService = airportService;
            _notificationService = notificationService;
        }

        //EmailService emailService = new EmailService();

        public ActionResult Index()
        {
            var model = new PaginationModel();
            model.AircraftList = new SelectList(_aircraftService.GetAll(), "AircraftCode", "AircraftCode");
            model.AirportList = new SelectList(_airportService.GetAll(), "AirportCode", "Name");
            return View(model);
        }

        [HttpPost]
        public PartialViewResult GetFlightsOffset(PaginationRequest request)
        {
            var flightList = new PagedList<FlightViewModel>
            {
                PageNumber = request.PageNumber,
                PageSize = 5
            };
            var m = new PaginationModel
            {
                FlightList = flightList
            };
            var model = _flightService.GetFlightsOffset(m);
            return PartialView("/Views/Admin/Components/Flights/AdminFlights.cshtml", model);
        }

        [HttpPost]
        public PartialViewResult GetPassengersOffset(PaginationRequest request)
        {
            var passengerList = new PagedList<PassengerViewModel>
            {
                PageNumber = request.PageNumber,
                PageSize = Amount
            };
            var m = new PaginationModel
            {
                PassengerList = passengerList
            };
            var model = _passengerService.GetPassengersOffset(m);
            return PartialView("/Views/Admin/Components/Passengers/AdminPassengers.cshtml", model);
        }

        [HttpPost]
        public PartialViewResult GetBookingsOffset(PaginationRequest request)
        {
            var bookingList = new PagedList<BookingViewModel>
            {
                PageNumber = request.PageNumber,
                PageSize = Amount
            };
            var m = new PaginationModel
            {
                BookingList = bookingList
            };
            var model = _bookingService.GetBookingsOffset(m);
            return PartialView("/Views/Admin/Components/Bookings/AdminBookings.cshtml", model);
        }

        [HttpPost]
        public PartialViewResult GetTicketsOffset(PaginationRequest request)
        {
            var ticketList = new PagedList<TicketViewModel>
            {
                PageNumber = request.PageNumber,
                PageSize = Amount
            };
            var m = new PaginationModel
            {
                TicketList = ticketList
            };
            var model = _ticketService.GetTicketsOffset(m);
            return PartialView("/Views/Admin/Components/Tickets/AdminTickets.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            var passengerId = _passengerService.GetByEmail(User.Identity.Name).PassengerId;
            _bookingService.IsExists(bookingId);
            _bookingService.IsThereAccess(passengerId, null);
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(_bookingService.GetOwner(bookingId), null, null, bookingId);
            _notificationService.NewNotification(_bookingService.GetOwner(bookingId), 0, bookingId, 0, 0);
            _bookingService.CancelBooking(bookingId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTicket(int ticketId)
        {
            var passengerId = _passengerService.GetByEmail(User.Identity.Name).PassengerId;
            _ticketService.IsExists(ticketId);
            _ticketService.IsThereAccess(passengerId, null);
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(_ticketService.GetOwner(ticketId), null, ticketId, null);
            _notificationService.NewNotification(_ticketService.GetOwner(ticketId), ticketId, 0, 0, 0);
            _ticketService.DeleteTicket(ticketId, null);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(int passengerId)
        {
            _passengerService.IsExists(passengerId);
            _passengerService.IsThereAccess(_passengerService.GetByEmail(User.Identity.Name).PassengerId);
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(_passengerService.GetById(passengerId).Email, passengerId, null, null);
            _passengerService.Delete(passengerId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteFlight(int flightId)
        {
            _flightService.IsExists(flightId);
            _flightService.IsThereAccess(_passengerService.GetByEmail(User.Identity.Name).PassengerId);
            var ticketList = _ticketService.GetAllByFlight(flightId);
            var bookingList = _bookingService.GetAllByFlight(flightId);
            foreach (var t in ticketList)
            {
                _notificationService.NewNotification(_ticketService.GetOwner(t.TicketId), 0, 0, flightId, 0);
            }
            foreach (var b in bookingList)
            {
                _notificationService.NewNotification(_bookingService.GetOwner(b.BookingId), 0, 0, flightId, 0);
            }
            _flightService.CancelFlight(flightId);
            return RedirectToAction("Index");
        }

        public ActionResult NewFlight(NewFlightViewModel model)
        {
            if (model.ArrivalAirport == model.DepartureAirport) 
                ModelState.AddModelError(string.Empty, "Departure and arrival airports cannot match");
            else
                _flightService.NewFlight(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ChangeFlightStatus(int flightId)
        {
            _flightService.IsExists(flightId);
            var model = new FlightDelayModel()
            {
                FlightId = flightId,
                DelayTime = 1,
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeFlightStatus(FlightDelayModel model)
        {
            _flightService.GhangeFlightStatus(model.FlightId, model.DelayTime);
            var ticketList = _ticketService.GetAllByFlight(model.FlightId);
            var bookingList = _bookingService.GetAllByFlight(model.FlightId);
            foreach(var t in ticketList)
            {
                _notificationService.NewNotification(_ticketService.GetOwner(t.TicketId), 0, 0, model.FlightId, model.DelayTime);
            }
            foreach (var b in bookingList)
            {
                _notificationService.NewNotification(_bookingService.GetOwner(b.BookingId), 0, 0, model.FlightId, model.DelayTime);
            }
            return RedirectToAction("Index");
        }
    }
}
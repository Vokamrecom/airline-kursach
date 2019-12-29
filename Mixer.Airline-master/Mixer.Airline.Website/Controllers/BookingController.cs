using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Airline.Logic.Services;
using Airline.Model.Models;
using Airline.Website.Controllers;

namespace Airline.Website.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IFlightService _flightService;
        private readonly IPassengerService _passengerService;

        public const int Amount = 3;

        public BookingController(
            IFlightService flightService,
            IBookingService bookingService,
            IPassengerService passengerService)
        {
            _bookingService = bookingService;
            _flightService = flightService;
            _passengerService = passengerService;
        }

        public ActionResult Index()
        {
            var bookingList = new PagedList<BookingViewModel>();
            bookingList.PageNumber = 0;
            bookingList.PageSize = Amount;
            var model = new PaginationModel();
            model.UserId = _passengerService.GetByEmail(User.Identity.Name).PassengerId;
            model.BookingList = bookingList;
            model = _bookingService.GetBookingsOffset(model);
            return View(model);
        }

        public ActionResult GetAll()
        {
            var list = _bookingService.GetAll();
            return View(list);
        } 

        [HttpPost]
        public PartialViewResult GetBookingsOffset(PaginationRequest number)
        {
            var bookingList = new PagedList<BookingViewModel>();
            bookingList.PageNumber = number.PageNumber;
            bookingList.PageSize = Amount;
            var model = new PaginationModel();
            model.UserId = _passengerService.GetByEmail(User.Identity.Name).PassengerId;
            model.BookingList = bookingList;
            model = _bookingService.GetBookingsOffset(model);
            return PartialView("~/Views/Booking/BookingPartialView.cshtml", model);
        }

        //[HttpGet("{flightId}")]
        public ActionResult NewBooking(int flightId)
        {
            _flightService.IsExists(flightId);
            var model = new NewBookingViewModel()
            {
                FlightId = flightId,
                Amount = 1,
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult NewBooking(NewBookingViewModel model)
        {
            var passengerId = _passengerService.GetByEmail(User.Identity.Name).PassengerId;
            var booking = _bookingService.NewBooking(passengerId, model.FlightId, model.Amount);
            if (booking.Message == null)
            {
                return RedirectToAction("Index");
            }
            else
                model.Message = booking.Message;
            return View(model);
        }

        public ActionResult CancelBooking(int bookingId)
        {
            _bookingService.IsThereAccess(_passengerService.GetByEmail(User.Identity.Name).PassengerId, bookingId);
            _bookingService.CancelBooking(bookingId);
            return RedirectToAction("Index");
        }

        public ActionResult NewTicket(int bookingId)
        {
            _bookingService.IsThereAccess(_passengerService.GetByEmail(User.Identity.Name).PassengerId, bookingId);
            return RedirectToAction("NewTicket", "Ticket", new { bookingId });
        }
    }
}

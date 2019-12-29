using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Airline.Logic.Services;
using Airline.Model.Models;
using Airline.Website.Controllers;

namespace SAM.Airline.Website.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly ITicketService _ticketService;
        private readonly IPassengerService _passengerService;

        public TicketController(
            IBookingService bookingService,
            ITicketService ticketService,
            IPassengerService passengerService)
        {
            _ticketService = ticketService;
            _bookingService = bookingService;
            _passengerService = passengerService;
        }

        public const int Amount = 3;

        public ActionResult Index()
        {
            var ticketList = new PagedList<TicketViewModel>();
            ticketList.PageNumber = 0;
            ticketList.PageSize = Amount;
            var model = new PaginationModel();
            model.UserId = _passengerService.GetByEmail(User.Identity.Name).PassengerId;
            model.TicketList = ticketList;
            model = _ticketService.GetTicketsOffset(model);
            return View(model);
        }

        public PartialViewResult GetTicketsOffset(PaginationRequest number)
        {
            var ticketList = new PagedList<TicketViewModel>();
            ticketList.PageNumber = number.PageNumber;
            ticketList.PageSize = Amount;
            var model = new PaginationModel();
            model.UserId = _passengerService.GetByEmail(User.Identity.Name).PassengerId;
            model.TicketList = ticketList;
            model = _ticketService.GetTicketsOffset(model);
            return PartialView("~/Views/Ticket/TicketPartialView.cshtml", model);
        }

        [HttpGet]
        public ActionResult NewTicket(int bookingId)
        {
            _bookingService.IsThereAccess(_passengerService.GetByEmail(User.Identity.Name).PassengerId, bookingId);
            var booking = _bookingService.GetById(bookingId);
            var model = new NewTicketViewModel
            {
                FlightId = booking.FlightId,
                Amount = booking.Amount,
                TotalPrice = booking.Amount * Convert.ToInt32(booking.Price)
            };
            for (int i = 0; i < model.Amount; i++)
                model.Passengers.Add(new PassengerInfo());
            return View(model);
        }

        [HttpPost]
        public ActionResult NewTicket(NewTicketViewModel model)
        {
            var passengerId = _passengerService.GetByEmail(User.Identity.Name).PassengerId;
            foreach (var p in model.Passengers)
                _ticketService.NewTicket(passengerId, model.FlightId, p.Name, p.Surname, p.Passport);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int ticketId, string passport)
        {
            _ticketService.IsThereAccess(_passengerService.GetByEmail(User.Identity.Name).PassengerId, ticketId);
            _ticketService.DeleteTicket(ticketId, passport);
            return RedirectToAction("Index");
        }
    }
}

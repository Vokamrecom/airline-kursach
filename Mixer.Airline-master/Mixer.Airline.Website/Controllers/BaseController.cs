using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Airline.Logic.Services;

namespace Airline.Website.Controllers
{
    public class BaseController : Controller
    {
        public IPassengerService PassengerService { get; set; }
        public INotificationService NotificationService { get; set; }
        public ICommitService CommitService { get; set; }

        public int CurrentUserId()
        {
            return PassengerService.GetByEmail(User.Identity.Name).PassengerId;
        }
    }
}
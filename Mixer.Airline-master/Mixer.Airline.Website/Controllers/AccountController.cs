using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Airline.Logic.Services;
using Airline.Model.Models;

namespace Airline.Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly IPassengerService _passengerService;
        private readonly INotificationService _notificationService;

        public AccountController(
            IPassengerService passengerService,
            INotificationService notificationService)
        {
            _passengerService = passengerService;
            _notificationService = notificationService;
        }

        public ActionResult MyAccount()
        {
            var passenger = _passengerService.GetById(_passengerService.GetByEmail(User.Identity.Name).PassengerId);
            return View(passenger);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var redirectUrl = HttpContext.Session.GetString("RedirectUrl");
            var logmodel = _passengerService.Login(model);
            if (logmodel.Message != null)
                return View(logmodel);
            await Authenticate(model.Email);
            if (redirectUrl != null)
            {
                HttpContext.Session.Remove("RedirectUrl");
                return Redirect(Convert.ToString(redirectUrl));
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View(new RegistrationViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var redirectUrl = HttpContext.Session.GetString("RedirectUrl");
            var regmodel = _passengerService.Registration(model);
            if (regmodel.Message != null)
                return View(regmodel);
            await Authenticate(model.Email);

            if (redirectUrl != null)
            {
                HttpContext.Session.Remove("RedirectUrl");
                return Redirect(Convert.ToString(redirectUrl));
            }
            ViewBag.Title = "My account";
            return RedirectToAction("Index", "Home");
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, _passengerService.GetRole(userName))
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult ChangePassword()
        {
            var editUser = new ChangePasswordViewModel() { Id = _passengerService.GetByEmail(User.Identity.Name).PassengerId };
            return View(editUser);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            _passengerService.ChangePassword(model);
            return RedirectToAction("MyAccount");
        }

        [HttpPost]
        public ActionResult Upload(PassengerViewModel model, IFormFile photo)
        {
            if (photo == null)
                ModelState.AddModelError(string.Empty, "Please choose file");
            else
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(photo.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)photo.Length);
                }

                model.ProfileImage = imageData; 
                _passengerService.UploadProfileImage(_passengerService.GetByEmail(User.Identity.Name).PassengerId, imageData);
            }
            return RedirectToAction("MyAccount");
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditInfo()
        {
            var passenger = _passengerService.GetById(_passengerService.GetByEmail(User.Identity.Name).PassengerId);
            var editUser = new EditUserInfoViewModel()
            {
                Id = passenger.PassengerId,
                Name = passenger.Name,
                Surname = passenger.Surname
            };
            return View(editUser);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditInfo(EditUserInfoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            _passengerService.EditInfo(model);
            return RedirectToAction("MyAccount");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteNotification(int notificationId)
        {
            _notificationService.DeleteNotification(notificationId);
            return Ok();
        }
    }
}
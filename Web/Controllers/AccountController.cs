using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Web.Common;
using Web.Dtos;
using Web.Dtos.RegionalCenterDtos;
using Web.Models;
using Web.Repositories;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AccountController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [AllowAnonymous]
        public async Task<ActionResult> Signup()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var regionalCenters = await _unitOfWork.RegionalCenterRepo.GetRegionalCenters();
            var regionalCenterDtos = new List<RegionalCenterDto>();
            foreach (var region in regionalCenters)
            {
                var regionalCenterDto = _mapper.Map<RegionalCenterDto>(region);
                regionalCenterDtos.Add(regionalCenterDto);
            }



            var listItem = new List<SelectListItem>();
            foreach (var regionalCenter in regionalCenters)
            {
                listItem.Add(new SelectListItem()
                {
                    Text = regionalCenter.Name,
                    Value = regionalCenter.RegionalCenter_ID.ToString()
                });
            }

            var regionalCenterSelectList = new SelectList(listItem, "Value", "Text");
            ViewData["regionalCenterSelectList"] = regionalCenterSelectList;

            var registrationForm = new RegistrationForm();
            return View(registrationForm);
        }
        [AllowAnonymous]
        public ActionResult LogIn()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            var loginForm = new LoginForm();
            return View(loginForm);
        }
        [Route("account/login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> LogIn(LoginForm loginForm)
        {
            var user = (await _unitOfWork.UserRepo.Find(s => s.Email == loginForm.Email)).SingleOrDefault();

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.User_ID.ToString(), false);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Incorrect email or password!");
            var newLoginForm = new LoginForm();
            return View(newLoginForm);
        }
        [Route("account/signin")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Signup(RegistrationForm registrationForm)
        {
            bool userExists = (await _unitOfWork.UserRepo.Find(s => s.Email == registrationForm.Email)).Any();

            if (userExists)
            {
                return View();
            }


            var passwordHasher = new PasswordHasher();
            var hashedPassword = passwordHasher.HashPassword(registrationForm.Password);



            var newUser = new User()
            {
                Age = registrationForm.Age,
                Email = registrationForm.Email,
                FirstName = registrationForm.FirstName,
                LastName = registrationForm.LastName,
                MiddleName = registrationForm.MiddleName,
                PhoneNumber = registrationForm.PhoneNumber,
                PasswordHash = hashedPassword,
                RegionalCenter_ID = long.Parse(registrationForm.RegionalCenter_ID)

            };
            _unitOfWork.UserRepo.Create(newUser);
            await _unitOfWork.SaveChangesAsync();


            return RedirectToAction("Login");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public async Task<ActionResult> ValidateEmailAddress(string email)
        {
            if ((await _unitOfWork.UserRepo.Find(s => s.Email == email)).Any())
            {
                return Json(string.Format("an account for address {0} already exists.", email), JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
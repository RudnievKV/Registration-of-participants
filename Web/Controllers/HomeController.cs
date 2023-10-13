using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Dtos.UserDtos;
using Web.Models;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public HomeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "firstName" : "";
            var users = await _unitOfWork.UserRepo.GetUsers();
            switch (sortOrder)
            {
                case "firstName":
                    users = users.OrderBy(s => s.FirstName);
                    break;
                case "secondName":
                    users = users.OrderBy(s => s.MiddleName);
                    break;
                case "lastName":
                    users = users.OrderBy(s => s.LastName);
                    break;
                case "age":
                    users = users.OrderBy(s => s.Age);
                    break;
                case "regionalCenter":
                    users = users.OrderBy(s => s.RegionalCenter.Name);
                    break;
            }

            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                var newUserDto = _mapper.Map<UserDto>(user);
                userDtos.Add(newUserDto);
            }
            return View(userDtos);
        }
    }
}
using AutoMapper;
using Hotel_BLL.DTO;
using Hotel_BLL.Interfaces;
using Hotel_PL.Models;
using Hotel_PL.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        IMapper mapper;
        IUserService guestService;

        public UserController(IUserService guestService)
        {
            this.guestService = guestService;

            mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<UserDTO, UserModel>();
                    cfg.CreateMap<UserRequest, UserDTO>().ReverseMap();
                }).CreateMapper();
        }




        // GET: GuestController
        public ActionResult AllGuests()
        {
            var guestModels = mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserModel>>(guestService.GetAllGuests());
            return View(guestModels);
        }

        // GET: GuestController/Details/5
        public ActionResult Details(int id)
        {
            var guestModel = mapper.Map<UserDTO, UserModel>(guestService.Get(id));
            return View("GuestDetails", guestModel);
        }


        // GET: GuestController/Edit/5
        public ActionResult Edit(int id)
        {
            var userRequest = mapper.Map<UserDTO, UserRequest>(guestService.Get(id));
            return View("EditGuest", userRequest);
        }

        // POST: GuestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserRequest userRequest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var UserDTO = mapper.Map<UserRequest, UserDTO>(userRequest);
                    UserDTO.Id = id;
                    guestService.UpdateGuest(UserDTO);
                    return RedirectToAction("AllGuests");
                }
                catch (ArgumentException)
                {
                    ModelState.AddModelError("PhoneNumber", "Пользователь с таким  номером телефона уже сущесвует");
                }
                return View("EditGuest", userRequest);
            }
            else
            {
                return View("EditGuest", userRequest);
            }
        }

        // GET: GuestController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                guestService.DeleteGuest(id);
                return RedirectToAction("AllGuests");
            }
            catch
            {
                return View("Error");
            }
        }

    }
}

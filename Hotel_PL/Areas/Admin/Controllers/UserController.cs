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
        IUserService userService;
        ILogService logService;

        public UserController(IUserService userService, ILogService logService)
        {
            this.userService = userService;
            this.logService = logService;

            mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<UserDTO, UserModel>();
                    cfg.CreateMap<UserRequest, UserDTO>().ReverseMap();
                    cfg.CreateMap<LogDataModel, LogDataDTO>();
                }).CreateMapper();
        }


        public ActionResult AllGuests()
        {
            var guestModels = mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserModel>>(userService.GetAllUsers());
            return View(guestModels);
        }

        public ActionResult Details(int id)
        {
            try
            {
                var guestModel = mapper.Map<UserDTO, UserModel>(userService.Get(id));
                return View("GuestDetails", guestModel);
            }
            catch
            {
                var guestModels = mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserModel>>(userService.GetAllUsers());
                return View("AllGuests",guestModels);
            }
        }

        public ActionResult Edit(int id)
        {
            var userRequest = mapper.Map<UserDTO, UserRequest>(userService.Get(id));
            return View("EditGuest", userRequest);
        }

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
                    userService.UpdateUser(UserDTO);

                    var adminId = userService.GetByPhoneNumber(User.Identity.Name).Id;

                    LogDataModel logDataModel = new LogDataModel()
                    {
                        AdminId = adminId,
                        EntityId = id,
                        EntityName = "User",
                        ObjectState = userRequest.ToString(),
                    };

                    logService.AddEditLog(mapper.Map<LogDataModel, LogDataDTO>(logDataModel));


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

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var user = userService.Get(id);
                userService.DeleteUser(id);

                var adminId = userService.GetByPhoneNumber(User.Identity.Name).Id;

                LogDataModel logDataModel = new LogDataModel()
                {
                    AdminId = adminId,
                    EntityId = id,
                    EntityName = "User",
                    ObjectState = user.ToString(),
                };

                logService.AddDeleteLog(mapper.Map<LogDataModel, LogDataDTO>(logDataModel));


                return RedirectToAction("AllGuests");
            }
            catch
            {
                return View("Error");
            }
        }

    }
}

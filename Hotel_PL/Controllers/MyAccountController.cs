
using AutoMapper;
using Hotel_BLL.DTO;
using Hotel_BLL.Interfaces;
using Hotel_BLL.Services;
using Hotel_DAL.Entities;
using Hotel_PL.Models;
using Hotel_PL.RequestModels;
using Hotel_PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.Controllers
{
    [Authorize(Roles = "Admin, Guest")]
    public class MyAccountController : Controller
    {
        IUserService userService;
        IBookingService bookingService;
        IMapper mapper;
        UserManager<User> userManager;
        SignInManager<User> signInManager;



        public MyAccountController(IUserService userService, IBookingService bookingService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userService = userService;
            this.bookingService = bookingService;
            this.userManager = userManager;
            this.signInManager = signInManager;

            mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<UserDTO, UserModel>();
                    cfg.CreateMap<UserDTO, User>();
                    cfg.CreateMap<UserDTO, UserRequest>().ReverseMap();
                    cfg.CreateMap<BookingDTO, BookingModel>().ReverseMap();
                    cfg.CreateMap<RoomDTO, RoomModel>();
                    cfg.CreateMap<CategoryDTO, CategoryModel>();
                }).CreateMapper();


        }

        // GET: MyAccountController
        public ActionResult Index()
        {
            var user = userService.GetAllGuests().SingleOrDefault(u => u.PhoneNumber == User.Identity.Name);

            return View(mapper.Map<UserDTO,UserModel>(user));
        }


        // GET: MyAccountController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = userService.GetByPhoneNumber(User.Identity.Name);
            return View(mapper.Map<UserDTO,UserRequest>(user));
        }

        // POST: MyAccountController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, UserRequest user)
        //{
        //    try
        //    {
        //        var userDTO = mapper.Map<UserRequest, UserDTO>(user);
        //        userDTO.Id = id;
        //        userService.UpdateGuest(userDTO);

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> Edit(UserRequest model)
        {
            if (ModelState.IsValid)//Возможно Перенести в сервисы
            {
                User user = userManager.FindByNameAsync(User.Identity.Name).Result;
                if (user != null)
                {
                    var OldName = user.UserName;

                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.UserName = model.PhoneNumber;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Address = model.Address;


                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        if (OldName != user.UserName)
                            await signInManager.SignOutAsync();

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("PhoneNumber", "Пользователь с таким номером телефона уже существует");
                        }
                    }
                }
            }
            return View(model);
        }


        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByNameAsync(User.Identity.Name);
                if (user != null)
                {
                    var _passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                    var _passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                    IdentityResult result =
                        await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                        await userManager.UpdateAsync(user);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult MyBookings()
        {
            var myBookings = bookingService.GetAllBookings().Where(b => b.user.PhoneNumber == User.Identity.Name);
            var myBokingsModel = mapper.Map<IEnumerable<BookingDTO>, IEnumerable<BookingModel>>(myBookings);
           
            foreach(var bm in myBokingsModel)
            {
                bm.TotalPrice = bookingService.GetTotalPrice(bm.Id);
            }

            return View(myBokingsModel);
        }

    }
}

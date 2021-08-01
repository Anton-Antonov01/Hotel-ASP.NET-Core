using AutoMapper;
using Hotel_BLL.DTO;
using Hotel_BLL.Interfaces;
using Hotel_DAL.Entities;
using Hotel_PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.Controllers
{
    public class AccountController : Controller
    {

        IMapper mapper;
        IUserService userService;
        IAccountService accountService;
        SignInManager<User> signInManager;
        UserManager<User> userManager;
        RoleManager<IdentityRole<int>> roleManager;

        public AccountController(IAccountService accountService ,IUserService userService, SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<RegisterViewModel, UserDTO>();
                    cfg.CreateMap<RegisterViewModel, RegistrationModelDTO>();
                    cfg.CreateMap<LoginViewModel, LoginModelDTO>();
                    cfg.CreateMap<UserDTO, User>();
                }).CreateMapper();

            this.userService = userService;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.accountService = accountService;
            this.roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                //Сделать проверки на валидность и тд

                //userService.AddGuest(mapper.Map<RegisterViewModel, UserDTO>(model));

                await accountService.Register(mapper.Map<RegisterViewModel,RegistrationModelDTO>(model));
                var user = userService.GetAllGuests().SingleOrDefault(u => u.PhoneNumber == model.PhoneNumber);

                if (userManager.IsInRoleAsync(mapper.Map<UserDTO, User>(user), "Admin").Result)
                {
                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                }
                return RedirectToAction("Index", "Home");

            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var result = await accountService.Login(mapper.Map<LoginViewModel,LoginModelDTO>(model));


                if (result.Succeeded)
                {
                    var user = userService.GetAllGuests().SingleOrDefault(u => u.PhoneNumber == model.PhoneNumber);
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        if(userManager.IsInRoleAsync(mapper.Map<UserDTO,User>(user), "Admin").Result)
                        {
                            return RedirectToAction("Index", "Home", new { Area = "Admin"} );
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}

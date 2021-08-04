using AutoMapper;
using Hotel_BLL.DTO;
using Hotel_BLL.Interfaces;
using Hotel_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_BLL.Services
{
    public class AccountService : IAccountService
    {
        SignInManager<User> signInManager;
        UserManager<User> userManager;
        RoleManager<IdentityRole<int>> roleManager;
        IMapper mapper;
        public AccountService(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {

            mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<RegistrationModelDTO, User>();
                }).CreateMapper();

            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        //public async Task Register(RegistrationModelDTO regModel)
        //{
        //    var user = mapper.Map<RegistrationModelDTO, User>(regModel);
        //    user.UserName = regModel.PhoneNumber;
        //    var result = await userManager.CreateAsync(user, regModel.Password);

        //    var UserRoles = from r in roleManager.Roles.ToList()
        //                   where r.Name == "Guest"
        //                   select r.Name;


        //    await userManager.AddToRolesAsync(user, UserRoles);

        //    if (result.Succeeded)
        //    {

        //    }
        //    else
        //    {
        //        throw new ArgumentException();
        //    }

        //}

        //public async Task<SignInResult> Login(LoginModelDTO loginModel)
        //{
        //    var result =  await signInManager.PasswordSignInAsync(loginModel.PhoneNumber, loginModel.Password, loginModel.RememberMe, false);
        //    return result;
        //}

        //public void Logout()
        //{
        //    throw new NotImplementedException();
        //}


    }
}

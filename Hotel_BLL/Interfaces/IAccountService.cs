using Hotel_BLL.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_BLL.Interfaces
{
    public interface IAccountService
    {
        public Task Register(RegistrationModelDTO regModel);

        public Task<SignInResult> Login(LoginModelDTO loginModel);

        public void Logout();
    }
}

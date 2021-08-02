using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_PL.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "Страый пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Заполните поле")]
        public string OldPassword { get; set; }

        [Display(Name = "Новый пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Заполните поле")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        [Required(ErrorMessage = "Заполните поле")]
        public string ConfirmNewPassword { get; set; }
    }
}

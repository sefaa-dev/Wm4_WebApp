using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ItServiceApp.ViewModels
{
    public class LoginViewModel
    {

        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı adı gereklidir")]

        public string UserName { get; set; }
       [Required(ErrorMessage = "Şifre alanı gereklidir.")]
       [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifreniz minimum 6 karakterli olmalıdır !!")]

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }


    }
}

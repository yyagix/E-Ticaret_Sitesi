using System.ComponentModel.DataAnnotations;

namespace ProductApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı gerekli.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre gerekli.")]
        public string Password { get; set; }
    }
}

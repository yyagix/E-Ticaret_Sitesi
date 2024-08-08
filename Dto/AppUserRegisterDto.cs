using System.ComponentModel.DataAnnotations;

namespace ProductApp.Dto
{
    public class AppUserRegisterDto
    {
        [Display(Name = "Adınız")]
        [Required(ErrorMessage = "Ad Alanı Boş Bırakılamaz")]
        public string FirstName { get; set; }

        [Display(Name = "Soyadınız")]
        [Required(ErrorMessage = "Soyadı Alanı Boş Bırakılamaz")]
        public string LastName { get; set; }

        [Display(Name = "Kullanıcı Adınız")]
        [Required(ErrorMessage = "Kullanıcı Adı Alanı Boş Bırakılamaz")]
        public string UserName { get; set; }

        [Display(Name = "Şehir")]
        [Required(ErrorMessage = "Şehir Alanı Boş Bırakılamaz")]
        public string City { get; set; }

        [Display(Name = "Emailiniz")]
        [Required(ErrorMessage = "Email Alanı Boş Bırakılamaz")]
        public string Email { get; set; }

        [Display(Name = "Telefon Numarası")]
        [Required(ErrorMessage = "Telefon Numarası Alanı Boş Bırakılamaz")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "Şifre en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir.")]
        public string Password { get; set; }

        [Display(Name = "Şifreyi Onayla")]
        [Required(ErrorMessage = "Şifre doğrulaması zorunludur.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
    }
}

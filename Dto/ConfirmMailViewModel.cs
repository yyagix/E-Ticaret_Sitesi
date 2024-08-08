using System.ComponentModel.DataAnnotations;

namespace ProductApp.Models
{
    public class ConfirmMailViewModel
    {
        [Required(ErrorMessage = "E-posta adresi gereklidir.")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Onay kodu gereklidir.")]
        [StringLength(6, ErrorMessage = "Onay kodu 6 haneli olmalıdır.", MinimumLength = 6)]
        public string ConfirmCode { get; set; }
    }
}

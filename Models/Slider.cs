using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApp.Models
{
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }

        [Display(Name = "Slider Adı")]
        public string? SliderName { get; set; } = string.Empty;
        [Display(Name = "Küçük Başlık")]
        public string? Header1 { get; set; } = string.Empty;
        [Display(Name = "Büyük Başlık")]
        public string? Header2 { get; set; } = string.Empty;
        [Display(Name = "Buton")]
        public string? Context { get; set; } = string.Empty;
        [Display(Name = "Slider Resmi")]

        [Required(ErrorMessage = "Slider Resmi Alanı Boş Bırakılamaz")]
        public string? SliderImage { get; set; }
        
        [NotMapped]
        public IFormFile? ImageUpload { get; set; }
    }
}

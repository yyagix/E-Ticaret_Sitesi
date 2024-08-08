using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApp.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "Ürün Adı")]
        [Required(ErrorMessage = "Ürün adı gereklidir.")]
        [StringLength(100, ErrorMessage = "Ürün adı en fazla 100 karakter olabilir.")]
        public string ProductName { get; set; } = string.Empty;

        [Display(Name = "Ürün Kodu")]
        [Required(ErrorMessage = "Ürün kodu gereklidir.")]
        public int ProductCode { get; set; }

        [Display(Name = "Ürün Açıklaması")]
        [Required(ErrorMessage = "Ürün açıklaması gereklidir.")]
        [StringLength(600, ErrorMessage = "Ürün açıklaması en fazla 600 karakter olabilir.")]
        public string? ProductDescription { get; set; } = string.Empty;

        [Display(Name = "Ürün Resmi")]
        public string? ProductPicture { get; set; } = string.Empty;

        [Display(Name = "Ürün Fiyatı")]
        [Required(ErrorMessage = "Ürün fiyatı gereklidir.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat geçerli bir değer olmalıdır.")]
        public decimal ProductPrice { get; set; }

        [Required(ErrorMessage = "Ürün kategorisi gereklidir.")]
        [Display(Name = "Ürün Kategori")]
        public int CategoryId { get; set; }

        public virtual Category? Category { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Resim yüklemesi gereklidir.")]
        public IFormFile ImageUpload { get; set; }
    }
}

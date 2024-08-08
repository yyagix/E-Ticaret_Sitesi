using System.ComponentModel.DataAnnotations;

namespace ProductApp.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Display(Name = "Kategori Adı")]
        [Required(ErrorMessage = "Kategori Adı Alanı Boş Bırakılamaz")]
        public string? CategoryName { get; set; }
        virtual public List<Products>? Products { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BasitAlisveris.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Kategori adı gereklidir.")]
        public string CategoryName { get; set; } = string.Empty;

        public virtual ICollection<SubCategory>? SubCategories { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}

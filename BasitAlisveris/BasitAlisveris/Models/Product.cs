using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasitAlisveris.Models
{
    public class Product
    {
        [Key]
        public int Product_Id { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string ProductDescription { get; set; } = string.Empty;

        public string ProductImage { get; set; } = string.Empty;

        public decimal? ProductPrice { get; set; }

        [ForeignKey("Category")]
        public int? Category_Id { get; set; }

        public virtual Category? Category { get; set; }

        [ForeignKey("SubCategory")]
        public int? SubCategory_Id { get; set; }
        public virtual SubCategory? SubCategory { get; set; }

        public string Product_Feature { get; set; } = string.Empty;

        


        [NotMapped]
        public IFormFile? PictureImage { get; set; }
    }
}

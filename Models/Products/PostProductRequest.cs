using System.ComponentModel.DataAnnotations;

namespace ShoppingApi.Models.Products
{
    public class PostProductRequest
    {
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        [Required]
        [StringLength(100)]
        public string Category { get; set; }
        [Required]
        public decimal? UnitPrice { get; set; }
    }
}

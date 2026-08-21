using System.ComponentModel.DataAnnotations;

namespace MVCApplication.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public decimal Price { get; set; } = 0.00m;
        public string ProductColor { get; set; } = null!;
    }
}

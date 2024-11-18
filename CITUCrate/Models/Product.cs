using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CITUCrate.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string ImageUrl { get; set; } = "\"C:\\Users\\dontm\\source\\repos\\CITUCrate\\CITUCrate\\wwwroot\\images\\OIP.jpeg\""; // Default image URL

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; }

        [Required]
        public int Quantity { get; set; }

        [StringLength(500)]
        public string ShortDescription { get; set; }
    }
}

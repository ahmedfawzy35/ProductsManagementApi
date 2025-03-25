using ProductsManagement.Models.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products_Management_API.Models.Domain
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "DATE")]
        public DateTime CreatedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        // Navigation Properties

        public ICollection<Product> Products { get; set; }
    }
}

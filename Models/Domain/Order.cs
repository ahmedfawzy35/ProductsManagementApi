using ProductsManagement.Models.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products_Management_API.Models.Domain
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "DATE")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [MaxLength(50)]
        public string OrderStatus { get; set; }

        [MaxLength(50)]
        public string PaymentMethod { get; set; }

        [MaxLength(100)]
        public string ShippingAddress { get; set; }

        // Navigation Properties

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}

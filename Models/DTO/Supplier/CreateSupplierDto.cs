using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Products_Management_API.Models.DTO.Supplier
{
    public class CreateSupplierDto
    {
        [Required(ErrorMessage = "Supplier name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [DefaultValue("Default Supplier Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [DefaultValue("supplier@example.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
        [DefaultValue("1234567890")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        [DefaultValue("123 Supplier Street, City, Country")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Company name is required.")]
        [MaxLength(100, ErrorMessage = "Company name cannot exceed 100 characters.")]
        [DefaultValue("Default Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Created date is required.")]
        [DefaultValue("2025-01-01")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "IsActive status is required.")]
        [DefaultValue(true)]
        public bool IsActive { get; set; } = true;
    }
}

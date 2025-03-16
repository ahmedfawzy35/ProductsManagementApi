using Products_Management_API.Enums;
using System.ComponentModel;

namespace Products_Management_API.Models.DTO.Product
{
    public class ProductFilterDto
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? IsAvailable { get; set; }
        public int? CategoryId { get; set; }

        [DefaultValue("name")]
        public string OrderBy { get; set; }

        [DefaultValue(OrderDirection.Ascending)]
        public OrderDirection OrderDirection { get; set; } = OrderDirection.Ascending;
    }
}
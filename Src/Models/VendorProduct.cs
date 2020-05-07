using System.ComponentModel.DataAnnotations;

namespace CommunitiesWinApi.Models
{
    public partial class VendorProduct
    {
        [Key]
        public long VendorProductId { get; set; }
        public long VendorId { get; set; }
        public long ProductId { get; set; }
        public decimal? MinOrderQuantity { get; set; }
        public decimal? Price { get; set; }
        public string Units { get; set; }
    }    
}

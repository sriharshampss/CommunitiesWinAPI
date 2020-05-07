using System.ComponentModel.DataAnnotations;
namespace CommunitiesWinApi.Models
{
    public partial class VendorCategory
    {
        [Key]
        public long VendorCategoryId { get; set; }
        public long CategoryId { get; set; }
        public long VendorId { get; set; }
        public bool IsActive { get; set; }
    }
}

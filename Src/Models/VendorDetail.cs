using System.ComponentModel.DataAnnotations;

namespace CommunitiesWinApi.Models
{
    public partial class VendorDetails
    {
        [Key]
        public long VendorId { get; set; }
        public string VendorName { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public long Phone { get; set; }
        public string Pin { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public bool? IsSocialDistance { get; set; }
        public bool? IsFeverScreen { get; set; }
        public bool? IsSanitizerUsed { get; set; }
        public bool? IsStampCheck { get; set; }
    }
}

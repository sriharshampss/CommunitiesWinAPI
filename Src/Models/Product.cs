using System.ComponentModel.DataAnnotations;

namespace CommunitiesWinApi.Models
{
    public partial class Product
    {
        [Key]
        public long ProductId { get; set; }
        public string ProductName { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CommunitiesWinApi.Models
{
    public partial class Category
    {
        [Key]
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BoaEasyPay.Models
{
    public class Bank
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }

        [Required]
        public required string SortCode { get; set; }

        public required string Address { get; set; }

        public required string BranchCode { get; set; }
    }
}

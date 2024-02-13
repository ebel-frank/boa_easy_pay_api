using System.ComponentModel.DataAnnotations;

namespace BoaEasyPay.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Number { get; set; }

        public required string Bank { get; set; }
    }
}

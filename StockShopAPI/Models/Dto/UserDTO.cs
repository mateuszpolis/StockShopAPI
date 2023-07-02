using System;
using System.ComponentModel.DataAnnotations;

namespace StockShopAPI.Models.Dto
{
	public class UserDTO
	{
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PaymentInfo { get; set; }
        public string Permissions { get; set; }
        public string OrderHistory { get; set; }
    }
}


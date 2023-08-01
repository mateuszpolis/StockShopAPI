using System;
using System.ComponentModel.DataAnnotations;

namespace StockShopAPI.Models.Dto
{
	public class UserDTO
	{
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;        
        public string Password { get; set; } = string.Empty;
    }
}


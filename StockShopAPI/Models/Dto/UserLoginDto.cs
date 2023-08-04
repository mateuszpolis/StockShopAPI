using System;
namespace StockShopAPI.Models.Dto
{
	public class UserLoginDto
	{
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Boolean RememberUser { get; set; } = false;
    }
}


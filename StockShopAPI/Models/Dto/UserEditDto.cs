using System;
namespace StockShopAPI.Models.Dto
{
	public class UserEditDto
	{
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}


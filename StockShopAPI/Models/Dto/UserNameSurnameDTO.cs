using System;
namespace StockShopAPI.Models.Dto
{
    public class UserNameSurnameDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}


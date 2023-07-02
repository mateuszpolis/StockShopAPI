
using System;
using StockShopAPI.Models.Dto;

namespace StockShopAPI.Data
{
	public static class UserStore
	{
		public static List<UserDTO> userList = new List<UserDTO>()
            {
                new UserDTO()
                {
                    Id = 0,
                    FirstName = "Mateusz",
                    LastName = "Polis",
                    BirthDate = new DateTime(2003,11,18),
                    Email = "polismateusz@gmail.com",
                    Address = "Poland Czestochowa 42-200 Malopolska 60d -",
                    PaymentInfo = "",
                    Permissions = "{customer: true, admin: false}",
                    OrderHistory = ""
                },
                new UserDTO()
                {
                    Id = 1,
                    FirstName = "Natalia",
                    LastName = "Kowalska",
                    BirthDate = new DateTime(2003,12,17),
                    Email = "nataliakonopiska@gmail.com",
                    Address = "Poland Konopiska 42-300 Gagarina 7 -",
                    PaymentInfo = "",
                    Permissions = "{customer: true, admin: false}",
                    OrderHistory = ""
                }
            };
    }
}


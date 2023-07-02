using System;
namespace StockShopAPI.Models
{
	public class User
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
		public string Address { get; set; }
		public string PaymentInfo { get; set; }
		public string Permissions { get; set; }
		public string OrderHistory { get; set; }
	}
}


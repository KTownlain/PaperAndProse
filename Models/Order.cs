using System.ComponentModel.DataAnnotations;


namespace PaperAndProse.Models
{
	public class Order
	{
		[Required]
		public string Name { get; set; }

		[Required, EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Address { get; set; }


		public List<Book> CartItems { get; set; }
		public decimal Total { get; set; }
	}
}

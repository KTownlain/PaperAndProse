namespace PaperAndProse.Models
{
	public class CartItem
	{
		public string BookId { get; set; }
		public string Title { get; set; }
		public string ImagePath { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; } = 1;
	}
}
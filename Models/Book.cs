namespace PaperAndProse.Models
{
	public class Book
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public string Description { get; set; }
		public string ImagePath { get; set; }
		public string Condition { get; set; }
		public decimal Price { get; set; }
		public string Category { get; set; }
	}
}

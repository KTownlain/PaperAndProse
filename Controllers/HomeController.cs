using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using PaperAndProse.Helpers;
using PaperAndProse.Models;

namespace PaperAndProse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

		public IActionResult Cart()
		{
			var cart = HttpContext.Session.GetObject<List<Book>>("Cart") ?? new List<Book>();
			return View(cart);
		}
		

		public IActionResult Index()
        {
			var featuredBooks = GetFeaturedBooks();
			return View(featuredBooks);
        }

        public IActionResult Privacy()
        {
            return View();
        }

		public IActionResult Shop(string category)
		{
			var books = GetBooks();

			if (!string.IsNullOrEmpty(category))
			{
				books = books.Where(b => b.Category != null &&
					b.Category.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
							  .Any(cat => cat.Equals(category, StringComparison.OrdinalIgnoreCase)))
					.ToList();
			}

			return View(books);
		}

		public IActionResult Categories()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }


        public IActionResult ProductDetail(string id)
        {
            var books = GetBooks(); // still need to implement
            var book = books.FirstOrDefault(books => books.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

		

		public IActionResult AddToCart(string id)
	{
		var books = GetBooks();
		var book = books.FirstOrDefault(b => b.Id == id);

		if (book == null)
		{
			return NotFound();
		}

		// Retrieve the existing cart from session, or start a new one
		var cart = HttpContext.Session.GetObject<List<Book>>("Cart") ?? new List<Book>();

		cart.Add(book);

		// Save updated cart back to session
		HttpContext.Session.SetObject("Cart", cart);

		return RedirectToAction("Cart");
	}

		public IActionResult RemoveFromCart(string id)
		{
			var cart = HttpContext.Session.GetObject<List<Book>>("Cart") ?? new List<Book>();
			var itemToRemove = cart.FirstOrDefault(b => b.Id == id);

			if (itemToRemove != null)
			{
				cart.Remove(itemToRemove);
				HttpContext.Session.SetObject("Cart", cart);
			}

			return RedirectToAction("Cart");
		}


		private List<Book> GetFeaturedBooks()
		{
			var allBooks = GetBooks();
			return allBooks.Where(b =>
			b.Id == "the_night_library" ||
			b.Id == "dust_and_ink" ||
			b.Id == "echoes_in_the_stacks"
			).ToList();
		}

		private List<Book> GetBooks()
        {
            return new List<Book> {
                new Book
                {
                    Id = "the_night_library",
                    Title = "The Night Library",
                    Author = "Lucy Archer",
					Category = "Gothic, Fiction",
                    Description = "A chilling mystery unfolds beneath the crumbling arches of a forgotten library where stories don't merely end - they linger, waiting for new readers.",
					Condition = "Very Good",
                    Price = 6.75M,
                    ImagePath = "~/img/the_night_library.png"
                },

			  new Book
{
	Id = "dust_and_ink",
	Title = "Dust and Ink",
	Author = "Henry Waller",
	Category = "Poetry, Literary",
	Description = "A vintage collection of soulful poems capturing fleeting moments, lost loves, and quiet resilience — penned in the fading ink of another time.",
	Condition = "Good",
	Price = 5.25M,
	ImagePath = "~/img/dust_and_ink.png"
},

new Book
{
	Id = "echoes_in_the_stacks",
	Title = "Echoes in the Stacks",
	Author = "Eleanor Milton",
	Category = "Historical, Fiction",
	Description = "Whispers of forgotten lives drift through the aisles of an abandoned library. A haunting historical tale of memory, love, and secrets bound between dusty covers.",
	Condition = "Acceptable",
	Price = 4.95M,
	ImagePath = "~/img/echoes_in_the_stacks.png"
},

new Book
{
	Id = "a_study_in_crimson",
	Title = "A Study in Crimson",
	Author = "Benedict Hale",
	Category = "Mystery, Detective",
	Description = "A faded detective's final case leads him into the shadowed corridors of a forgotten boarding school.",
	Condition = "Like New",
	Price = 7.50M,
	ImagePath = "~/img/a_study_in_crimson.png"
},

new Book
{
	Id = "tomes_of_the_withering_court",
	Title = "Tomes of the Withering Court",
	Author = "Arabella Vane",
	Category = "Fantasy, Magical",
	Description = "Betrayal festers in the marble halls of a cursed kingdom where every book holds a binding oath.",
	Condition = "Very Good",
	Price = 6.95M,
	ImagePath = "~/img/tomes_of_the_withering_court.png"
},

new Book
{
	Id = "clockwork_and_cobwebs",
	Title = "Clockwork and Cobwebs",
	Author = "Vivienne Moor",
	Category = "Steampunk, Adventure",
	Description = "In a fog-cloaked city, an inventor's apprentice must stop a clocktower built to end the world.",
	Condition = "Like New",
	Price = 7.25M,
	ImagePath = "~/img/clockwork_and_cobwebs.png"
},

new Book
{
	Id = "between_ash_and_stardust",
	Title = "Between Ash and Stardust",
	Author = "Cassian Reed",
	Category = "Sci-Fi, Dystopian",
	Description = "A lone survivor charts a course through the ruins of two worlds: one dying, one yet to be born.",
	Condition = "Good",
	Price = 6.25M,
	ImagePath = "~/img/between_ash_and_stardust.png"
},

new Book
{
	Id = "the_last_lanterns_glow",
	Title = "The Last Lantern's Glow",
	Author = "Seraphina Cross",
	Category = "Romance, Realism",
	Description = "In a town where the lanterns burn without flame, a grieving woman unearths a love story lost to time.",
	Condition = "Very Good",
	Price = 6.50M,
	ImagePath = "~/img/the_last_lanterns_glow.png"
},

new Book
{
	Id = "whispers_of_the_velvet_library",
	Title = "Whispers of the Velvet Library",
	Author = "Rowan Ashford",
	Category = "Gothic, Fiction",
	Description = "Beneath a crumbling manor, a hidden library guards secrets that were never meant to be read.",
	Condition = "Like New",
	Price = 7.95M,
	ImagePath = "~/img/whispers_in_the_velvet_library.png"
},

new Book
{
	Id = "the_bookbinders_secret",
	Title = "The Bookbinder’s Secret",
	Author = "Theodore Vale",
	Category = "Poetry, Literary",
	Description = "An aging bookbinder’s quiet life unravels when a peculiar manuscript demands to be rewritten — or else.",
	Condition = "Good",
	Price = 5.95M,
	ImagePath = "~/img/the_bookbinders_secret.png"
},

new Book
{
	Id = "inkheart_reveries",
	Title = "Inkheart Reveries",
	Author = "Lilith Montgomery",
	Category = "Fantasy, Magical",
	Description = "When a reclusive poet discovers a journal that writes back, reality and imagination dangerously entwine.",
	Condition = "Very Good",
	Price = 6.45M,
	ImagePath = "~/img/inkheart_reveries.png"
},

new Book
{
	Id = "beneath_the_silent_pages",
	Title = "Beneath the Silent Pages",
	Author = "Elena Thorne",
	Category = "Paranormal, Suspense",
	Description = "A forgotten journal hidden in the spine of a library book sets off a chain of eerie events in a sleepy town with buried secrets.",
	Condition = "Good",
	Price = 6.30M,
	ImagePath = "~/img/beneath_the_silent_pages.png"
}

			};
        }

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

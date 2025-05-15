using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftyPinko.DAL;
using SoftyPinko.Models;

namespace SoftyPinko.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles ="Admin")]
    public class CardController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CardController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var cards = _context.Cards.ToList();
            return View(cards);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Card card, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (card is null)
            {
                ModelState.AddModelError("", "Bos qoyula bilmez sectionlar");
            }
            string filename = Guid.NewGuid() + file.FileName;
            var path = Path.Combine(_env.WebRootPath, "Upload", filename);
            using FileStream fileStream = new FileStream(path, FileMode.Create);

            await file.CopyToAsync(fileStream);

            card.PhotoUrl = "/Upload/" + filename;
            await _context.AddAsync(card);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var card = _context.Cards.FirstOrDefault(x => x.Id == id);
            return View(card);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Card card, IFormFile file, int id)
        {
            var oldCard = _context.Cards.FirstOrDefault(x => x.Id == id);
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (card is null)
            {
                ModelState.AddModelError("", "Bos qoyula bilmez sectionlar");
            }
            string filename = Guid.NewGuid() + file.FileName;
            var path = Path.Combine(_env.WebRootPath, "Upload", filename);
            using FileStream fileStream = new FileStream(path, FileMode.Create);

            await file.CopyToAsync(fileStream);

            card.PhotoUrl = "/Upload/" + filename;

            oldCard.PhotoUrl = card.PhotoUrl;
            oldCard.FullName = card.FullName;
            oldCard.Position = card.Position;
            oldCard.Description = card.Description;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var card = _context.Cards.FirstOrDefault(x => x.Id == id);
            _context.Cards.Remove(card);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

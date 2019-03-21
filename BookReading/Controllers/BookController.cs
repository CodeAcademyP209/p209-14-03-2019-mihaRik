using BookReading.DAL;
using BookReading.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookReading.Controllers
{
    public class BookController : Controller
    {
        private readonly BookContext _db;

        public BookController()
        {
            _db = new BookContext();
        }

        // GET: Book
        public ActionResult About(int? id)
        {
            if (id == null) return HttpNotFound();

            var book = _db.Books.Find(id);

            if (book == null) return HttpNotFound();

            var vm = new AboutBookViewModel
            {
                Book = book,
                RelatedBooks = _db.Books.Where(b => b.Id != id && b.Category.Id == book.Category.Id).Take(4)
            };

            return View(vm);
        }
    }
}
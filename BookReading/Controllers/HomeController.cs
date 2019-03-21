using BookReading.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BookReading.Models.ViewModels;

namespace BookReading.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookContext _db;

        public HomeController()
        {
            _db = new BookContext();
        }

        public ActionResult Index()
        {
            var vm = new IndexHomeViewModel
            {
                Books = _db.Books.Include(b => b.Authors),
                SliderItems = _db.SliderItems
            };

            return View(vm);
        }

        public ActionResult ShowMoreBooks(int skipCount)
        {
            var books = _db.Books
                        .OrderBy(b => b.CreatedAt)
                        .Skip(skipCount)
                        .Take(5)
                        .ToList();

            return PartialView("_ShowMoreBooks", books);
        }
    }
}
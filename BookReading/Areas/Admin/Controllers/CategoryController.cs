using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookReading.DAL;
using BookReading.Models;
using BookReading.Models.ViewModels;
using BookReading.Extensions;
using System.Data.Entity;
using static BookReading.Utility.Utility;
using BookReading.CustomFilterAttribute;
using System.IO;

namespace BookReading.Areas.Admin.Controllers
{
    [AdminAuthentication]
    public class CategoryController : Controller
    {
        private readonly BookContext _db;

        public CategoryController()
        {
            ViewBag.ActivePageCategory = "active";
            _db = new BookContext();
        }

        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(_db.Categories);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")]Category category)
        {
            if (!ModelState.IsValid) return View(category);

            _db.Categories.Add(category);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return HttpNotFound();

            var category = _db.Categories.Find(id);

            if (category == null) return HttpNotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name")]Category category)
        {
            if (!ModelState.IsValid) return View(category);

            _db.Entry(category).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return HttpNotFound();

            var category = _db.Categories.Find(id);

            if (category == null) return HttpNotFound();

            return View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(int? id)
        {
            if (id == null) return HttpNotFound();

            var category = _db.Categories.Find(id);

            if (category == null) return HttpNotFound();

            _db.Entry(category).State = EntityState.Deleted;
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
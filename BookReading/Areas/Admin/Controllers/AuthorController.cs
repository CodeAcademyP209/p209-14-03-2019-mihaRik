using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookReading.CustomFilterAttribute;
using BookReading.DAL;
using BookReading.Extensions;
using BookReading.Models;
using static BookReading.Utility.Utility;

namespace BookReading.Areas.Admin.Controllers
{
    [AdminAuthentication]
    public class AuthorController : Controller
    {
        private readonly BookContext _db;

        public AuthorController()
        {
            ViewBag.ActivePageAuthor = "active";
            _db = new BookContext();
        }

        // GET: Admin/Author
        public ActionResult Index()
        {
            return View(_db.Authors.ToList());
        }

        // GET: Admin/Author/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Surname, Photo")] Author author)
        {
            if (!ModelState.IsValid) return View(author);

            if (author.Photo != null)
            {
                if (!author.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Please select image.");
                    return View(author);
                }

                author.Image = author.Photo.Save("author", FileType.Image);
            }

            _db.Authors.Add(author);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Author/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Author author = _db.Authors.Find(id);

            if (author == null) return HttpNotFound();

            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name, Surname, Photo")] Author author)
        {
            if (!ModelState.IsValid) return View(author);

            var authorFromDb = _db.Authors.Find(author.Id);

            if (author.Photo != null)
            {
                if (!author.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Please select image.");
                    return View(author);
                }

                RemoveFile(authorFromDb.Image, FileType.Image);
                authorFromDb.Image = author.Photo.Save("author", FileType.Image);
            }

            authorFromDb.Name = author.Name;
            authorFromDb.Surname = author.Surname;

            _db.Entry(authorFromDb).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Admin/Author/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Author author = _db.Authors.Find(id);

            if (author == null) return HttpNotFound();

            return View(author);
        }

        // POST: Admin/Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAuthor(int id)
        {
            Author author = _db.Authors.Find(id);

            RemoveFile(author.Image, FileType.Image);
            _db.Authors.Remove(author);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

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

namespace BookReading.Areas.Admin.Controllers
{
    [AdminAuthentication]
    public class BookController : Controller
    {
        private readonly BookContext _db;

        public BookController()
        {
            ViewBag.ActivePageBook = "active";
            _db = new BookContext();
        }

        // GET: Admin/Book
        public ActionResult Index()
        {
            return View(_db.Books.Include(b => b.Authors).Include(b => b.Category));
        }

        public ActionResult Create()
        {
            var vm = new CreateBookViewModel
            {
                Authors = _db.Authors,
                Categories = _db.Categories
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Description, PageCount, Photo, BookFile")]Book book,
                                   [Bind(Include = "Authors")]int[] authors, [Bind(Include = "Categories")]int? categories)
        {
            if (!ModelState.IsValid && (categories == null || authors == null))
            {
                var vm = new CreateBookViewModel
                {
                    Book = book,
                    Authors = _db.Authors,
                    Categories = _db.Categories
                };

                return View(vm);
            }

            //custom model binding (authors to book)
            foreach (var author in authors)
            {
                book.Authors.Add(_db.Authors.Find(author));
            }

            if (book.BookFile == null)
            {
                ModelState.AddModelError("BookFile", "Please add pdf.");
                var vm = new CreateBookViewModel
                {
                    Book = book,
                    Authors = _db.Authors,
                    Categories = _db.Categories
                };

                return View(vm);
            }

            book.Category = _db.Categories.Find(categories);

            if (book.BookFile.IsPDF())
            {
                book.BookPath = book.BookFile.Save("book", FileType.PDF);
            }

            if (book.Photo != null && book.Photo.IsImage())
            {
                book.Image = book.Photo.Save("book", FileType.Image);
            }

            book.CreatedAt = DateTime.Now;
            book.UpdatedAt = DateTime.Now;

            _db.Books.Add(book);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return HttpNotFound();

            var book = _db.Books.Find(id);

            if (book == null) return HttpNotFound();

            var vm = new CreateBookViewModel
            {
                Book = book,
                Authors = _db.Authors,
                Categories = _db.Categories
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name, Description, PageCount, Photo, BookFile")]Book book,
                                 [Bind(Include = "Authors")]int[] authors, [Bind(Include = "Categories")]int? categories)
        {
            var bookFromDb = _db.Books.Find(book.Id);

            if (!ModelState.IsValid && (categories == null || authors == null))
            {
                var vm = new CreateBookViewModel
                {
                    Book = book,
                    Authors = _db.Authors,
                    Categories = _db.Categories
                };

                return View(vm);
            }

            //custom model binding (authors to book)
            foreach (var author in authors)
            {
                book.Authors.Add(_db.Authors.Find(author));
            }

            book.Category = _db.Categories.Find(categories);


            #region check book pdf file
            if (book.BookFile != null)
            {
                if (!book.BookFile.IsPDF())
                {
                    ModelState.AddModelError("BookFile", "Please select pdf file.");
                    var vm = new CreateBookViewModel
                    {
                        Book = book,
                        Authors = _db.Authors,
                        Categories = _db.Categories
                    };

                    return View(vm);
                }

                RemoveFile(bookFromDb.BookPath, FileType.PDF);

                bookFromDb.BookPath = book.BookFile.Save("book", FileType.PDF);
            }
            #endregion

            #region check book photo file
            if (book.Photo != null)
            {
                if (!book.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Please select image.");
                    var vm = new CreateBookViewModel
                    {
                        Book = book,
                        Authors = _db.Authors,
                        Categories = _db.Categories
                    };

                    return View(vm);
                }

                RemoveFile(bookFromDb.Image, FileType.Image);

                bookFromDb.Image = book.Photo.Save("book", FileType.Image);
            }
            #endregion

            bookFromDb.Name = book.Name;
            bookFromDb.Authors.Clear();
            bookFromDb.Authors = book.Authors;
            bookFromDb.Category = book.Category;
            bookFromDb.PageCount = book.PageCount;
            bookFromDb.Description = book.Description;
            bookFromDb.UpdatedAt = DateTime.Now;

            _db.Entry(bookFromDb).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return HttpNotFound();

            var book = _db.Books.Find(id);

            if (book == null) return HttpNotFound();

            return View(book);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBook(int? id)
        {
            if (id == null) return HttpNotFound();

            var book = _db.Books.Find(id);

            if (book == null) return HttpNotFound();

            RemoveFile(book.BookPath, FileType.PDF);
            RemoveFile(book.Image, FileType.Image);

            _db.Entry(book).State = EntityState.Deleted;
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
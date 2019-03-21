using BookReading.DAL;
using BookReading.Extensions;
using BookReading.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BookReading.Utility.Utility;
using BookReading.CustomFilterAttribute;

namespace BookReading.Areas.Admin.Controllers
{
    [AdminAuthentication]
    public class SliderController : Controller
    {
        private readonly BookContext _db;

        public SliderController()
        {
            _db = new BookContext();
        }

        // GET: Admin/Slider
        public ActionResult Index()
        {
            return View(_db.SliderItems);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return HttpNotFound();

            var slider = _db.SliderItems.Find(id);

            if (slider == null) return HttpNotFound();

            return View(slider);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Title, Button, Photo")]SliderItem slider)
        {
            var sliderFromDb = _db.SliderItems.Find(slider.Id);

            if (!ModelState.IsValid) return View(sliderFromDb);

            if (slider.Photo != null)
            {
                if (!slider.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Please select image.");
                    return View(sliderFromDb);
                }

                RemoveFile(sliderFromDb.Image, FileType.Image);
                sliderFromDb.Image = slider.Photo.Save("slider", FileType.Image);
            }

            sliderFromDb.Title = slider.Title;
            sliderFromDb.Button = slider.Button;

            _db.Entry(sliderFromDb).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
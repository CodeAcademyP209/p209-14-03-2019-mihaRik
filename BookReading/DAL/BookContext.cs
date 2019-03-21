using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BookReading.Models;

namespace BookReading.DAL
{
    public class BookContext : DbContext
    {
        public BookContext() : base("BookContext"){}

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SliderItem> SliderItems { get; set; }
    }
}
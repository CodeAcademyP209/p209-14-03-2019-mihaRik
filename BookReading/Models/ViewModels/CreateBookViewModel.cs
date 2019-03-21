using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReading.Models.ViewModels
{
    public class CreateBookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
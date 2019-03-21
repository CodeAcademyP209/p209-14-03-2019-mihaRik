using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReading.Models.ViewModels
{
    public class AboutBookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Book> RelatedBooks { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReading.Models.ViewModels
{
    public class IndexHomeViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<SliderItem> SliderItems { get; set; }
    }
}
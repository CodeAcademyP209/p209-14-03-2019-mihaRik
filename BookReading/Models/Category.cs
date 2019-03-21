using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookReading.Models
{
    public class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }

        [Required, StringLength(100), MinLength(3)]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
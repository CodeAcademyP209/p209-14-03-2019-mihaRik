using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookReading.Models
{
    public class Book
    {
        public Book()
        {
            Authors = new HashSet<Author>();
        }

        public int Id { get; set; }

        [Required, StringLength(100), MinLength(1)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name ="Page count")]
        public int PageCount { get; set; }

        [StringLength(300)]
        public string Image { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [NotMapped]
        public HttpPostedFileBase Photo { get; set; }

        [StringLength(300)]
        public string BookPath { get; set; }

        [NotMapped]
        [Display(Name ="Book file")]
        public HttpPostedFileBase BookFile { get; set; }

        public virtual ICollection<Author> Authors { get; set; }

        [Required]
        public virtual Category Category { get; set; }
    }
}
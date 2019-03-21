using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookReading.Models
{
    public class SliderItem
    {
        public int Id { get; set; }

        [Required, StringLength(500)]
        public string Title { get; set; }

        [Required, StringLength(50)]
        public string Button { get; set; }

        [StringLength(300)]
        public string Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase Photo { get; set; }
    }
}
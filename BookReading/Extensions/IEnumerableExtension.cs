using BookReading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookReading.Extensions
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<SelectListItem> ToSelectList(this IEnumerable<Category> list)
        {
            foreach (var item in list)
            {
                yield return new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                };
            }
        }
    }
}
using Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Pro.Models
{
    public class BookCopiesCreateEditViewModel
    {
        public BookCopies BookCopies { get; set; }
        public List<SelectListItem> Books { get; set; }
        public List<int> Selected { get; set; }
        public BookCopiesCreateEditViewModel()
        {
            Books = new List<SelectListItem>();
        }
    }
}

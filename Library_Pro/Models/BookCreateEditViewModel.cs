using Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Pro.Models
{
    public class BookCreateEditViewModel
    {
        public Book Book { get; set; }
        [Display(Name = "Choose Publisher")]
        public List<SelectListItem> AddPublisher { get; set; }
    }
}

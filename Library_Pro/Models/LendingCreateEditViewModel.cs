using Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Pro.Models
{
    public class LendingCreateEditViewModel
    {
        public Lending Lending { get; set; }
        public int BookCopiesId { get; set; }
        public int LibraryId { get; set; }
        public List<SelectListItem> Clients { get; set; }
    }
}

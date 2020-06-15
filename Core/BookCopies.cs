using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core
{
    public class BookCopies
    {
        public int Id { get; set; }
        [Display(Name = "Book"), Required]
        public int BookId { get; set; }
        public Book Book { get; set; }
        [Display(Name = "Number Of Copies"), Required]
        public int NumberOfCopies { get; set; }
        public int LibraryId { get; set; }
        public Library Library { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Library
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        public List<BookCopies> BookCopies { get; set; }
        public Library()
        {
            BookCopies = new List<BookCopies>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Display(Name = "Year of Issue"), DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D}"), Required]
        public DateTime YearOfIssue { get; set; }
        [Required]
        public int NumberOfPages { get; set; }
        public List<BookPublishers> BookPublishers { get; set; }
        public Book()
        {
            BookPublishers = new List<BookPublishers>();
        }
    }
}

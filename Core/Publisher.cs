using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core
{
    public class Publisher
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        public List<BookPublishers> BookPublishers { get; set; }
        public Publisher()
        {
            BookPublishers = new List<BookPublishers>();
        }
    }
}

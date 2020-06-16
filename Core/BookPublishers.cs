using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class BookPublishers
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}

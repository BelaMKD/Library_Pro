using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Pro.Models
{
    public class ReturnedViewModel
    {
        public IEnumerable<Lending> Lendings { get; set; }
        public int LibraryId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class Lending
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public DateTime DatumZajmuvanje { get; set; }
        public DateTime DatumVratena { get; set; }
    }
}

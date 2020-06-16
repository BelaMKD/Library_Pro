using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core
{
    public class Lending
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        [Required, Display(Name = "Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }
        [Required, DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D}")]
        public DateTime DatumZajmuvanje { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DatumVratena { get; set; }
    }
}

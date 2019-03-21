using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class BookTaken
    {
        public int ID { get; set; }

        [Required]
        public int TakenBy { get; set; }

        [Required]
        public int TakenBook { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TakingDate { get; set; }
    }
}

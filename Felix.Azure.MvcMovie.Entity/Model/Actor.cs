using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Felix.Azure.MvcMovie.Entity.Model
{
    public class Actor
    {
        public int ID { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        public bool Gender { get; set; }

        [Display(Name = "Birthday")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [StringLength(4000)]
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }
    }
}

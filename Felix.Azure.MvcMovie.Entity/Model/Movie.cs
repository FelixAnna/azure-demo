using Felix.Azure.MvcMovie.Entity.Model;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Felix.Azure.MvcMovie.Entity
{
    public class Movie
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Range(0.01, 1000000)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }

        [Range(0.01, 10)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Rating { get; set; }

        public Item AsItem()
        {
            return new Item()
            {
                Id = $"{this.ID}-id",
                Name = this.Title,
                Category = this.Genre,
                Price = this.Price,
                Rating = this.Rating,
                ReleaseDate = this.ReleaseDate
            };
        }
    }
}

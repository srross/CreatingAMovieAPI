using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CreateMovieApi.Models
{
    public partial class Movie
    {
        [Key]
        public int MovieId { get; set; }
        public string MovieTitle { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string Category { get; set; } = null!;
    }
}

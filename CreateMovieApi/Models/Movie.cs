using System;
using System.Collections.Generic;

namespace CreateMovieApi.Models
{
    public partial class Movie
    {
        public int MovieId { get; set; }
        public string? MovieTitle { get; set; }
        public string? Genre { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}

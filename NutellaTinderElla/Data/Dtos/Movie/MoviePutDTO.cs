﻿using System.ComponentModel.DataAnnotations;

namespace WebMovieApi.Data.Dtos.Movie
{
    public class MoviePutDTO
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; } = null!;
        [StringLength(50)]
        public string Genre { get; set; } = null!;
        [StringLength(4)]
        public string ReleaseYear { get; set; } = null!;
        [StringLength(50)]
        public string Director { get; set; } = null!;
        [StringLength(255)]
        public string Picture { get; set; } = null!;
        [StringLength(255)]
        public string Trailer { get; set; } = null!;
        //public int FranchiseId { get; set; }
    }
}
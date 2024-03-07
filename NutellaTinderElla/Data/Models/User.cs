﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NutellaTinderElla.Data.Models;

namespace NutellaTinderEllaApi.Data.Models
{
    [Table("CurrentUser")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int GenderPreference { get; set; }

        public int Gender { get; set; }

        public int Seeking { get; set; }

        [Required]
        [StringLength(255)]
        public string Bio { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string Picture { get; set; }

        public int Age { get; set; }

        public ICollection<Like> Likes { get; set; }
        public ICollection<Swipes> Swipes { get; set; }

        public User()
        {
            Likes = new List<Like>();
            Swipes = new List<Swipes>();

        }

    }
}
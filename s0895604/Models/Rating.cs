﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace s0895604.Models
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }

        [Required]
        [DisplayName("Review")]
        public int ReviewId { get; set; }
        public virtual Review Review { get; set; }

        [Required]
        [DisplayName("Gebruiker")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        [DisplayName("Beoordeling")]
        public int RatingNumber { get; set; }
    }
}
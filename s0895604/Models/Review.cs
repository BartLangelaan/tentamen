using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace s0895604.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        [DisplayName("Productnaam")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }


        [DisplayName("Gebruiker")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [DisplayName("Gemaakt op")]
        public DateTime CreatedDateTime { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public bool Active { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
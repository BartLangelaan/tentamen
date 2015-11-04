using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using s0895604.App_Logic;

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


        [Required]
        [DisplayName("Gebruiker")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [DisplayName("Gemaakt op")]
        public DateTime CreatedDateTime { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public Review()
        {
            UserId = LoggedInUser.User.UserId;
        }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
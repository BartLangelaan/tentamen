using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using s0895604.App_Logic;

namespace s0895604.Models
{
    public class Review
    {


        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime CreatedDateTime { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public Review()
        {
            UserId = LoggedInUser.User.UserId;
        }
    }
}
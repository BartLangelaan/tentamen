using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace s0895604.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [DisplayName("Naam")]
        public string Name { get; set; }

        public virtual ICollection<Review> Reviews { get; set; } 
    }
}
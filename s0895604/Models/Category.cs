using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace s0895604.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [DisplayName("Naam")]
        public string Name { get; set; }

        //public virtual ICollection<Review> Reviews { get; set; } 
    }
}
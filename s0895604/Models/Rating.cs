using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace s0895604.Models
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }

        [DisplayName("Review")]
        public int ReviewId { get; set; }
        public virtual Review Review { get; set; }

        [DisplayName("Gebruiker")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        [DisplayName("Beoordeling")]
        public int RatingNumber { get; set; }
    }
}
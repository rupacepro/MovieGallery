using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MovieGallery.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Reviews can not be blank!")]
        public string Content { get; set; } // The text of the review
        
        [Required(ErrorMessage = "Reviews date id required")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } // Timestamp of the review

        [Required(ErrorMessage = "User Id is required")]
        public string UserId { get; set; } // FK to the User who wrote the review

        [ForeignKey("UserId")]
        public IdentityUser? User { get; set; }

        [Required(ErrorMessage ="Movie id is mandatory")]
        public int MovieId { get; set; } // FK to the associated Movie

        [ForeignKey("MovieId")]
        public Movie? Movie { get; set; }
    }
}

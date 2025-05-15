using System.ComponentModel.DataAnnotations;

namespace SoftyPinko.Models
{
    public class Card
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public  string Description { get; set; }
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(20)]
        public string Position { get; set; }
        public string? PhotoUrl { get; set; }
    }
}

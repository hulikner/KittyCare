using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KittyCare.Models
{
    public class Cat
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(35)]
        public string Name { get; set; }
        [Required]
        [DisplayName("Owner")]
        public int OwnerId { get; set; }
        [Required]
        [MaxLength(40)]
        public string Breed { get; set; }
        public string ImageUrl { get; set; }
    }
}
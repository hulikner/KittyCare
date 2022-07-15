using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace KittyCare.Models
{
    public class Provider
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(35)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(35)]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Neighborhood")]
        public int NeighborhoodId { get; set; }
        public string ImageUrl { get; set; }
        public Neighborhood Neighborhood { get; set; }
    }
}

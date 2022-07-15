using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KittyCare.Models
{
    public class Owner
    {
        public int Id { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hmm... You should really add a name...")]
        [MaxLength(35)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Hmm... You should really add a name...")]
        [MaxLength(35)]
        public string LastName { get; set; }

        [Required]
        [StringLength(55, MinimumLength = 5)]
        public string Address { get; set; }

        [Required]
        [DisplayName("Neighborhood")]
        public int NeighborhoodId { get; set; }

        public Neighborhood Neighborhood { get; set; }

        [Phone]
        [DisplayName("Phone Number")]
        public string Phone { get; set; }

        public List<Cat> Cats { get; set; }
        private List<string> CatNames
        {
            get
            {
                List<string> names = new List<string>();
                foreach (Cat d in Cats)
                {
                    names.Add(d.Name);
                }
                return names;
            }
        }
        public string CatNamesString()
        {
            return string.Join(", ", CatNames);
        }
    }
}

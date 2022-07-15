using System.Collections.Generic;
using System.ComponentModel;

namespace KittyCare.Models.ViewModels
{
    public class ProvisionFormViewModel
    {
        public List<Provider> Providers { get; set; }
        public List<Cat> Cats { get; set; }
        [DisplayName("Cat(s)")]
        public List<int> CatIds { get; set; }
        public Provision Provision { get; set; }
    }
}
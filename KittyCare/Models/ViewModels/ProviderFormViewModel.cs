using System.Collections.Generic;
using System.ComponentModel;
namespace KittyCare.Models.ViewModels
{
    public class ProviderFormViewModel
    {
        public Provider Provider { get; set; }
        public List<Neighborhood> Neighborhoods { get; set; }

    }
}

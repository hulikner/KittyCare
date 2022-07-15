using System.Collections.Generic;
using System.Linq;
namespace KittyCare.Models.ViewModels
{
    public class ProviderProfileViewModel
    {
        public Provider Provider { get; set; }
        public List<Provision> Provisions { get; set; }
        public string TotalProvisionTime
        {
            get
            {
                int totalMins = Provisions.Select(w => w.Duration).Sum() / 60;
                int hrs = totalMins / 60;
                int mins = totalMins % 60;
                return $"{hrs}hr {mins}mins";
            }
        }
    }
}

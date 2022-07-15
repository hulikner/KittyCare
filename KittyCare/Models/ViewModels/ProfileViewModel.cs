using System;
using System.Collections.Generic;

namespace KittyCare.Models.ViewModels
{
    public class ProfileViewModel
    {
        public Owner Owner { get; set; }
        public List<Provider> Providers { get; set; }
        public List<Cat> Cats { get; set; }
    }
}

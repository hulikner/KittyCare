using System;
using System.ComponentModel.DataAnnotations;
namespace KittyCare.Models
{
    public class Provision
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public int ProviderId { get; set; }
        public int CatId { get; set; }
        public Cat Cat { get; set; }
        public Provider Provider { get; set; }
        public Neighborhood Neighborhood { get; set; }
    }
}

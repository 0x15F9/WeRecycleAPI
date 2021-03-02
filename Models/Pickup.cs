using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Pickup
    {

        [Key]
        public string Id { set; get; }
        public string BeforeImage { set; get; }
        public string AfterImage { set; get; }
        public string Weight { set; get; }
        
        [Required]
        public Bin Bin { set; get; }
    }
}
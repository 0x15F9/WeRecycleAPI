using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Pickup
    {

        [Key]
        public int Id { set; get; }
        public string BeforeImage { set; get; }
        public string AfterImage { set; get; }
        public string Weight { set; get; }

        public Route Route { set; get; }

    }
}
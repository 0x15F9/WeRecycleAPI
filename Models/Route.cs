using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Route
    {
        [Key]
        public int Id { set; get; }

        public DateTime Date { set; get; }

        public IEnumerable<Pickup> Pickups{ set; get; }

        public Driver Driver { set; get; }
    }
}
using System;
using System.Collections.Generic;
using API.Models;

namespace API.DTO
{
    public class RouteRes
    {
        public int id { set; get; }

        public DateTime Date { set; get; }
        
        public IEnumerable<PickupRes> Pickups { set; get; }

        public int DriverId { set; get; }
        public string DriverName { set; get; }
    }
}
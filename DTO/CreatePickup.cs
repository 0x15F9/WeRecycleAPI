using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace API.DTO
{
    public class CreatePickup
    {
        [Required]
        public int RouteId { set; get; }

        [Required]
        public IFormFile BeforeImage { set; get; }

        [Required]
        public IFormFile AfterImage { set; get; }
        
        public string Weight { set; get; }
    }
}
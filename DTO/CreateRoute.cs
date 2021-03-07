using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using API.Models;
using Microsoft.AspNetCore.Http;

namespace API.DTO
{
    public class CreateRoute
    {
        [Required]
        public DateTime Date { set; get; }
        [Required]
        public IFormFile BeforeImage { set; get; }
        [Required]
        public IFormFile AfterImage { set; get; }
        public string Weight { set; get; }

    }
}
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace API.DTO
{
    public class CreateDriverForm
    {

        [Required]
        [RegularExpression("^5?[0-9]{7}$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { set; get; }
        
        [Required]
        public string FirstName { set; get; }
        
        [Required]
        public string LastName { set; get; }
        
        [Required]
        public string Password { get; set; }
        
        [Required]
        public IFormFile IdCard { set; get; }
        
        [Required]
        public IFormFile DrivingLicense { set; get; }
        
        [Required]
        public IFormFile ProofOfAddress { set; get; }
    }
}
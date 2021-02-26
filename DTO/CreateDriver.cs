using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class CreateDriver
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
        
        // [Required]
        public string IdCard { set; get; }
        
        // [Required]
        public string DrivingLicense { set; get; }
        
        // [Required]
        public string ProofOfAddress { set; get; }
    }
}
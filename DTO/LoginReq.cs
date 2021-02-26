using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class LoginReq
    {
        [Required]
        [RegularExpression("^5?[0-9]{7}$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { set; get; }

        [Required]
        public string Password { set; get; }
    }
}
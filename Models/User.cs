using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public abstract class User
    {
        public int Id { set; get; }
        
        [Required]
        [RegularExpression("^5?[0-9]{7}$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public UserStatus Status { set; get; }

        [Required]
        public abstract UserRole Role { set; get; }

    }
}
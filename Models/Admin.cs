using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Admins")]
    public class Admin : User
    {
        public override UserRole Role { get; set; } = UserRole.ADMIN;
    }
}
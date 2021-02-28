using API.Models;

namespace API.DTO
{
    public class LoginRes
    {
        public int Id { set; get; }
        public string Token { set; get; }
        public UserRole Role { set; get; }
        public UserStatus Status { set; get; }
    }
}
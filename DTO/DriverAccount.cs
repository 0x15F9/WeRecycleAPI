using API.Models;

namespace API.DTO
{
    public class DriverAccount
    {
        public int Id { set; get; }
        public string IdCard { set; get; }
        public string DrivingLicense { set; get; }
        public string ProofOfAddress { set; get; }
        public string PhoneNumber { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public UserStatus Status { set; get; }

    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Drivers")]
    public class Driver : User
    {
        public string IdCard { set; get; }
        public string DrivingLicense { set; get; }
        public string ProofOfAddress { set; get; }
        public override UserRole Role { get; set; } = UserRole.DRIVER;

        public IEnumerable<Route> Routes { set; get; }

    }
}
using System.ComponentModel.DataAnnotations;
using API.DTO;

namespace API.Models
{
    public class Bin : Location
    {
        [Key]
        public int Id { set; get; }
        public BinMaterial Material { set; get; }
        
    }
}
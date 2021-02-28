using System.ComponentModel.DataAnnotations;
using API.Models;

namespace API.DTO
{
    public class UpdateBin : Location
    {
        [Required]
        public int Id { set; get; }
        public BinMaterial Material { set; get; }
    }
}
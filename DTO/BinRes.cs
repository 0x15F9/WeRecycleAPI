using API.Models;

namespace API.DTO
{
    public class BinRes : Location
    {
        public int Id { set; get; }
        public BinMaterial Material { set; get; }
    }
}
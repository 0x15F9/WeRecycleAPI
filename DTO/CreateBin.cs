using API.Models;

namespace API.DTO
{
    public class CreateBin : Location
    {
        public BinMaterial material { set; get; }   
    }
}
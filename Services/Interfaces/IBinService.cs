using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Models;

namespace API.Services
{
    public interface IBinService
    {
        // Create bin
        Task<BinRes> CreateBin(CreateBin dto);

        // Read one bin
        Task<BinRes> ReadBin(int id);

        // Read all bins
        IEnumerable<BinRes> ReadBin();
        
        // Update bin
        Task<BinRes> UpdateBin(UpdateBin dto);

        // Delete bin 
        Task<bool> DeleteBin(int id);
    }
}
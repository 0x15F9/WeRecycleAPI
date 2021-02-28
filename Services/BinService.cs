using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Services
{
    public class BinService : IBinService
    {
        private readonly ILogger<BinService> _logger;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BinService(ILogger<BinService> logger, DataContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<BinRes> CreateBin(CreateBin dto)
        {
            Bin newBin = _mapper.Map<Bin>(dto);
            await _context.Bins.AddAsync(newBin);
            await _context.SaveChangesAsync();

            return _mapper.Map<BinRes>(newBin);
        }

        public async Task<bool> DeleteBin(int id)
        {
            Bin bin = await _context.Bins.FirstOrDefaultAsync(b => b.Id == id);
            
            if(bin == null)
                return false;

            _context.Bins.Remove(bin);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<BinRes> ReadBin(int id)
        {
            Bin bin = await _context.Bins.FirstOrDefaultAsync(b => b.Id == id);
            return _mapper.Map<BinRes>(bin);
        }

        public IEnumerable<BinRes> ReadBin()
        {
            var bins = _context.Bins.AsAsyncEnumerable();
            return _mapper.Map<IEnumerable<BinRes>>(bins);
        }

        public async Task<BinRes> UpdateBin(UpdateBin dto)
        {
            Bin bin = await _context.Bins.AsNoTracking().FirstOrDefaultAsync(b => b.Id == dto.Id);
            bin = _mapper.Map<Bin>(dto);
            _context.Update(bin);
            await _context.SaveChangesAsync();
            return _mapper.Map<BinRes>(bin);
        }
    }
}
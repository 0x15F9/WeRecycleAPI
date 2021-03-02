using API.DTO;
using API.Models;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CreateDriverForm, CreateDriver>();
            CreateMap<CreateDriver, Driver>();
            CreateMap<User, LoginRes>();

            CreateMap<CreateBin, Bin>();
            CreateMap<UpdateBin, Bin>();
            CreateMap<Bin, BinRes>();
        }
    }
}
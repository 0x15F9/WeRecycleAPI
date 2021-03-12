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
            CreateMap<Driver, DriverAccount>();
            CreateMap<Admin, AdminAccount>();

            CreateMap<CreateBin, Bin>();
            CreateMap<UpdateBin, Bin>();
            CreateMap<Bin, BinRes>();

            CreateMap<CreatePickup, Pickup>();
            CreateMap<Route, CreatePickup>();
            CreateMap<CreateRoute, Route>();
            CreateMap<Route, RouteRes>()
                .ForMember(
                    dest => dest.DriverId,
                    opts => opts.MapFrom(source => source.Driver.Id)
                );
            CreateMap<Pickup, PickupRes>();
        }
    }
}
using AutoMapper;
using FuelInApi.Models;
using FuelInApi.Models.Dtos;

namespace FuelInApi.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<FuelOrder, CreateFuelOrderDto>();
            CreateMap<CreateFuelOrderDto, FuelOrder>();

            CreateMap<FuelTokenRequest, CreateFuelTokenDto>();
            CreateMap<CreateFuelTokenDto, FuelTokenRequest>();
        }
    }
}

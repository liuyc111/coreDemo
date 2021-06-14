using AutoMapper;
using Core1._0.Dtos;
using Core1._0.Dtos.Request;
using Core1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core1._0.Profiles
{
    public class TouristRouteProfile : Profile
    {
        public TouristRouteProfile()
        {
            CreateMap<TouristRoute, TouristRouteDto>().ForMember(
                des => des.Price,
                src => src.MapFrom(src => src.OriginalPrice * (decimal)(src.DiscountPresent ?? 1))
                ).ForMember(
                des => des.TravelDays,
                src => src.MapFrom(src => src.TravelDays.ToString())
                ).ForMember(des => des.DepartureCity, src => src.MapFrom(src => src.Description.ToString()))
                ;

            CreateMap<TouristRouteForCreateDto, TouristRoute>().ForMember(
                des => des.Id,
                opt => opt.MapFrom(src => Guid.NewGuid())
                ).ForMember(des => des.TravelDays, opt => opt.MapFrom(src => src.TravelDays)).ForMember(des => des.TripType, opt => opt.MapFrom(src => src.TripType));

            CreateMap<TouristRouteForUpdateDto, TouristRoute>();
        }
    }
}

using AutoMapper;
using Core1._0.Dtos;
using Core1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core1._0.Profiles
{
    public class TouristRoutePictureProfile : Profile
    {
        public TouristRoutePictureProfile()
        {

            CreateMap<TouristRoutePicture, TouristRoutePictureDto>();
        }
    }
}

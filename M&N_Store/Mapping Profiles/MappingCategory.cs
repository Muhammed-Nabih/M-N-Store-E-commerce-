﻿using AutoMapper;
using M_N_Store.Dtos;
using N_Store.Domain.Entities;

namespace M_N_Store.Mapping_Profiles
{
    public class MappingCategory: Profile
    {
        public MappingCategory()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<ListingCategoryDto, Category>().ReverseMap();
        }
    }
}

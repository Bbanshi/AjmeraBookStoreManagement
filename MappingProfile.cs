using AjmeraBookStoreManagement.ApiRequestModels;
using AjmeraBookStoreManagement.ApiResponseModels;
using AutoMapper;
using BookStoreManagement.BusinessLayer;
using BookStoreManagement.Entities.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjmeraBookStoreManagement
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<AddBookRequest, AddBookModel>();
            CreateMap<AddBookModel, AddBookRequest>();


            CreateMap<BookAuthorResponse, BookAuthorModel>();
            CreateMap<BookAuthorModel, BookAuthorResponse>();

        }
    }
}

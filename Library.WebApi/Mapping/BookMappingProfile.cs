using AutoMapper;
using Library.WebApi.RestModels;
using Library.Models;

namespace Library.WebApi.Mapping
{
    public class BookMappingProfile: Profile
    {
        public BookMappingProfile()
        {
            CreateMap<BookCreateDto, Book>();
            CreateMap<BookUpdateDto, Book>();
        }
    }
}

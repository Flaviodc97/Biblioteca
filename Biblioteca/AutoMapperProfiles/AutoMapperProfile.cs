using AutoMapper;
using BibliotecaDAL.Entities;
using BibliotecaBLL.DTOs;

namespace Biblioteca.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, BookDTO>()
                .ReverseMap();
            CreateMap<Publisher, PublisherDTO>()
                .ReverseMap();
        }
    }
}

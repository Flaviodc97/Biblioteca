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

            CreateMap<Author, AuthorDTO>()
                .ReverseMap();

            CreateMap<Category, CategoryDTO>()
                .ReverseMap();

            CreateMap<Notification, NotificationDTO>()
                .ReverseMap();

            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.MembershipType, opt => opt.MapFrom(src => src.MembershipType.ToString()))
                .ForMember(dest => dest.MaxLoansAllowed, opt => opt.MapFrom(src => MappingHelper.GetMaxLoansAllowed(src.MembershipType)))
                .ReverseMap();
        }
    }
}

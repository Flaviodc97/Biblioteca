using AutoMapper;
using BibliotecaDAL.Entities;
using BibliotecaBLL.DTOs;
using BibliotecaBLL.DTOs.AuthorDTOS;

namespace Biblioteca.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Book
            CreateMap<Book, BookDTO>()
                .ReverseMap();

            // Publisher
            CreateMap<Publisher, PublisherDTO>()
                .ReverseMap();

            // Author
            CreateMap<Author, AuthorDTO>()
                .ReverseMap();


            CreateMap<Author, AuthorWithBooksDTO>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));

            // Category
            CreateMap<Category, CategoryDTO>()
                .ReverseMap();

            // Notification
            CreateMap<Notification, NotificationDTO>()
                .ReverseMap();

            // User
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.MembershipType, opt => opt.MapFrom(src => src.MembershipType.ToString()))
                .ForMember(dest => dest.MaxLoansAllowed, opt => opt.MapFrom(src => MappingHelper.GetMaxLoansAllowed(src.MembershipType)));

            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.MembershipType, opt => opt.MapFrom(src => Enum.Parse<MembershipType>(src.MembershipType)));

        }
    }
}

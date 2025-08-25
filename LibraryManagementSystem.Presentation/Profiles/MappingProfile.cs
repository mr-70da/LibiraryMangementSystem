
using AutoMapper;
using LibraryManagementSystem.Application.DTOs;

using LibraryManagementSystem.Domain.Entities;


namespace LibraryManagementSystem.Application.Profiles
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            // Entity -> ReadDto
            CreateMap<Author, AuthorReadDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}".Trim()));
                
            // CreateDto -> Entity
            CreateMap<AuthorCreateDto, Author>();



            CreateMap<UserBorrowingHistoryDto, BorrowingHistory>();
            CreateMap<BorrowingHistory, UserBorrowingHistoryDto>()
                .ForMember(dest => dest.BookTitle,
                    opt => opt.MapFrom(src => src.Book != null ? src.Book.Title : string.Empty))
                .ForMember(dest => dest.BorrowDate,
                    opt => opt.MapFrom(src => src.BorrowDate))
                .ForMember(dest => dest.ReturnDate,
                    opt => opt.MapFrom(src => src.ReturnDate));
            // Entity -> ReadDto
            CreateMap<Book, BookReadDto>();

            // Entity -> CreateDto
            CreateMap<Book, BookCreateDto>();
            CreateMap<Book, BookWithoutAuthorDto>();

            // CreateDto -> Entity
            CreateMap<BookCreateDto, Book>();
            // Entity -> ReadDto
            CreateMap<User, UserReadDto>().ForMember(d=>d.FullName, o=> o.MapFrom(src =>$"{src.FirstName} {src.LastName}"));
 
            // Entity -> ReadDto
            CreateMap<User,UserCreateDto>();
            // CreateDto -> Entity
            CreateMap<UserCreateDto, User>();
        }
    }
}

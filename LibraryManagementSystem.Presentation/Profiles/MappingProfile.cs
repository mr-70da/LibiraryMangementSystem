
using AutoMapper;
using LibraryManagementSystem.Application.DTOs;

using LibraryManagementSystem.Domain.Entities;


namespace LibraryManagementSystem.Application.Profiles
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            
            //Author
            CreateMap<Author, AuthorReadDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}".Trim()));
            CreateMap<AuthorCreateDto, Author>();
            CreateMap<Author, AuthorUpdateRequestDto>();
            CreateMap<AuthorUpdateRequestDto, Author>();



            //Borrowing History
            CreateMap<UserBorrowingHistoryDto, BorrowingHistory>();
            CreateMap<BorrowingHistory, UserBorrowingHistoryDto>()
                .ForMember(dest => dest.BookTitle,
                    opt => opt.MapFrom(src => src.Book != null ? src.Book.Title : string.Empty))
                .ForMember(dest => dest.BorrowDate,
                    opt => opt.MapFrom(src => src.BorrowDate))
                .ForMember(dest => dest.ReturnDate,
                    opt => opt.MapFrom(src => src.ReturnDate));
            CreateMap<BorrowRequestDto, BorrowingHistory>()
                .ForMember(dest => dest.BorrowDate,
                    opt => opt.MapFrom(src => DateOnly.FromDateTime(DateTime.Now)));

            //Book
            CreateMap<Book, BookReadDto>();
            CreateMap<Book, BookUpdateRequestDto>();
            CreateMap<BookUpdateRequestDto, Book>();
            CreateMap<Book, BookWithoutAuthorDto>();
            CreateMap<Book, BookCreateDto>();
            CreateMap<BookCreateDto, Book>();

            //User

            CreateMap<User,UserCreateDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserReadDto>().ForMember(d=>d.FullName, o=> o.MapFrom(src =>$"{src.FirstName} {src.LastName}"));
            CreateMap<RegisterRequestDto, User>();
            CreateMap<User, RegisterRequestDto>();
            CreateMap<LoginRequestDto, User>();
            CreateMap<User, LoginRequestDto>();
            CreateMap<LoginResponseDto, User>();
            CreateMap<User, LoginResponseDto>();



        }
    }
}

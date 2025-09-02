
using AutoMapper;
using LibraryManagementSystem.Application.Commands.Authentications;
using LibraryManagementSystem.Application.Commands.Authors;
using LibraryManagementSystem.Application.Commands.Books;
using LibraryManagementSystem.Application.Commands.Users;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Queries.Authors;
using LibraryManagementSystem.Application.Queries.Books;
using LibraryManagementSystem.Application.Queries.Users;
using LibraryManagementSystem.Domain.Entities;


namespace LibraryManagementSystem.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //Author
            CreateMap<Author, AuthorReadResponse>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}".Trim()));
            CreateMap<CreateAuthorCommand, Author>().ReverseMap();
            CreateMap<Author, UpdateAuthorCommand>().ReverseMap();
            CreateMap<Author, GetAuthorByIdQuery>().ReverseMap();




            //Borrowing History
            CreateMap<UserBorrowingHistoryResponse, BorrowingHistory>();
            CreateMap<BorrowingHistory, UserBorrowingHistoryResponse>()
                .ForMember(dest => dest.BookTitle,
                    opt => opt.MapFrom(src => src.Book != null ? src.Book.Title : string.Empty))
                .ForMember(dest => dest.BorrowDate,
                    opt => opt.MapFrom(src => src.BorrowDate))
                .ForMember(dest => dest.ReturnDate,
                    opt => opt.MapFrom(src => src.ReturnDate));
            CreateMap<BorrowBookCommand, BorrowingHistory>()
                .ForMember(dest => dest.BorrowDate,
                    opt => opt.MapFrom(src => DateOnly.FromDateTime(DateTime.Now)));

            //Book
            CreateMap<Book, CreateBookCommand>().ReverseMap();
            CreateMap<Book, UpdateBookCommand>().ReverseMap();
            CreateMap<Book, BorrowBookCommand>().ReverseMap();
            CreateMap<Book, DeleteBookCommand>().ReverseMap();
            CreateMap<Book, GetAllBooksQuery>().ReverseMap();
            CreateMap<Book, ReturnBookCommand>().ReverseMap();
            CreateMap<Book, GetBooksCountPerBranchQuery>().ReverseMap();
            CreateMap<Book, GetMostBorrowedBooksQuery>().ReverseMap();
           
            CreateMap<Book, BooksFilterResponse>().ReverseMap();
            CreateMap<Book, BooksPerBranchResponse>().ReverseMap();
            CreateMap<Book, BookReadResponse>().ReverseMap();
            CreateMap<Book, MostBorrowedBooksResponse>().ReverseMap();
            CreateMap<User, UserReadResponse>().ReverseMap();




            //User
            CreateMap<User,LoginResponse>().ReverseMap();
            CreateMap<RegisterCommand, User>().ReverseMap();
            CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<User, GetUserBorrowingHistoryQuery>().ReverseMap();
            



        }
    }
}
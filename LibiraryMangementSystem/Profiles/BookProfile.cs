using AutoMapper;
using LibraryManagementSystem.Dtos.Book;
using LibraryManagementSystem.Models;
namespace LibraryManagementSystem.Profiles
{
    public class BookProfile:Profile
    {
        public BookProfile()
        {
            // Entity -> ReadDto
            CreateMap<Book, BookReadDto>()
                .ForMember(dest => dest.AuthorNames,
                    opt => opt.MapFrom(src => src.Authors.Select(a => $"{a.FirstName} {a.LastName}".Trim()).ToList()));

            // CreateDto -> Entity
            CreateMap<BookCreateDto, Book>();
        }
    }
}

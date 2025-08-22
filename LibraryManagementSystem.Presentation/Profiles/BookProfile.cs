using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Entities;


namespace LibraryManagementSystem.Application.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            // Entity -> ReadDto
            CreateMap<Book, BookReadDto>()
                .ForMember(dest => dest.AuthorNames,
                    opt => opt.MapFrom(src => src.Authors.Select(a => $"{a.FirstName} {a.LastName}".Trim()).ToList()));

            // Entity -> CreateDto
            CreateMap<Book, BookCreateDto>();
            CreateMap<Book, BookWithoutAuthorDto>();

            // CreateDto -> Entity
            CreateMap<BookCreateDto, Book>();

        }
    }
}

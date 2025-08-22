
using AutoMapper;
using LibraryManagementSystem.Application.DTOs;

using LibraryManagementSystem.Domain.Entities;


namespace LibraryManagementSystem.Application.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            // Entity -> ReadDto
            CreateMap<Author, AuthorReadDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}".Trim()))
                .ForMember(dest => dest.BookTitles,
                    opt => opt.MapFrom(src => src.BookIsbns.Select(b => b.Title).ToList()));

            // CreateDto -> Entity
            CreateMap<AuthorCreateDto, Author>();
        }
    }
}

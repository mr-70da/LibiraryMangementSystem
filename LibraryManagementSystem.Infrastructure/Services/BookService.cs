using AutoMapper;
using LibraryManagementSystem.Infrastructure.UnitOfWork;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Interfaces.Services;
using LibraryManagementSystem.Domain.UnitOfWork;
using LibraryManagementSystem.Application.DTOs;

namespace LibraryManagementSystem.Application.Interfaces.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public void Create(BookCreateDto bookDto)
        {
            var newBook = _mapper.Map<Book>(bookDto);
            var author = _unitOfWork.Authors.GetById(bookDto.AuthorId);
            _unitOfWork.Books.Add(newBook);
            _unitOfWork.Complete();
        }

        public void Delete(int id)
        {
            var book = _unitOfWork.Books.GetById(id);
            _unitOfWork.Books.Remove(book);
            _unitOfWork.Complete();
        }
        
        public BooksByAuthorDto GetAllByAuthor(int authorId)
        {
            var books = _unitOfWork.Books.GetAllByAuthor(authorId);
                
            var bookDto = _mapper.Map<List<BookWithoutAuthorDto>>(books);
            var booksByAuthorDto = new BooksByAuthorDto(authorId, bookDto, bookDto.Count);    
            return booksByAuthorDto;
        }



        public void Update(int id , BookCreateDto newBookDto)
        {
            var newBook = _mapper.Map<Book>(newBookDto);
            newBook.Isbn = id;
            _unitOfWork.Books.Update( newBook);
            _unitOfWork.Complete(); 
        }

        public void UpdateStatus(int bookIsbn)
        {
            throw new NotImplementedException();
        }
        public List<BookReadDto> GetBooksCountPerBranch()
        {
            throw new NotImplementedException();
        }

        public List<BookReadDto> GetMostBorrowed(int listSize)
        {
            throw new NotImplementedException();
        }
        public void Return(int TransactionId)
        {
            throw new NotImplementedException();
        }
        public void Borrow(int UserId, int BookIsbn, int BranchId)
        {
            throw new NotImplementedException();
        }
    }
}

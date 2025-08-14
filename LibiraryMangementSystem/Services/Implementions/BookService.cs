using AutoMapper;
using LibraryManagementSystem.Dtos.Book;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.UnitOfWork;

namespace LibraryManagementSystem.Services
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

        public void Create(int autherId, BookCreateDto bookDto)
        {
            var newBook = _mapper.Map<Book>(bookDto);
            var author = _unitOfWork.Authors.Get(autherId);
            _unitOfWork.Books.Create(author,newBook);
            _unitOfWork.Complete();
        }

        public void Delete(int id)
        {
            var book = _unitOfWork.Books.Get(id);
            _unitOfWork.Books.Remove(book);
            _unitOfWork.Complete();
        }

        public IEnumerable<BookReadDto> GetAllByAuthor(int authorId)
        {
            var books = _unitOfWork.Books.GetByAuthor(authorId);
            return _mapper.Map<IEnumerable<BookReadDto>>(books);

        }

        public void Update(int id , BookCreateDto newBookDto)
        {
            var newBook = _mapper.Map<Book>(newBookDto);
            _unitOfWork.Books.Update(id, newBook);
            _unitOfWork.Complete(); 
        }
    }
}

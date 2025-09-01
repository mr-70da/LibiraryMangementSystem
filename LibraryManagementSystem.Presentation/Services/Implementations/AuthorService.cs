
using AutoMapper;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Services.Interface;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.UnitOfWork;
using System.Net;


namespace LibraryManagementSystem.Application.Services.Implementations
{
    public class AuthorService :IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        
        //Checked
        public async Task<GeneralResponse<AuthorReadDto>> CreateAsync(AuthorCreateDto dto)
        {
            try
            {
                GeneralResponse<AuthorReadDto> response;


                var author = _mapper.Map<Author>(dto);
                await _unitOfWork.Authors.AddAsync(author);
                await _unitOfWork.Complete();
                response = new GeneralResponse<AuthorReadDto>
                    (_mapper.Map<AuthorReadDto>(author),
                    true, "Author created successfully.", System.Net.HttpStatusCode.Created);
                return response;

            } catch(Exception ex)
            {
                throw new Exception("An error occurred while creating the author.", ex);
            } 
        }
        //Checked
        public async Task<GeneralResponse<AuthorReadDto>> DeleteAsync(int id)
        {
            GeneralResponse<AuthorReadDto> response;
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if (author == null)
            {
                response = new GeneralResponse<AuthorReadDto>
                   (null, false, "Author not found.", System.Net.HttpStatusCode.NotFound);
                
            }
            
            _unitOfWork.Authors.Remove(author);
            await _unitOfWork.Complete();
            return response = new GeneralResponse<AuthorReadDto>
                   (_mapper.Map<AuthorReadDto>(author),
                   true, "Author deleted successfully.", System.Net.HttpStatusCode.OK);

        }

        //Checked
        public async Task<GeneralResponse<AuthorReadDto>> GetAsync(int id)
        {
            try
            {
                GeneralResponse<AuthorReadDto> response;
                var author = await _unitOfWork.Authors.GetByIdAsync(id);
                if (author == null)
                {

                    response = 
                        new GeneralResponse<AuthorReadDto>
                        (null, false, "Author Not Found.", HttpStatusCode.BadRequest);
                    throw new KeyNotFoundException("Author not found");
                }
                response = new GeneralResponse<AuthorReadDto>
                    (_mapper.Map<AuthorReadDto>(author),
                    true, "Author retrieved successfully.", HttpStatusCode.OK);
                return response;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the author.", ex);
            }
        }
        //Checked
        public async Task<GeneralResponse<AuthorReadDto>> UpdateAsync(AuthorUpdateRequestDto updateAuthorDto)
        {
            try
            {
                GeneralResponse<AuthorReadDto> response;

                if (await _unitOfWork.Authors.GetByIdAsync(updateAuthorDto.Id) == null)
                {
                    response =
                        new GeneralResponse<AuthorReadDto>
                        (null, false, "Author Not Found.", HttpStatusCode.BadRequest);
                    
                }

                Author updatedAuthor = _mapper.Map<Author>(updateAuthorDto);

                _unitOfWork.Authors.Update(updatedAuthor);
                await _unitOfWork.Complete();
                response = new GeneralResponse<AuthorReadDto>
                    (_mapper.Map<AuthorReadDto>(updatedAuthor),
                    true, "Author updated successfully.", HttpStatusCode.OK);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the author.", ex);
            }
        }
    }
}

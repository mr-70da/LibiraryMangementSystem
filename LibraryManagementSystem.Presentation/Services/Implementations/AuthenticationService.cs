using AutoMapper;
using Azure.Core;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Services.Interface;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace LibraryManagementSystem.Application.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
    
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        
        public AuthenticationService(IUnitOfWork unitOfWork,
           IMapper mapper , IConfiguration configuration)
        {
            
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<GeneralResponse<LoginResponseDto>> Login(LoginRequestDto request)
        {
            try
            {
                GeneralResponse<LoginResponseDto> response;
                var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                {
                    response =
                        new GeneralResponse<LoginResponseDto>
                        (null, false, "Invalid email or password.", HttpStatusCode.Unauthorized);

                    return response;
                }

                var token = await GenerateToken(user);

                response = new GeneralResponse<LoginResponseDto>
                    (new LoginResponseDto
                    {
                        Token = token,
                        Expiration = DateTime.UtcNow.AddHours(1)
                    },
                    true, "Login successful.", HttpStatusCode.OK);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GeneralResponse<LoginResponseDto>> Register(RegisterRequestDto requestDto)
        {
            try
            {
                GeneralResponse<LoginResponseDto> response;
                var email = requestDto.Email.ToLower();
                var newUser = _mapper.Map<User>(requestDto);
                newUser.Email = email;
                var checkUser = await _unitOfWork.Users.GetByEmailAsync(email);
                if (checkUser != null)
                {
                    response =
                        new GeneralResponse<LoginResponseDto>
                        (null, false, "this email already exist.", HttpStatusCode.BadRequest);
                    throw new Exception("this email already exist.");
                }
                newUser.Password = BCrypt.Net.BCrypt.HashPassword(requestDto.Password);
                newUser.RegistrationDate = DateOnly.FromDateTime(DateTime.Now);

                await _unitOfWork.Users.AddAsync(newUser);
                await _unitOfWork.Complete();

                var token = await GenerateToken(newUser);
                response = new GeneralResponse<LoginResponseDto>
                    (new LoginResponseDto
                    {
                        Token = token,
                        Expiration = DateTime.UtcNow.AddHours(1)
                    },
                    true, "Register successful.", HttpStatusCode.OK);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        private async Task<string> GenerateToken(User user )
        {

            var role =  await _unitOfWork.Users.GetRoleName(user.RoleId);
       
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

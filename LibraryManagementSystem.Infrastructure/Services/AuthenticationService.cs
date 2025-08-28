using AutoMapper;
using Azure.Core;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Application.Services;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.UnitOfWork;
using LibraryManagementSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagementSystem.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;


        public AuthenticationService(IUnitOfWork unitOfWork,
            IJWTService jWTService, IMapper mapper , IConfiguration configuration)
        {
            _jwtService = jWTService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<LoginResponseDto> Login(LoginRequestDto request)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return null;
            }

            var token = await _jwtService.GenerateToken(user.Id);

            return new LoginResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1)
            };
        }

        public async Task<LoginResponseDto> Register(RegisterRequestDto requestDto)
        {
            var email = requestDto.Email.ToLower();
            var newUser = _mapper.Map<Domain.Entities.User>(requestDto);
            newUser.Email = email;
            var checkUser = await _unitOfWork.Users.GetByEmailAsync(email);
            if(checkUser  != null)
            {
                throw new Exception("this email already exist.");
            }
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(requestDto.Password);
            await _unitOfWork.Users.AddAsync(newUser);
            await _unitOfWork.Complete();
            Console.WriteLine(newUser.Id);
            //var token = await _jwtService.GenerateToken(newUser.Id);
            return new LoginResponseDto
            {
                Token = null,
                Expiration = DateTime.UtcNow.AddHours(1)
            };
        }

        public async Task<string> GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, role.Name)
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

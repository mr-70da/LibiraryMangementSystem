using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using LibraryManagementSystem.Application.Commands.Authentications;
using LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
//done
namespace LibraryManagementSystem.Application.Handlers.Authors
{
    public class LoginHandler : IRequestHandler<LoginCommand, GeneralResponse<LoginResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public LoginHandler(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(request.LoginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.LoginDto.Password, user.Password))
            {
                return new GeneralResponse<LoginResponseDto>(
                    null, false, "Invalid email or password.", HttpStatusCode.Unauthorized);
            }

            var token = await GenerateToken(user);

            return new GeneralResponse<LoginResponseDto>(
                new LoginResponseDto
                {
                    Token = token,
                    Expiration = DateTime.UtcNow.AddHours(1)
                },
                true, "Login successful.", HttpStatusCode.OK);
        }

        private async Task<string> GenerateToken(User user)
        {
            var role = await _unitOfWork.Users.GetRoleName(user.RoleId);

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

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
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
//done
namespace LibraryManagementSystem.Application.Handlers.Authentications
{
    public class LoginHandler : IRequestHandler<LoginCommand, GeneralResponse<LoginResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<LoginHandler> _logger;

        public LoginHandler(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper , ILogger<LoginHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GeneralResponse<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                _logger.LogWarning("Invalid login attempt for email: {Email}", request.Email);
                return new GeneralResponse<LoginResponse>(
                    null, false, "Invalid email or password.", HttpStatusCode.Unauthorized);
            }

            var token = await GenerateToken(user);
            _logger.LogInformation("User logged in successfully with email: {Email}", request.Email);
            return new GeneralResponse<LoginResponse>(
                new LoginResponse
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
            _logger.LogInformation("JWT token generated for user ID: {UserId}", user.Id);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

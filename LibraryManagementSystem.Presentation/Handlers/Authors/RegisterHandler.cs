using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
    public class RegisterHandler :
        IRequestHandler<RegisterCommand, GeneralResponse<LoginResponseDto>>
    {
        private readonly IConfiguration _configuration;
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        public RegisterHandler(IUnitOfWork unitOfWork,
            IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<GeneralResponse<LoginResponseDto>> 
            Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralResponse<LoginResponseDto> response;
                var email = request.RegisterDto.Email.ToLower();
                var newUser = _mapper.Map<User>(request.RegisterDto);
                newUser.Email = email;
                var checkUser = await _unitOfWork.Users.GetByEmailAsync(email);
                if (checkUser != null)
                {
                    response =
                        new GeneralResponse<LoginResponseDto>
                        (null, false, "this email already exist.", HttpStatusCode.BadRequest);
                    throw new Exception("this email already exist.");
                }
                newUser.Password = BCrypt.Net.BCrypt.HashPassword(request.RegisterDto.Password);
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

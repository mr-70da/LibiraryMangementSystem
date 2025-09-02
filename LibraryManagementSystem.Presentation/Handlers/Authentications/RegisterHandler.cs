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
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
//done
namespace LibraryManagementSystem.Application.Handlers.Authentications
{
    public class RegisterHandler :
        IRequestHandler<RegisterCommand, GeneralResponse<LoginResponse>>
    {
        private readonly IConfiguration _configuration;
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        readonly ILogger<RegisterHandler> _logger;
        public RegisterHandler(IUnitOfWork unitOfWork,
            IMapper mapper, IConfiguration configuration , ILogger<RegisterHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<GeneralResponse<LoginResponse>> 
            Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                GeneralResponse<LoginResponse> response;
                var email = request.Email.ToLower();
                var newUser = _mapper.Map<User>(request);
                newUser.Email = email;
                var checkUser = await _unitOfWork.Users.GetByEmailAsync(email);
                if (checkUser != null)
                {
                    _logger.LogWarning("Attempted to register with an existing email: {Email}", email);
                    response =
                        new GeneralResponse<LoginResponse>
                        (null, false, "this email already exist.", HttpStatusCode.BadRequest);
                    throw new Exception("this email already exist.");
                }
                newUser.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                newUser.RegistrationDate = DateOnly.FromDateTime(DateTime.Now);

                await _unitOfWork.Users.AddAsync(newUser);
                await _unitOfWork.Complete();

                var token = await GenerateToken(newUser);
                response = new GeneralResponse<LoginResponse>
                    (new LoginResponse
                    {
                        Token = token,
                        Expiration = DateTime.UtcNow.AddHours(1)
                    },
                    true, "Register successful.", HttpStatusCode.OK);
                _logger.LogInformation("User registered successfully with email: ", email);

                return response;
            }
            catch (Exception ex)
            {
                
                throw new Exception(("An error occurred during registration for email: ", request.Email) 
                    + ex.Message);
               
            }
        }
        private async Task<string> GenerateToken(User user)
        {
            try
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
            catch (Exception ex) {
                throw new Exception("Ocurrs"+ex.Message);
            }
        }
    }
}

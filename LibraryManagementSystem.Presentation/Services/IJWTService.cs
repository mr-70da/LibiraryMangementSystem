using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Services
{
    public interface IJWTService
    {
        string GenerateToken(User user);
    }
}

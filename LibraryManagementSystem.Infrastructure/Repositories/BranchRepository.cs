using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Interfaces.Repositories;
using LibraryManagementSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class BranchRepository: GenericRepository<LibraryBranch>, IBranchRepository
    {
        public BranchRepository(LibraryDbContext context) : base(context)
        {

        }
        public LibraryDbContext LibraryContext
        {
            get { return _context as LibraryDbContext; }
        }
    }
}

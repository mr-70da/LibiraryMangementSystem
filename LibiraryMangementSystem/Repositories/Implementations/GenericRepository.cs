using LibraryManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
namespace LibraryManagementSystem.Repositories.Implementation
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly LibraryContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public GenericRepository(LibraryContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }
        public IEnumerable<TEntity> GetAll()
        {
            
            return _dbSet.ToList();
        }

        public TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);

        }
        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}

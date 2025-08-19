using LibraryManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
namespace LibraryManagementSystem.Repositories.Implementation
{
    public class GenricRepository<TEntity> : IGenricRepository<TEntity> where TEntity : class
    {
        private readonly LibraryContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public GenricRepository(LibraryContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }
        public List<TEntity> GetAll(Object id)
        {
            
            return _dbSet.ToList();
        }

        public TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public void Remove(Object id)
        {
            TEntity entity = _dbSet.Find(id);
            _dbSet.Remove(entity);

        }

        public void Update(object id, TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}

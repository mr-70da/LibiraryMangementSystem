using LibraryManagementSystem.Application.Service;
using LibraryManagementSystem.Domain.Interfaces.Repositories;
using LibraryManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly LibraryDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly ICacheService _cache;
        private readonly string _cachePrefix;
        public GenericRepository(LibraryDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cache = cacheService;
            _dbSet = _context.Set<TEntity>();
            _cachePrefix = typeof(TEntity).Name.ToLower() + ":";
        }
        public async Task AddAsync(TEntity entity)
        {
            
            await _dbSet.AddAsync(entity);
            await _cache.RemoveAsync($"{_cachePrefix}all");
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var cacheKey = $"{_cachePrefix}all";
            var cached = await _cache.GetAsync<IEnumerable<TEntity>>(cacheKey);
            if (cached != null)
                return cached;
            var entities = await _dbSet.ToListAsync();
            await _cache.SetAsync(cacheKey, entities, TimeSpan.FromMinutes(10));

            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(object id)
        {
            var cacheKey = $"{_cachePrefix}{id}";
            var cached = await _cache.GetAsync<TEntity>(cacheKey);
            if(cached!= null) return cached;
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
                await _cache.SetAsync(cacheKey, entity, TimeSpan.FromMinutes(10));
            return entity;
        }

        public  void Remove(TEntity entity)
        {

            _dbSet.Remove(entity);
            _cache.RemoveAsync($"{_cachePrefix}all").Wait();

        }
        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _cache.RemoveAsync($"{_cachePrefix}all").Wait();
        }
    }
}

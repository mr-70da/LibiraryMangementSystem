
namespace LibraryManagementSystem.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        //create
        Task AddAsync(TEntity entity);
        
        //update
        void Update(TEntity entity);
        //read
        Task <TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetAllAsync();

        //delete
        void Remove(TEntity id);


    }
}

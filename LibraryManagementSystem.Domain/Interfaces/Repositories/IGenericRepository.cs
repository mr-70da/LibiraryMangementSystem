
namespace LibraryManagementSystem.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        //create
        void Add(TEntity entity);
        
        //update
        void Update(TEntity entity);
        //read
        TEntity GetById(object id);
        IEnumerable<TEntity> GetAll();

        //delete
        void Remove(TEntity id);


    }
}

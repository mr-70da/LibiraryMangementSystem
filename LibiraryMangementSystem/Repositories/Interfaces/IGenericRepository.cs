using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace LibraryManagementSystem.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        //create
        void Add(TEntity entity);
        
        //update
        void Update(TEntity entity);
        //read
        TEntity GetById(Object id);
        IEnumerable<TEntity> GetAll();

        //delete
        void Remove(TEntity id);


    }
}

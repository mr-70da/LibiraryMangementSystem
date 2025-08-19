using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace LibraryManagementSystem.Repositories
{
    public interface IGenricRepository<TEntity> where TEntity : class
    {
        //create
        void Add(TEntity entity);
        
        //update
        void Update(Object id, TEntity entity);
        //read
        TEntity GetById(Object id);
        List<TEntity> GetAll(Object id);

        //delete
        void Remove(Object id);


    }
}

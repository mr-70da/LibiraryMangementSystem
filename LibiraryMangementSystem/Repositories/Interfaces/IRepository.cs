using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace LibraryManagementSystem.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);


        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entity);


        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entity);


    }
}

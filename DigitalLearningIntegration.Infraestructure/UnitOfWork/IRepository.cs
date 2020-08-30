using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DigitalLearningIntegration.Infraestructure.UnitOfWork
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T GetByIdSingle(int id);
        void Delete(int id);
        void AddRange(IEnumerable<T> entity);
        void Commit();
        void ReActive(int id);
    }

}

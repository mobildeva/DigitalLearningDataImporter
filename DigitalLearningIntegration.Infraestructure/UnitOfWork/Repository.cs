using DigitalLearningDataImporter.DALstd;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;

namespace DigitalLearningIntegration.Infraestructure.UnitOfWork
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbContext _context;
        //private readonly IHttpContextAccessor _httpContextAccessor;


        //public Repository(IUnitOfWork unitOfWork, DataContext context)
        //{
        //    _unitOfWork = unitOfWork;
        //    _context = context;
        //}
        public Repository(DbContext context)
        {
            _unitOfWork = new UnitOfWork(context);
            _context = context;
        }

        public Repository()
        {

        }

        //public Repository(DataContext context, IHttpContextAccessor httpContextAccessor)
        //{
        //    _context = context;
        //    _unitOfWork = new UnitOfWork(context);
        //    _httpContextAccessor = httpContextAccessor;
        //}

        public void Add(T entity)
        {
            //var userSession = _httpContextAccessor.HttpContext.User.GetUserId();

            //if (entity is IAuditable ent)
            //{
            //    _context.Entry(entity).CurrentValues[nameof(IAuditable.CreatedBy)] = userSession;
            //    _context.Entry(entity).CurrentValues[nameof(IAuditable.CreatedDate)] = DateTime.Now;
            //}

            _context.Set<T>().Add(entity);
            _unitOfWork.Commit();
        }

        public void Delete(T entity)
        {
            T existing = _context.Set<T>().Find(entity);

            if (existing != null)
            {
                //    var userSession = _httpContextAccessor.HttpContext.User.GetUserId();

                //    if (entity is IAuditable Audit)
                //    {
                //        _context.Entry(entity).CurrentValues[nameof(IAuditable.UpdatedBy)] = userSession;
                //        _context.Entry(entity).CurrentValues[nameof(IAuditable.LastModifiedDate)] = DateTime.Now;
                //    }

                if (entity is IIsDeleted)
                {
                    _context.Entry(entity).CurrentValues[nameof(IIsDeleted.IsDeleted)] = true;

                    _context.Set<T>().Update(entity);
                    _unitOfWork.Commit();
                }
                else
                {
                    _context.Set<T>().Remove(existing);
                    _unitOfWork.Commit();
                }
            }
        }

        public void Delete(int id)
        {
            T existing = _context.Set<T>().Find(id);
            if (existing != null)
            {
                //var userSession = _httpContextAccessor.HttpContext.User.GetUserId();

                //if (existing is IAuditable Audit)
                //{
                //    _context.Entry(existing).CurrentValues[nameof(IAuditable.UpdatedBy)] = userSession;
                //    _context.Entry(existing).CurrentValues[nameof(IAuditable.LastModifiedDate)] = DateTime.Now;
                //}

                if (existing is IIsDeleted)
                {
                    _context.Entry(existing).CurrentValues[nameof(IIsDeleted.IsDeleted)] = true;

                    _context.Set<T>().Update(existing);
                    _unitOfWork.Commit();
                }
                else
                {
                    _context.Set<T>().Remove(existing);
                    _unitOfWork.Commit();
                }

                //_context.Set<T>().Remove(existing);
                //_unitOfWork.Commit();
            }
        }

        public IEnumerable<T> Get()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public T GetByIdSingle(int id)
        {
            var entity = _context.Set<T>().Find(id);

            if (!_context.Entry(entity).CurrentValues[nameof(IIsDeleted.IsDeleted)].Equals(true))
            {
                return entity;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).AsEnumerable<T>();
        }

        public IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return _context.Set<T>().Where(predicate).AsEnumerable();
            }
            else return _context.Set<T>().AsEnumerable();
        }
        public virtual IEnumerable<T> GetAll(int index, int count)
        {
            return _context.Set<T>().Skip(index).Take(count).AsEnumerable();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            //var userSession = _httpContextAccessor.HttpContext.User.GetUserId();

            //if (entity is IAuditable ent)
            //{
            //    _context.Entry(entity).CurrentValues[nameof(IAuditable.UpdatedBy)] = userSession;
            //    _context.Entry(entity).CurrentValues[nameof(IAuditable.LastModifiedDate)] = DateTime.Now;
            //}

            _context.Set<T>().Update(entity);
            _unitOfWork.Commit();
        }

        public abstract T GetById(int id);        
    }
}

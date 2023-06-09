﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TransactionAssignment.Data;

namespace TransactionAssignment.Services
{
    public interface ILazySingletonRepo<T> where T : class
    {
        Task<bool> IsExist(Expression<Func<T, bool>> where);
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> where);
        Task<int> InsertAsync(T entity);
    }

    public interface ISingletonRepo<T> where T : class
    {
        Task<bool> IsExist(Expression<Func<T, bool>> where);
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> where);
        Task<int> InsertAsync(T entity);
    }

    public interface IRepository<T> where T : class
    {
        Task<bool> IsExist(Expression<Func<T, bool>> where);
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> where);
        Task<int> InsertAsync(T entity);
    }

    public class LazySingletonRepo<T> : ILazySingletonRepo<T> where T : class
    {
        protected readonly TxnDbContext _txnDbContext;
        DbSet<T> entities;
        public LazySingletonRepo()
        {
            _txnDbContext = LazySingletonDBManager.Instance;
            entities = _txnDbContext.Set<T>();
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> where)
        {
            return await entities.Where(where).ToListAsync();
        }

        public async Task<int> InsertAsync(T entity)
        {
            await entities.AddAsync(entity);
            return await _txnDbContext.SaveChangesAsync();
        }

        public async Task<bool> IsExist(Expression<Func<T, bool>> where)
        {
            return await entities.Where(where).AnyAsync();
        }
    }

    public class SingletonRepo<T> : ISingletonRepo<T> where T : class
    {
        protected readonly TxnDbContext _txnDbContext;
        DbSet<T> entities;
        public SingletonRepo()
        {
            _txnDbContext = SingletonDbManager.Instance;
            entities = _txnDbContext.Set<T>();
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> where)
        {
            return await entities.Where(where).ToListAsync();
        }

        public async Task<int> InsertAsync(T entity)
        {
            await entities.AddAsync(entity);
            return await _txnDbContext.SaveChangesAsync();
        }

        public async Task<bool> IsExist(Expression<Func<T, bool>> where)
        {
            return await entities.Where(where).AnyAsync();
        }
    }

    public class Repository<T> : IRepository<T> where T: class
    {
        protected readonly TxnDbContext _txnDbContext;
        private DbSet<T> entities;

        public Repository(TxnDbContext txnDbContext)
        {
            _txnDbContext = txnDbContext;
            entities = _txnDbContext.Set<T>();
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> where)
        {
            return await entities.Where(where).ToListAsync();
        }

        public async Task<int> InsertAsync(T entity)
        {
            await entities.AddAsync(entity);
            return await _txnDbContext.SaveChangesAsync();
        }

        public async Task<bool> IsExist(Expression<Func<T, bool>> where)
        {
            return await entities.Where(where).AnyAsync();
        }
    }
}

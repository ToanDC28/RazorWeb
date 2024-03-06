﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Eyeglasses_DoanCongToan.Repo.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository()
        {
        }

        public DbSet<T> DbSet
        {
            get
            {
                return DbContext.Set<T>();
            }
        }

        public DbContext DbContext { get; set; }

        /// <summary>
        /// Add new entity
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            T existing = DbSet.Find(entity);
            if (existing != null)
                DbSet.Remove(existing);
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities"></param>
        public void DeleteRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(int id, bool allowTracking = true)
        {
            return DbSet.FirstOrDefault(c =>
            ((int)c.GetType().GetProperty("Id").GetValue(c) == id));
        }

        /// <summary>
        /// Get entity by lambda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T Get(Expression<Func<T, bool>> predicate, bool allowTracking = true)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Get list of entities
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll(bool allowTracking = true)
        {
            return DbSet.AsEnumerable();
        }

        /// <summary>
        /// Get entites by lambda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate, bool allowTracking = true)
        {
            return DbSet.Where(predicate).AsEnumerable();
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            DbSet.Attach(entity);
        }

        /// <summary>
        /// Get entities from sql string
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IEnumerable<T> FromSqlQuery(string sql, bool allowTracking = true)
        {
            if (allowTracking)
            {
                return DbSet.FromSqlRaw(sql).AsEnumerable();
            }

            return DbSet.FromSqlRaw(sql).AsNoTracking().AsEnumerable();
        }

        /// <summary>
        /// Get all entities async
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync(bool allowTracking = true)
        {
            var data = await DbSet.ToListAsync();
            return data;
        }

        /// <summary>
        /// Get entities by lambda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate,
            bool allowTracking = true)
        {
            var data = await DbSet.Where(predicate).ToListAsync();
            return data;
        }

        /// <summary>
        /// Get entity by id async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(int id, bool allowTracking = true)
        {
            var data = await DbSet.FirstOrDefaultAsync(c =>
            ((int)c.GetType().GetProperty("Id").GetValue(c) == id));

            return data;
        }

        /// <summary>
        /// Get entities by lambda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool allowTracking = true)
        {
            T data;

            if (allowTracking)
            {
                data = await DbSet.FirstOrDefaultAsync(predicate);
            }
            else
            {
                data = await DbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
            }
            return data;
        }

        /// <summary>
        /// Get entities from sql string async
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FromSqlQueryAsync(string sql, bool allowTracking = true)
        {
            IEnumerable<T> data;

            if (allowTracking)
            {
                data = await DbSet.FromSqlRaw(sql).ToListAsync();
            }
            else
            {
                data = await DbSet.FromSqlRaw(sql).AsNoTracking().ToListAsync();
            }
            return data;
        }

        // Updated Get method with pagination
        public IEnumerable<T> GetByPagination(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int? pageIndex = null, // Optional parameter for pagination (page number)
            int? pageSize = null)  // Optional parameter for pagination (number of records per page)
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            // Implementing pagination
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                // Ensure the pageIndex and pageSize are valid
                int validPageIndex = pageIndex.Value > 0 ? pageIndex.Value - 1 : 0;
                int validPageSize = pageSize.Value > 0 ? pageSize.Value : 10; // Assuming a default pageSize of 10 if an invalid value is passed

                query = query.Skip(validPageIndex * validPageSize).Take(validPageSize);
            }

            return query.ToList();
        }
    }
}
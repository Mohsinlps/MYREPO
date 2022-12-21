using BeajLearner.Infrastructure.Identity.Contexts;
using BeajLearner.Infrastructure.Identity.Repositories.Base;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeajLearner.Domain.Interfaces;
using BeajLearner.Application.Exceptions;
using BeajLearner.Domain.Entities;
using BeajLearner.Application.DTOs;
using BeajLearner.Domain.DomainDtos;

namespace BeajLearner.Infrastructure.Identity.Repositories
{
    public class EfRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly IdentityContext _dbContext;
        private DbSet<T> table;
        public EfRepository(IdentityContext dbContext)
        {
            _dbContext = dbContext;
            table = _dbContext.Set<T>();
        }

     
        //**************** Identity *****************

        public T Add(T entity)
        {
            table.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }
        public void Delete(int id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
            _dbContext.SaveChanges();
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public T GetById(int id)
        {
            return table.Find(id);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public T Update(T entity)
        {
            table.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }





        public IEnumerable<CourseCategory> GetCourseCategoryAllData(int courseCategoryId)
        {
            return null;
        }
        public IQueryable<GetCourseWithCategoryNameDto> GetCourseWithCategory()
        { return null; }

        public IQueryable<GetCourseWithCategoryNameDto> GetCourseWithCategoryById(int id) 
        {
            return null;
        }

        public IQueryable<GetLessonWithCourseAndCategoryDto> GetLessonWithCourseAndCategory() 
        {
            return null;
        }
        public int deleteBulk(List<T> entity) { return 0; }
        public IEnumerable<Lesson> getLessonWithMcqs(getMcqsDto getDto) { return null; }
        public IEnumerable<Lesson> GetLessonByCourseIdwithMcqs(int id) { return null; }

       // ********************************************************
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public T AddWithoutAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApiException("Oops.. Error Occurred during update.");
            }
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }

        public IEnumerable<T> ListAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public Task DeleteByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddRange(List<T> entilyList)
        {
            _dbContext.AddRange(entilyList);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteRange(List<T> entityList)
        {
            _dbContext.RemoveRange(entityList);
            return await _dbContext.SaveChangesAsync();
        }
        public IDbContextTransaction BeginTransaction()
        {
            return (_dbContext.Database.BeginTransaction());
        }

        /// <summary>
        /// CommitTransaction in change tracker to db
        /// </summary>
        public void CommitTransaction()
        {
            _dbContext.Database.CurrentTransaction.Commit();
        }

        /// <summary>
        /// Rollback changes tracker to db
        /// </summary>
        /// <returns></returns>
        public void RollbackTransaction()
        {
            _dbContext.Database.CurrentTransaction.Rollback();
        }
    }
}

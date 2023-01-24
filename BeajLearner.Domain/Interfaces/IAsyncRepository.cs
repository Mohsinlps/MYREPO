using BeajLearner.Application.DTOs;
using BeajLearner.Domain.DomainDtos;
using BeajLearner.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Domain.Interfaces
{
    public interface IAsyncRepository<T> where T : class
    {

        public IEnumerable<CourseCategory> GetCourseCategoryAllData(int courseCategoryId);
        public IQueryable<GetCourseWithCategoryNameDto> GetCourseWithCategory();
        public IQueryable<GetCourseWithCategoryNameDto> GetCourseWithCategoryById(int id);
        public IQueryable<GetLessonWithCourseAndCategoryDto> GetLessonWithCourseAndCategory();
        public IQueryable<GetLessonWithCourseAndCategoryDto> GetLessonWithCourseAndCategoryById(int id);

        public IEnumerable<Lesson> GetLessonByCourseIdwithMcqs(int id);
        public IEnumerable<Lesson> GetLessonByIdwithMcqs(int id);
        public IEnumerable<Lesson> getLessonWithMcqs(getMcqsDto getDto);
        public int deleteBulk(List<T> entity);
        public IEnumerable<Lesson> GetLessonByCourseIdAndActivity(int id, string activity);

        public T Add(T entity);


        public void Delete(int id);


        public IEnumerable<T> GetAll();


        public T GetById(int id);



        public void Save();



        public T Update(T entity);
      


















        Task<T> GetByIdAsync(Guid id);
        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> ListAllAsync();

        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize);

        Task<T> AddAsync(T entity);
        T AddWithoutAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<int> CountAsync(ISpecification<T> spec);

        Task<int> AddRange(List<T> entilyList);

        Task<int> DeleteRange(List<T> entityList);
        IDbContextTransaction BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}

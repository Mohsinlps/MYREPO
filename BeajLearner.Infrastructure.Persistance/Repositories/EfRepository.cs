using BeajLearner.Domain.Interfaces;
using BeajLearner.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeajLearner.Application.Exceptions;
using BeajLearner.Infrastructure.Persistance.Repositories.Base;
using BeajLearner.Domain.Entities;
using BeajLearner.Application.DTOs;
using BeajLearner.Domain.DomainDtos;
using EFCore.BulkExtensions;

namespace BeajLearner.Infrastructure.Persistance.Repositories
{
    public class EfRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<T> table;

        public EfRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            table= _dbContext.Set<T>();
        }
        // ********************
        //  DBRepository
        // *******************


        //---------------------------Course---------------------------------------
        public IEnumerable<CourseCategory> GetCourseCategoryAllData(int courseCategoryId)
        {
            IEnumerable<CourseCategory> data = _dbContext.CourseCategories.Where(x => x.CourseCategoryId == courseCategoryId).Include(x => x.Course).ThenInclude(x => x.lessons);
            return data;
        }

        public IQueryable< GetCourseWithCategoryNameDto> GetCourseWithCategory()
        {
         var result=   from c in _dbContext.Courses
            join cc in _dbContext.CourseCategories
                on c.CourseCategoryId equals cc.CourseCategoryId
            select new GetCourseWithCategoryNameDto
            {
               CourseId  = c.CourseId,
                CourseName =    c.CourseName,
                CourseCategoryName=    cc.CourseCategoryName,
                CourseCategoryId=cc.CourseCategoryId
            };

            return result;
            
        }
        //----------------------------------------------------------------
        public IQueryable<GetCourseWithCategoryNameDto> GetCourseWithCategoryById(int id)
        {
            var result = from c in _dbContext.Courses
                         join cc in _dbContext.CourseCategories
                             on c.CourseCategoryId equals cc.CourseCategoryId
                             where(cc.CourseCategoryId==id)
                         select new GetCourseWithCategoryNameDto
                         {
                             CourseId = c.CourseId,
                             CourseName = c.CourseName,
                             CourseCategoryName = cc.CourseCategoryName,
                             CourseCategoryId = cc.CourseCategoryId
                         };

            return result;

        }
        //-------------------------------Lesson --------------------------------

        public IQueryable<GetLessonWithCourseAndCategoryDto> GetLessonWithCourseAndCategory()
        {
            var result = from L in _dbContext.Lesson
                         join c in _dbContext.Courses
                         on L.courseId equals c.CourseId
                         join cc in _dbContext.CourseCategories
                         on c.CourseCategoryId equals cc.CourseCategoryId
                         select new GetLessonWithCourseAndCategoryDto
                         {
                             LessonId= L.LessonId,
                           //  week=L.week,
                             text=L.text,
                             //videos=L.videos,
                             //Audios=L.Audios,
                           //  image=L.image,
                             lessonType=L.lessonType,
                             dayNumber=L.dayNumber,
                             activity=L.activity,
                             activityAlias=L.activityAlias,
                             mcqquestion=L.mcqquestion,
                             courseId=c.CourseId,
                             CourseName=c.CourseName,
                             weekNumber=L.weekNumber,

                             CourseCategoryId=cc.CourseCategoryId,
                             CourseCategoryName=cc.CourseCategoryName
                         };
            return result;
        }

        public IQueryable<GetLessonWithCourseAndCategoryDto> GetLessonWithCourseAndCategoryById(int id)
        {
            var result = from L in _dbContext.Lesson
                         join c in _dbContext.Courses
                         on L.courseId equals c.CourseId
                         join cc in _dbContext.CourseCategories
                         on c.CourseCategoryId equals cc.CourseCategoryId
                         where L.LessonId==id
                         select new GetLessonWithCourseAndCategoryDto
                         {
                             LessonId = L.LessonId,
                             //  week=L.week,
                             text = L.text,
                             //videos = L.videos,
                             //Audios = L.Audios,
                           //  image = L.image,
                             lessonType = L.lessonType,
                             dayNumber = L.dayNumber,
                             activity = L.activity,
                             activityAlias = L.activityAlias,
                             mcqquestion = L.mcqquestion,
                             speakactivity=L.speakactivity,
                             documentFiles=L.documentfiles,
                             courseId = c.CourseId,
                             CourseName = c.CourseName,
                             weekNumber = L.weekNumber,

                             CourseCategoryId = cc.CourseCategoryId,
                             CourseCategoryName = cc.CourseCategoryName
                         };
            return result;
        }


        // get specific activity data by course Id
        public IEnumerable<Lesson> getLessonWithMcqs(getMcqsDto getDto)
        {
            IEnumerable<Lesson> model = _dbContext.Lesson.Where(x => x.courseId == getDto.courseId && x.activity == getDto.activity).Include(y=>y.mcqquestion).Include(z=>z.speakactivity).Include(j => j.documentfiles);
            return model;
        }
      
        public IEnumerable<Lesson> GetLessonByCourseIdAndWeekId(getMcqsDto getDto)
        {
            IEnumerable<Lesson> model = _dbContext.Lesson.Where(x => x.courseId == getDto.courseId && x.activity == getDto.activity).Include(y => y.mcqquestion).Include(z=>z.speakactivity).Include(j => j.documentfiles);
            return model;
        }

        //   get data with mcqs by  course Id
        //   also gets speak data
        public IEnumerable<Lesson> GetLessonByCourseIdwithMcqs(int id)
        {
            IEnumerable<Lesson> model = _dbContext.Lesson.Where(x => x.courseId ==id).Include(y => y.mcqquestion).Include(z => z.speakactivity).Include(j=>j.documentfiles);
            return model;
        }
        public IEnumerable<Lesson> GetLessonByCourseIdAndActivity(int id,string activity)
        {
            IEnumerable<Lesson> model = _dbContext.Lesson.Where(x => x.courseId == id && x.activity==activity).Include(y => y.mcqquestion).Include(z => z.speakactivity).Include(j => j.documentfiles);
            return model;
        }


        public IEnumerable<Lesson> GetLessonByIdwithMcqs(int id)
        {
            IEnumerable<Lesson> model = _dbContext.Lesson.Where(x => x.LessonId == id).Include(y => y.mcqquestion).Include(z=>z.speakactivity).Include(j => j.documentfiles);
            return model;
        }
        //public IEnumerable<DocumentFiles> GetDocumentFilesInfo(int id)
        //{
        //    IEnumerable<Lesson> model = _dbContext.documentFiles.Where(x=>x.).Include(y => y.mcqquestion).Include(z => z.speakactivity).Include(j => j.documentfiles);
        //    return model;
        //}


        //----------------------------------------------------------------


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

         public int deleteBulk(List<T> entity)
        {
            _dbContext.BulkDelete(entity);
            return 0;
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




        // -----------------------------------------------------------
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public virtual async Task<T> GetByIdAsync(Guid id)
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

                throw new ApiException(ex.Message.ToString());
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

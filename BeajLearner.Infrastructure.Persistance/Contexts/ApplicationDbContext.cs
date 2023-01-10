
using BeajLearner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Infrastructure.Persistance.Contexts
{
    public class ApplicationDbContext : DbContext
    {
      //  public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<Lesson> Lesson { get; set; }
      //  public DbSet<TeachersAssignedCourse> TeachersAssignedCourses { get; set;}
        public DbSet<mcqQuestions> mcqQuestions { get; set; }
        public DbSet<Otp> Otps { get; set; } 
        public DbSet<purchasedCourse> purchasedCourses { get; set; }
        public DbSet<CourseWeek> courseWeek { get; set; }
        public DbSet<ActivityAlias> activityAlias { get; set; }
        public DbSet<SpeakAnswer> speakAnswer { get; set; }
        public DbSet<SpeakQuestion> speakQuestion { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        
    }
}

//D:\VS projects\Beaj Learner LMS _ LPS\BeajLearnerUpdatdBranch    \   BeajLearner.sln

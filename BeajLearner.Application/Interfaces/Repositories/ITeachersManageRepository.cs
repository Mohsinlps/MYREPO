using BeajLearner.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.Interfaces.Repositories
{
    public interface ITeachersManageRepository
    {
        public void AssignCourse(AssignTeacherDto dto);
    }
}

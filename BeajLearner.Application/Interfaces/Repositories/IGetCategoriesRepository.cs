﻿using BeajLearner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.Interfaces.Repositories
{
    public interface IGetCategoriesRepository
    {
        public IEnumerable<CourseCategory> GetAllCategories();
    }
}

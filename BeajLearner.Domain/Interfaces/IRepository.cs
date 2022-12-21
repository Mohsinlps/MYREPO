using BeajLearner.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Domain.Interfaces
{
    public interface IRepository<T> where T : GuidBaseEntity
    {
        T GetById(int id);
        T GetById(Guid id);
        IEnumerable<T> ListAll();
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

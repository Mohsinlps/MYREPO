using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Domain.Entities.Base
{
    public class GuidBaseEntity
    {
        public virtual Guid Id { get; set; }
        //public string CreatedBy { get; set; }
        //public DateTime Created { get; set; }
        //public string LastModifiedBy { get; set; }
        //public DateTime? LastModified { get; set; }
    }
}

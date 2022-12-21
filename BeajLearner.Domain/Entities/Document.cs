using BeajLearner.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Domain.Entities
{
    public class Document : GuidBaseEntity
    {
        public string FileName { get; set; }
        public string Type { get; set; }
        public string File { get; set; }
    }
}

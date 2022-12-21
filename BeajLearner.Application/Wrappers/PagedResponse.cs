using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Application.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalRecord { get; set; }

        public PagedResponse(T Data, int PageNumber, int PageSize, int TotalRecord = 0)
        {
            this.pageNumber = PageNumber;
            this.pageSize = PageSize;
            this.totalRecord = TotalRecord;
            this.data = Data;
            this.message = null;
            this.succeeded = true;
            this.errors = null;
        }
    }
}

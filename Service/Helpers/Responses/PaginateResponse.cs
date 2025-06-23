using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.Responses
{
    public class PaginateResponse<T>
    {
        public List<T> Datas { get; set; }
        public int PageCount { get; set; }
    }
}

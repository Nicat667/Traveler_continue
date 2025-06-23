using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Comments
{
    public class CommentAdminVM
    {
        public string AuthorName { get; set; }
        public string Message { get; set; }
        public int Rate { get; set; }
        public DateTime Created { get; set; }
        public string Hotel { get; set; }
    }
}

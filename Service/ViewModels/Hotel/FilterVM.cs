using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Hotel
{
    public class FilterVM
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public List<int> StarCount { get; set; }
    }
}

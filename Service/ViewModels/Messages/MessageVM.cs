using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Messages
{
    public class MessageVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Time { get; set; }
        public bool IsRead { get; set; }
    }
}

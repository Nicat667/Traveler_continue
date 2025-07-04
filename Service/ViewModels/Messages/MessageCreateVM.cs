using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Messages
{
    public class MessageCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        [EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Text { get; set; }
    }
}

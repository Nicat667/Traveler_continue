using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Messages
{
    public class AnswerVM
    {
        [Required]
        public string Answer {  get; set; }
    }
}

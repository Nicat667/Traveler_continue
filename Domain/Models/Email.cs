using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Email : BaseEntity
    {
        public string EmailAddress { get; set; }   
    }
}

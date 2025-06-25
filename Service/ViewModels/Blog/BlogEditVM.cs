using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Blog
{
    public class BlogEditVM
    {
        [Required]
        public IFormFile Image { get; set; }
        public string ExistImage { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string ExistAuthorImage { get; set; }
        public IFormFile AuthorImage { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public bool IsVisible { get; set; }
    }
}

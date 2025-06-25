using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModels.Blog
{
    public class BlogCreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public IFormFile AuthorImage { get; set; }
        [Required]
        public string AuthorName { get; set; }
        public DateTime CreateDate { get; set; }
        [Required]
        public bool IsVisible { get; set; }
    }
}

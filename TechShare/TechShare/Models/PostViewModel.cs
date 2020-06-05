using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechShare.Models
{
    public class PostViewModel
    {                
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? SubmitedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public int Status { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public string[] CategoriesId { get; set; }
        public string[] CategoriesName { get; set; }
        public string DisplayCategories { get; set; }
    }
}

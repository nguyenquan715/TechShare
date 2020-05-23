using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechShare.Models
{
    public class PostModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public List<Guid> CategoriesID { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int Status { get; set; }

        public DateTime? SubmitedAt { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace TechShare.Entity
{
    public partial class Posts
    {
        public Posts()
        {
            PostCategories = new HashSet<PostCategories>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public int Status { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<PostCategories> PostCategories { get; set; }
    }
}

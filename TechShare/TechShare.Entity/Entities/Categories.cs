using System;
using System.Collections.Generic;

namespace TechShare.Entity
{
    public partial class Categories
    {
        public Categories()
        {
            PostCategories = new HashSet<PostCategories>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PostCategories> PostCategories { get; set; }
    }
}

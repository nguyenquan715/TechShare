using System;
using System.Collections.Generic;

namespace TechShare.Entity
{
    public partial class PostCategories
    {
        public Guid PostId { get; set; }
        public Guid CategoryId { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Posts Post { get; set; }
    }
}

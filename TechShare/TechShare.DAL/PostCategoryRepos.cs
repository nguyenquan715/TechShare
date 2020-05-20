using System;
using System.Collections.Generic;
using System.Text;
using TechShare.Entity;

namespace TechShare.DAL
{
    public class PostCategoryRepos:GenericRepository<PostCategories>,IPostCategoryRepos
    {
        public PostCategoryRepos(TechShareDBContext context) : base(context)
        {
        }
    }
}

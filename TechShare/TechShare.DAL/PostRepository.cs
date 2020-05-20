using System;
using System.Collections.Generic;
using System.Text;
using TechShare.Entity;

namespace TechShare.DAL
{
    public class PostRepository:GenericRepository<Posts>, IPostRepository
    {
        public PostRepository(TechShareDBContext context) : base(context)
        {

        }
    }
}

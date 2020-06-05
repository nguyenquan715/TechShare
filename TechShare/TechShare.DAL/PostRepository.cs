using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechShare.Entity;

namespace TechShare.DAL
{
    public class PostRepository:GenericRepository<Posts>, IPostRepository
    {
        public PostRepository(TechShareDBContext context) : base(context)
        {

        }

        public int CheckPostOfUser(Guid postId, string userId)
        {
            var result=_context.Posts.FirstOrDefault(post => post.Id == postId&&post.UserId==userId );
            if (result != null) return 1;
            return 0;
        }
    }
}

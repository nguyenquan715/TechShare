using System;
using System.Collections.Generic;
using System.Text;
using TechShare.Entity;

namespace TechShare.DAL
{
    public interface IPostRepository
    {
        int CheckPostOfUser(Guid postId, string userId);
    }
}

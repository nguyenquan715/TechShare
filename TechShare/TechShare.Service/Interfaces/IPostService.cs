using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using TechShare.Entity;

namespace TechShare.Service
{
    public interface IPostService
    {
        IEnumerable<Categories> GetAllCategories();
        void CreateNewPost(Posts post, List<PostCategories> categories);
        Categories FindCategoryById(Guid id);
        IEnumerable<object> GetListPostsOfUser(string userId,int begin,int end);
        IEnumerable<object> GetPostOfUser(Guid postId);
        IEnumerable<object> GetListPostsPublished(int begin, int end);
        void DeletePost(Guid postId);
        int CheckPostOfUser(Guid postId, string userId);
        int NumberOfPublishedPosts();
        IEnumerable<object> GetPublishedPostsByTitle(string keyword);
        IEnumerable<object> GetPostsOfAuthor(string userId);
    }
}

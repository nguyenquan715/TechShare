using System;
using System.Collections.Generic;
using System.Text;
using TechShare.Entity;

namespace TechShare.Service
{
    public interface IPostService
    {
        IEnumerable<Categories> GetAllCategories();
        void CreateNewPost(Posts post, List<PostCategories> categories);
        Categories FindCategoryById(Guid id);
    }
}

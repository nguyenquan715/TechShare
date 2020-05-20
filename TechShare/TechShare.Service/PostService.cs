using System;
using System.Collections.Generic;
using System.Text;
using TechShare.Entity;
using TechShare.DAL;

namespace TechShare.Service
{
    public class PostService : BaseService, IPostService
    {
        private UnitOfWork _uow;
        public PostService(IUnitOfWork uow)
        {
            _uow = uow as UnitOfWork;
        }

        /*Lấy về thông tin toàn bộ các thể loại*/
        public IEnumerable<Categories> GetAllCategories()
        {
            return _uow.CategoryRepository.GetAll();
        }

        /*Tạo bài viết mới*/
        public void CreateNewPost(Posts post, List<PostCategories> categories)
        {
            try
            {
                _uow.CreateTransaction();
                _uow.PostRepository.Insert(post);
                foreach(var category in categories)
                {
                    _uow.PostCategoryRepos.Insert(category);
                }
                _uow.Save();
                _uow.Commit();
            }
            catch(Exception ex)
            {
                _uow.Rollback();
                throw new Exception(ex.Message, ex);
            }
        }

        public Categories FindCategoryById(Guid id)
        {
            return _uow.CategoryRepository.GetByID(id);
        }
    }
}

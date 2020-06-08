using System;
using System.Collections.Generic;
using System.Text;
using TechShare.Entity;
using TechShare.DAL;
using System.Dynamic;
using System.Threading.Tasks;

namespace TechShare.Service
{
    public class PostService : BaseService, IPostService
    {
        private UnitOfWork _uow;
        private IBaseDAL _baseDAL;
        public PostService(IUnitOfWork uow,IBaseDAL baseDAL)
        {
            _uow = uow as UnitOfWork;
            _baseDAL = baseDAL;
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

        /*Tìm thể loại theo id*/
        public Categories FindCategoryById(Guid id)
        {
            return _uow.CategoryRepository.GetByID(id);
        }

        /*Lấy ra danh sách các bài viết theo user*/
        public IEnumerable<object> GetListPostsOfUser(string userId,int begin,int end)
        {
            try
            {
                List<object> result = new List<object>();
                result.Add(_baseDAL.ExecuteReader("Proc_GetListPostsOfUser", new { UserId = userId, Begin=begin, End=end }));
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /*Lấy ra 1 bài viết của user*/
        public IEnumerable<object> GetPostOfUser(Guid postId)
        {
            try
            {
                List<object> result = new List<object>();
                result.Add(_baseDAL.ExecuteReader("Proc_GetPostOfUser", new { PostId = postId }));
                return result;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        
        /*Lấy ra danh sách bài viết đã đăng*/
        public IEnumerable<object> GetListPostsPublished(int begin,int end)
        {
            try
            {
                List<object> result = new List<object>();
                result.Add(_baseDAL.ExecuteReader("Proc_GetListPostsPublished", new { Begin = begin, End = end }));
                return result;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        /*Xóa bài viết đã lưu*/
        public void DeletePost(Guid postId)
        {
            try
            {
                _uow.PostRepository.Delete(postId);
                _uow.Save();
                //Xóa bài viết thì các bản ghi liên quan đến bài viết đó trong PostCategories cũng tự động được xóa
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        /*Check xem bài viết của phải của user hay không*/
        public int CheckPostOfUser(Guid postId, string userId)
        {
            return _uow.PostRepository.CheckPostOfUser(postId, userId);
        }

        /*Xem số lượng bài viết đã đăng*/
        public int NumberOfPublishedPosts()
        {
            return _uow.PostRepository.NumberOfPublishedPosts();
        }

        public IEnumerable<object> GetPublishedPostsByTitle(string keyword)
        {
            try
            {
                List<object> res = new List<object>();
                res.Add(_baseDAL.ExecuteReader("Proc_GetPostsByTitle", new { keyword = $"%{keyword}%" }));
                return res;
            }
            catch(Exception ex)
            {
                throw ex;
            }            
        }

        public IEnumerable<object> GetPostsOfAuthor(string userId)
        {
            try
            {
                List<object> res = new List<object>();
                res.Add(_baseDAL.ExecuteReader("Proc_GetPublishedPostsOfUser", new { UserId=userId}));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

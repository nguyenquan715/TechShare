using System;
using System.Collections.Generic;
using System.Text;
using TechShare.Entity;

namespace TechShare.DAL
{
    public class CategoryRepository : GenericRepository<Categories>,ICategoryRepository
    {        
        public CategoryRepository(TechShareDBContext context) : base(context)
        {           
        }
    }
}

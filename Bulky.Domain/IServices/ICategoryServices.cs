using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Domain.IServices
{
    public interface ICategoryServices
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int? id);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int? id);
        
    }
}

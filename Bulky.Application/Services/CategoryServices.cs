using Bulky.Domain;
using Bulky.Domain.IRepository;
using Bulky.Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Application.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddCategory(Category category)
        {
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
        }

        public void DeleteCategory(int? id)
        {
            var category = GetCategoryById(id);
            if (category != null)
            {
                _unitOfWork.Category.Remove(category);
            }
            _unitOfWork.Save();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _unitOfWork.Category.GetAll().ToList();
        }

        public Category GetCategoryById(int? id)
        {
            return _unitOfWork.Category.Get(c => c.Id == id.GetValueOrDefault());
        }

        public void UpdateCategory(Category category)
        {
            _unitOfWork.Category.Update(category);
            _unitOfWork.Save();

        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Domain.IServices
{
    public interface IProductServices
    {
        IEnumerable<SelectListItem> GetAllCategories();
        IEnumerable <Product> GetAllProducts();
        Product GetProductById(int id);
        void UpsertProduct(Product product, IFormFile? file);
        bool DeleteProductById(int id);
    }
}

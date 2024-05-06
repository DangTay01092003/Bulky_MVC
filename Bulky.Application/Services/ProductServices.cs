using Bulky.Domain;
using Bulky.Domain.IRepository;
using Bulky.Domain.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Application.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ProductServices(IUnitOfWork unitOfWork, IHostingEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

      

        public IEnumerable<SelectListItem> GetAllCategories()
        {
            return _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()

            });

        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _unitOfWork.Product.GetAll(includeProperties: "Category");
        }

        public Product GetProductById(int id)
        {
            return _unitOfWork.Product.Get(u => u.Id == id);
        }

        public void UpsertProduct(Product product, IFormFile? file)
        {
            string wwwRootPath = _hostingEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\product");

                if (!string.IsNullOrEmpty(product.ProductImages))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, product.ProductImages.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                product.ProductImages = @"\images\product\" + fileName;
            }

            if (product.Id == 0)
            {
                _unitOfWork.Product.Add(product);
            }
            else
            {
                _unitOfWork.Product.Update(product);
            }

            _unitOfWork.Save();
        }
        public bool DeleteProductById(int id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return false;
            }

            string productPath = @"images\products\product-" + id;
            string finalPath = Path.Combine(_hostingEnvironment.WebRootPath, productPath);

            if (Directory.Exists(finalPath))
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach (string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }

                Directory.Delete(finalPath);
            }

            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

            return true;
        }

        
    }
    }



using Bulky.Domain;
using Bulky.Domain.ViewModel;
using Bulky.Domain.IRepository;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bulky.Domain.IServices;
using Bulky.Utility;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IProductServices _productService;


        public ProductController(IProductServices productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
          var products = _productService.GetAllProducts();
            return View(products);
        }

        public IActionResult Upsert(int? id)
        {
            var productVM = new ProductVM
            {
                CategoryList = _productService.GetAllCategories(),
                Product = id == null ? new Product() : _productService.GetProductById((int)id)
            };

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                _productService.UpsertProduct(productVM.Product, file);
                TempData["success"] = "Product created/updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
               productVM.CategoryList = _productService.GetAllCategories();
            return View(productVM);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            var objProductList = _productService.GetAllProducts().ToList();
            return Json(new { data = objProductList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var deleteResult = _productService.DeleteProductById((int)id);
            if (!deleteResult)
            {
                return NotFound(new { success = false, message = "Error while deleting" });
            }

            return Ok(new { success = true, message = "Delete Successful" });
        }
    }
}
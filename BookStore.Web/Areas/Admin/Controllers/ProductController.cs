using BookStore.DataAccess.IRepositories;
using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment webHostEnviroment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnviroment)
        {
            _unitOfWork = unitOfWork;
            this.webHostEnviroment = webHostEnviroment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? productId)
        {
            ProductViewModel productViewModel = new ProductViewModel
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    }
                ),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(x =>
                new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
            };
            //Product product = new Product();
            //ViewBag.CategoryList = _unitOfWork.Category.GetAll().Select(x => 
            //    new SelectListItem 
            //    { 
            //        Value = x.Id.ToString(),
            //        Text = x.Name
            //    }
            //);
            //ViewData["CoverTypeList"] = _unitOfWork.CoverType.GetAll().Select(x =>
            //    new SelectListItem
            //    {
            //        Value = x.Id.ToString(),
            //        Text = x.Name
            //    }
            //);

            if (productId == null || productId == 0)
            {
                return View(productViewModel);
            }
            else 
            {
                return View();
            }
        }
        
        [HttpPost]
        public IActionResult Upsert(ProductViewModel productViewModel, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                var webRootPath = this.webHostEnviroment.WebRootPath;
                if (file != null)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var uploadFilesPath = Path.Combine(webRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    using (var fileStream = new FileStream(Path.Combine(uploadFilesPath, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                        productViewModel.Product.ImageURL = @"images\products\" + fileName + extension;
                    }
                }
                _unitOfWork.Product.Add(productViewModel.Product);
                _unitOfWork.Save();
                TempData["success"] = "Producto agregado exitosamente!!!";
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }

        public IActionResult Delete(int id)
        {
            return View();
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            return NotFound();
        }

        [HttpPost]
        public IActionResult Create(CoverType coverType)
        {
            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll();
            return Json(new 
            {
                data = productList
            });
        }
        #endregion
    }
}

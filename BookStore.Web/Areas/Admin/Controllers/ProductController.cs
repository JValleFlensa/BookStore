using BookStore.DataAccess.IRepositories;
using BookStore.Models;
using BookStore.Models.ViewModels;
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
                Product product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == productId)!;
                if (product != null)
                { 
                    productViewModel.Product = product;
                    return View(productViewModel);
                }
                return NotFound();
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

                    if (productViewModel.Product.ImageURL != null)
                    {
                        var oldImagePath = Path.Combine(webRootPath, productViewModel.Product.ImageURL.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(uploadFilesPath, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                        productViewModel.Product.ImageURL = @"images\products\" + fileName + extension;
                    }
                }

                if (productViewModel.Product.Id != 0)
                {
                    _unitOfWork.Product.Update(productViewModel.Product);
                    TempData["success"] = "Producto actualizado exitosamente!!!";
                }
                else 
                {
                    _unitOfWork.Product.Add(productViewModel.Product);
                    TempData["success"] = "Producto agregado exitosamente!!!";
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll("Category,CoverType");
            return Json(new 
            {
                data = productList
            });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var webRootPath = this.webHostEnviroment.WebRootPath;
            Product productFromDB = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id)!;
            if (productFromDB != null) 
            {
                if (productFromDB.ImageURL != null)
                {
                    var oldImagePath = Path.Combine(webRootPath, productFromDB.ImageURL.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                _unitOfWork.Product.Remove(productFromDB);
                _unitOfWork.Save();
                return Json(new
                {
                    sucess = true,
                    message = "Producto eliminado correctamente"
                });
            }
            return Json(new 
            {
                sucess = false, 
                message = "Ocurrió un error al eliminar"
            });
        }
        #endregion
    }
}

//using BookStore.DataAccess;
using BookStore.DataAccess.IRepositories;
//using BookStore.DataAccess.Repositories;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly ApplicationDbContext _context;
        //private readonly ICategoryRepository _categoryRepository;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            //_categoryRepository = new CategoryRepository(context);
            this._unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //IEnumerable<Category> categoryList = _categoryRepository.GetAll();
            IEnumerable<Category> categoryList = _unitOfWork.Category.GetAll();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "El nombre de la categoría no puede ser el mismo al del orden!");
            }

            if (ModelState.IsValid)
            {
                //_categoryRepository.Add(category);
                //_categoryRepository.Save();
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "Categoria creada satisfactoriamente";

                return RedirectToAction(nameof(Index));

            }
            return View(category);
        }

        public IActionResult Edit(int id)
        {
            //Category category = _categoryRepository.GetFirstOrDefault(c => c.Id == id)!;
            Category category = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id)!;
            if (category == null)
            {
                return NotFound();
                //return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "El nombre de la categoría no puede ser el mismo al del orden!");
            }

            if (ModelState.IsValid)
            {
                //Category updatedCategory = _context.Categories.FirstOrDefault(c => c.Id == category.Id)!;
                //if (updatedCategory != null)
                //{
                //    updatedCategory.Name = category.Name;
                //    updatedCategory.DisplayOrder = category.DisplayOrder;
                //    _context.Categories.Update(updatedCategory);
                //    _context.SaveChanges();
                //    return RedirectToAction(nameof(Index));
                //}


                if (category != null && category.Id != 0)
                {
                    //_categoryRepository.Update(category);
                    //_categoryRepository.Save();
                    _unitOfWork.Category.Update(category);
                    _unitOfWork.Save();

                    TempData["success"] = "Categoria modificada satisfactoriamente";

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            //Category category = _categoryRepository.GetFirstOrDefault(c => c.Id == id)!;
            Category category = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id)!;
            if (category == null)
            {
                return NotFound();
                //return RedirectToAction(nameof(Index));
            }
            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {

            //Category category = _categoryRepository.GetFirstOrDefault(c => c.Id == id)!;
            Category category = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id)!;
            if (category != null)
            {
                //_categoryRepository.Remove(category);
                //_categoryRepository.Save();
                _unitOfWork.Category.Remove(category);
                _unitOfWork.Save();
                TempData["success"] = "Categoria eliminada satisfactoriamente";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
    }
}

using BookStore.DataAccess.IRepositories;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> coverTypeList = _unitOfWork.CoverType.GetAll();
            return View(coverTypeList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(coverType);
                _unitOfWork.Save();
                TempData["success"] = "Tipo de cubierta creada satisfactoriamente";
                return RedirectToAction(nameof(Index));
            }
            return View(coverType);
        }

        public IActionResult Edit(int id)
        {
            CoverType coverType = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id)!;
            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        [HttpPost]
        public IActionResult Edit(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                if (coverType != null && coverType.Id != 0)
                {
                    _unitOfWork.CoverType.Update(coverType);
                    _unitOfWork.Save();
                    TempData["success"] = "Tipo de cubierta modificada satisfactoriamente";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(coverType);
        }

        public IActionResult Delete(int id)
        {
            CoverType coverType = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id)!;
            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            CoverType category = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id)!;
            if (category != null)
            {
                _unitOfWork.CoverType.Remove(category);
                _unitOfWork.Save();
                TempData["success"] = "Tipo de cubierta eliminada satisfactoriamente";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
    }
}

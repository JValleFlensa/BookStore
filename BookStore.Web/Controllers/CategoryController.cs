﻿using BookStore.Web.Data;
using BookStore.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context) { 
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _context.Categories.ToList();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString()) {
                ModelState.AddModelError("Name", "El nombre de la categoría no puede ser el mismo al del orden!");
            }

            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(category);
        }

        public IActionResult Edit(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id)!;
            if (category == null) 
            {
                return RedirectToAction(nameof(Index));
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
                Category updatedCategory = _context.Categories.FirstOrDefault(c => c.Id == category.Id)!;
                if (updatedCategory != null)
                {
                    updatedCategory.Name = category.Name;
                    updatedCategory.DisplayOrder = category.DisplayOrder;
                    _context.Categories.Update(updatedCategory);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(category);
        }
    }
}
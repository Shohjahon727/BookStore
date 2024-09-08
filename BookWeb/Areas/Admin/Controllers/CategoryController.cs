using Book.DataAccess.Repository.IRepository;
using Book.Models.Entities;
using Book.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace BookWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    [Authorize(Roles = SD.Role_Admin)]
    public IActionResult Index()
    {
        List<Category> objCategory = _unitOfWork.Category.GetAll().ToList();
        return View(objCategory);
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
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction("Index");
        }

        return View();
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
        //Category? categoryFromDb1 = _context.Categories.FirstOrDefault(i => i.Id == id);
        //Category? categoryFromDb2 = _context.Categories.Where(u => u.Id == id).FirstOrDefault();

        if (categoryFromDb == null)
        {
            return NotFound();
        }
        return View(categoryFromDb);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {

        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Update(category);
            _unitOfWork.Save();
            TempData["success"] = "Category Update Successfully";
            return RedirectToAction("Index");
        }

        return View();
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);

        if (categoryFromDb == null)
        {
            return NotFound();
        }
        return View(categoryFromDb);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Category? category = _unitOfWork.Category.Get(u => u.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        _unitOfWork.Category.Remove(category);
        _unitOfWork.Save();
        TempData["success"] = "Category Delete Successfully";
        return RedirectToAction("Index");
    }
}

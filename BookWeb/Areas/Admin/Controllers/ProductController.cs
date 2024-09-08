using Book.DataAccess.Repository.IRepository;
using Book.Models.Entities;
using Book.Models.ViewModels;
using Book.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BookWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }
    public IActionResult Index()
    {
        List<Product> objProduct = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
       
        return View(objProduct);
    }

    public IActionResult Upsert(int? id)
    {
        
        ProductVM productVM = new()
        {
            CategoryList = _unitOfWork.Category
			.GetAll().Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = u.Id.ToString()
			}),
            Product = new Product()
        };
        if(id == null || id == 0)
        {
            //create
        return View(productVM);
        }
        else
        {
            //update
            productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
            return View(productVM);
        }
    }

    [HttpPost]
    public IActionResult Upsert(ProductVM productVM, IFormFile? file)
    {
       
        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file!= null) 
            { 
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\product");

                if(!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                {
                    //delete old image
                    var oldImagePath = 
                        Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                    if(System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                productVM.Product.ImageUrl = @"\images\product\" + fileName;
            }
            if(productVM.Product.Id==0)
            {
                _unitOfWork.Product.Add(productVM.Product);
            }
            else
            {
                _unitOfWork.Product.Update(productVM.Product);
            }

            _unitOfWork.Save();
            TempData["success"] = "Product Created Successfully";
            return RedirectToAction("Index");
        }
        else
        {
			productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
			return View(productVM);
		}
       
    }


    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Product> objProduct = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        return Json(new {data = objProduct});
    }

    [HttpDelete]
	public IActionResult Delete(int? id)
	{
        var obj = _unitOfWork.Product.Get(u => u.Id == id);
        if(obj == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

		//delete old image
		var oldImagePath =
			Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));

		if (System.IO.File.Exists(oldImagePath))
		{
			System.IO.File.Delete(oldImagePath);
		}

        _unitOfWork.Product.Remove(obj);
        _unitOfWork.Save();
		return Json(new {seccess = true, message = "Delete Successful" });
	}
	#endregion
}

using HaneenStore.Data;
using HaneenStore.Models;
using HaneenStore.ViewModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace HaneenStore.Controllers
{
    public class CategoriesController : Controller
    {
        ApplicationDbContext context;
        public CategoriesController(ApplicationDbContext context
            )
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var categories = context.Categories.ToList();
            var category = categories.Select(category => new CategoryMV
            {
                Id = category.Id,
                Name=category.Name,
                UpdatedOn=category.UpdatedOn,
                CreatedOn=category.CreatedOn,
                
            }).ToList();

            return View("index", categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public IActionResult Create(CategoryMV categoryVM)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = categoryVM.Name
                };
                try
                {
                    context.Categories.Add(category);
                    context.SaveChanges();
                    return RedirectToAction("index");

                }
                catch
                {
                    ModelState.AddModelError("Name", "Category Name Already Exists");
                    return View(categoryVM);

                }

                //return Content(category.Name);
            }
            else
            {
                return View("create", categoryVM);
            }
        
        }
        [HttpGet]
      public IActionResult Edit(int id)
        {
           
            // return Content($"{id}");
            var category = context.Categories.Find(id);
            if (category is null)
            {
                return NotFound();
            }
            else
            {
                var viewmodel = new CategoryMV { Id = id, Name = category.Name };
                return View("Create", viewmodel);
            }
        }

        [HttpPost]
        public IActionResult Edit(int id ,CategoryMV categoryVm)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", categoryVm);
            }
            var category = context.Categories.Find(categoryVm.Id);
            if(category is null)
            {
                return NotFound();
            }

         
            category.Name = categoryVm.Name;
            category.UpdatedOn = DateTime.Now;
            context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Details(int id)
        {
            var category = context.Categories.Find(id);
            if (category is null)
            {
                return NotFound();
            }
            else {
                var viewModle = new CategoryMV { Id = id, Name = category.Name };
                return View("Details", viewModle);
            
            }

        }
        public IActionResult Remove(int id)
        {
            var category = context.Categories.Find(id);
            if (category is null)
            {
                return NotFound();
            }
            context.Categories.Remove(category);
            context.SaveChanges();
            return Ok();


        }
        public IActionResult checkName(CategoryMV categorymv)
        {
            var isExsits = context.Categories.Any(category => category.Name == categorymv.Name
                );

            return Json(!isExsits);
        }
    }

   
}

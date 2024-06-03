using HaneenStore.Data;
using Microsoft.AspNetCore.Mvc;
using HaneenStore.ViewModel;
using HaneenStore.Models;
using Microsoft.AspNetCore.Http.HttpResults;
namespace HaneenStore.Controllers
{
    public class AutherController : Controller
    {
        ApplicationDbContext context;

        public AutherController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var authers = context.Authers.ToList();
            var authersVm = authers.Select(auther => new AutherVm
            {
                Id = auther.Id,
                AutherName = auther.AutherName,
                CreatedOn = auther.CreatedOn,
                UpdatedOn = auther.UpdatedOn,

            }).ToList();
           

            return View("index",authersVm);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("AutherForm");
        }
        [HttpPost]
        public IActionResult Create(AutherFormModel autherformmodel)
        {
            if (ModelState.IsValid)
            {
                var auther = new Auther
                {
                    AutherName = autherformmodel.AutherName
                };
                try
                {
                    context.Authers.Add(auther);
                    context.SaveChanges();
                    return RedirectToAction("index");

                }
                catch
                {
                    ModelState.AddModelError("AutherName", "Auther Name Already Exists");
                    return View("AutherForm");

                }

            }
            else
            {
                return View("AutherForm", autherformmodel);
            }


        }


        [HttpGet]
        public IActionResult Edit(int id)
        {

            var auther = context.Authers.Find(id);
            if (auther is null)
            {
                return NotFound();
            }
            else
            {
               

                var viewmodel = new AutherFormModel { Id = id, AutherName = auther.AutherName };
                return View("AutherForm", viewmodel);
            }
        }
        [HttpPost]
        public IActionResult Edit(int id, AutherFormModel autherformmodel)
        {
            if (!ModelState.IsValid)
            {
                return View("AutherForm", autherformmodel);
            }
            var auther = context.Authers.Find(autherformmodel.Id);
            if (auther is null)
            {
                return NotFound();
            }


            auther.AutherName = autherformmodel.AutherName;
            auther.UpdatedOn = DateTime.Now;
            context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Details(int id)
        {
            var auther = context.Authers.Find(id);
            if (auther is null)
            {
                return NotFound();
            }
            else
            {
                var viewModle = new AutherVm { Id = id, AutherName = auther.AutherName };
                return View("Detailes", viewModle);

            }

        }
        public IActionResult Remove(int id)
        {
            var auther = context.Authers.Find(id);
            if (auther is null)
            {
                return NotFound();
            }
            context.Authers.Remove(auther);
            context.SaveChanges();
            return Ok();


        }

    }
}

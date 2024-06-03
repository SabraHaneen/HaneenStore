using HaneenStore.Data;
using HaneenStore.Models;
using HaneenStore.ViewModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HaneenStore.Controllers
{
    
        public class BookController : Controller
    {
        ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BookController(ApplicationDbContext context,IWebHostEnvironment webHostEnvironment
            )
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;//to enter inside www root folder directly
        }
        public IActionResult Index()
        {
            var books = context.Books.Include(book=>book.Auther).
                Include(book=>book.Categories).//i get list
                ThenInclude(book=>book.category).//to get category from list
                ToList();
            //new way to mapp data
            var bookVm = books.Select(book => new BookViewModel
            {
                Id = book.Id,
                BookTitle = book.BookTitle,
                publishedDate = book.publishedDate,
                Publisher = book.Publisher,
                Auther = book.Auther.AutherName,
                ImageUrl = book.ImageUrl,
                Categories = book.Categories.Select(book=>book.category.Name).ToList(),
            }).ToList();

            //this is the old way 
            /* var bookVm = new List<BookViewModel>();
              foreach(var book in books)
              {
                  var bookvm = new BookViewModel
                  {
                      Id = book.Id,
  BookTitle=book.BookTitle,
  publishedDate=book.publishedDate,
  Publisher=book.Publisher,
  Auther=book.Auther.AutherName,
  ImageUrl=book.ImageUrl,
  Categories=new List<string>(),
  CreatedOn=book.CreatedOn,
  UpdatedOn=book.UpdatedOn,
                  };
                  foreach(var cat in book.Categories)
                  {
                      bookvm.Categories.Add(cat.category.Name);
                  }
            bookVm.Add(bookvm);
            }*/
            return View("index", bookVm);
        }
        [HttpGet]

        public IActionResult Create()
        {
           var authers = context.Authers.OrderBy(auther => auther.AutherName).ToList();
            var autherList = new List<SelectListItem>();
            foreach (var auther in authers)
            {
                autherList.Add(new SelectListItem
                {
                    Value = auther.Id.ToString(),
                    Text = auther.AutherName,
                }); //mapping for data to select list item

            }
         
            var categories = context.Categories.ToList();
            var categoriesList = new List<SelectListItem>();

            foreach (var category in categories)
            {
                categoriesList.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name,
                }); //mapping for data to select list item

            }
            var viewModel = new BookVM
            {
                Authers = autherList,
                Categories = categoriesList,
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Create(BookVM viewModel)
        {
            if (ModelState.IsValid)
            {
                string imageName = null;
                //to upload image or file store it in my folder
                if (viewModel.ImageUrl != null)
                {
                     imageName = Path.GetFileName(viewModel.ImageUrl.FileName);//to get image name
                                                                                     // string imageName=viewModel.ImageUrl.FileName;//another way to get image name
                    var path = Path.Combine($"{webHostEnvironment.WebRootPath}/images", imageName);
                    //i will be sure that he is at www root
                    var stream = System.IO.File.Create(path);//image store at temprory place
                    viewModel.ImageUrl.CopyTo(
                        stream);//to copy img  from temprory place to images folder at www
                }
                var book = new BookModel
                {
                    BookTitle = viewModel.BookTitle,
                    AutherId = viewModel.AutherId,
                    Publisher = viewModel.Publisher,
                    publishedDate = viewModel.publishedDate,
                    Description = viewModel.Description,
                    ImageUrl = imageName,//store img at db
                    Categories = viewModel.SelectedCategories.Select(id => new BookCategory
                    {
                        CategoryId = id,
                    }).ToList(), //categories is bookcategory type where selectedCategories is selectedlistitem that's why we need to convert
                };
                context.Books.Add(book);
                context.SaveChanges();
              //  return Content("done");
                return RedirectToAction("index");

            }
            else
            {
               // return Content("faild");
             return View("create", viewModel);


            }
        }
        public IActionResult Remove(int id)
        {
            var book = context.Books.Find(id);
            if (book is null)
            {
                return NotFound();
            }
            else
            {
                var path = Path.Combine(webHostEnvironment.WebRootPath, "images", book.ImageUrl);
          if(System.IO.File.Exists(
              path))
            {
                System.IO.File.Delete(path);

            }
         
                context.Books.Remove(book);
                context.SaveChanges();
                return Ok();

            }
          


        }
    }
}

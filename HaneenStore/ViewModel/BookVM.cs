using HaneenStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HaneenStore.ViewModel
{
    public class BookVM
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string BookTitle { get; set; } = null!;
        [Display(Name = "Auther")]
        public int AutherId { get; set; }//prefered to define it so i can deal with it easily
        public List<SelectListItem>? Authers { get; set; }
        public string Publisher { get; set; } = null;
        public DateTime publishedDate
        {
            get; set;
        } = DateTime.Now;
        [Display(Name ="Insert Image")]
        public IFormFile? ImageUrl { get; set; }
        public string Description { get; set; } = null!;
        public List<int> SelectedCategories { get; set; } = new List<int>();
        public List<SelectListItem> ?Categories { get; set; } 

    }
}

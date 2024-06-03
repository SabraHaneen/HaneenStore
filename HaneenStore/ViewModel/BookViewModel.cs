using HaneenStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HaneenStore.ViewModel
{
    public class BookViewModel
    {
        
            public int Id { get; set; }

            public string BookTitle { get; set; } = null!;
            public string Auther { get; set; } = null!;
            public string Publisher { get; set; } = null!;
            public DateTime publishedDate { get; set; }
           
            public string ImageUrl { get; set; }
            public List<string> Categories { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; } 


    }
}

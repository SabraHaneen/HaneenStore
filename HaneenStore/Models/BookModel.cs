using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace HaneenStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string BookTitle { get; set; } = null!;

        public int AutherId { get; set; }//prefered to define it so i can deal with it easily
        public Auther Auther { get; set; } //relation 1-m from auther
        public string Publisher { get; set; }
        public DateTime publishedDate
        {
            get;set;
        }
        public string? ImageUrl { get; set; }//?thats image is optional not nesessary
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        //relation between category and book consider it m-m 
        public List<BookCategory> Categories { get; set; } = new List<BookCategory>();
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HaneenStore.Models
{
    [Index(nameof(Name), IsUnique = true)]

    public class Category
    {

        public int Id { get; set; }
        [MaxLength(30)]
        public bool IsDeleted { get; set; }//defualt va;ue is false
        public string Name { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public List<BookCategory> Books { get; set; } = new List<BookCategory>();

    }
}

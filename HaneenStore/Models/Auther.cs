using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace HaneenStore.Models
{
    [Index(nameof(AutherName), IsUnique = true)]

    public class Auther
    {
        public int Id { get; set; }
        public string AutherName { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}

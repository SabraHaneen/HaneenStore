using System.ComponentModel.DataAnnotations;

namespace HaneenStore.ViewModel
{
    public class AutherVm
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "name must be less than 50 char")]

        public string AutherName { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}

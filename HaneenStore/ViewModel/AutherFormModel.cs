using System.ComponentModel.DataAnnotations;

namespace HaneenStore.ViewModel
{
    public class AutherFormModel
    {
        public int Id { get; set; }
        [MaxLength(50,ErrorMessage ="name must be less than 50 char")]
        public string AutherName { get; set; } = null!;

    }
}

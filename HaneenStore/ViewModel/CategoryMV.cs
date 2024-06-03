using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HaneenStore.ViewModel
{
    public class CategoryMV
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        //  public bool IsDeleted { get; set; }//defualt va;ue is false
        [Remote("chechName",null,ErrorMessage ="Already there")]//to active checkname fun on this filed
     //null for the controller that should retrun to it and here cause will return to same controller set it null
        public string Name { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

    }
}

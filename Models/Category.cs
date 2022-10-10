using System.ComponentModel.DataAnnotations;

namespace ToDo.Models;
public class Category
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [MaxLength(30, ErrorMessage = "This field must contain between 3 and 30 characters")]
    [MinLength(3, ErrorMessage = "This field must contain between 3 and 30 characters")]
    public string Name { get; set; }

    [MaxLength(60, ErrorMessage = "This field must contain up to 60 characters")]
    public string Description { get; set; }
}
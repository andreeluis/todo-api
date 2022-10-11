using System.ComponentModel.DataAnnotations;

namespace ToDo.Models;
public class ToDoTask
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "This field is required")]
    [MaxLength(30, ErrorMessage = "This field must contain between 3 and 30 characters")]
    [MinLength(3, ErrorMessage = "This field must contain between 3 and 30 characters")]
    public string Name { get; set; }

    [MaxLength(120, ErrorMessage = "This field must contain up to 120 characters")]
    public string Description { get; set; }

    [Required(ErrorMessage = "This field is required")]
    public bool IsComplete { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid category")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}
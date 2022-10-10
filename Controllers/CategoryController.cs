using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Models;
using ToDo.Data;

namespace ToDo.Controllers;

[Route("categories")]
[ApiController]
public class CategoryController : ControllerBase
{
    [Route("")]
    [HttpGet]
    public async Task<ActionResult<List<Category>>> Get(
        [FromServices] DataContext context)
    {
        try
        {
            return Ok(await context.Categories.AsNoTracking().ToListAsync());
        }
        catch
        {
            return BadRequest(new { message = "No categories found" });
        }
    }

    [Route("{id:int}")]
    [HttpGet]
    public async Task<ActionResult<Category>> Get(
        int id, [FromServices] DataContext context)
    {
        try
        {
            return Ok(await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id));
        }
        catch
        {
            return BadRequest(new { message = "Category not found" });
        }
    }

    [Route("")]
    [HttpPost]
    public async Task<ActionResult<Category>> Post(
        Category category, [FromServices] DataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            return Ok(category);
        }
        catch
        {
            return BadRequest(new { message = "Category not created" });
        }
    }

    [Route("{id:int}")]
    [HttpPut]
    public async Task<ActionResult<Category>> Put(
        int id, Category category, [FromServices] DataContext context)
    {
        if (category.Id != id)
            return NotFound(new { message = "Task not found" });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            context.Entry<Category>(category).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return category;
        }
        catch
        {
            return BadRequest(new { message = "Category not updated" });
        }
    }

    [Route("{id:int}")]
    [HttpDelete]
    public async Task<ActionResult<Category>> Delete(
        int id, [FromServices] DataContext context)
    {
        var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null)
            return NotFound(new { message = "Category not found" });

        try
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return Ok(new { message = "Category deleted" });
        }
        catch
        {
            return BadRequest(new { message = "Category not deleted" });
        }
    }
}
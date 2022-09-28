using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Models;
using ToDo.Data;

namespace ToDo.Controllers;

[Route("tasks")]
[ApiController]
public class TaskController : ControllerBase
{
    [Route("")]
    [HttpGet]
    public async Task<ActionResult<List<ToDoTask>>> Get(
        [FromServices] DataContext context)
    {
        try
        {
            return Ok(await context.ToDoTasks.AsNoTracking().ToListAsync());
        }
        catch
        {
            return BadRequest(new { message = "No tasks found" });
        }
    }

    [Route("{id:int}")]
    [HttpGet]
    public async Task<ActionResult<ToDoTask>> Get(
        int id, [FromServices] DataContext context)
    {
        try
        {
            return Ok(await context.ToDoTasks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id));
        }
        catch
        {
            return BadRequest(new { message = "Task not found" });
        }
    }

    [Route("")]
    [HttpPost]
    public async Task<ActionResult<ToDoTask>> Post(
        ToDoTask task, [FromServices] DataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            context.ToDoTasks.Add(task);
            await context.SaveChangesAsync();
            return Ok(task);
        }
        catch
        {
            return BadRequest(new { message = "Task not created" });
        }
    }

    [Route("{id:int}")]
    [HttpPut]
    public async Task<ActionResult<ToDoTask>> Put(
        int id, ToDoTask task, [FromServices] DataContext context)
    {
        if (task.Id != id)
            return NotFound(new { message = "Task not found" });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            context.Entry<ToDoTask>(task).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return task;
        }
        catch
        {
            return BadRequest(new { message = "Task not updated" });
        }
    }

    [Route("{id:int}")]
    [HttpDelete]
    public async Task<ActionResult<ToDoTask>> Delete(
        int id, [FromServices] DataContext context)
    {
        var task = await context.ToDoTasks.FirstOrDefaultAsync(x => x.Id == id);
            if (task == null)
                return NotFound(new { message = "Task not found" });

        try
        {
            context.ToDoTasks.Remove(task);
            await context.SaveChangesAsync();
            return Ok(new { message = "Task deleted" });
        }
        catch
        {
            return BadRequest(new { message = "Task not deleted" });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTI.Practicas.Data;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly ToDoListContext _context;

        public ToDoItemController(ToDoListContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.Include(td => td.Category).ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem.CategoryId is not null)
            {
                if (todoItem.Category is null)
                {
                    _context.Entry(todoItem).Navigation(nameof(TodoItem.Category)).Load();
                }
                if (todoItem.Category.Name == "Cat2")
                {

                }
            }
            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }
        [HttpGet("withoutCategory")]
        public ActionResult<IEnumerable<TodoItem>> GetTodoItemWithoutCategory()
        {
            //var res = _context.TodoItems.AsQueryable();

            //if (par1 is not null)
            //    res = res.Where(_ => _.Name == par1);
            //if (par2 is not null)
            //    res = res.Where(_ => _.Name == par2);
            //if (par3 is not null)
            //    res = res.Where(_ => _.Name == par3);

            //if (ordered == ascente)
            //    res = res.OrderBy(_ => _.Name);
            //else
            //    res = res.OrderByDescending(_ => _.Name);

            //var numbers = new int[] { 1, 2, 3, 4, 5 };
            //var numbersLabels = numbers.Select(_ => _.ToString()); // { "1","2",....

            //foreach(TodoItem item in res)
            //{

            //}

            return _context.TodoItems
                .Where(td => td.CategoryId == null)
                .ToList();
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;


            // Creo otro TodoListContext




            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using System.Linq;
using System.Collections.Generic;

namespace TodoApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase {
        private readonly TodoContext _context;
        public TodoItemsController(TodoContext context) {
            _context = context;
            if(_context.TodoItems.Count() == 0) {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }
        /// <summary>
        /// Get all Todo Items
        /// </summary>
        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll() {
            return _context.TodoItems.ToList();
        }
        /// <summary>
        /// Get a specific Todo Item by Id
        /// </summary>
        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoItem> GetById(long id){
            var item = _context.TodoItems.Find(id);
            if(item == null){
                return NotFound();
            }
            return item;
        }
    }
}
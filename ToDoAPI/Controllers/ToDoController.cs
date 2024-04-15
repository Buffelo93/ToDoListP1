using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ToDoAPI.Services;
using System.Data;
using ToDoAPI.Models;
using ToDoAPI.ToDoDbContext;
using Newtonsoft.Json.Linq;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoService todoService;
        public ToDoController(ToDoService todoService) 
        { 
            this.todoService = todoService;
        }

        [HttpGet]
        [Route("GetToDoItems")]
        public JsonResult GetNotes()
        {
            return new JsonResult(todoService.GetToDoItems());
        }

        [HttpPost]
        [Route("AddToDoItem")]
        public JsonResult AddToDoItem([FromBody] ToDoItem newToDoItem)
        {
            int newId = todoService.AddToDoItem(newToDoItem);
            if (newId != 0) {
                JsonResult successfullAdd = new JsonResult(newId);
                successfullAdd.StatusCode = 201;
                return successfullAdd;
            } else {
                return new JsonResult(0);
            }
        }

        [HttpPost]
        [Route("UpdateIsComplete")]
        public JsonResult UpdateIsComplete([FromBody] UpdateIsComplete updateIsComplete)
        {
            if (todoService.UpdateIsComplete(updateIsComplete.ToDoId, updateIsComplete.IsComplete))
            {
                return new JsonResult("ToDoItem Updated Successfully");
            }
            else
            {
                return new JsonResult("ToDoItem Did Not Update");
            }
        }

        [HttpDelete]
        [Route("DeleteToDoItem")]
        public JsonResult DeleteToDoItem([FromQuery] int toDoId) 
        {
            if (todoService.DeleteToDoItem(toDoId))
            {
                return new JsonResult("ToDoItem Deleted Successfully");
            }
            else
            {
                return new JsonResult("ToDoItem Did Not Delete");
            }
        }

    }
}

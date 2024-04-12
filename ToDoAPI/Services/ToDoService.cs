using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Models;
using ToDoAPI.ToDoDbContext;

namespace ToDoAPI.Services
{
    public class ToDoService
    {
        private readonly ToDoContext toDoItemContext;

        public ToDoService(ToDoContext toDoItemContext)
        {
            this.toDoItemContext = toDoItemContext;
        }

        public List<ToDoItem> GetToDoItems()
        {
            List<ToDoItem> toDoItemList = toDoItemContext.ToDoItems.Select(t => t).OrderBy(t => t.DeadLine == null).ThenBy(t => t.DeadLine).ToList();

            return toDoItemList;
        }

        public int AddToDoItem(ToDoItem addToDoItem)
        {
            ToDoItem newToDoItem = new ToDoItem { Title = addToDoItem.Title, Note = addToDoItem.Note, IsComplete = addToDoItem.IsComplete, DeadLine = addToDoItem.DeadLine };
            try
            {
                toDoItemContext.ToDoItems.Add(newToDoItem);
                toDoItemContext.SaveChanges();
                return newToDoItem.ToDoId;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return 0;
            }
        }

        public bool UpdateIsComplete(int todoId, bool isComplete)
        {
            ToDoItem? toDoItemToUpdate = toDoItemContext.ToDoItems.Find(todoId);

            if (toDoItemToUpdate != null)
            {
                try
                {
                    toDoItemToUpdate.IsComplete = isComplete;
                    toDoItemContext.ToDoItems.Update(toDoItemToUpdate);
                    toDoItemContext.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool DeleteToDoItem(int todoId)
        {
            ToDoItem? toDoItemToRemove = toDoItemContext.ToDoItems.Find(todoId);

            if (toDoItemToRemove != null)
            {
                try
                {
                    toDoItemContext.ToDoItems.Remove(toDoItemToRemove);
                    toDoItemContext.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
